using Hjalte.InventorApiExtensions.Common;
using Inventor;

namespace Hjalte.InventorApiExtensions.UserInterface
{
    public static class CustomInterfaceCreator
    {
        public static UserInterfaceSetup CreateByXmlFile(string xmlFileName, UserInterfaceManager interfaceManager)
        {
            var customInterface = XmlFileSerializer.Deserialize<UserInterfaceSetup>(xmlFileName);
            customInterface.Create(interfaceManager);
            return customInterface;
        }
    }
}
