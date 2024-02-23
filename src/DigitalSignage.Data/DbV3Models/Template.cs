// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
global using System.Collections.Generic;
using DigitalSignage.Data.JsonData;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalSignage.Data.DbV3Models;

[Table("Templates")]
[PrimaryKey(nameof(Id))]
public class Template<T> : IBaseModel<T>
    where T : struct
{
    public T Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = "";

    public List<TemplateHtmlJson> Html { get; set; } = new();

    public List<TemplateCssJson> Css { get; set; } = new();

    [JsonIgnore]
    public ICollection<Display<T>> Displays { get; set; } = new List<Display<T>>();
    [JsonIgnore]
    public ICollection<Notification<T>> Notifications { get; set; } = new List<Notification<T>>();
}
