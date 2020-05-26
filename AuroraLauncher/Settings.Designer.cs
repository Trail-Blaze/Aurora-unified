namespace AuroraLauncher
{
    partial class Settings
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
            this.materialLabelComingSoon = new MaterialSkin.Controls.MaterialLabel();
            this.materialRaisedButtonSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialSingleLineTextFieldInstallLocation = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabelInstallLocation = new MaterialSkin.Controls.MaterialLabel();
            this.materialFlatButtonBrowse = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialRaisedButtonReset = new MaterialSkin.Controls.MaterialRaisedButton();
            this.folderBrowserDialogBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.materialRadioButtonDark = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialRadioButtonLight = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialLabelTheme = new MaterialSkin.Controls.MaterialLabel();
            this.materialSingleLineTextFieldArguments = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabelArguments = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // materialLabelComingSoon
            // 
            this.materialLabelComingSoon.AutoSize = true;
            this.materialLabelComingSoon.Depth = 0;
            this.materialLabelComingSoon.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelComingSoon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelComingSoon.Location = new System.Drawing.Point(12, 292);
            this.materialLabelComingSoon.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelComingSoon.Name = "materialLabelComingSoon";
            this.materialLabelComingSoon.Size = new System.Drawing.Size(383, 19);
            this.materialLabelComingSoon.TabIndex = 0;
            this.materialLabelComingSoon.Text = "NOTE: More coming soon, including Anti-Cheat options!";
            // 
            // materialRaisedButtonSave
            // 
            this.materialRaisedButtonSave.Depth = 0;
            this.materialRaisedButtonSave.Location = new System.Drawing.Point(413, 285);
            this.materialRaisedButtonSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButtonSave.Name = "materialRaisedButtonSave";
            this.materialRaisedButtonSave.Primary = true;
            this.materialRaisedButtonSave.Size = new System.Drawing.Size(75, 23);
            this.materialRaisedButtonSave.TabIndex = 1;
            this.materialRaisedButtonSave.Text = "Save";
            this.materialRaisedButtonSave.UseVisualStyleBackColor = true;
            this.materialRaisedButtonSave.Click += new System.EventHandler(this.materialRaisedButtonSave_Click);
            // 
            // materialSingleLineTextFieldInstallLocation
            // 
            this.materialSingleLineTextFieldInstallLocation.Depth = 0;
            this.materialSingleLineTextFieldInstallLocation.Hint = "";
            this.materialSingleLineTextFieldInstallLocation.Location = new System.Drawing.Point(135, 79);
            this.materialSingleLineTextFieldInstallLocation.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextFieldInstallLocation.Name = "materialSingleLineTextFieldInstallLocation";
            this.materialSingleLineTextFieldInstallLocation.PasswordChar = '\0';
            this.materialSingleLineTextFieldInstallLocation.SelectedText = "";
            this.materialSingleLineTextFieldInstallLocation.SelectionLength = 0;
            this.materialSingleLineTextFieldInstallLocation.SelectionStart = 0;
            this.materialSingleLineTextFieldInstallLocation.Size = new System.Drawing.Size(322, 23);
            this.materialSingleLineTextFieldInstallLocation.TabIndex = 2;
            this.materialSingleLineTextFieldInstallLocation.UseSystemPasswordChar = false;
            // 
            // materialLabelInstallLocation
            // 
            this.materialLabelInstallLocation.AutoSize = true;
            this.materialLabelInstallLocation.Depth = 0;
            this.materialLabelInstallLocation.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelInstallLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelInstallLocation.Location = new System.Drawing.Point(12, 83);
            this.materialLabelInstallLocation.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelInstallLocation.Name = "materialLabelInstallLocation";
            this.materialLabelInstallLocation.Size = new System.Drawing.Size(117, 19);
            this.materialLabelInstallLocation.TabIndex = 3;
            this.materialLabelInstallLocation.Text = "Install Location:";
            // 
            // materialFlatButtonBrowse
            // 
            this.materialFlatButtonBrowse.AutoSize = true;
            this.materialFlatButtonBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButtonBrowse.Depth = 0;
            this.materialFlatButtonBrowse.Location = new System.Drawing.Point(464, 75);
            this.materialFlatButtonBrowse.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButtonBrowse.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButtonBrowse.Name = "materialFlatButtonBrowse";
            this.materialFlatButtonBrowse.Primary = false;
            this.materialFlatButtonBrowse.Size = new System.Drawing.Size(23, 36);
            this.materialFlatButtonBrowse.TabIndex = 4;
            this.materialFlatButtonBrowse.Text = "...";
            this.materialFlatButtonBrowse.UseVisualStyleBackColor = true;
            this.materialFlatButtonBrowse.Click += new System.EventHandler(this.materialFlatButtonBrowse_Click);
            // 
            // materialRaisedButtonReset
            // 
            this.materialRaisedButtonReset.Depth = 0;
            this.materialRaisedButtonReset.Location = new System.Drawing.Point(382, 108);
            this.materialRaisedButtonReset.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButtonReset.Name = "materialRaisedButtonReset";
            this.materialRaisedButtonReset.Primary = true;
            this.materialRaisedButtonReset.Size = new System.Drawing.Size(75, 23);
            this.materialRaisedButtonReset.TabIndex = 5;
            this.materialRaisedButtonReset.Text = "Reset";
            this.materialRaisedButtonReset.UseVisualStyleBackColor = true;
            this.materialRaisedButtonReset.Click += new System.EventHandler(this.materialRaisedButtonReset_Click);
            // 
            // materialRadioButtonDark
            // 
            this.materialRadioButtonDark.AutoSize = true;
            this.materialRadioButtonDark.Depth = 0;
            this.materialRadioButtonDark.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialRadioButtonDark.Location = new System.Drawing.Point(74, 107);
            this.materialRadioButtonDark.Margin = new System.Windows.Forms.Padding(0);
            this.materialRadioButtonDark.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialRadioButtonDark.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRadioButtonDark.Name = "materialRadioButtonDark";
            this.materialRadioButtonDark.Ripple = true;
            this.materialRadioButtonDark.Size = new System.Drawing.Size(57, 30);
            this.materialRadioButtonDark.TabIndex = 6;
            this.materialRadioButtonDark.TabStop = true;
            this.materialRadioButtonDark.Text = "Dark";
            this.materialRadioButtonDark.UseVisualStyleBackColor = true;
            this.materialRadioButtonDark.CheckedChanged += new System.EventHandler(this.materialRadioButtonDark_CheckedChanged);
            // 
            // materialRadioButtonLight
            // 
            this.materialRadioButtonLight.AutoSize = true;
            this.materialRadioButtonLight.Depth = 0;
            this.materialRadioButtonLight.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialRadioButtonLight.Location = new System.Drawing.Point(131, 107);
            this.materialRadioButtonLight.Margin = new System.Windows.Forms.Padding(0);
            this.materialRadioButtonLight.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialRadioButtonLight.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRadioButtonLight.Name = "materialRadioButtonLight";
            this.materialRadioButtonLight.Ripple = true;
            this.materialRadioButtonLight.Size = new System.Drawing.Size(60, 30);
            this.materialRadioButtonLight.TabIndex = 7;
            this.materialRadioButtonLight.TabStop = true;
            this.materialRadioButtonLight.Text = "Light";
            this.materialRadioButtonLight.UseVisualStyleBackColor = true;
            // 
            // materialLabelTheme
            // 
            this.materialLabelTheme.AutoSize = true;
            this.materialLabelTheme.Depth = 0;
            this.materialLabelTheme.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelTheme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelTheme.Location = new System.Drawing.Point(12, 112);
            this.materialLabelTheme.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelTheme.Name = "materialLabelTheme";
            this.materialLabelTheme.Size = new System.Drawing.Size(59, 19);
            this.materialLabelTheme.TabIndex = 8;
            this.materialLabelTheme.Text = "Theme:";
            // 
            // materialSingleLineTextFieldArguments
            // 
            this.materialSingleLineTextFieldArguments.Depth = 0;
            this.materialSingleLineTextFieldArguments.Hint = "";
            this.materialSingleLineTextFieldArguments.Location = new System.Drawing.Point(104, 256);
            this.materialSingleLineTextFieldArguments.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextFieldArguments.Name = "materialSingleLineTextFieldArguments";
            this.materialSingleLineTextFieldArguments.PasswordChar = '\0';
            this.materialSingleLineTextFieldArguments.SelectedText = "";
            this.materialSingleLineTextFieldArguments.SelectionLength = 0;
            this.materialSingleLineTextFieldArguments.SelectionStart = 0;
            this.materialSingleLineTextFieldArguments.Size = new System.Drawing.Size(384, 23);
            this.materialSingleLineTextFieldArguments.TabIndex = 9;
            this.materialSingleLineTextFieldArguments.UseSystemPasswordChar = false;
            // 
            // materialLabelArguments
            // 
            this.materialLabelArguments.AutoSize = true;
            this.materialLabelArguments.Depth = 0;
            this.materialLabelArguments.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelArguments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelArguments.Location = new System.Drawing.Point(12, 260);
            this.materialLabelArguments.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelArguments.Name = "materialLabelArguments";
            this.materialLabelArguments.Size = new System.Drawing.Size(86, 19);
            this.materialLabelArguments.TabIndex = 10;
            this.materialLabelArguments.Text = "Arguments:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 320);
            this.Controls.Add(this.materialLabelArguments);
            this.Controls.Add(this.materialSingleLineTextFieldArguments);
            this.Controls.Add(this.materialLabelTheme);
            this.Controls.Add(this.materialRadioButtonLight);
            this.Controls.Add(this.materialRadioButtonDark);
            this.Controls.Add(this.materialRaisedButtonReset);
            this.Controls.Add(this.materialFlatButtonBrowse);
            this.Controls.Add(this.materialLabelInstallLocation);
            this.Controls.Add(this.materialSingleLineTextFieldInstallLocation);
            this.Controls.Add(this.materialRaisedButtonSave);
            this.Controls.Add(this.materialLabelComingSoon);
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Sizable = false;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabelComingSoon;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButtonSave;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldInstallLocation;
        private MaterialSkin.Controls.MaterialLabel materialLabelInstallLocation;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButtonBrowse;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButtonReset;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogBrowse;
        private MaterialSkin.Controls.MaterialRadioButton materialRadioButtonDark;
        private MaterialSkin.Controls.MaterialRadioButton materialRadioButtonLight;
        private MaterialSkin.Controls.MaterialLabel materialLabelTheme;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldArguments;
        private MaterialSkin.Controls.MaterialLabel materialLabelArguments;
    }
}