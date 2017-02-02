using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenumberDoors
{
    public static class LevelSelector
    {
        public static List<Level> FindAndSortLevels(Document doc)
        {
            return new FilteredElementCollector(doc)
                            .WherePasses(new ElementClassFilter(typeof(Level), false))
                            .Cast<Level>()
                            .OrderBy(e => e.Elevation)
                                .ToList();
        }
    }
    /// <summary>
    /// Retrieves all doors on a given level
    /// </summary>
    public static class DoorSelector
    {
        public static List<Element> GetDoors(Document doc, ElementId levelId)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            List<Element> elements = collector
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .ToList()
                .Where(x => x.LevelId == levelId)
                .ToList();

            return elements;
        }

    }
    /// <summary>
    /// Door Renumber Class
    /// Will Renumber all doors on particular level 
    /// </summary>
    class DoorRenumber
    {
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Autodesk.Revit.ApplicationServices.Application app;
        private Document doc;
        private List<Element> elements;

        private int increment = 0;

        public DoorRenumber(UIApplication uiapp, UIDocument uidoc, List<Element> elements)
        {
            this.uiapp = uiapp;
            this.uidoc = uidoc;
            this.app = uiapp.Application;
            this.doc = uidoc.Document;
            this.elements = elements;
        }

        public void DoorRenumbering()
        {
            ISelectionFilter filter = new LineSelectionFilter();

            Reference reference = uidoc.Selection.PickObject(ObjectType.Element, filter, "Select direction curve");

            Curve curve = (doc.GetElement(reference.ElementId) as ModelCurve).GeometryCurve;


            List<LocationPoint> points = new List<LocationPoint>();
            
            Dictionary<Element, double> sort = new Dictionary<Element, double>();

            foreach (Element element in elements)
            {
                FamilyInstance door = element as FamilyInstance;

                XYZ point = door.GetTransform().Origin;

                if (point == null)
                {
                    continue;
                }

                IntersectionResult closestPoint = curve.Project(point);

                sort.Add(element, curve.ComputeNormalizedParameter(closestPoint.Parameter));
            }

            List<Element> sorted = sort.OrderBy(x => x.Value).Select(x => x.Key).ToList();

            using (Transaction t = new Transaction(doc, "Rename Mark Values"))
            {
                t.Start();
                foreach (Element el in sorted)
                {
                    el.LookupParameter("Mark").Set(increment.ToString());
                    increment++;
                }
                t.Commit();
            }
        }

        public class LineSelectionFilter : ISelectionFilter
        {
            // determine if the element should be accepted by the filter
            public bool AllowElement(Element element)
            {
                // Convert the element to a ModelLine
                ModelLine line = element as ModelLine;
                ModelCurve curve = element as ModelCurve;
                // line is null if the element is not a model line
                if (line == null && curve == null)
                {
                    return false;
                }
                // return true if the line is a model line
                return true;
            }

            // references will never be accepeted by this filter, so always return false
            public bool AllowReference(Reference refer, XYZ point)
            { return false; }
        }
    }
}