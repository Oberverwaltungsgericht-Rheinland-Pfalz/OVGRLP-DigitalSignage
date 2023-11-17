﻿// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using NUnit.Framework;

namespace DigitalSignage.ImportCLI;

public class TestBase
{
    protected const string COMMAND_ADD = "-add";
    protected const string COMMAND_UPDATE = "-update";
    protected const string COMMAND_CONNECTION = "-con";
    protected const string COMMAND_DELETE = "-delete";
    protected const string CONNECTION_NAME_LABOR = "DSKoblenzLabor";
    protected const string CONNECTION_STRING_LABOR = @"Server=SERVER\EUREKAFACH; Database=DigitalSignage_Labor; Integrated Security=True";
    protected const string CONNECTION_STRING_PROD = @"Server=SERVER\EUREKAFACH; Database=DigitalSignage; Integrated Security=True";
    protected const string EXAMPLE_XML1 = @"C:\dev\repos\digitalsignage\src\DigitalSignage.ImportCLI.NUnitTests\example_XMLs\OVG_TO.XML";
    protected const string EXAMPLE_XML2 = @"C:\dev\repos\digitalsignage\src\DigitalSignage.ImportCLI.NUnitTests\example_XMLs\VGH_TO.XML";
    protected const string EXAMPLE_XML3 = @"C:\dev\repos\digitalsignage\src\DigitalSignage.ImportCLI.NUnitTests\example_XMLs\VGKO_TO.XML";
    protected const string EXAMPLE_XML4 = @"C:\dev\repos\digitalsignage\src\DigitalSignage.ImportCLI.NUnitTests\example_XMLs\ARG_TO.XML";
    protected const string EXAMPLE_XML5 = @"C:\dev\repos\digitalsignage\src\DigitalSignage.ImportCLI.NUnitTests\example_XMLs\SOG_TO.XML";
    protected const string EXAMPLE_UPDATE_XML = @"C:\dev\repos\digitalsignage\src\DigitalSignage.ImportCLI.NUnitTests\example_XMLs\ARG_TO_Update.XML";

    [OneTimeSetUp]
    public void SetUp()
    { }

    [OneTimeTearDown]
    public void TearDown()
    { }
}