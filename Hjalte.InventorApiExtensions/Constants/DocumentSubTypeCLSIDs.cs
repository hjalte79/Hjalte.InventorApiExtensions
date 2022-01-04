using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjalte.InventorApiExtensions.Constants
{
    public static class DocumentSubTypeClsIDs
    {
        public static string Part => "{4D29B490-49B2-11D0-93C3-7E0706000000}";
        public static string SheetMetalPart => "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}";
        public static string GenericProxyPart => "{92055419-B3FA-11D3-A479-00C04F6B9531}";
        public static string CompatibilityProxyPart => "{9C464204-9BAE-11D3-8BAD-0060B0CE6BB4}";
        public static string CatalogProxyPart => "{9C88D3AF-C3EB-11D3-B79E-0060B0F159EF}";
        public static string MoldedPart => "{4D8D80D4-F5B0-4460-8CEA-4CD222684469}";


        public static string Assembly => "{E60F81E1-49B3-11D0-93C3-7E0706000000}";
        public static string WeldmentAssembly => "{28EC8354-9024-440F-A8A2-0E0E55D635B0}";

        public static string Drawing => "{BBF9FDF1-52DC-11D0-8C04-0800090BE8EC}";

        public static string DesignElement => "{62FBB030-24C7-11D3-B78D-0060B0F159EF}";

        public static string Presentation => "{76283A80-50DD-11D3-A7E3-00C04F79D7BC}";
        public static string CompositePresentation => "{A2B4C17D-F0D2-4C0F-9799-DD5F71DFB291}";

        public static string DesignView => "{81B95C5D-8E31-4F65-9790-CCF6ECABD141}";
    }
}
