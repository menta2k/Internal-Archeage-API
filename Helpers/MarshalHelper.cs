using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Helpers
{
    public static class MarshalHelper
    {
        public static IntPtr ObjToPtr(object obj)
        {
            GCHandle handle = GCHandle.Alloc(obj);
            return (IntPtr)handle;
        }

        public static T PtrToObj<T>(IntPtr ptr)
        {
            GCHandle handle = (GCHandle)ptr;
            return (T)handle.Target;
        }
    }
}
