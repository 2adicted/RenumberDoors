using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenumberDoors
{
    public partial class DRInterface : Form
    {
        private Command cmd;

        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;

        private String [] levels;
        private String[] doors;

        private List<string> doorTypes;

        private string number = "01";

        public DRInterface(Autodesk.Revit.UI.UIDocument uidoc, Command cmd)
        {
            this.uidoc = uidoc;
            this.doc = uidoc.Document;
            this.cmd = cmd;

            InitializeComponent();
            InitializeUI();

            doorTypes = new List<string>();
        }

        private void InitializeUI()
        {
            levels = LevelSelector.FindAndSortLevelNames(doc);
            levelDrop.Items.AddRange(levels);
            levelDrop.SelectedItem = levels.FirstOrDefault();

            doors = DoorSelector.GetDoorTypes(doc);
            doorTypeDropDefault.Items.AddRange(doors);

            doorTypeDropDefault.SelectedIndex = 0;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            cmd.level = levelDrop.SelectedItem.ToString();
            if(doorTypeDropDefault.SelectedItem.ToString().Equals("Any"))
            {
                doorTypes.AddRange(doors);
            }
            else
            {
                doorTypes.Add(doorTypeDropDefault.SelectedItem.ToString());
            }
            cmd.doorType = doorTypes;
            cmd.prefix = prefixTextBox.Text.Trim().Equals("") ? "" : prefixTextBox.Text + "-";
            cmd.suffix = suffixTextBox.Text.Trim().Equals("") ? "" : "-" + suffixTextBox.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void prefixTextBox_TextChanged(object sender, EventArgs e)
        {
            if(suffixTextBox.Text.Equals(""))
            {
                doorNameLabel.Text = String.Format("{0}-{1}", prefixTextBox.Text, number);
            }
            else
            {
                doorNameLabel.Text = String.Format("{0}-{1}-{2}", prefixTextBox.Text, number, suffixTextBox.Text);
            }
        }

        private void suffixTextBox_TextChanged(object sender, EventArgs e)
        {
            if (prefixTextBox.Text.Equals(""))
            {
                doorNameLabel.Text = String.Format("{0}-{1}", number, prefixTextBox.Text);
            }
            else
            {
                doorNameLabel.Text = String.Format("{0}-{1}-{2}", prefixTextBox.Text, number, suffixTextBox.Text);
            }
        }
    }
}
