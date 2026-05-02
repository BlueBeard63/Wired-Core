using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class WiringToolAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public WiringToolAsset(Guid guid)
        {
            GUID = guid;
        }
    }
}
