using GameAPI.Extensions;
using GameAPI.Helpers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native
{
    public class IEntity : GameStruct
    {
        private enum vTableOffsets
        {
            GetId = 0x0,
            GetEntityClass = 0x4,
            GetName = 0x30,
            SetPos = 0x78,
            SetRotation = 0x80,
            GetScale = 0x8C,
            SetPosRotScale = 0x94,
            GetWorldPos = 0x98,
            GetWorldAngles = 0x9C,
            GetWorldRotation = 0xA0,
        }

        private enum offsets
        {
            pName = 0x1C,
        }

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate UInt32 dgGetId(IntPtr This);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgGetEntityClass(IntPtr This);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate string dgGetName(IntPtr This);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgGetWorldPos(IntPtr This, InternalVec3 vec);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgGetWorldAngles(IntPtr This, InternalVec3 vec);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgGetWorldRotation(IntPtr This, InternalQuat quat);

        internal dgGetId _GetId;

        internal dgGetEntityClass _GetEntityClass;

        internal dgGetName _GetName;

        internal dgGetWorldPos _GetWorldPos;

        internal dgGetWorldAngles _GetWorldAngles;

        internal dgGetWorldRotation _GetWorldRotation;

        public IEntity(IntPtr address) : base(address) 
        {
            _GetId = Marshal.GetDelegateForFunctionPointer<dgGetId>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetId));
            _GetEntityClass = Marshal.GetDelegateForFunctionPointer<dgGetEntityClass>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetEntityClass));
            _GetName = Marshal.GetDelegateForFunctionPointer<dgGetName>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetName));
            _GetWorldPos = Marshal.GetDelegateForFunctionPointer<dgGetWorldPos>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetWorldPos));
            _GetWorldAngles = Marshal.GetDelegateForFunctionPointer<dgGetWorldAngles>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetWorldAngles));
            _GetWorldRotation = Marshal.GetDelegateForFunctionPointer<dgGetWorldRotation>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetWorldRotation));
        }

        public UInt32 Id
        {
            get
            {
                return _GetId(address);
            }
        }

        public Vector3 WorldPos
        {
            get
            {
                InternalVec3 vec3 = Marshal.PtrToStructure<InternalVec3>(_GetWorldPos(address, new InternalVec3()));
                return new Vector3(vec3.X, vec3.Y, vec3.Z);
            }
        }

        public Vector3 WorldAngles
        {
            get
            {
                InternalVec3 vec3 = Marshal.PtrToStructure<InternalVec3>(_GetWorldAngles(address, new InternalVec3()));
                return new Vector3(vec3.X, vec3.Y, vec3.Z);
            }
        }

        public Quaternion WorldRotation
        {
            get
            {
                InternalQuat quat = Marshal.PtrToStructure<InternalQuat>(_GetWorldRotation(address, new InternalQuat()));
                return new Quaternion(quat.X, quat.Y, quat.Z, quat.W);
            }
        }

        public string Name
        {
            get
            {
                IntPtr nameAddr = ArcheAPI.Instance.proc.Memory.Read<IntPtr>(address + (int)offsets.pName);
                return ArcheAPI.Instance.proc.Memory.Read(nameAddr, Encoding.UTF8, 256);
            }
        }


    }
}
