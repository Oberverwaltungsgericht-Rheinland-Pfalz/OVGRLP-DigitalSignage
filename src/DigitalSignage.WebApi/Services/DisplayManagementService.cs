﻿// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Infrastructure.Models.Settings;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace DigitalSignage.WebApi.Services
{
  public class DisplayManagementService
  {
    public DisplayStatus GetDisplayStatus(Display display, int timeout = 500)
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
      catch (PingException)
      {
        status = DisplayStatus.Offline;
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
      }

      return status;
    }

    public string GetDisplayScreenshotUrl(Display display)
    {
      return string.Concat(display.ControlUrl, "/api/screenshot?", DateTime.Now.ToString("MMddyyHHmmss"));
    }

    public void StartDisplay(Display display)
    {
      WolHelper.SendMagicPacket(display.WolMacAddress, display.WolIpAddress, display.WolUdpPort);
    }

    public void RestartDisplay(Display display)
    {
      string completeUrl = string.Concat(display.ControlUrl, "/api/restart");
      var request = UrlHelper.CallUrl(completeUrl);
      request.Close();
    }

    public void StopDisplay(Display display)
    {
      string completeUrl = string.Concat(display.ControlUrl, "/api/shutdown");
      var request = UrlHelper.CallUrl(completeUrl);
      request.Close();
    }

    internal class WolHelper
    {
      internal static void SendMagicPacket(string macAddress, string ipAddress, int updPort)
      {
        byte[] macBytes = GetMacStringToBytes(macAddress);
        byte[] magicPaket = GetMagicPacket(macBytes);

        UdpClient udpClient = new UdpClient();
        udpClient.Send(
          magicPaket,
          magicPaket.Length,
          new IPEndPoint(IPAddress.Parse(ipAddress), updPort));
        udpClient.Close();
      }

      private static byte[] GetMagicPacket(byte[] macAddress)
      {
        byte[] packet = new byte[17 * 6];

        for (int i = 0; i < 6; i++)
        {
          packet[i] = 0xFF;
        }

        for (int i = 1; i <= 16; i++)
        {
          for (int j = 0; j < 6; j++)
          {
            packet[(i * 6) + j] = macAddress[j];
          }
        }

        return packet;
      }

      private static byte[] GetMacStringToBytes(String mac)
      {
        byte[] macBytes = new byte[6];
        string[] macSplited = mac.Split(new char[] { '-', ':', ' ' });

        for (Int32 i = 0; i < macBytes.Length; i++)
        {
          macBytes[i] = Convert.ToByte(macSplited[i], 16);
        }

        return macBytes;
      }
    }

    internal class UrlHelper
    {
      internal static HttpWebResponse CallUrl(string Url)
      {
        WebRequest request = WebRequest.Create(Url);
        request.Credentials = CredentialCache.DefaultCredentials;
        return (HttpWebResponse)request.GetResponse();
      }
    }
  }
}