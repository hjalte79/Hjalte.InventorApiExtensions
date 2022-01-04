using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using Hjalte.InventorApiExtensions.UserInterface;
using Inventor;
using Hjalte.InventorApiExtensions.Constants;

namespace Hjalte.ExampleUsage
{
    [TestClass]
    public class CustomInterfaceCreatorExample
    {
		[TestMethod]
		public void Example1()
		{
			Inventor.Application inv = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");

			var interfaceSetup = new UserInterfaceSetup();
			var ribbonSetup = new RibbonSetup() { InternalName = RibbonNames.Part };
			var tabSetup = new TabSetup() { DisplayName = "Test tab", InternalName = "TestTab" };
			var panelSetup = new PanelSetup() { DisplayName = "Test panel", InternalName = "TestPanel" };
			var buttonSetup = new ButtonSetup() 
			{ 
				AssemblyName = "Hjalte.ExampleUsage", 
				ButtonDefinitionName = "Hjalte.ExampleUsage.TestButtonDefinition" 
			};

			interfaceSetup.Ribbons.Add(ribbonSetup);
			ribbonSetup.Tabs.Add(tabSetup);
			tabSetup.Panels.Add(panelSetup);
			panelSetup.Buttons.Add(buttonSetup);
			
			interfaceSetup.Create(inv.UserInterfaceManager);
		}

		[TestMethod]
        public void Example2()
        {
            Inventor.Application inv = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");

            CustomInterfaceCreator.CreateByXmlFile(@"c:\path\to\your\setup.xml", inv.UserInterfaceManager);
        }

		public class TestButtonDefinition : ButtonDefinitionSetup
		{
			public override string DisplayName { get; set; } = "Test Button";
			public override string InternalName { get; set; } = "TestButtonDefinition";
			public override string ToolTipText { get; set; } = "Test description";

			protected override void OnExecute(NameValueMap Context)
			{
				// do you stuff here when the button is clicked
			}
		}

		// "c:\path\to\your\setup.xml" could look like this.

		/*
		<?xml version="1.0" encoding="utf-8" ?>
		<UserInterface>
			<Ribbons>
				<Ribbon>
					<InternalName>Part</InternalName>
					<Tabs>
						<Tab>
							<DisplayName>Test tab</DisplayName> 
							<InternalName>TestTab</InternalName>
							<Panels>
								<Panel>
									<DisplayName>Test panel</DisplayName> 
									<InternalName>TestPanel</InternalName>
									<Buttons>
										<Button>
											<AssemblyName>ILogicRuleTester</AssemblyName>
											<ButtonDefinitionName>ILogicRuleTester.TestButtonDefinition</ButtonDefinitionName>
										</Button>
										<Button>
											<Type>Separator</Type>
										</Button>
										<Button>
											<AssemblyName>ILogicRuleTester</AssemblyName>
											<ButtonDefinitionName>ILogicRuleTester.TestButtonDefinition2</ButtonDefinitionName>
										</Button>
									</Buttons>
								</Panel>
							</Panels>
						</Tab>
					</Tabs>
				</Ribbon>
			</Ribbons>
		</UserInterface>    
		*/

	}
}
