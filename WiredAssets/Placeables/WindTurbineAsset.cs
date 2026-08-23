using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class WindTurbineAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public float Supply { get; }
        public WindTurbineAsset(Guid GUID, float supply)
        {
            this.GUID = GUID;
            Supply = supply;
        }
    }
}
