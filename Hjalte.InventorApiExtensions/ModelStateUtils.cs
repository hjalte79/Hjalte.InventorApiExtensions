using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjalte.InventorApiExtensions
{
	using Inventor;

	/// <summary>
	/// Utilities for documents and components with model states.
	/// </summary>
	public static class ModelStateUtils
	{
		/// <summary>
		/// For a part or assembly, gets the factory document.
		/// If the argument is not a member document, this returns the argument unchanged.
		/// If the argument is a drawing, returns null.
		/// </summary>
		public static Document GetFactoryDocument(Document doc)
		{
			if (doc == null)
			{
				return null;
			}
			DocumentTypeEnum documentType = doc.DocumentType;
			if (documentType == DocumentTypeEnum.kPartDocumentObject || documentType == DocumentTypeEnum.kAssemblyDocumentObject)
			{
				if (documentType == DocumentTypeEnum.kPartDocumentObject)
				{
					PartComponentDefinition componentDefinition = (doc as PartDocument).ComponentDefinition;
					if (componentDefinition.IsModelStateMember)
					{
						return componentDefinition.FactoryDocument;
					}
				}
				if (documentType == DocumentTypeEnum.kAssemblyDocumentObject)
				{
					AssemblyComponentDefinition componentDefinition2 = (doc as AssemblyDocument).ComponentDefinition;
					if (componentDefinition2.IsModelStateMember)
					{
						return componentDefinition2.FactoryDocument;
					}
				}
				return doc;
			}
			return null;
		}

		/// <summary>
		/// Gets the ModelStates object. Can only be applied to a factory document.
		/// </summary>
		public static ModelStates GetModelStates(Document doc)
		{
			DocumentTypeEnum documentType = doc.DocumentType;
			if (documentType == DocumentTypeEnum.kPartDocumentObject)
			{
				PartComponentDefinition componentDefinition = (doc as PartDocument).ComponentDefinition;
				if (componentDefinition.IsModelStateFactory)
				{
					return componentDefinition.ModelStates;
				}
			}
			if (documentType == DocumentTypeEnum.kAssemblyDocumentObject)
			{
				AssemblyComponentDefinition componentDefinition2 = (doc as AssemblyDocument).ComponentDefinition;
				if (componentDefinition2.IsModelStateFactory || componentDefinition2.ModelStates.Count > 1)
				{
					return componentDefinition2.ModelStates;
				}
			}
			return default;
		}


		public static void UpdateComponentDocument(ComponentOccurrence occ)
		{
			Document memberDocument = GetMemberDocument(occ);
			if (memberDocument != null && memberDocument.RequiresUpdate)
			{
				memberDocument.Update();
			}
		}

		public static Document GetMemberDocument(ComponentOccurrence occ)
		{
			if (occ.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject)
			{
				return (occ.Definition as PartComponentDefinition).ModelStates[occ.ActiveModelState].Document;
			}
			return null;
		}

		public static void AssignTableCell(Document doc, string modelStateName, string columnName, string value)
		{
            if (doc is PartDocument partDocument)
            {
                (partDocument.ComponentDefinition.FactoryDocument as PartDocument).ComponentDefinition.ModelStates.ModelStateTable.TableRows[modelStateName][columnName].Value = value;
            }
        }
	}
}
