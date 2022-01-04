using System.Collections.Generic;
using System.Xml.Serialization;
using Hjalte.InventorApiExtensions.Common;
using Inventor;

namespace Hjalte.InventorApiExtensions.UserInterface
{
    [XmlRoot("Ribbon")]
    public class RibbonSetup
    {
        public string InternalName { get; set; } = "ZeroDoc";

        [XmlArray("Tabs")]
        [XmlArrayItem("Tab")]
        public List<TabSetup> Tabs { get; set; } = new List<TabSetup>();

        public void AddToUserinterface(UserInterfaceManager userInterfaceManager)
        {
            Guard.StringArgumentIsNotNullOrEmpty(InternalName, "The Name of the ribbon Can't be null or empty.");
            var ribbon = userInterfaceManager.Ribbons[InternalName];
            foreach (var tab in Tabs)
            {                
                tab.AddToRibbon(ribbon);
            }
        }

        
    }


}
