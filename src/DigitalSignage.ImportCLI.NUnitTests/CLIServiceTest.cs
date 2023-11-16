// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using NUnit.Framework;

namespace DigitalSignage.ImportCLI.NUnitTests;

[TestFixture]
public class CLIServiceTest : TestBase
{
    [Test]
    public void CommandLineParserReturnCLIAction()
    {
        var cliService = new Service.CLIService();
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, EXAMPLE_XML1 }, false);
        Assert.IsInstanceOf<CLIActions>(cliActions);
    }

    [Test]
    public void CommandLineParserDelete()
    {
        var cliService = new Service.CLIService();
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_DELETE }, false);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.IsTrue(cliActions.ClearDatabase);
    }

    [Test]
    public void CommandLineParserNotDelete()
    {
        var cliService = new Service.CLIService();
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, EXAMPLE_XML1 }, false);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.IsFalse(cliActions.ClearDatabase);
    }

    [Test]
    public void CommandLineParserConnectionName()
    {
        var cliService = new Service.CLIService();
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_CONNECTION, CONNECTION_NAME_LABOR }, false);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_NAME_LABOR);
    }

    [Test]
    public void CommandLineParserConnectionString()
    {
        var cliService = new Service.CLIService();
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_CONNECTION, CONNECTION_STRING_LABOR }, false);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_STRING_LABOR);
    }

    [Test]
    public void CommandLineParserConnectionXML()
    {
        var cliService = new Service.CLIService();
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, EXAMPLE_XML1 }, false);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.AreEqual(cliActions.InputFiles.Count, 1);
        Assert.AreEqual(cliActions.InputFiles[0].ToString(), EXAMPLE_XML1);
    }

    [Test]
    public void CommandLineParserConnectionTwoXMLWithQuote()
    {
        var cliService = new Service.CLIService();
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { COMMAND_ADD, "\"" + EXAMPLE_XML1 + "\"", COMMAND_ADD, "\"" + EXAMPLE_XML2 + "\"" }, false);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.AreEqual(cliActions.InputFiles.Count, 2);
        Assert.AreEqual(cliActions.InputFiles[0].ToString(), EXAMPLE_XML1);
        Assert.AreEqual(cliActions.InputFiles[1].ToString(), EXAMPLE_XML2);
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
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.IsTrue(cliActions.ClearDatabase);
        Assert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_NAME_LABOR);
        Assert.AreEqual(cliActions.InputFiles.Count, 5);
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
        Assert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args);
        Assert.IsInstanceOf<CLIActions>(cliActions);

        Assert.IsFalse(cliActions.ClearDatabase);
        Assert.AreEqual(cliActions.NameOrConnectionString, CONNECTION_STRING_LABOR);
        Assert.AreEqual(cliActions.InputFiles.Count, 5);
    }
}