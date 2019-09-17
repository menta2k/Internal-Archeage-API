using GameAPI.Lua;
using Process.NET;
using Process.NET.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI
{
    public class ArcheAPI
    {
        public static ArcheAPI Instance;

        public ProcessSharp proc;

        private Native.Native native;

        private Hooks.Hooks hooks;

        private LuaAPI lua;

        private BotClasses.Auction auction;

        public ArcheAPI()
        {
            proc = new ProcessSharp(System.Diagnostics.Process.GetCurrentProcess(), Process.NET.Memory.MemoryType.Local);
            Instance = this;
        }

        public Native.Native Native
        {
            get
            {
                if(native == null)
                {
                    native = new GameAPI.Native.Native();
                }

                return native;
            }
        }

        public Hooks.Hooks Hooks
        {
            get
            {
                if (hooks == null)
                {
                    hooks = new GameAPI.Hooks.Hooks();
                }

                return hooks;
            }
        }

        public LuaAPI Lua
        {
            get
            {
                if (lua == null)
                {
                    lua = new LuaAPI();
                }

                return lua;
            }
        }

        public BotClasses.Auction Auction
        {
            get
            {
                if (auction == null)
                {
                    auction = new BotClasses.Auction();
                }

                return auction;
            }
        }

        public PatternScanResult Find(string moduleName, string pattern)
        {
            var scanner = new PatternScanner(proc[moduleName]);
            return scanner.Find(new DwordPattern(pattern));
        }
    }
}
