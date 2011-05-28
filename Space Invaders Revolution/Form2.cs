using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Space_Invaders_Revolution
{
    public partial class Form2 : Form
    {
        #region variables
        // Settings Menu
        private byte Temp_Dipswitch;

        private Label lblMess;
        private Label lblLives;
        private ComboBox cmbLives;
        private Label lblBonus;
        private ComboBox cmbBonus;
        private Label lblCoin;
        private ComboBox cmbCoin;
        private Button btnSave;
        private Button btnClose;
        #endregion

        #region constructor
        public Form2(string type)
        {
            InitializeComponent();

            switch (type)
            {
                case "settings":
                    {
                        startUp_settings();
                        break;
                    }
            }
        }
        #endregion

        #region startup settings
        private void startUp_settings()
        {
            application_title("Settings Menu");
            application_resize(292, 172);
            application_misc();
            //MessageBox.Show("current monitor workarea size is: " + SystemInformation.WorkingArea.Width + "x" + SystemInformation.WorkingArea.Height);

            this.lblMess = new System.Windows.Forms.Label();
            this.lblLives = new System.Windows.Forms.Label();
            this.cmbLives = new System.Windows.Forms.ComboBox();
            this.lblBonus = new System.Windows.Forms.Label();
            this.cmbBonus = new System.Windows.Forms.ComboBox();
            this.lblCoin = new System.Windows.Forms.Label();
            this.cmbCoin = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.lblMess.AutoSize = true;
            this.lblMess.Location = new System.Drawing.Point(26, 9);
            this.lblMess.Name = "lblMess";
            this.lblMess.Size = new System.Drawing.Size(243, 13);
            this.lblMess.TabIndex = 0;
            this.lblMess.Text = "These settings control how space invaders works.";

            this.lblLives.AutoSize = true;
            this.lblLives.Location = new System.Drawing.Point(12, 39);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(77, 13);
            this.lblLives.TabIndex = 1;
            this.lblLives.Text = "Starting Lives: ";

            this.cmbLives.FormattingEnabled = true;
            this.cmbLives.Items.AddRange(new object[] { "3", "4", "5", "6"});
            this.cmbLives.Location = new System.Drawing.Point(95, 36);
            this.cmbLives.Name = "cmbLives";
            this.cmbLives.Size = new System.Drawing.Size(46, 21);
            this.cmbLives.TabIndex = 2;

            this.lblBonus.AutoSize = true;
            this.lblBonus.Location = new System.Drawing.Point(12, 71);
            this.lblBonus.Name = "lblBonus";
            this.lblBonus.Size = new System.Drawing.Size(76, 13);
            this.lblBonus.TabIndex = 3;
            this.lblBonus.Text = "Bonus Life At: ";

            this.cmbBonus.FormattingEnabled = true;
            this.cmbBonus.Items.AddRange(new object[] { "1000", "1500"});
            this.cmbBonus.Location = new System.Drawing.Point(95, 68);
            this.cmbBonus.Name = "cmbBonus";
            this.cmbBonus.Size = new System.Drawing.Size(66, 21);
            this.cmbBonus.TabIndex = 4;

            this.lblCoin.AutoSize = true;
            this.lblCoin.Location = new System.Drawing.Point(12, 105);
            this.lblCoin.Name = "lblCoin";
            this.lblCoin.Size = new System.Drawing.Size(55, 13);
            this.lblCoin.TabIndex = 5;
            this.lblCoin.Text = "Coin Info: ";

            this.cmbCoin.FormattingEnabled = true;
            this.cmbCoin.Items.AddRange(new object[] { "On", "Off"});
            this.cmbCoin.Location = new System.Drawing.Point(95, 102);
            this.cmbCoin.Name = "cmbCoin";
            this.cmbCoin.Size = new System.Drawing.Size(54, 21);
            this.cmbCoin.TabIndex = 6;

            this.btnSave.Location = new System.Drawing.Point(124, 137);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_settings_Click);

            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(205, 137);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Settings_Click);

            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbCoin);
            this.Controls.Add(this.lblCoin);
            this.Controls.Add(this.cmbBonus);
            this.Controls.Add(this.lblBonus);
            this.Controls.Add(this.cmbLives);
            this.Controls.Add(this.lblLives);
            this.Controls.Add(this.lblMess);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
        }
        #endregion

        #region misc
        public void application_misc()
        {
            try
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region resize
        public void application_resize(int size_x, int size_y)
        {
            try
            {
                this.ClientSize = new Size(size_x, size_y);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region title
        public void application_title(string title)
        {
            try
            {
                this.Text = title;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region get lives
        void Get_Lives()
        {
            switch (cmbLives.Text)
            {
                case "3":
                    {
                        //Properties.Settings.Default.Starting_Lives = "3";

                        if ((Temp_Dipswitch & 0x1) != 0)
                        {
                            Temp_Dipswitch ^= 0x1;
                        }
                        if (((Temp_Dipswitch >> 1) & 0x1) != 0)
                        {
                            Temp_Dipswitch ^= 0x2;
                        }
                        break;
                    }
                case "4":
                    {
                        //Properties.Settings.Default.Starting_Lives = "4";

                        if ((Temp_Dipswitch & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x1;
                        }
                        if (((Temp_Dipswitch >> 1) & 0x1) != 0)
                        {
                            Temp_Dipswitch ^= 0x2;
                        }
                        break;
                    }
                case "5":
                    {
                        //Properties.Settings.Default.Starting_Lives = "5";

                        if ((Temp_Dipswitch & 0x1) != 0)
                        {
                            Temp_Dipswitch ^= 0x1;
                        }
                        if (((Temp_Dipswitch >> 1) & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x2;
                        }
                        break;
                    }
                case "6":
                    {
                        //Properties.Settings.Default.Starting_Lives = "6";

                        if ((Temp_Dipswitch & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x1;
                        }
                        if (((Temp_Dipswitch >> 1) & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x2;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region get bonus
        void Get_Bonus()
        {
            switch (cmbBonus.Text)
            {
                case "1000":
                    {
                        //Properties.Settings.Default.Bonus_Life = "1000";

                        if (((Temp_Dipswitch >> 3) & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x8;
                        }
                        break;
                    }
                case "1500":
                    {
                        //Properties.Settings.Default.Bonus_Life = "1500";

                        if (((Temp_Dipswitch >> 3) & 0x1) != 0)
                        {
                            Temp_Dipswitch ^= 0x8;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region get coin
        void Get_Coin()
        {
            switch (cmbCoin.Text)
            {
                case "Off":
                    {
                        //Properties.Settings.Default.Coin_Info = "Off";

                        if (((Temp_Dipswitch >> 7) & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x80;
                        }
                        break;
                    }
                case "On":
                    {
                        //Properties.Settings.Default.Coin_Info = "On";

                        if (((Temp_Dipswitch >> 7) & 0x1) != 0)
                        {
                            Temp_Dipswitch ^= 0x80;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region close settings
        private void btnClose_Settings_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region save settings
        private void btnSave_settings_Click(object sender, EventArgs e)
        {
            Get_Lives();
            Get_Bonus();
            Get_Coin();
            //Properties.Settings.Default.Dip_Switch_Total = Temp_Dipswitch;
            //Properties.Settings.Default.Save();
            //Cpu.DipSwitch = Temp_Dipswitch;
            MessageBox.Show("Settings Successfully Saved.", "Success!", MessageBoxButtons.OK);
        }
        #endregion
    }
}
