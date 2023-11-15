// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.Infrastructure.Models.EurekaFach;

public class ParteienSV
{
    public int ParteiId { get; set; }
    public Int64 VerfahrensId { get; set; }
    public string Partei { get; set; }
}