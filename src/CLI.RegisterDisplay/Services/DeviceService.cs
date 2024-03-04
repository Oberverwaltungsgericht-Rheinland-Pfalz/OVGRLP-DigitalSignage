using System.Net.NetworkInformation;
using System.Net;
using Microsoft.Win32;
using System.Security;

namespace CLI.Register.Services;

public struct NetworkDeviceData
{
    public NetworkDeviceData() { }

    public string Hostname = "";
    public string IpAddr = "";
    public string MacAddr = "";

    public override readonly string ToString()
    {
        return $"{GetType()}:\n" +
            $"\tHostname:\t{Hostname}\n" +
            $"\tIpAddr:\t\t{IpAddr}\n" +
            $"\tMacAddr:\t{MacAddr}\n";
    }
}

/// <summary>
/// Class for Device Services, such as gathering Information from the current Device
/// or Managing states.
/// </summary>
public class DeviceService
{
    /// <summary>
    /// Returns a struct with all NetworkDeviceData
    /// </summary>
    /// <returns>A NetworkDeviceData object</returns>
    public static NetworkDeviceData? GetDeviceInfo()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

        foreach (var n in nics)
        {
            if (n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                // Gather all Informations about this Machine...
                var devicedata = new NetworkDeviceData
                {
                    Hostname = GetHostname(),
                    IpAddr = GetIPv4Address(n).ToString(),
                    MacAddr = GetMacAddress(n).ToString(),
                };

                return devicedata;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns the IPv4 Address for the currently active NIC
    /// </summary>
    /// <param name="nic">The Nic to retrieve an IP-Address from</param>
    /// <returns>The IPAdress</returns>
    public static IPAddress GetIPv4Address(NetworkInterface nic)
    {
        if (OperatingSystem.IsWindows())
            foreach (var adr in nic.GetIPProperties().UnicastAddresses)
                if (adr.SuffixOrigin == SuffixOrigin.OriginDhcp)
                    return adr.Address;

        return IPAddress.Parse("0.0.0.0");
    }

    /// <summary>
    /// Returns the Mac Address for the currently active NIC
    /// </summary>
    /// <param name="nic">The Nic to retrieve an IP-Address from</param>
    /// <returns>The Mac Address</returns>
    public static PhysicalAddress GetMacAddress(NetworkInterface nic)
    {
        return nic.GetPhysicalAddress();
    }

    /// <summary>
    /// Returns the current Hostname of this computer
    /// </summary>
    /// <returns>The Hostname</returns>
    public static string GetHostname()
    {
        return Dns.GetHostName();
    }

    /// <summary>
    /// Reads a Value from the Registry
    /// </summary>
    /// <param name="path">The Path to Read from</param>
    /// <param name="key">The Key to Read from</param>
    /// <returns>The Object that was read</returns>
    public static object? ReadRegistryValue(string path, string? key)
    {
        try
        {
            if (OperatingSystem.IsWindows())
                return Registry.GetValue(path, key, null) ?? "";
        } catch (Exception ex) when (ex is SecurityException || ex is IOException || ex is ArgumentException)
        {
            return "";
        }

        return "";
    }

    /// <summary>
    /// Sets a Value to a Registry Key
    /// </summary>
    /// <param name="path">The Path to write to</param>
    /// <param name="key">The Key to write to</param>
    /// <param name="value">The Value to write</param>
    /// <returns>True on success; false otherwise</returns>
    public static bool SetRegistryValue(string path, string? key, object value)
    {
        try
        {
            if (OperatingSystem.IsWindows())
                Registry.SetValue(path, key, value);

            return true;
        }
        catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException || ex is UnauthorizedAccessException || ex is SecurityException)
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes an Registry from a given Path in the Registry
    /// </summary>
    /// <param name="path">The Path in the Registry-Tree</param>
    /// <returns>true on success; false otherwise</returns>
    public static bool DeleteRegistryValue(string path)
    {
        try
        {
            if (OperatingSystem.IsWindows())
            {
                Registry.LocalMachine.DeleteSubKeyTree(path);
            }
            return true;
        }
        catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException || ex is UnauthorizedAccessException || ex is SecurityException)
        {
            return false;
        }
    }
}
