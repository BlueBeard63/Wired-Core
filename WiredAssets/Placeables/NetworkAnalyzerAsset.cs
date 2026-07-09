using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    internal class NetworkAnalyzerAsset : IWiredAsset
    {
        public Guid GUID { get; private set; }

        public float Consumption { get; set; }
        public ushort DisplaySignID { get; set; }
        public NetworkAnalyzerAsset(Guid guid, float consumption, ushort displayID)
        {
            GUID = guid;
            Consumption = consumption;
            DisplaySignID = displayID;
        }
    }
}
