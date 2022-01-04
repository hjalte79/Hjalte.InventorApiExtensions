using System;
using Hjalte.InventorApiExtensions.Common;
using Inventor;

namespace Hjalte.InventorApiExtensions.UserInterface
{
    /// <summary>
    /// The setup data for a ButtonDefinition
    ///     The following Parameters are always requiered
    ///     - DisplayName
    ///     - InternalName
    /// </summary>
    public abstract class ButtonDefinitionSetup 
    {
        public abstract string DisplayName { get; set; }
        public abstract string InternalName { get; set; }
        public virtual CommandTypesEnum Classification { get; set; } = CommandTypesEnum.kEditMaskCmdType;
        public virtual object ClientId { get; set; } = null;
        public virtual string DescriptionText { get; set; } = "";
        public virtual string ToolTipText { get; set; } = "";

        /// <summary>
        /// Optional input Picture (IPictureDisp) object that specifies the standard size icon to 
        /// use for the controls using this definition. A standard size icon is 16 pixels wide and 
        /// 16 pixels high. If not supplied the button will be created as a text only button and 
        /// the LargeIcon argument is ignored.
        /// </summary>
        public virtual object StandardIcon { get; set; } = null;
        public virtual object LargeIcon { get; set; } = null;
        public ButtonDisplayEnum ButtonDisplay { get; set; } = ButtonDisplayEnum.kDisplayTextInLearningMode;

        public Inventor.ButtonDefinition GetInventorObject(Inventor.ControlDefinitions controlDefinitions)
        {
            Guard.StringArgumentIsNotNullOrEmpty(DisplayName, "The DisplayName of the 'ButtonDefinition' can't be null or empty.");
            Guard.StringArgumentIsNotNullOrEmpty(InternalName, "The InternalName of the 'ButtonDefinition' can't be null or empty.");

            ButtonDefinition Definition;
            
            try
            {
                ControlDefinition exsistingDef = controlDefinitions[InternalName];
                if (exsistingDef != null)
                {
                    if (exsistingDef is ButtonDefinition definition)
                    {
                        return definition;
                    }
                    else
                    {
                        throw new CustomInterfaceException($"{InternalName} exsists but is not a ButtonDefinition");
                    }
                }
            }
            catch (Exception)
            {
                // exception is to be expected if the definition is created for the first time.
                // Checking if the definition exsists is slowwer then catching this exception.
            }            

            Definition = controlDefinitions.AddButtonDefinition(
                DisplayName,
                InternalName,
                Classification,
                ClientId,
                DescriptionText,
                ToolTipText,
                StandardIcon,
                LargeIcon,
                ButtonDisplay);
            Definition.OnExecute += OnExecute;
            Definition.OnHelp += OnHelp;

            return Definition;
        }

        protected abstract void OnExecute(NameValueMap Context);
        protected virtual void OnHelp(NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            HandlingCode = HandlingCodeEnum.kEventHandled;
        }
    }


}
