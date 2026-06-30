using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wired.WiredInteractables;

namespace Wired.WiredAssets
{
    public class LogicGateAsset : IWiredAsset
    {
        public Guid GUID { get; }
        public LogicGateType Type { get; }
        public LogicGateAsset(Guid guid, LogicGateType type)
        {
            GUID = guid;
            Type = type;
        }
    }
}
