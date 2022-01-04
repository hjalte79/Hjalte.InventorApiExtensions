using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using Inventor;
using System;
using System.Collections.Generic;
using Hjalte.InventorApiExtensions;

namespace Hjalte.ExampleUsage
{
    [TestClass]
    public class OccurenceSearchExamples
    {
        [TestMethod]
        public void Example1()
        {
            Inventor.Application inv = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");

            AssemblyDocument doc = inv.ActiveDocument as AssemblyDocument;

            IEnumerable<ComponentOccurrence> list;

            list = OccurenceSearch.FindAll(doc, occ => occ.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject);
        }

        [TestMethod]
        public void Example2()
        {
            Inventor.Application inv = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");

            AssemblyDocument doc = inv.ActiveDocument as AssemblyDocument;

            IEnumerable<ComponentOccurrence> list;

            list = OccurenceSearch.FindAll(doc, this.PartNrContains010);
        }



        private bool PartNrContains010(ComponentOccurrence occ)
        {
            ComponentDefinition def = occ.Definition;
            Document doc = def.Document as Document;
            PropertySet propSet = doc.PropertySets["Design Tracking Properties"];
            Property prop = propSet["Part Number"];
            string val = prop.Value.ToString();
            return (val.Contains("010"));
        }
    }
}
