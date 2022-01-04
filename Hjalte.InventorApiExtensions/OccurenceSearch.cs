using Inventor;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Hjalte.InventorApiExtensions
{
    public static class OccurenceSearch
    {
        public static IEnumerable<ComponentOccurrence> FindAll(AssemblyDocument doc, Func<ComponentOccurrence,bool> predicate)
        {
            IEnumerable<ComponentOccurrence> occs = doc.ComponentDefinition.Occurrences.Cast<ComponentOccurrence>();

            IEnumerable<ComponentOccurrence> list = occs.Where(predicate);
            IEnumerable<ComponentOccurrence> assemblyList = occs.
                Where(o => o.DefinitionDocumentType == DocumentTypeEnum.kAssemblyDocumentObject);

            foreach (ComponentOccurrence assOcc in assemblyList)
            {
                if (assOcc.Definition is VirtualComponentDefinition) continue;

                AssemblyDocument assDoc = assOcc.ReferencedDocumentDescriptor.ReferencedDocument as AssemblyDocument;
                IEnumerable<ComponentOccurrence> listToAdd = FindAll(assDoc, predicate);
                list = list.Concat(listToAdd);
            }
            return list.ToList();
        }
    }
}
