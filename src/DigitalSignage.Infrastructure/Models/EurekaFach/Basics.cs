// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.Infrastructure.Models.EurekaFach;

public class Basics
{
    public int Nummer { get; set; }
    public string Gerichtsname { get; set; }
    public string Kuerzel { get; set; }
    public string toXMLFullPath { get; set; }
    public string xsltFullPath { get; set; }
    public string globalXMLFullPath { get; set; }
}