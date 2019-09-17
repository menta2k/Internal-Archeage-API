    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native
{
    public unsafe struct Pointers
    {
        public static IntPtr* SSystem = (IntPtr*)0x36623364;
    }

    public unsafe class Native
    {
        public SSystemGlobalEnvironment SSystem
        {
            get
            {
                return new SSystemGlobalEnvironment(*Pointers.SSystem);
            }
        }
    }
}
