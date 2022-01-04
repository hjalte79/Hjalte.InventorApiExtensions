using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Hjalte.InventorApiExtensions.Common
{
    public static class XmlFileSerializer
    {
        public static T Deserialize<T>(string xmlFileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            T newObject;
            using (XmlReader reader = XmlReader.Create(xmlFileName))
            {
                newObject = (T)ser.Deserialize(reader);
            }
            return newObject;
        }
        public static void Serialize<T>(T obj, string xmlFileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(xmlFileName))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}
