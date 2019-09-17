using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native
{
    [StructLayout(LayoutKind.Explicit)]
    public class InternalQuat
    {
        [FieldOffset(0)]
        public float X; //0x0000
        [FieldOffset(4)]
        public float Y; //0x0004
        [FieldOffset(8)]
        public float Z; //0x0008
        [FieldOffset(12)]
        public float W; //0x000C
    }

    [StructLayout(LayoutKind.Explicit)]
    public class InternalVec3 // Z and Y we're swapped. Remember to change them.
    {
        [FieldOffset(0)]
        public float X; //0x0000
        [FieldOffset(4)]
        public float Z; //0x0004
        [FieldOffset(8)]
        public float Y; //0x0008

    }
}