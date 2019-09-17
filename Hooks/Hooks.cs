using EasyHook;
using Process.NET.Applied.Detours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Hooks
{
    public class Hooks
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int dgEndScene(IntPtr devicePtr);

        dgEndScene endScene;

        public Queue<Action> luaQueue = new Queue<Action>();

        public IntPtr globalLuaState;

        public bool HasFinishedHooking = false;

        internal DetourManager detourManager = new DetourManager(ArcheAPI.Instance.proc.Memory);    

        public Detour oPCall;

        public Detour oEndScene;

        public void HookEndScene()
        {
        
        }

        public void HookPCall()
        {
            oPCall = detourManager.CreateAndApply(ArcheAPI.Instance.Lua.hookedPCall, (GameAPI.Lua.LuaAPI.dgPCall)HookedPCall, "HookedPCall", false);
        }

        public void UnhookPCall()
        {
            oPCall.Disable();
        }

        private int HookedPCall(IntPtr pLuaState, int nargs, int nresults, int errfunc)
        {
            if(globalLuaState == IntPtr.Zero)
            {
                ArcheAPI.Instance.Lua.GetGlobal(pLuaState, "X2Chat");
                if(!ArcheAPI.Instance.Lua.IsNil(pLuaState, -1))
                {
                    globalLuaState = pLuaState;
                }
                ArcheAPI.Instance.Lua.Pop(pLuaState, 1);
            }

            if(ArcheAPI.Instance.Lua.auctionLuaState == IntPtr.Zero)
            {
                ArcheAPI.Instance.Lua.GetGlobal(pLuaState, "X2Auction");
                if (!ArcheAPI.Instance.Lua.IsNil(pLuaState, -1))
                {
                    ArcheAPI.Instance.Lua.auctionLuaState = pLuaState;
                }
                ArcheAPI.Instance.Lua.Pop(pLuaState, 1);
            }

            if (pLuaState == globalLuaState)
            {
                lock (luaQueue)
                {
                        foreach (var action in luaQueue)
                        {
                            action();
                        }

                        luaQueue.Clear();
                }
            }

            return (int)oPCall.CallOriginal(pLuaState, nargs, nresults, errfunc);
           // return ArcheAPI.Instance.Native.Lua.PCall(pLuaState, nargs, nresults, errfunc);
        }

    }
}
