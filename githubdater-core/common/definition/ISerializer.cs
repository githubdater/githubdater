using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface ISerializer
    {
        T Load<T>(string path);
        void Save<T>(string path, T obj);
    }
}
