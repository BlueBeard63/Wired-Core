using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class ButtonAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public float StaysPressedSecons { get; }
        public ButtonAsset(Guid guid, float staysPressedSecons)
        {
            GUID = guid;
            StaysPressedSecons = staysPressedSecons;
        }
    }
}
