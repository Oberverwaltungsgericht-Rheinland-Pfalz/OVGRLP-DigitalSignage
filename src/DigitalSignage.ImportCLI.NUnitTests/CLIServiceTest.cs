// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DigitalSignage.ImportCLI.NUnitTests;

[TestFixture]
public class CLIServiceTest 
{
    [Test]
    public void CommandLineParserReturnCLIAction()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML1 }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);
    }

    [Test]
    public void CommandLineParserDelete()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { TestSets.COMMAND_DELETE }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsTrue(cliActions.ClearDatabase);
    }

    [Test]
    public void CommandLineParserNotDelete()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML1 }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsFalse(cliActions.ClearDatabase);
    }

    [Test]
    public void CommandLineParserConnectionName()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { TestSets.COMMAND_CONNECTION, TestSets.CONNECTION_NAME_LABOR }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, TestSets.CONNECTION_NAME_LABOR);
    }

    [Test]
    public void CommandLineParserConnectionString()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { TestSets.COMMAND_CONNECTION, TestSets.CONNECTION_STRING_LABOR }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, TestSets.CONNECTION_STRING_LABOR);
    }

    [Test]
    public void CommandLineParserConnectionXML()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML1 }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 1);
        ClassicAssert.AreEqual(cliActions.InputFiles[0].ToString(), TestSets.EXAMPLE_XML1);
    }

    [Test]
    public void CommandLineParserConnectionTwoXMLWithQuote()
    {
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(new string[] { TestSets.COMMAND_ADD, "\"" + TestSets.EXAMPLE_XML1 + "\"", TestSets.COMMAND_ADD, "\"" + TestSets.EXAMPLE_XML2 + "\"" }, false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 2);
        ClassicAssert.AreEqual(cliActions.InputFiles[0].ToString(), TestSets.EXAMPLE_XML1);
        ClassicAssert.AreEqual(cliActions.InputFiles[1].ToString(), TestSets.EXAMPLE_XML2);
    }

    [Test]
    public void CommandLineParserConnectionAndDeleteAndFiveXML()
    {
        string[] args = { TestSets.COMMAND_CONNECTION, TestSets.CONNECTION_NAME_LABOR,
                    TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML1,
                    TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML2,
                    TestSets.COMMAND_DELETE,
                    TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML3,
                    TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML4,
                    TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML5};

        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsTrue(cliActions.ClearDatabase);
        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, TestSets.CONNECTION_NAME_LABOR);
        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 5);
    }

    [Test]
    public void CommandLineParserConnectionStringAndFiveXMLWithQuote()
    {
        string[] args = { TestSets.COMMAND_ADD, "\""+TestSets.EXAMPLE_XML1+"\"",
                    TestSets.COMMAND_ADD, "\""+TestSets.EXAMPLE_XML2+"\"",
                    TestSets.COMMAND_CONNECTION, TestSets.CONNECTION_STRING_LABOR,
                    TestSets.COMMAND_ADD, "\""+TestSets.EXAMPLE_XML3+"\"",
                    TestSets.COMMAND_ADD, "\""+TestSets.EXAMPLE_XML4+"\"",
                    TestSets.COMMAND_ADD, "\""+TestSets.EXAMPLE_XML5+"\""};

        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        ClassicAssert.IsFalse(cliActions.ClearDatabase);
        ClassicAssert.AreEqual(cliActions.NameOrConnectionString, TestSets.CONNECTION_STRING_LABOR);
        ClassicAssert.AreEqual(cliActions.InputFiles.Count, 5);
    }
}