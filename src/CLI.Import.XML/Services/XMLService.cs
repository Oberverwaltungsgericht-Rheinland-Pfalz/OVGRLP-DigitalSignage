using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CLI.Import.XML.Services;

public class XMLService
{
    public static T? DeserializeFromXml<T>(string sourceFile, string nameSpace = "")
    {
        XmlSerializer xmlSerializer = 
            (string.IsNullOrEmpty(nameSpace) ? 
                new(typeof(T)) :
                new(typeof(T), nameSpace));

        using (FileStream fileStream = File.OpenRead(sourceFile))
        {
            var data = xmlSerializer.Deserialize(fileStream);
            if (data == null) return default;

            return (T)data;
        }
    }

    public static T? TryDeserializeFromXml<T>(string sourceFile, string nameSpace = "")
    {
        try
        {
            T? rval = DeserializeFromXml<T>(sourceFile, nameSpace);
            return rval;
        }
        catch
        {
            XmlSerializer xmlSerializer =
                (string.IsNullOrEmpty(nameSpace) ?
                    new(typeof(T)) :
                    new(typeof(T), nameSpace));
            return DeserializeEnvelopeFromBrokenXml<T>(sourceFile, xmlSerializer);
        }
    }

    private static T? DeserializeEnvelopeFromBrokenXml<T>(string sourceFile, XmlSerializer xmlSerializer)
    {
        if (string.IsNullOrEmpty(sourceFile) && !File.Exists(sourceFile))
            return default;

        string xmlString = XmlConvert.VerifyXmlChars(
            File.ReadAllText(sourceFile)
                .Replace("&", " ")
                .Replace("§", " "));
        byte[] byteArray = Encoding.UTF8.GetBytes(xmlString);
        var data = xmlSerializer.Deserialize(new MemoryStream(byteArray));

        if (data == null) return default;

        return (T)data;
    }
}
