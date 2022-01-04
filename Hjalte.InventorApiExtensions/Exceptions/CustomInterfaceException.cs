using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hjalte.InventorApiExtensions
{
    [Serializable]
    public class CustomInterfaceException : Exception
    {
        public CustomInterfaceException() { }
        public CustomInterfaceException(string message) : base(message) { }
        public CustomInterfaceException(string message, Exception inner) : base(message, inner) { }

        protected CustomInterfaceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
