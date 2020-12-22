using System;
using SmartB1t.Database.ClassGeneratorUtil.SmartB1tGenerationClasses;
using SmartB1t.Database.ClassGeneratorUtil.XML;
using System.Windows.Forms;

namespace SmartB1t.Database.ClassGeneratorUtil.Forms
{
    public partial class frmMain : Form
    {
        bool projectOpen;
        string path;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditClass frmAddCS = new frmAddEditClass();
            if (frmAddCS.ShowDialog() == DialogResult.OK)
            {
                ReloadClasses();
            }
        }

        private void ReloadClasses()
        {
            cbClasses.Items.Clear();
            foreach (SmartB1tCSClass cs in GlobalData.GlobalProject.Classes)
                cbClasses.Items.Add(cs);
            if (cbClasses.Items.Count > 0)
                cbClasses.SelectedIndex = 0;
            ReloadAttributes();
        }

        private void btnEditName_Click(object sender, EventArgs e)
        {
            if (cbClasses.Items.Count > 0)
            {
                frmAddEditClass frmEditCS = new frmAddEditClass(cbClasses.SelectedItem as SmartB1tCSClass);
                if (frmEditCS.ShowDialog() == DialogResult.OK)
                {
                    ReloadClasses();
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (cbClasses.Items.Count > 0)
            {
                DialogResult dr = MessageBox.Show($"Do you confirm you wish to remove the class '{(cbClasses.SelectedItem as SmartB1tCSClass).ClassName}'? \r\nAll of its attribute's data will be lost!!!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    SmartB1tCSClass cs = cbClasses.SelectedItem as SmartB1tCSClass;
                    GlobalData.GlobalProject.Classes.Remove(cs);
                    ReloadClasses();
                }
            }
        }

        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            if (cbClasses.Items.Count > 0)
            {
                frmAddEditAttribute frmAddAtt = new frmAddEditAttribute(cbClasses.SelectedItem as SmartB1tCSClass);
                if (frmAddAtt.ShowDialog() == DialogResult.OK)
                {
                    ReloadAttributes();
                }
            }
        }

        private void ReloadAttributes()
        {
            cbAttributes.Items.Clear();
            if (cbClasses.Items.Count > 0)
            {
                foreach (SmartB1tCSVar csv in (cbClasses.SelectedItem as SmartB1tCSClass).Fields)
                    cbAttributes.Items.Add(csv);
                if (cbAttributes.Items.Count > 0)
                    cbAttributes.SelectedIndex = 0;
            }
        }

        private void btnEditAttribute_Click(object sender, EventArgs e)
        {
            if (cbClasses.Items.Count > 0)
            {
                if (cbAttributes.Items.Count > 0)
                {
                    frmAddEditAttribute frmEditVar = new frmAddEditAttribute(cbClasses.SelectedItem as SmartB1tCSClass, cbAttributes.SelectedItem as SmartB1tCSVar);
                    if (frmEditVar.ShowDialog() == DialogResult.OK)
                    {
                        ReloadAttributes();
                    }
                }
            }
        }

        private void btnRemoveAttribute_Click(object sender, EventArgs e)
        {
            if (cbAttributes.Items.Count > 0)
            {
                if (MessageBox.Show("Do you confirm you wish to remove the attribute?", "Remove attribute", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SmartB1tCSClass cs = cbClasses.SelectedItem as SmartB1tCSClass;
                    SmartB1tCSVar csv = cbAttributes.SelectedItem as SmartB1tCSVar;
                    GlobalData.GlobalProject.Classes.Remove(cs);
                    ReloadAttributes();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProjectname.Text != "")
                {
                    GlobalData.GlobalProject.ProjectName = txtProjectname.Text;
                    GlobalData.GlobalProject.Export(AppDomain.CurrentDomain.BaseDirectory);
                    MessageBox.Show("Export completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Project name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadAttributes();
        }

        private void btnCreateNewProject_Click(object sender, EventArgs e)
        {
            if (projectOpen)
            {
                DialogResult dr = MessageBox.Show("There is a project open. Would you like to save changes before creating the new project?", "Save project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    SaveProject(path);
                }
                else
                {
                    if (dr == DialogResult.Cancel)
                        return;
                }
            }
            projectOpen = false;
            GlobalData.GlobalProject = new SmartB1tCSProject("");
            ResetControls();
        }

        private void ResetControls()
        {
            txtProjectname.ResetText();
            cbClasses.Items.Clear();
            cbAttributes.Items.Clear();
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (projectOpen)
                {
                    DialogResult dr = MessageBox.Show("There is a project open. Would you like to save changes before opening the new project?", "Save project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (txtProjectname.Text != "")
                        {
                            GlobalData.GlobalProject.ProjectName = txtProjectname.Text;
                            SaveProject(path);
                        }
                        else
                        {
                            MessageBox.Show("Project name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        if (dr == DialogResult.Cancel)
                            return;
                    }
                }
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML Files (*.xml) | *.xml";
                ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    projectOpen = true;
                    path = ofd.FileName;
                    GlobalData.GlobalProject = new SmartB1tCSProject("");
                    LoadProjectData();
                    txtProjectname.Text = GlobalData.GlobalProject.ProjectName;
                    ReloadClasses();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadProjectData()
        {
            XMLTree tree = XMLTree.LoadFrom(path);
            foreach (XMLTag tgProj in tree.MainTag.Children)
            {
                foreach (XMLTag tgProjCh in tgProj.Children)
                {
                    if (tgProjCh.TagName == "Classes")
                    {
                        foreach (XMLTag tgCS in tgProjCh.Children)
                        {
                            foreach (XMLTag tgCSCh in tgCS.Children)
                            {
                                if (tgCSCh.TagName == "Vars")
                                {
                                    foreach (XMLTag tgVar in tgCSCh.Children)
                                    {
                                        SmartB1tCSVar csv = new SmartB1tCSVar("", "", false);
                                        foreach (XMLTag tgVarData in tgVar.Children)
                                        {
                                            if (tgVarData.TagName == "name")
                                            {
                                                csv.VarName = tgVarData.Body;
                                            }
                                            else
                                            {
                                                if (tgVarData.TagName == "custom_type")
                                                {
                                                    csv.IsCustomType = bool.Parse(tgVarData.Body);
                                                }
                                                else
                                                {
                                                    if (tgVarData.TagName == "data_type")
                                                        csv.DataType = tgVarData.Body;
                                                }
                                            }
                                        }
                                        GlobalData.GlobalProject.Classes[GlobalData.GlobalProject.Classes.Count - 1].Fields.Add(csv);
                                    }
                                }
                                else
                                {
                                    if (tgCSCh.TagName == "name")
                                        GlobalData.GlobalProject.Classes.Add(new SmartB1tCSClass(tgCSCh.Body));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tgProjCh.TagName == "namespace")
                            GlobalData.GlobalProject.ProjectName = tgProjCh.Body;
                    }
                    
                }
            }
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProjectname.Text != "")
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "XML File (*.xml) | *.xml";
                    sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GlobalData.GlobalProject.ProjectName = txtProjectname.Text;
                        SaveProject(sfd.FileName);
                    }
                }
                else
                    MessageBox.Show("Project name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveProject(string path)
        {
            XMLTree tree = new XMLTree("1.0", System.Text.Encoding.UTF8);
            XMLTag tgProj = tree.MainTag.AddChild("Project");
            tgProj.AddChild("namespace").Body = GlobalData.GlobalProject.ProjectName;
            XMLTag tgClasses = tgProj.AddChild("Classes");
            foreach (SmartB1tCSClass cs in GlobalData.GlobalProject.Classes)
            {
                XMLTag tgCS = tgClasses.AddChild("Class");
                tgCS.AddChild("name").Body = cs.ClassName;
                XMLTag tgVars = tgCS.AddChild("Vars");
                foreach (SmartB1tCSVar csv in cs.Fields)
                {
                    XMLTag tgVarData = tgVars.AddChild("Var");
                    tgVarData.AddChild("name").Body = csv.VarName;
                    tgVarData.AddChild("custom_type").Body = csv.IsCustomType.ToString();
                    tgVarData.AddChild("data_type").Body = csv.DataType;
                }
            }
            tree.SaveTo(path);
            MessageBox.Show("Project saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

