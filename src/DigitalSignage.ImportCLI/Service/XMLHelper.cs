using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitalSignage.ImportCLI.Service
{
  public class XMLHelper
  {
    public static void SerializeToXml(object obj, string targetFile, string nameSpace = "")
    {
      using (FileStream fs = File.OpenWrite(targetFile))
      {
        XmlSerializer xs;
        if (nameSpace != "") { xs = new XmlSerializer(obj.GetType(), nameSpace); }
        else { xs = new XmlSerializer(obj.GetType()); }
        xs.Serialize(fs, obj);
        fs.Flush();
      }
    }

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
  }
}