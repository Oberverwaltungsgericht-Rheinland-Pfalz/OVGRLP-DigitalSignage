// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

public enum DisplayStatus
{
    Created = 0,
    Registered = 1,
    Online = 2,
    Offline = 3,
    Disabled = 4
}

[Table("Displays")]
[PrimaryKey(nameof(Id))]
public class Display<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    [NotMapped]
    public string IpStr
    {
        get  
        {
            return Ip.ToString();
        }
        set
        {
            Ip = IPAddress.Parse(value);
        }
    }

    [NotMapped]
    public string MacStr
    {
        get
        {
            return Mac.ToString();
        }
        set
        {
            Mac = PhysicalAddress.Parse(value);
        }
    }

    [Required]
    [JsonIgnore]
    public PhysicalAddress Mac { get; set; } = PhysicalAddress.Parse("00:00:00:00:00:00");

    [Required]
    [JsonIgnore]
    public IPAddress Ip { get; set; } = IPAddress.Parse("0.0.0.0");

    public string PublicKey { get; set; } = "";

    public bool Dummy { get; set; } = false;

    public DisplayStatus Status { get; set; } = DisplayStatus.Created;

    [JsonIgnore]
    public T? TemplateId { get; set; }
    [JsonIgnore]
    public Template<T>? Template { get; set; }

    [JsonIgnore]
    public T? GroupId { get; set; }
    [JsonIgnore]
    public Group<T>? Group { get; set; }

    [JsonIgnore]
    public T? FilterId { get; set; }
    [JsonIgnore]
    public Filter<T>? Filter { get; set; }

    [JsonIgnore]
    public T? RoomId { get; set; }
    [JsonIgnore]
    public Room<T>? Room { get; set; }
}
