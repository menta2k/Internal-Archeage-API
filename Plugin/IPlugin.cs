using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.Plugin
{
    public abstract class IPlugin : MarshalByRefObject
    {

        public abstract string GetName();

        public abstract string GetDescription();

        public abstract string GetVersion();

        public abstract string GetAuthor();
    }
}
