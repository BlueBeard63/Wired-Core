using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class BatteryAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public float Supply { get; }
        public float Capacity { get; }
        public float MaxBurnPerSecond { get; }
        public BatteryAsset(Guid guid, float supply, float capacity, float maxburn)
        {
            GUID = guid;
            Capacity = capacity;
            MaxBurnPerSecond = maxburn;
            Supply = supply;
        }
    }
}
