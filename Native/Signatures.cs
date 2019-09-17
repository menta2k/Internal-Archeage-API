using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Native.Signatures
{
    public static class sigsLua
    {
        public static string getTop = "55 8B EC 8B 4D 08 8B 41 08 2B 41 0C";

        public static string getField = "55 8B EC 8B 4D 0C 83 EC 08 53 56 8B 75 08 57 8B D6 E8 ? ? ? ? 8B 55 10 8B F8 8B C2 8D 58 01 8A 08 40 84 C9 75 F9 2B C3 50 52 56 E8 ? ? ? ? 89 45 F8 8B 46 08 50";

        public static string insert = "55 8B EC 8B 4D 0C 56 8B 75 08 8B D6 E8 ? ? ? ? 8B 4E 08 3B C8";

        public static string load = "55 8B EC 83 EC 14 56 57";

        public static string loadBuffer = "55 8B EC 83 EC 08 8B 45 0C 8B 55 14";

        public static string next = "55 8B EC 8B 4D 0C 56 8B 75 08 8B D6 E8 ? ? ? ? 8B 4E 08 8B 10 83 E9 08 51 52 56";

        public static string type = "55 8B EC 8B 4D 0C 8B 55 08 E8 ? ? ? ? 3D ? ? ? ? 75 05";

        public static string toNumber = "55 8B EC 8B 4D 0C 8B 55 08 83 EC 08 E8 ? ? ? ? 83 78 04 03 74 17";

        public static string pCall = "55 8B EC 8B 4D 14 83 EC 08";

        public static string pushNil = "55 8B EC 8B 45 08 8B 48 08 C7 41";

        public static string pushNumber = "55 8B EC 8B 45 08 8B 48 08 F3 0F 10 45";

        public static string pushString = "55 8B EC 8B 45 0C 85 C0";

        public static string pushBoolean = "55 8B EC 8B 45 08 8B 48 08 33 D2";

        public static string pushvalue = "55 8B EC 8B 4D 0C 56 8B 75 08 8B D6 E8 ? ? ? ? 8B 4E 08 8B 10 89 11";

        public static string settop = "55 8B EC 8B 4D 0C 8B 45 08";



    }
}
