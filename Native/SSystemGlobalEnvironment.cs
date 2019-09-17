using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native
{
    public class SSystemGlobalEnvironment : GameStruct
    {
        private enum offsets
        {
            pGame = 0x4,
            pNetwork = 0x8,
            pRenderer = 0xC,
            pConsole = 0x1C,
            pAISystem = 0x40,
            pEntitySystem = 0x44,
            pCryFont = 0x48,
            pCryPak = 0x4C,
            pHardwareMouse = 0x60,
            IsMultiplayer = 0x96,
        }

        public SSystemGlobalEnvironment(IntPtr address) : base(address)
        {
          
        }

        public IEntitySystem IEntitySystem
        {
            get
            {
                IntPtr entSys = ArcheAPI.Instance.proc.Memory.Read<IntPtr>(address + (int)offsets.pEntitySystem);
                return new IEntitySystem(entSys);
            }
        }

        public IGame IGame
        {
            get
            {
                IntPtr iGame = ArcheAPI.Instance.proc.Memory.Read<IntPtr>(address + (int)offsets.pGame);
                return new IGame(iGame);
            }
        }
    }
}
