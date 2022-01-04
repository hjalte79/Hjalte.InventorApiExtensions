using System;
using System.Runtime.Remoting;
using System.Xml.Serialization;
using Hjalte.InventorApiExtensions.Common;
using Inventor;

namespace Hjalte.InventorApiExtensions.UserInterface
{
    public enum ButtonType { Button, Separator } // Others can be implemeted later


    [XmlRoot("Button")]
    public class  ButtonSetup
    {
        public string AssemblyName { get; set; } = null;
        public string ButtonDefinitionName { get; set; }
        public bool UseLargeIcon { get; set; } = false;
        public bool ShowText { get; set; } = true;
        public string TargetControlInternalName { get; set; } = "";
        public bool InsertBeforeTargetControl { get; set; } = false;
        public ButtonType Type { get; set; } = ButtonType.Button;

        public void AddToPanel(Inventor.RibbonPanel panel)
        {
            switch (Type)
            {
                case ButtonType.Button:
                    AddButton(panel);
                    break;
                case ButtonType.Separator:
                    panel.CommandControls.AddSeparator(TargetControlInternalName, InsertBeforeTargetControl);
                    break;
            }
        }
         
        
        private void AddButton(Inventor.RibbonPanel panel)
        {
            Guard.StringArgumentIsNotNullOrEmpty(ButtonDefinitionName, "The ButtonDefinitionName of the 'Button' can't be null or empty.");

            Inventor.Application inventor = panel.Application;

            ObjectHandle handle = Activator.CreateInstance(AssemblyName, ButtonDefinitionName);
            ButtonDefinitionSetup definition = (ButtonDefinitionSetup)handle.Unwrap();
            Inventor.ButtonDefinition inventorDefinition = definition.GetInventorObject(inventor.CommandManager.ControlDefinitions);

            var exsistingControl = GetControl(panel, inventorDefinition);
            if (exsistingControl == null)
            {
                panel.CommandControls.AddButton(
                    inventorDefinition,
                    UseLargeIcon,
                    ShowText,
                    TargetControlInternalName,
                    InsertBeforeTargetControl);
            }            
        }

        private CommandControl GetControl(RibbonPanel panel, ButtonDefinition definition)
        {
            try
            {
                return panel.CommandControls[definition.InternalName];

            }
            catch (Exception)
            {
                // exception is to be expected if the definition is created for the first time.
                // Checking if the definition exsists is slowwer then catching this exception.
                return null;
            }
        }
    }


}
