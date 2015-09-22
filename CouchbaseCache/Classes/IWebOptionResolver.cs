using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchbaseCache.Classes
{
    public interface IWebOptionResolver
    {
        WebOptions Resolve();
        void ClearCache();
    }
}
