using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class ConnectorAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public ConnectorAsset(Guid guid)
        {
            GUID = guid;
        }
    }
}
