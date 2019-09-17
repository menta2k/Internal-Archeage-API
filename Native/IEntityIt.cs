using GameAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native
{
    public class IEntityIt : GameStruct
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate int dgIsEnd(IntPtr thiscall);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgNext(IntPtr thiscall);

        dgIsEnd _IsEnd;

        dgNext _Next;

        private enum vTableOffsets
        {
            IsEnd = 0x8,
            Next = 0xC,
        }

        public IEntityIt(IntPtr address) : base(address)
        {
            _IsEnd = Marshal.GetDelegateForFunctionPointer<dgIsEnd>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.IsEnd));
            _Next = Marshal.GetDelegateForFunctionPointer<dgNext>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.Next));
        }

        public int IsEnd()
        {
            return _IsEnd(address);
        }

        public IEntity Next()
        {
            return new IEntity(_Next(address));
        }
    }
}
