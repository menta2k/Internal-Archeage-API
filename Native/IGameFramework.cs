using GameAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native
{
    public class IGameFramework : GameStruct
    {
        private enum vTableOffsets
        {
            GetIActorSystem = 0x54,
            GetClientActor = 0xA4,
            GetClientActorId = 0xA8,
        }

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgGetClientActor(IntPtr thiscall);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate UInt32 dgGetClientActorId(IntPtr thiscall);

        dgGetClientActor _GetClientActor;

        dgGetClientActorId _GetClientActorId;

        public IGameFramework(IntPtr address) : base(address)
        {
            _GetClientActorId = Marshal.GetDelegateForFunctionPointer<dgGetClientActorId>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetClientActorId));
            _GetClientActor = Marshal.GetDelegateForFunctionPointer<dgGetClientActor>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetClientActor));
        }

        public UInt32 ClientActorId
        {
            get
            {
                return _GetClientActorId(address);
            }
        }
    }
}
