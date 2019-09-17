using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native.Lua
{
    class LuaState : GameStruct
    {
        public LuaState(IntPtr address) : base(address)
        {

        }
    }
}
