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

        public static String [] FindAndSortLevelNames(Document doc)
        {
            return new FilteredElementCollector(doc)
                            .WherePasses(new ElementClassFilter(typeof(Level), false))
                            .Cast<Level>()
                            .OrderBy(e => e.Elevation)
                            .Select(x => x.Name)
                            .ToArray();
        }

        public static ElementId levelId(Document doc, string level)
        {
            return new FilteredElementCollector(doc)
                            .WherePasses(new ElementClassFilter(typeof(Level), false))
                            .Cast<Level>()
                            .Where(x => x.Name.Equals(level))
                            .Select(x => x.Id)
                            .First();
        }
    }
    /// <summary>
    /// Retrieves all doors on a given level
    /// </summary>
    public static class DoorSelector
    {
        public static List<Element> GetDoors(Document doc, ElementId levelId, List<string> doorType)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            List<Element> elements = collector
                .OfCategory(BuiltInCategory.OST_Doors)
                .WherePasses(new ElementClassFilter(typeof(FamilyInstance), false))
                .Cast<FamilyInstance>()
                .Where(x => x.LevelId == levelId)
                .Where(x => doorType.Contains(x.Symbol.FamilyName))
                .Cast<Element>()
                .ToList();

            return elements;
        }
        public static List<Element> GetDoors(Document doc, ElementId levelId, List<string> doorType, bool inverse)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            if (inverse)
            {
                List<Element> elements = collector
                .OfCategory(BuiltInCategory.OST_Doors)
                .WherePasses(new ElementClassFilter(typeof(FamilyInstance), false))
                .Cast<FamilyInstance>()
                .Where(x => x.LevelId == levelId)
                .Where(x => !doorType.Contains(x.Symbol.FamilyName))
                .Cast<Element>()
                .ToList();

                return elements;
            }
            else
            {
                return GetDoors(doc, levelId, doorType);
            }
        }

        public static String [] GetDoorTypes(Document doc)
        {
            return new FilteredElementCollector(doc)
                            .OfCategory(BuiltInCategory.OST_Doors)
                            .WherePasses(new ElementClassFilter(typeof(FamilySymbol), false))
                            .Cast<FamilySymbol>()
                            .GroupBy(e => e.FamilyName)
                            .Select(e => e.Key)
                            .ToArray();
        }

    }
    /// <summary>
    /// Door Renumber Class
    /// Will Renumber all doors on particular level 
    /// </summary>
    class DoorRenumber
    {
        //private UIApplication uiapp;
        private UIDocument uidoc;
        //private Autodesk.Revit.ApplicationServices.Application app;
        private Document doc;
        private List<Element> elements;
        private string prefix, suffix;

        private int increment = 1;

        private string format;

        public DoorRenumber(UIDocument uidoc, List<Element> elements, string prefix, string suffix)
        {
            //this.uiapp = uiapp;
            //this.app = uiapp.Application;
            this.uidoc = uidoc;
            this.doc = uidoc.Document;
            this.elements = elements;
            this.prefix = prefix;
            this.suffix = suffix;
            SetFormat(elements.Count);
        }

        private void SetFormat(int count)
        {
            format = String.Format("{0}{1}", "D", count.ToString().ToCharArray().Count().ToString());
        }

        public void DoorRenumbering(Curve curve)
        {
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
                    el.LookupParameter("Mark").Set(String.Format("{0}{1}{2}", prefix, increment.ToString(format), suffix));
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