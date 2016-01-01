using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public class SerializerFactory
    {
        public static ISerializer GetInstance()
        {
            return new LocalXmlSerializer();
        }
    }
}
