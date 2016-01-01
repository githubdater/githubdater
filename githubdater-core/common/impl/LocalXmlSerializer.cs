using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGitHubdater
{
    class LocalXmlSerializer : ISerializer
    {
        public T Load<T>(string path)
        {
            T instance;

            using (TextReader reader = new StreamReader(path))
            {
                var xml = new XmlSerializer(typeof(T));
                instance = (T)xml.Deserialize(reader);
            }

            return instance;
        }

        public void Save<T>(string path, T obj)
        {
            if (File.Exists(path))
                File.Delete(path);

            using (Stream writer = new FileStream(path, FileMode.Create))
            {
                var xml = new XmlSerializer(typeof(T));
                xml.Serialize(writer, obj);
            }
        }
    }
}
