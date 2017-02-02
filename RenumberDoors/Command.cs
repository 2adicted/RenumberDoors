#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
#endregion

namespace RenumberDoors
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<Level> levels = LevelSelector.FindAndSortLevels(doc);

            ElementId levelId = levels.First().Id;

            List<Element> doors = DoorSelector.GetDoors(doc, levelId);

            DoorRenumber renumerator = new DoorRenumber(uiapp, uidoc, doors);
            renumerator.DoorRenumbering();

            return Result.Succeeded;
        }
    }
}
