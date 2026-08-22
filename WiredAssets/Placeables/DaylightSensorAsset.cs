using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class DaylightSensorAsset : IWiredAsset
    {
        public Guid GUID { get; set; }
        public DaylightSensorMode Mode { get; set; }
        public DaylightSensorAsset(Guid guid, DaylightSensorMode mode)
        {
            GUID = guid;
            Mode = mode;
        }
    }
    public enum DaylightSensorMode
    {
        Day,
        Night
    }
}
