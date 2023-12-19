// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DigitalSignage.ImportCLI;

public static class TestSets
{
    public static string COMMAND_ADD = "-add";
    public static  string COMMAND_UPDATE = "-update";
    public static  string COMMAND_CONNECTION = "-con";
    public static  string COMMAND_DELETE = "-delete";
    public static  string CONNECTION_NAME_LABOR = "DSKoblenzLabor";
    public static  string CONNECTION_STRING_LABOR = @"Server=SERVER\EUREKAFACH; Database=DigitalSignage_Labor; Integrated Security=True";
    public static  string CONNECTION_STRING_PROD = @"Server=SERVER\EUREKAFACH; Database=DigitalSignage; Integrated Security=True";
    public static DbContextOptions<DigitalSignageDbContext> dbContextOptions(string connectionString)
        => new DbContextOptionsBuilder<DigitalSignageDbContext>()
        .UseSqlServer(connectionString,
          sqlServerOptions => sqlServerOptions.CommandTimeout(120)).Options;
    public static  string EXAMPLE_XML1 = @"example_XMLs\OVG_TO.XML";
    public static  string EXAMPLE_XML2 = @"example_XMLs\VGH_TO.XML";
    public static  string EXAMPLE_XML3 = @"example_XMLs\VGKO_TO.XML";
    public static  string EXAMPLE_XML4 = @"example_XMLs\ARG_TO.XML";
    public static  string EXAMPLE_XML5 = @"example_XMLs\SOG_TO.XML";
    public static  string EXAMPLE_UPDATE_XML = @"example_XMLs\ARG_TO_Update.XML";
}