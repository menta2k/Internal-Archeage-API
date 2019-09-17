using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI
{
    public abstract class GameStruct
    {
        public IntPtr address;

        public GameStruct(IntPtr _address)
        {
            address = _address;
        }
    }
}
