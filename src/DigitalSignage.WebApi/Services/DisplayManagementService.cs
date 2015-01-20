using DigitalSignage.Infrastructure.Models.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;

namespace DigitalSignage.WebApi.Services
{
    public class DisplayManagementService
    {
        public async Task<DisplayStatus> GetDisplayStatus(Display display, int timeout = 500)
        {
            DisplayStatus status = DisplayStatus.Unknown;

            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(display.NetAddress, timeout);
                if (reply.Status == IPStatus.Success) 
                    status = DisplayStatus.Online;
                else
                    status = DisplayStatus.Offline;
            }
            catch(PingException ex)
            {
                status = DisplayStatus.Offline;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return status;
        }
    }
}