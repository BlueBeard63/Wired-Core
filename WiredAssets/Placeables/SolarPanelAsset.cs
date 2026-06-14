using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class SolarPanelAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public float Supply { get; set; }
        public float NightSupplyModifier { get; set; }
        public bool HasMovingPart { get; set; }
        public ushort MovingPartId { get; set; }
        public float MovingPartMaxAngle { get; set; }
        public SolarPanelAsset(Guid guid, float supply, float nightsupplymodifier, float movingPartMaxAngle, ushort movingPartId = 0)
        {
            GUID = guid;
            Supply = supply;
            NightSupplyModifier = nightsupplymodifier;
            MovingPartMaxAngle = movingPartMaxAngle;
            MovingPartId = movingPartId;
            if(MovingPartId != 0)
            {
                HasMovingPart = true;
            }
        }
    }
}
