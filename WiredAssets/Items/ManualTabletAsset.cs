using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    internal class ManualTabletAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public ManualTabletAsset(Guid guid)
        {
            GUID = guid;
        }
    }
}
