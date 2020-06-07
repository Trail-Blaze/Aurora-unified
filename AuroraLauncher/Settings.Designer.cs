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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.materialLabelWarning = new MaterialSkin.Controls.MaterialLabel();
            this.materialRaisedButtonSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialSingleLineTextFieldInstallLocation = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabelInstallLocation = new MaterialSkin.Controls.MaterialLabel();
            this.materialFlatButtonInstallLocationBrowse = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialRaisedButtonInstallLocationReset = new MaterialSkin.Controls.MaterialRaisedButton();
            this.folderBrowserDialogInstallLocationBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.materialRadioButtonThemeDark = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialRadioButtonThemeLight = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialLabelTheme = new MaterialSkin.Controls.MaterialLabel();
            this.materialSingleLineTextFieldArguments = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabelArguments = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabelComingSoon = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // materialLabelWarning
            // 
            this.materialLabelWarning.AutoSize = true;
            this.materialLabelWarning.Depth = 0;
            this.materialLabelWarning.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelWarning.Location = new System.Drawing.Point(12, 289);
            this.materialLabelWarning.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelWarning.Name = "materialLabelWarning";
            this.materialLabelWarning.Size = new System.Drawing.Size(371, 19);
            this.materialLabelWarning.TabIndex = 0;
            this.materialLabelWarning.Text = "WARNING: Do not share your \"Configuration.json\" file!";
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
            // materialFlatButtonInstallLocationBrowse
            // 
            this.materialFlatButtonInstallLocationBrowse.AutoSize = true;
            this.materialFlatButtonInstallLocationBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButtonInstallLocationBrowse.Depth = 0;
            this.materialFlatButtonInstallLocationBrowse.Location = new System.Drawing.Point(464, 75);
            this.materialFlatButtonInstallLocationBrowse.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButtonInstallLocationBrowse.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButtonInstallLocationBrowse.Name = "materialFlatButtonInstallLocationBrowse";
            this.materialFlatButtonInstallLocationBrowse.Primary = false;
            this.materialFlatButtonInstallLocationBrowse.Size = new System.Drawing.Size(23, 36);
            this.materialFlatButtonInstallLocationBrowse.TabIndex = 4;
            this.materialFlatButtonInstallLocationBrowse.Text = "...";
            this.materialFlatButtonInstallLocationBrowse.UseVisualStyleBackColor = true;
            this.materialFlatButtonInstallLocationBrowse.Click += new System.EventHandler(this.materialFlatButtonInstallLocationBrowse_Click);
            // 
            // materialRaisedButtonInstallLocationReset
            // 
            this.materialRaisedButtonInstallLocationReset.Depth = 0;
            this.materialRaisedButtonInstallLocationReset.Location = new System.Drawing.Point(382, 108);
            this.materialRaisedButtonInstallLocationReset.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButtonInstallLocationReset.Name = "materialRaisedButtonInstallLocationReset";
            this.materialRaisedButtonInstallLocationReset.Primary = true;
            this.materialRaisedButtonInstallLocationReset.Size = new System.Drawing.Size(75, 23);
            this.materialRaisedButtonInstallLocationReset.TabIndex = 5;
            this.materialRaisedButtonInstallLocationReset.Text = "Reset";
            this.materialRaisedButtonInstallLocationReset.UseVisualStyleBackColor = true;
            this.materialRaisedButtonInstallLocationReset.Click += new System.EventHandler(this.materialRaisedButtonInstallLocationReset_Click);
            // 
            // materialRadioButtonThemeDark
            // 
            this.materialRadioButtonThemeDark.AutoSize = true;
            this.materialRadioButtonThemeDark.Depth = 0;
            this.materialRadioButtonThemeDark.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialRadioButtonThemeDark.Location = new System.Drawing.Point(74, 107);
            this.materialRadioButtonThemeDark.Margin = new System.Windows.Forms.Padding(0);
            this.materialRadioButtonThemeDark.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialRadioButtonThemeDark.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRadioButtonThemeDark.Name = "materialRadioButtonThemeDark";
            this.materialRadioButtonThemeDark.Ripple = true;
            this.materialRadioButtonThemeDark.Size = new System.Drawing.Size(57, 30);
            this.materialRadioButtonThemeDark.TabIndex = 6;
            this.materialRadioButtonThemeDark.TabStop = true;
            this.materialRadioButtonThemeDark.Text = "Dark";
            this.materialRadioButtonThemeDark.UseVisualStyleBackColor = true;
            this.materialRadioButtonThemeDark.CheckedChanged += new System.EventHandler(this.materialRadioButtonThemeDark_CheckedChanged);
            // 
            // materialRadioButtonThemeLight
            // 
            this.materialRadioButtonThemeLight.AutoSize = true;
            this.materialRadioButtonThemeLight.Depth = 0;
            this.materialRadioButtonThemeLight.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialRadioButtonThemeLight.Location = new System.Drawing.Point(139, 107);
            this.materialRadioButtonThemeLight.Margin = new System.Windows.Forms.Padding(0);
            this.materialRadioButtonThemeLight.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialRadioButtonThemeLight.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRadioButtonThemeLight.Name = "materialRadioButtonThemeLight";
            this.materialRadioButtonThemeLight.Ripple = true;
            this.materialRadioButtonThemeLight.Size = new System.Drawing.Size(60, 30);
            this.materialRadioButtonThemeLight.TabIndex = 7;
            this.materialRadioButtonThemeLight.TabStop = true;
            this.materialRadioButtonThemeLight.Text = "Light";
            this.materialRadioButtonThemeLight.UseVisualStyleBackColor = true;
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
            // materialLabelComingSoon
            // 
            this.materialLabelComingSoon.AutoSize = true;
            this.materialLabelComingSoon.Depth = 0;
            this.materialLabelComingSoon.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelComingSoon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelComingSoon.Location = new System.Drawing.Point(12, 142);
            this.materialLabelComingSoon.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelComingSoon.Name = "materialLabelComingSoon";
            this.materialLabelComingSoon.Size = new System.Drawing.Size(336, 19);
            this.materialLabelComingSoon.TabIndex = 11;
            this.materialLabelComingSoon.Text = "More coming soon, including Anti-Cheat options.";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 320);
            this.Controls.Add(this.materialLabelComingSoon);
            this.Controls.Add(this.materialLabelArguments);
            this.Controls.Add(this.materialSingleLineTextFieldArguments);
            this.Controls.Add(this.materialLabelTheme);
            this.Controls.Add(this.materialRadioButtonThemeLight);
            this.Controls.Add(this.materialRadioButtonThemeDark);
            this.Controls.Add(this.materialRaisedButtonInstallLocationReset);
            this.Controls.Add(this.materialFlatButtonInstallLocationBrowse);
            this.Controls.Add(this.materialLabelInstallLocation);
            this.Controls.Add(this.materialSingleLineTextFieldInstallLocation);
            this.Controls.Add(this.materialRaisedButtonSave);
            this.Controls.Add(this.materialLabelWarning);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Sizable = false;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabelWarning;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButtonSave;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldInstallLocation;
        private MaterialSkin.Controls.MaterialLabel materialLabelInstallLocation;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButtonInstallLocationBrowse;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButtonInstallLocationReset;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogInstallLocationBrowse;
        private MaterialSkin.Controls.MaterialRadioButton materialRadioButtonThemeDark;
        private MaterialSkin.Controls.MaterialRadioButton materialRadioButtonThemeLight;
        private MaterialSkin.Controls.MaterialLabel materialLabelTheme;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldArguments;
        private MaterialSkin.Controls.MaterialLabel materialLabelArguments;
        private MaterialSkin.Controls.MaterialLabel materialLabelComingSoon;
    }
}