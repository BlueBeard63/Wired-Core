using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class KeypadAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public float StaysOpenSeconds;
        public KeypadAsset(Guid guid, float staysOpenSeconds)
        {
            GUID = guid;
            StaysOpenSeconds = staysOpenSeconds;
        }
    }
}
