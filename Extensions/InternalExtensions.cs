using Process.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Extensions
{
    public static class InternalExtensions
    {
        public static IntPtr GetStructVtableFunction(this ProcessSharp pSharp, IntPtr structAddress, int offset)
        {
            IntPtr functionPtr = pSharp.Memory.Read<IntPtr>(structAddress, 1)[0] + offset;
            return pSharp.Memory.Read<IntPtr>(functionPtr, 1)[0];
        }

        public static T ReadPtr<T>(this ProcessSharp pSharp, IntPtr intPtr, int[] offsets)
        {
            for (int i = 0; i <= offsets.Length - 1; i++)
            {
                if (i == offsets.Length - 1)
                {
                    return pSharp.Memory.Read<T>(intPtr + offsets[i]);
                }

                else
                {
                    intPtr = pSharp.Memory.Read<IntPtr>(intPtr + offsets[i]);
                }
            }

            return default(T);
        }
    }
}
