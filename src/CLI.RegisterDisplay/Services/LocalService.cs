using Microsoft.Extensions.Configuration;
using System.Management.Automation;
using Serilog;
using System.DirectoryServices.AccountManagement;

namespace CLI.Register.Services;

public class LocalService
{
    /// <summary>
    /// Registers the Device with the DigitalSignage Service and downloads the client binary
    /// </summary>
    /// <param name="config">Configuration object</param>
    /// <param name="logger">Logger object</param>
    public static void RegisterDevice(IConfiguration config, ILogger logger)
    {
        // Check if the Configuration has already been run...
        var deviceId = (string)(DeviceService.ReadRegistryValue(
            config["Registry:DigitalSignage:Path"] ?? "", 
            config["Registry:DigitalSignage:Value"]) ?? "");
        if (!string.IsNullOrEmpty(deviceId))
        {
            logger.Information("The Registry-Process has already been run.");
            logger.Information("Current Device-Id: {deviceId}", deviceId);
            return;
        }

        // Gather Information about the Device...
        NetworkDeviceData? devicedata = DeviceService.GetDeviceInfo();
        logger.Information("Gathered the following Information:\n{devicedata}", devicedata);
        if (devicedata == null)
        {
            logger.Error("Could not gather any Device Info");
            return;
        }

        var httpClient = new HttpClient();

        // Create the Entity in the Database...
        var display = APIService.RegisterDisplay(
            httpClient,
            config["Services:Register"],
            (NetworkDeviceData)devicedata);
        if (display == null)
        {
            logger.Error("Could not find Display-Entity in Datatbase. Please create one first");
            return;
        }

        // Download Client-Software...
        var clientFile = APIService.DownloadClientBinary(
            httpClient,
            $"{config["Services:Register"]}/client/{PlatformID.Win32NT}",
            config["Options:ClientPath"] ?? "");
        if (clientFile == null)
        {
            // Delete the Display if something went wrong
            logger.Error("Could not download Client-Executable");
            APIService.DeleteDisplay(
                httpClient,
                $"{config["Services:Displays"]}/{display?.Id}");
            return;
        }

        PrepareKioskMode(config, logger);
        AddFirewallException(config, logger);

        // Set DeviceId in Registry
        var devId = DeviceService.SetRegistryValue(
            config["Registry:DigitalSignage:Device:Path"] ?? "",
            config["Registry:DigitalSignage:Device:Value"],
            display.Id);
        if (!devId)
        {
            logger.Error("Could not set DeviceId in Registry");
            APIService.DeleteDisplay(
                httpClient,
                $"{config["Services:Displays"]}/{display?.Id}");
            return;
        }

        // Set Client for Autostart
        var auto = DeviceService.SetRegistryValue(
            config["Registry:Autostart:Path"] ?? "",
            config["Registry:Autostart:Value"],
            Path.Combine(
                config["Options:ClientPath"] ?? "",
                clientFile.Value.FileName));
        if (!auto)
        {
            logger.Error("Could not set Autostart in Registry");
            if (!DeviceService.DeleteRegistryValue(config["Registry:DigitalSignage:Device:Path"] ?? ""))
                logger.Warning("Could not delete DeviceId from Registry. Please check if this Key exists and delete it if necessary: {path}", config["Registry:DigitalSignage:Device:Path"]);
            APIService.DeleteDisplay(
                httpClient,
                $"{config["Services:Displays"]}/{display?.Id}");
            return;
        }
    }

    // TODO: Test
    private static void PrepareKioskMode(IConfiguration config, ILogger logger)
    {
        // Hide Desktop Icons
        if (bool.Parse(config["Kiosk:HideDesktopIcons"] ?? "False"))
        {
            logger.Information("Hide Desktiop Icons ...");
            DeviceService.SetRegistryValue("HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "HideIcons", 1);
        }

        // Hide Taskbar
        if (bool.Parse(config["Kiosk:HideTaskbar"] ?? "False"))
        {
            logger.Information("Hide Taskbar ...");
            if (DeviceService.ReadRegistryValue("HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\StuckRects3", "Settings") is byte[] value)
            {
                if (value[8] != 3)
                {
                    value[8] = 3;
                    DeviceService.SetRegistryValue("HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\StuckRects3", "Settings", value);
                }
            }
        }

        // Desktop-Background Black
        if (bool.Parse(config["Kiosk:BlackBackground"] ?? "False"))
        {
            logger.Information("Make Background Black ...");
            DeviceService.SetRegistryValue("HKCU\\Control Panel\\Desktop", "WallPaper", "");
        }

        // AutoLogin User
        if (!string.IsNullOrEmpty(config["Kiosk:AutoLogin:User"]))
        {
            // TODO: Check if User exists
            if (OperatingSystem.IsWindows())
            {
                using PrincipalContext pc = new(ContextType.Machine);

                UserPrincipal up = UserPrincipal.FindByIdentity(
                    pc,
                    IdentityType.SamAccountName,
                    config["Kiosk:AutoLogin:User"]);

                if (up == null)
                {
                    // TODO: Create User
                    logger.Warning("The Autologin User does not exists on the local Machine. Please make sure it is a valid User");
                }
            }

            logger.Information("Auto Login User {user} on Startup ...", config["Kiosk:AutoLogin:User"]);
            DeviceService.SetRegistryValue("HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "AutoAdminLogon", 1);
            DeviceService.SetRegistryValue("HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "DefaultDomainName", config["Kiosk:AutoLogin:Domain"] ?? "");
            DeviceService.SetRegistryValue("HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "DefaultUserName", config["Kiosk:AutoLogin:User"] ?? "");
            DeviceService.SetRegistryValue("HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "DefaultPassword", config["Kiosk:AutoLogin:Password"] ?? "");
        }
    }

    // TODO: Test
    private static void AddFirewallException(IConfiguration config, ILogger logger)
    {
        var ps = PowerShell.Create();

        ps.AddCommand($"netsh.exe http add urlacl url=http://*:{config["Options:ClientPort"]}/ user=Jeder");
        ps.Invoke();

        ps.AddCommand("Get-NetFirewallRule -DisplayName \"DigitalSignage Webservice Client\" -ErrorAction SilentlyContinue");
        var rules = ps.Invoke();

        if (rules.Count <= 0)
        {
            ps.AddCommand($"New-NetFirewallRule -DisplayName \"DigitalSignage Webservice Client\" -LocalPort {config["Options:ClientPort"]} -Protocol TCP");
            ps.Invoke();
        }

        ps.Stop();
    }
}
