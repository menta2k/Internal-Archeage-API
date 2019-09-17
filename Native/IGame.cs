using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native
{
    public class IGame : GameStruct
    {
        public enum offsets
        {
            IGameFramework = 0x18,
        }
        public IGame(IntPtr address) : base(address)
        {

        }

        public IGameFramework IGameFramework
        {
            get
            {
                return new IGameFramework(ArcheAPI.Instance.proc.Memory.Read<IntPtr>(address + (int)offsets.IGameFramework));
            }
        }
    }
}
