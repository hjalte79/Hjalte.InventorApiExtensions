using Hjalte.InventorApiExtensions.Common;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hjalte.InventorApiExtensions.UserInterface
{
    [XmlRoot("Tab")]
    public class TabSetup
    {
        public string DisplayName { get; set; }
        public string InternalName { get; set; }
        public string TargetTabInternalName { get; set; } = "";
        public bool InsertBeforeTargetTab { get; set; }  = false;
        public bool Contextual { get; set; }  = false;

        [XmlArray("Panels")]
        [XmlArrayItem("Panel")]
        public List<PanelSetup> Panels { get; set; } = new List<PanelSetup>();

        public void AddToRibbon(Inventor.Ribbon ribbon)
        {
            Guard.StringArgumentIsNotNullOrEmpty(DisplayName, "The DisplayName of the 'tab' can't be null or empty.");
            Guard.StringArgumentIsNotNullOrEmpty(InternalName, "The InternalName of the 'tab' can't be null or empty.");

            Inventor.RibbonTab tab;
            try
            {
                tab = ribbon.RibbonTabs[InternalName];
            }
            catch (Exception)
            {
                tab = ribbon.RibbonTabs.Add(
                    DisplayName, 
                    InternalName, 
                    Guid.NewGuid().ToString(),
                    TargetTabInternalName,
                    InsertBeforeTargetTab,
                    Contextual);
            }

            foreach (var panel in Panels)
            {
                panel.AddToTab(tab);
            }
        }
    }


}
