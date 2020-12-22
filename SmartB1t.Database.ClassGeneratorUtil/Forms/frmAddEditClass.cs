using System;
using System.Windows.Forms;
using SmartB1t.Database.ClassGeneratorUtil.SmartB1tGenerationClasses;
using System.Linq;

namespace SmartB1t.Database.ClassGeneratorUtil.Forms
{
    public partial class frmAddEditClass : Form
    {
        SmartB1tCSClass sb_class;
        public frmAddEditClass()
        {
            InitializeComponent();
        }

        public frmAddEditClass(SmartB1tCSClass sb_class)
        {
            InitializeComponent();
            Text = "Edit class";
            this.sb_class = sb_class;
            txtClassname.Text = sb_class.ClassName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtClassname.Text != "")
            {
                if (!ClassExistsByName(txtClassname.Text))
                {
                    if (sb_class == null)
                    {
                        //Add class
                        SmartB1tCSClass cs = new SmartB1tCSClass(txtClassname.Text);
                        GlobalData.GlobalProject.Classes.Add(cs);
                    }
                    else
                    {
                        //Update class
                        sb_class.ClassName = txtClassname.Text;
                    }
                    GlobalData.GlobalProject.Classes.Sort(new Comparison<SmartB1tCSClass>(CompareClassesByName));
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Already exists a class with such name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Class name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private int CompareClassesByName(SmartB1tCSClass cs1, SmartB1tCSClass cs2)
        {
            return string.CompareOrdinal(cs1.ClassName, cs2.ClassName);
        }

        private bool ClassExistsByName(string text)
        {
            return GlobalData.GlobalProject.Classes.Where(cs => cs.ClassName == text && cs != sb_class).Count() > 0;
        }

        private void txtClassname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, null);
        }
    }
}
