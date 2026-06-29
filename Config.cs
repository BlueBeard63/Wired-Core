using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wired
{
    public class Config : IRocketPluginConfiguration, IDefaultable
    {
        public bool LogDebugMessages;
        public ushort RecalculationRateLimit;
        public void LoadDefaults()
        {
            RecalculationRateLimit = 1;
            LogDebugMessages = true;
        }
    }
}
