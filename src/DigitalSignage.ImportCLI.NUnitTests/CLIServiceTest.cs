// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DigitalSignage.ImportCLI.NUnitTests;

[TestFixture]
public class CLIServiceTest : TestBase
{
    [Test]
    public void CommandLineParserReturnCLIAction()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, EXAMPLE_XML1 }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);
    }

    [Test]
    public void CommandLineParserDelete()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_DELETE }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsTrue(cliActions.ClearDatabase);
    }

    [Test]
    public void CommandLineParserNotDelete()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, EXAMPLE_XML1 }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsFalse(cliActions.ClearDatabase);
    }

    [Test]
    public void CommandLineParserConnectionName()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_CONNECTION, CONNECTION_NAME_LABOR }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_NAME_LABOR);
    }

    [Test]
    public void CommandLineParserConnectionString()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_CONNECTION, CONNECTION_STRING_LABOR }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_STRING_LABOR);
    }

    [Test]
    public void CommandLineParserConnectionXML()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, EXAMPLE_XML1 }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 1);
        ClassicAssert.AreEqual(cliActions.InputFiles[0].ToString(), EXAMPLE_XML1);
    }

    [Test]
    public void CommandLineParserConnectionTwoXMLWithQuote()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, "\"" + EXAMPLE_XML1 + "\"", COMMAND_ADD, "\"" + EXAMPLE_XML2 + "\"" }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 2);
        ClassicAssert.AreEqual(cliActions.InputFiles[0].ToString(), EXAMPLE_XML1);
        ClassicAssert.AreEqual(cliActions.InputFiles[1].ToString(), EXAMPLE_XML2);
    }

    [Test]
    public void CommandLineParserConnectionAndDeleteAndFiveXML()
    {
        string[] args = { COMMAND_CONNECTION, CONNECTION_NAME_LABOR,
                    COMMAND_ADD, EXAMPLE_XML1,
                    COMMAND_ADD, EXAMPLE_XML2,
                    COMMAND_DELETE,
                    COMMAND_ADD, EXAMPLE_XML3,
                    COMMAND_ADD, EXAMPLE_XML4,
                    COMMAND_ADD, EXAMPLE_XML5};

        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsTrue(cliActions.ClearDatabase);
        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_NAME_LABOR);
        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 5);
    }

    [Test]
    public void CommandLineParserConnectionStringAndFiveXMLWithQuote()
    {
        string[] args = { COMMAND_ADD, "\""+EXAMPLE_XML1+"\"",
                    COMMAND_ADD, "\""+EXAMPLE_XML2+"\"",
                    COMMAND_CONNECTION, CONNECTION_STRING_LABOR,
                    COMMAND_ADD, "\""+EXAMPLE_XML3+"\"",
                    COMMAND_ADD, "\""+EXAMPLE_XML4+"\"",
                    COMMAND_ADD, "\""+EXAMPLE_XML5+"\""};

        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsFalse(cliActions.ClearDatabase);
        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_STRING_LABOR);
        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 5);
    }
}