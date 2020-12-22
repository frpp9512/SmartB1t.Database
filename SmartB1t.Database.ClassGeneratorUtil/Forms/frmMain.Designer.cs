namespace SmartB1t.Database.ClassGeneratorUtil.Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjectname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbClasses = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAttributes = new System.Windows.Forms.ComboBox();
            this.btnAddAttribute = new System.Windows.Forms.Button();
            this.btnEditAttribute = new System.Windows.Forms.Button();
            this.btnRemoveAttribute = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEditName = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnOpenProject = new System.Windows.Forms.Button();
            this.btnSaveProject = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCreateNewProject = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project name:";
            // 
            // txtProjectname
            // 
            this.txtProjectname.Location = new System.Drawing.Point(103, 69);
            this.txtProjectname.Name = "txtProjectname";
            this.txtProjectname.Size = new System.Drawing.Size(221, 24);
            this.txtProjectname.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Class:";
            // 
            // cbClasses
            // 
            this.cbClasses.DisplayMember = "ClassName";
            this.cbClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClasses.FormattingEnabled = true;
            this.cbClasses.Location = new System.Drawing.Point(103, 99);
            this.cbClasses.Name = "cbClasses";
            this.cbClasses.Size = new System.Drawing.Size(221, 25);
            this.cbClasses.TabIndex = 2;
            this.cbClasses.SelectedIndexChanged += new System.EventHandler(this.cbClasses_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbAttributes);
            this.groupBox1.Controls.Add(this.btnAddAttribute);
            this.groupBox1.Controls.Add(this.btnEditAttribute);
            this.groupBox1.Controls.Add(this.btnRemoveAttribute);
            this.groupBox1.Location = new System.Drawing.Point(15, 161);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 90);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Class attributes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Attribute:";
            // 
            // cbAttributes
            // 
            this.cbAttributes.DisplayMember = "VarName";
            this.cbAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAttributes.FormattingEnabled = true;
            this.cbAttributes.Location = new System.Drawing.Point(88, 23);
            this.cbAttributes.Name = "cbAttributes";
            this.cbAttributes.Size = new System.Drawing.Size(215, 25);
            this.cbAttributes.TabIndex = 2;
            // 
            // btnAddAttribute
            // 
            this.btnAddAttribute.Location = new System.Drawing.Point(88, 54);
            this.btnAddAttribute.Name = "btnAddAttribute";
            this.btnAddAttribute.Size = new System.Drawing.Size(59, 23);
            this.btnAddAttribute.TabIndex = 4;
            this.btnAddAttribute.Text = "Add...";
            this.btnAddAttribute.UseVisualStyleBackColor = true;
            this.btnAddAttribute.Click += new System.EventHandler(this.btnAddAttribute_Click);
            // 
            // btnEditAttribute
            // 
            this.btnEditAttribute.Location = new System.Drawing.Point(153, 54);
            this.btnEditAttribute.Name = "btnEditAttribute";
            this.btnEditAttribute.Size = new System.Drawing.Size(81, 23);
            this.btnEditAttribute.TabIndex = 4;
            this.btnEditAttribute.Text = "Edit...";
            this.btnEditAttribute.UseVisualStyleBackColor = true;
            this.btnEditAttribute.Click += new System.EventHandler(this.btnEditAttribute_Click);
            // 
            // btnRemoveAttribute
            // 
            this.btnRemoveAttribute.Location = new System.Drawing.Point(240, 54);
            this.btnRemoveAttribute.Name = "btnRemoveAttribute";
            this.btnRemoveAttribute.Size = new System.Drawing.Size(63, 23);
            this.btnRemoveAttribute.TabIndex = 4;
            this.btnRemoveAttribute.Text = "Remove";
            this.btnRemoveAttribute.UseVisualStyleBackColor = true;
            this.btnRemoveAttribute.Click += new System.EventHandler(this.btnRemoveAttribute_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(261, 130);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(63, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEditName
            // 
            this.btnEditName.Location = new System.Drawing.Point(168, 130);
            this.btnEditName.Name = "btnEditName";
            this.btnEditName.Size = new System.Drawing.Size(87, 23);
            this.btnEditName.TabIndex = 4;
            this.btnEditName.Text = "Edit name...";
            this.btnEditName.UseVisualStyleBackColor = true;
            this.btnEditName.Click += new System.EventHandler(this.btnEditName_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(103, 130);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(59, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(255, 259);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(69, 26);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.Location = new System.Drawing.Point(156, 17);
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Size = new System.Drawing.Size(81, 26);
            this.btnOpenProject.TabIndex = 5;
            this.btnOpenProject.Text = "Open...";
            this.btnOpenProject.UseVisualStyleBackColor = true;
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.Location = new System.Drawing.Point(243, 17);
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Size = new System.Drawing.Size(66, 26);
            this.btnSaveProject.TabIndex = 5;
            this.btnSaveProject.Text = "Save...";
            this.btnSaveProject.UseVisualStyleBackColor = true;
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCreateNewProject);
            this.groupBox2.Controls.Add(this.btnOpenProject);
            this.groupBox2.Controls.Add(this.btnSaveProject);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 51);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Project options";
            // 
            // btnCreateNewProject
            // 
            this.btnCreateNewProject.Location = new System.Drawing.Point(12, 17);
            this.btnCreateNewProject.Name = "btnCreateNewProject";
            this.btnCreateNewProject.Size = new System.Drawing.Size(138, 26);
            this.btnCreateNewProject.TabIndex = 5;
            this.btnCreateNewProject.Text = "Create new";
            this.btnCreateNewProject.UseVisualStyleBackColor = true;
            this.btnCreateNewProject.Click += new System.EventHandler(this.btnCreateNewProject_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 297);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEditName);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cbClasses);
            this.Controls.Add(this.txtProjectname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Class Generator Util";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjectname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbClasses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbAttributes;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEditName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddAttribute;
        private System.Windows.Forms.Button btnEditAttribute;
        private System.Windows.Forms.Button btnRemoveAttribute;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnOpenProject;
        private System.Windows.Forms.Button btnSaveProject;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCreateNewProject;
    }
}

