using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GameAPI.Extensions;
namespace GameAPI.Native
{
    public class IEntitySystem : GameStruct
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgGetEntityFromId(IntPtr thiscall, UInt32 id);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal unsafe delegate IntPtr dgFindEntityByName(IntPtr thiscall, char* sEntityName);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate int dgGetNumEntities(IntPtr thiscall);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate IntPtr dgGetEntityIterator(IntPtr thiscall);

        internal dgGetEntityFromId _GetEntityFromId;

        internal dgFindEntityByName _FindEntityByName;

        internal dgGetNumEntities _GetNumEntities;

        internal dgGetEntityIterator _GetEntityIterator;

        private enum vTableOffsets
        {
            GetEntityFromId = 0x38,
            GetClonedEntityId = 0x3C,
            FindEntityByName = 0x40,
            GetNumEntities = 0x50,
            GetEntityIterator = 0x54,
        }

        public IEntitySystem(IntPtr address) : base(address)
        {
            _GetEntityFromId = Marshal.GetDelegateForFunctionPointer<dgGetEntityFromId>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetEntityFromId));
            _FindEntityByName = Marshal.GetDelegateForFunctionPointer<dgFindEntityByName>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.FindEntityByName));
            _GetNumEntities = Marshal.GetDelegateForFunctionPointer<dgGetNumEntities>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetNumEntities));
            _GetEntityIterator = Marshal.GetDelegateForFunctionPointer<dgGetEntityIterator>(ArcheAPI.Instance.proc.GetStructVtableFunction(address, (int)vTableOffsets.GetEntityIterator));
        }

        public int GetNumEntities()
        {
            return _GetNumEntities(address);
        }

        public IEntityIt GetEntityIterator()
        {
            return new IEntityIt(_GetEntityIterator(address));
        }

        public IEntity GetEntityFromId(UInt32 id)
        {
            return new IEntity(_GetEntityFromId(address, id));
        }

        public unsafe IEntity FindEntityByName(char* sEntityName)
        {
            return new IEntity(_FindEntityByName(address, sEntityName));
        }

        public IEntity GetClientEntity()
        {
            var localEntId = ArcheAPI.Instance.Native.SSystem.IGame.IGameFramework.ClientActorId;
            return GetEntityFromId(localEntId);
        }

    }
}
