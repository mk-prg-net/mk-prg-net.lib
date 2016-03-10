using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace mko.algorithm {

    public class InfosSerializer<T>
    {
        public static void ReadFromXml(string XmlInfo, out T Info)
        {
            // XML- Fragment in einen Memstream kopieren
            MemoryStream memStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memStream);
            writer.Write(XmlInfo);
            writer.Flush();

            // Aus dem XML- Fragment im Memstream das Descr- Objekt deserialisieren
            memStream.Seek(0, SeekOrigin.Begin);
            XmlSerializer ser = new XmlSerializer(typeof(T));
            Info = (T)ser.Deserialize(memStream);
        }

        public static void WriteToXml(T Info, out string XmlInfo)
        {
            // Beschreibung eines Testszenarios in ein XmlDokument serialisieren
            XmlSerializer ser = new XmlSerializer(typeof(T));

            MemoryStream memStream = new MemoryStream();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.Unicode;
            XmlWriter _XmlWriter = XmlWriter.Create(memStream, settings);

            ser.Serialize(_XmlWriter, Info);
            memStream.Flush();
            memStream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(memStream);
            XmlInfo = reader.ReadToEnd();
        }
    }
}
