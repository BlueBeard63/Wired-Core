using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class DayNightSensorAsset : IWiredAsset
    {
        public Guid GUID { get; set; }
        public DayNightSensorMode Mode { get; set; }
        public DayNightSensorAsset(Guid guid, DayNightSensorMode mode)
        {
            GUID = guid;
            Mode = mode;
        }
    }
    public enum DayNightSensorMode
    {
        Day,
        Night
    }
}
