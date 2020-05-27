namespace AuroraLauncher
{
    partial class Gui
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
            this.components = new System.ComponentModel.Container();
            this.materialFlatButtonSettings = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialRaisedButtonLaunch = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabelMadeWithLove = new MaterialSkin.Controls.MaterialLabel();
            this.materialSingleLineTextFieldUsername = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialSingleLineTextFieldPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabelPassword = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabelUsername = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabelUpdate = new MaterialSkin.Controls.MaterialLabel();
            this.materialFlatButtonDiscord = new MaterialSkin.Controls.MaterialFlatButton();
            this.pictureBoxDiscord = new System.Windows.Forms.PictureBox();
            this.timerHeartbeat = new System.Windows.Forms.Timer(this.components);
            this.materialLabelOnline = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDiscord)).BeginInit();
            this.SuspendLayout();
            // 
            // materialFlatButtonSettings
            // 
            this.materialFlatButtonSettings.AutoSize = true;
            this.materialFlatButtonSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButtonSettings.Depth = 0;
            this.materialFlatButtonSettings.Location = new System.Drawing.Point(451, 299);
            this.materialFlatButtonSettings.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButtonSettings.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButtonSettings.Name = "materialFlatButtonSettings";
            this.materialFlatButtonSettings.Primary = false;
            this.materialFlatButtonSettings.Size = new System.Drawing.Size(76, 36);
            this.materialFlatButtonSettings.TabIndex = 0;
            this.materialFlatButtonSettings.Text = "Settings";
            this.materialFlatButtonSettings.UseVisualStyleBackColor = true;
            this.materialFlatButtonSettings.Click += new System.EventHandler(this.materialFlatButtonSettings_Click);
            // 
            // materialRaisedButtonLaunch
            // 
            this.materialRaisedButtonLaunch.Depth = 0;
            this.materialRaisedButtonLaunch.Location = new System.Drawing.Point(12, 299);
            this.materialRaisedButtonLaunch.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButtonLaunch.Name = "materialRaisedButtonLaunch";
            this.materialRaisedButtonLaunch.Primary = true;
            this.materialRaisedButtonLaunch.Size = new System.Drawing.Size(76, 36);
            this.materialRaisedButtonLaunch.TabIndex = 1;
            this.materialRaisedButtonLaunch.Text = "Launch";
            this.materialRaisedButtonLaunch.UseVisualStyleBackColor = true;
            this.materialRaisedButtonLaunch.Click += new System.EventHandler(this.materialRaisedButtonLaunch_Click);
            // 
            // materialLabelMadeWithLove
            // 
            this.materialLabelMadeWithLove.AutoSize = true;
            this.materialLabelMadeWithLove.Depth = 0;
            this.materialLabelMadeWithLove.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelMadeWithLove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelMadeWithLove.Location = new System.Drawing.Point(94, 307);
            this.materialLabelMadeWithLove.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelMadeWithLove.Name = "materialLabelMadeWithLove";
            this.materialLabelMadeWithLove.Size = new System.Drawing.Size(166, 19);
            this.materialLabelMadeWithLove.TabIndex = 2;
            this.materialLabelMadeWithLove.Text = "Made with <3 by Cyuubi";
            // 
            // materialSingleLineTextFieldUsername
            // 
            this.materialSingleLineTextFieldUsername.Depth = 0;
            this.materialSingleLineTextFieldUsername.Hint = "Ninja";
            this.materialSingleLineTextFieldUsername.Location = new System.Drawing.Point(99, 79);
            this.materialSingleLineTextFieldUsername.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextFieldUsername.Name = "materialSingleLineTextFieldUsername";
            this.materialSingleLineTextFieldUsername.PasswordChar = '\0';
            this.materialSingleLineTextFieldUsername.SelectedText = "";
            this.materialSingleLineTextFieldUsername.SelectionLength = 0;
            this.materialSingleLineTextFieldUsername.SelectionStart = 0;
            this.materialSingleLineTextFieldUsername.Size = new System.Drawing.Size(429, 23);
            this.materialSingleLineTextFieldUsername.TabIndex = 3;
            this.materialSingleLineTextFieldUsername.UseSystemPasswordChar = false;
            this.materialSingleLineTextFieldUsername.TextChanged += new System.EventHandler(this.materialSingleLineTextFieldUsername_TextChanged);
            // 
            // materialSingleLineTextFieldPassword
            // 
            this.materialSingleLineTextFieldPassword.Depth = 0;
            this.materialSingleLineTextFieldPassword.Hint = "";
            this.materialSingleLineTextFieldPassword.Location = new System.Drawing.Point(99, 108);
            this.materialSingleLineTextFieldPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextFieldPassword.Name = "materialSingleLineTextFieldPassword";
            this.materialSingleLineTextFieldPassword.PasswordChar = '\0';
            this.materialSingleLineTextFieldPassword.SelectedText = "";
            this.materialSingleLineTextFieldPassword.SelectionLength = 0;
            this.materialSingleLineTextFieldPassword.SelectionStart = 0;
            this.materialSingleLineTextFieldPassword.Size = new System.Drawing.Size(429, 23);
            this.materialSingleLineTextFieldPassword.TabIndex = 6;
            this.materialSingleLineTextFieldPassword.UseSystemPasswordChar = false;
            // 
            // materialLabelPassword
            // 
            this.materialLabelPassword.AutoSize = true;
            this.materialLabelPassword.Depth = 0;
            this.materialLabelPassword.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelPassword.Location = new System.Drawing.Point(14, 112);
            this.materialLabelPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelPassword.Name = "materialLabelPassword";
            this.materialLabelPassword.Size = new System.Drawing.Size(79, 19);
            this.materialLabelPassword.TabIndex = 5;
            this.materialLabelPassword.Text = "Password:";
            // 
            // materialLabelUsername
            // 
            this.materialLabelUsername.AutoSize = true;
            this.materialLabelUsername.Depth = 0;
            this.materialLabelUsername.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelUsername.Location = new System.Drawing.Point(12, 83);
            this.materialLabelUsername.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelUsername.Name = "materialLabelUsername";
            this.materialLabelUsername.Size = new System.Drawing.Size(81, 19);
            this.materialLabelUsername.TabIndex = 4;
            this.materialLabelUsername.Text = "Username:";
            // 
            // materialLabelUpdate
            // 
            this.materialLabelUpdate.AutoSize = true;
            this.materialLabelUpdate.Depth = 0;
            this.materialLabelUpdate.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelUpdate.Location = new System.Drawing.Point(12, 150);
            this.materialLabelUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelUpdate.Name = "materialLabelUpdate";
            this.materialLabelUpdate.Size = new System.Drawing.Size(147, 19);
            this.materialLabelUpdate.TabIndex = 7;
            this.materialLabelUpdate.Text = "materialLabelUpdate";
            // 
            // materialFlatButtonDiscord
            // 
            this.materialFlatButtonDiscord.AutoSize = true;
            this.materialFlatButtonDiscord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButtonDiscord.Depth = 0;
            this.materialFlatButtonDiscord.Location = new System.Drawing.Point(459, 140);
            this.materialFlatButtonDiscord.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButtonDiscord.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButtonDiscord.Name = "materialFlatButtonDiscord";
            this.materialFlatButtonDiscord.Primary = false;
            this.materialFlatButtonDiscord.Size = new System.Drawing.Size(69, 36);
            this.materialFlatButtonDiscord.TabIndex = 8;
            this.materialFlatButtonDiscord.Text = "Discord";
            this.materialFlatButtonDiscord.UseVisualStyleBackColor = true;
            this.materialFlatButtonDiscord.Click += new System.EventHandler(this.materialFlatButtonDiscord_Click);
            // 
            // pictureBoxDiscord
            // 
            this.pictureBoxDiscord.BackgroundImage = global::AuroraLauncher.Properties.Resources.Discord_Logo_Color;
            this.pictureBoxDiscord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDiscord.Location = new System.Drawing.Point(412, 137);
            this.pictureBoxDiscord.Name = "pictureBoxDiscord";
            this.pictureBoxDiscord.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxDiscord.TabIndex = 9;
            this.pictureBoxDiscord.TabStop = false;
            this.pictureBoxDiscord.Click += new System.EventHandler(this.pictureBoxDiscord_Click);
            // 
            // timerHeartbeat
            // 
            this.timerHeartbeat.Enabled = true;
            this.timerHeartbeat.Interval = 2000;
            this.timerHeartbeat.Tick += new System.EventHandler(this.timerHeartbeat_Tick);
            // 
            // materialLabelOnline
            // 
            this.materialLabelOnline.AutoSize = true;
            this.materialLabelOnline.Depth = 0;
            this.materialLabelOnline.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelOnline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelOnline.Location = new System.Drawing.Point(8, 277);
            this.materialLabelOnline.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelOnline.Name = "materialLabelOnline";
            this.materialLabelOnline.Size = new System.Drawing.Size(142, 19);
            this.materialLabelOnline.TabIndex = 10;
            this.materialLabelOnline.Text = "materialLabelOnline";
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 350);
            this.Controls.Add(this.materialLabelOnline);
            this.Controls.Add(this.pictureBoxDiscord);
            this.Controls.Add(this.materialFlatButtonDiscord);
            this.Controls.Add(this.materialLabelUpdate);
            this.Controls.Add(this.materialSingleLineTextFieldPassword);
            this.Controls.Add(this.materialLabelPassword);
            this.Controls.Add(this.materialLabelUsername);
            this.Controls.Add(this.materialSingleLineTextFieldUsername);
            this.Controls.Add(this.materialLabelMadeWithLove);
            this.Controls.Add(this.materialRaisedButtonLaunch);
            this.Controls.Add(this.materialFlatButtonSettings);
            this.MaximizeBox = false;
            this.Name = "Gui";
            this.Sizable = false;
            this.Text = "Aurora Launcher";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDiscord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialFlatButton materialFlatButtonSettings;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButtonLaunch;
        private MaterialSkin.Controls.MaterialLabel materialLabelMadeWithLove;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldUsername;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldPassword;
        private MaterialSkin.Controls.MaterialLabel materialLabelPassword;
        private MaterialSkin.Controls.MaterialLabel materialLabelUsername;
        private MaterialSkin.Controls.MaterialLabel materialLabelUpdate;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButtonDiscord;
        private System.Windows.Forms.PictureBox pictureBoxDiscord;
        private System.Windows.Forms.Timer timerHeartbeat;
        private MaterialSkin.Controls.MaterialLabel materialLabelOnline;
    }
}