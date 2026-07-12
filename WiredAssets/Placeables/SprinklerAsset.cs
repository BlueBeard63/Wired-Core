using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class SprinklerAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public float Consumption { get; }
        public float Radius { get; }
        public SprinklerAsset(Guid guid, float consumption, float radius)
        {
            GUID = guid;
            Consumption = consumption;
            Radius = radius;
        }
    }
}
