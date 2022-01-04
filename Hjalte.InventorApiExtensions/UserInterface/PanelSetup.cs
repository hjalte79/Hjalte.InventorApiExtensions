using Hjalte.InventorApiExtensions.Common;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hjalte.InventorApiExtensions.UserInterface
{
    [XmlRoot("Panel")]
    public class PanelSetup
    {
        public string DisplayName { get; set; }
        public string InternalName { get; set; }
        public string TargetPanelInternalName { get; set; } = "";
        public bool InsertBeforeTargetPanel { get; set; } = false;

        [XmlArray("Buttons")]
        [XmlArrayItem("Button")]
        public List<ButtonSetup> Buttons { get; set; } = new List<ButtonSetup>();

        public void AddToTab(Inventor.RibbonTab tab)
        {
            Guard.StringArgumentIsNotNullOrEmpty(DisplayName, "The DisplayName of the 'tab' can't be null or empty.");
            Guard.StringArgumentIsNotNullOrEmpty(InternalName, "The InternalName of the 'tab' can't be null or empty.");

            Inventor.RibbonPanel panel;
            try
            {
                panel = tab.RibbonPanels[InternalName];
            }
            catch (Exception)
            {
                panel = tab.RibbonPanels.Add(
                    DisplayName, 
                    InternalName, 
                    Guid.NewGuid().ToString(),
                    TargetPanelInternalName, 
                    InsertBeforeTargetPanel);
            }

            foreach (ButtonSetup button in Buttons)
            {
                button.AddToPanel(panel);
            }
        }
    }


}
