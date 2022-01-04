using System.Collections.Generic;
using System.Xml.Serialization;
using Inventor;

namespace Hjalte.InventorApiExtensions.UserInterface
{
    [XmlRoot("UserInterface")]
    public class UserInterfaceSetup
    {
        [XmlArray("Ribbons")]
        [XmlArrayItem("Ribbon")]
        public List<RibbonSetup> Ribbons { get; set; } = new List<RibbonSetup>();


        public void Create(UserInterfaceManager userInterfaceManager)
        {
            foreach (var ribbon in Ribbons)
            {
                ribbon.AddToUserinterface(userInterfaceManager);
            }
        }
    }


}
