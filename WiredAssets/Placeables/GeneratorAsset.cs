using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired.WiredAssets
{
    public class GeneratorAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public float Supply { get; set; }
        public GeneratorAsset(Guid guid, float supply)
        {
            GUID = guid;
            Supply = supply;
        }
    }
}
