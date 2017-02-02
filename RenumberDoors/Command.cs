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
        private static WindowHandle _hWndRevit = null;
        private DRInterface ui;

        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;

        internal string level, doorType;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;

            //Process process = Process.GetCurrentProcess();

            //IntPtr h = process.MainWindowHandle;

            //ShowForm(uiapp, uidoc);

            using (DRInterface ui = new DRInterface(uidoc, this))
            {
                var result = ui.ShowDialog();

                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    Run();
                }
                else
                {

                }
            }

            return Result.Succeeded;
        }

        private void Run()
        {
            List<Autodesk.Revit.DB.Element> doors = DoorSelector.GetDoors(doc, LevelSelector.levelId(doc, level), doorType);

            DoorRenumber renumerator = new DoorRenumber(uidoc, doors);

            ISelectionFilter filter = new DoorRenumber.LineSelectionFilter();

            Autodesk.Revit.DB.Reference reference = uidoc.Selection.PickObject(ObjectType.Element, filter, "Select direction curve");

            Autodesk.Revit.DB.Curve curve = (doc.GetElement(reference.ElementId) as Autodesk.Revit.DB.ModelCurve).GeometryCurve;

            renumerator.DoorRenumbering(curve);
        }

        /// <summary>
        /// De-facto the command is here.
        /// </summary>
        /// <param name="uiapp"></param>
        public void ShowForm(UIApplication uiapp, UIDocument uidoc)
        {
            //get the isntance of Revit Thread
            //to pass it to the Windows Form later
            if (null == _hWndRevit)
            {
                Process process
                  = Process.GetCurrentProcess();

                IntPtr h = process.MainWindowHandle;
                _hWndRevit = new WindowHandle(h);
            }

            //if (ui == null || ui.IsDisposed)
            //{
            //    //new handler
            //    //RequestHandler handler = new RequestHandler();
            //    //new event
            //    //ExternalEvent exEvent = ExternalEvent.Create(handler);

            //    ui = new DRInterface(uidoc);
            //    //pass parent (Revit) thread here
            //    ui.Show(_hWndRevit);
            //}
        }
    }


    /// <summary>
    /// Retrieve Revit Windows thread in order to pass it to the form as it's owner
    /// </summary>
    public class WindowHandle : System.Windows.Forms.IWin32Window
    {
        IntPtr _hwnd;

        public WindowHandle(IntPtr h)
        {
            Debug.Assert(IntPtr.Zero != h,
              "expected non-null window handle");

            _hwnd = h;
        }

        public IntPtr Handle
        {
            get
            {
                return _hwnd;
            }
        }
    }
}
