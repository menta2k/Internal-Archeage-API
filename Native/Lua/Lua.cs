using GameAPI.Native.Signatures;
using Process.NET.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Lua
{
    public class LuaAPI
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int dgPCall(IntPtr pLuaState, int nargs, int nresults, int errfunc);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int dgLoadBuffer(IntPtr pLuaState, [MarshalAs(UnmanagedType.LPStr)]string buff, UInt32 size, [MarshalAs(UnmanagedType.LPStr)]string name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void dgGetField(IntPtr pLuaState, int index, [MarshalAs(UnmanagedType.LPStr)]string k);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int dgType(IntPtr pLuaState, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int dgSetTop(IntPtr pLuaState, int index);

        internal dgPCall hookedPCall;

        public dgLoadBuffer LoadBuffer;

        dgGetField GetField;

        dgType Type;

        dgSetTop SetTop;

        private string moduleName = "CryScriptSystem";

        internal IntPtr pCallAddress;

        public IntPtr auctionLuaState;

        public LuaAPI()
        {
            pCallAddress = ArcheAPI.Instance.Find(moduleName, sigsLua.pCall).BaseAddress;
            hookedPCall = Marshal.GetDelegateForFunctionPointer<dgPCall>(pCallAddress);

            var loadBufferAddress = ArcheAPI.Instance.Find(moduleName, sigsLua.loadBuffer).BaseAddress;
            LoadBuffer = Marshal.GetDelegateForFunctionPointer<dgLoadBuffer>(loadBufferAddress);

            GetField = Marshal.GetDelegateForFunctionPointer<dgGetField>(ArcheAPI.Instance.Find(moduleName, sigsLua.getField).BaseAddress);

            Type = Marshal.GetDelegateForFunctionPointer<dgType>(ArcheAPI.Instance.Find(moduleName, sigsLua.type).BaseAddress);

            SetTop = Marshal.GetDelegateForFunctionPointer<dgSetTop>(ArcheAPI.Instance.Find(moduleName, sigsLua.settop).BaseAddress);
        }

        public void GetGlobal(IntPtr pLuaState, [MarshalAs(UnmanagedType.LPStr)]string k)
        {
            GetField(pLuaState, -10002, k);
        }

        public bool IsNil(IntPtr pLuaState, int index)
        {
            return Type(pLuaState, index) == 0;
        }

        public void Pop(IntPtr pLuaState, int index)
        {
            SetTop(pLuaState, -(index) - 1);
        }

        public int PCall(IntPtr pLuaState, int nargs, int nresults, int errfunc)
        {
            return (int)ArcheAPI.Instance.Hooks.oPCall.CallOriginal(pLuaState, nargs, nresults, errfunc);
        }

        /// <summary>
        /// Executes a lua script or plain lua text.
        /// </summary>
        /// <param name="pLuaState"></param>
        /// <param name="buff">The lua text, or string contents of a lua script.</param>
        /// <param name="size">the size of the lua script you're executing(or string length for plain text)</param>
        /// <param name="name">the name of the script you're exectuign</param>
        public void ExecuteLuaScript(IntPtr pLuaState, [MarshalAs(UnmanagedType.LPStr)]string buff, UInt32 size, [MarshalAs(UnmanagedType.LPStr)]string name)
        {
            GameAPI.ArcheAPI.Instance.Lua.LoadBuffer(pLuaState, buff, size, name);
            GameAPI.ArcheAPI.Instance.Lua.PCall(GameAPI.ArcheAPI.Instance.Hooks.globalLuaState, 0, 0, 0);
        }

        /// <summary>
        /// Executes a delegate inside of the lua thread. All lua function's must be called through this function, due to thread safety issues.
        /// </summary>
        /// <param name="action"></param>
        public void Execute(Action action)
        {
            bool isFinished = false;
            lock(ArcheAPI.Instance.Hooks.luaQueue)
            {
                ArcheAPI.Instance.Hooks.luaQueue.Enqueue(action);
                ArcheAPI.Instance.Hooks.luaQueue.Enqueue(new Action(() => { isFinished = true; }));
            }

            while(!isFinished)
            {

            }
        }
    }
}
