// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.Runtime.Serialization.Json;
using System.Text;

namespace DigitalSignage.ImportCLI.Logging;

public class SerializationService
{
    public string SerializeTypeAsJsonString<T>(T type)
    {
        var js = new DataContractJsonSerializer(typeof(T));
        var stream = new MemoryStream();
        js.WriteObject(stream, type);
        return StreamToString(stream);
    }

    public string StreamToString(Stream stream)
    {
        stream.Position = 0;
        using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
    }

    public Stream StringToStream(string src)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(src);
        return new MemoryStream(byteArray);
    }
}