using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets.Placeables
{
    public class DayNightSensorAsset : IWiredAsset
    {
        public Guid GUID { get; set; }
        public DayNightSensorMode Mode { get; set; }
    }
    public enum DayNightSensorMode
    {
        Day,
        Night
    }
}
