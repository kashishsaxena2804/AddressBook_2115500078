using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICacheService
    {
        void SetData(string key, string value, TimeSpan expiry);
        string GetData(string key);
        void RemoveData(string key);
    }

}
