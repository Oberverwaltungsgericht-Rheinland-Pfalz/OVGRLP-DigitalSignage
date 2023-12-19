// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.Xml.Serialization;

namespace DigitalSignage.ImportCLI.Service;

public class XMLHelper
{
    public static T DeserializeFromXml<T>(string sourceFile, string nameSpace = "")
    {
        XmlSerializer xs;
        if (nameSpace != "") { xs = new XmlSerializer(typeof(T), nameSpace); }
        else { xs = new XmlSerializer(typeof(T)); }

        using (FileStream fs = File.OpenRead(sourceFile))
        {
            var obj = xs.Deserialize(fs);
            return (T)obj;
        }
    }

    public static T TryDeserializeFromXml<T>(string sourceFile, string nameSpace = "")
    {
        try
        {
            T rval = DeserializeFromXml<T>(sourceFile, nameSpace);
            return rval;
        }
        catch
        {
            XmlSerializer xs;
            if (nameSpace != "") { xs = new XmlSerializer(typeof(T), nameSpace); }
            else { xs = new XmlSerializer(typeof(T)); }
            return DeserializeEnvelopeFromBrokenXml<T>(sourceFile, xs);
        }
    }

    // bei ungültigen XML-Dateien (bspw. Anhänge mit § oder & im Dateinamen)
    // Versuchen diese über UTF8-Encoding dennoch zu laden
    private static T DeserializeEnvelopeFromBrokenXml<T>(string sourceFile, XmlSerializer xs)
    {
        if (!string.IsNullOrEmpty(sourceFile) && File.Exists(sourceFile))
        {
            string xmlString = System.Xml.XmlConvert.VerifyXmlChars(File.ReadAllText(sourceFile).Replace("&", " ").Replace("§", " "));
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlString);
            var obj = xs.Deserialize(new MemoryStream(byteArray));
            return (T)obj;
        }
        return default(T);
    }
}