using System;
using SmartB1t.Database.ClassGeneratorUtil.SmartB1tGenerationClasses;
using System.Windows.Forms;
using System.Linq;

namespace SmartB1t.Database.ClassGeneratorUtil.Forms
{
    public partial class frmAddEditAttribute : Form
    {
        SmartB1tCSClass currCS;
        SmartB1tCSVar editVar;
        public frmAddEditAttribute(SmartB1tCSClass currCS)
        {
            InitializeComponent();
            this.currCS = currCS;
            FillWithCustomTypes();
            cbDataType.SelectedIndex = 0;
        }

        public frmAddEditAttribute(SmartB1tCSClass currCS, SmartB1tCSVar currVar)
        {
            InitializeComponent();
            Text = "Edit attribute";
            this.currCS = currCS;
            editVar = currVar;
            FillWithCustomTypes();
            txtVarname.Text = currVar.VarName;
            if (currVar.IsCustomType)
                cbDataType.SelectedItem = currCS;
            else
                cbDataType.SelectedItem = currVar.DataType;
        }

        private void FillWithCustomTypes()
        {
            foreach (SmartB1tCSClass cs in GlobalData.GlobalProject.Classes)
                cbDataType.Items.Add(cs);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtVarname.Text != "")
            {
                if (!VarExistsByName(txtVarname.Text))
                {
                    if (txtVarname.Text.Contains(" "))
                        txtVarname.Text = txtVarname.Text.Replace(' ', '_');
                    bool isCustomType = cbDataType.SelectedItem is SmartB1tCSClass;
                    if (editVar == null)
                    {
                        //Add new var
                        SmartB1tCSVar csv = new SmartB1tCSVar(txtVarname.Text, isCustomType ? (cbDataType.SelectedItem as SmartB1tCSClass).ClassName : cbDataType.SelectedItem.ToString(), isCustomType);
                        currCS.Fields.Add(csv);
                    }
                    else
                    {
                        //Edit current var
                        editVar.VarName = txtVarname.Text;
                        editVar.DataType = isCustomType ? (cbDataType.SelectedItem as SmartB1tCSClass).ClassName : cbDataType.SelectedItem.ToString();
                        editVar.IsCustomType = isCustomType;
                    }
                    currCS.Fields.Sort(new Comparison<SmartB1tCSVar>(CompareVarsByName));
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Already exists an attribute with such name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Attribute name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private int CompareVarsByName(SmartB1tCSVar csv1, SmartB1tCSVar csv2)
        {
            return string.CompareOrdinal(csv1.VarName, csv2.VarName);
        }


        private bool VarExistsByName(string v)
        {
            System.Collections.Generic.IEnumerable<SmartB1tCSVar> vars = currCS.Fields.Where(cs => cs.VarName == v && cs != editVar);
            return currCS.Fields.Where(cs => cs.VarName == v && cs != editVar).Count() > 0;
        }

        private void txtVarname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, null);
        }
    }
}
