﻿using System;
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
        private Form _form1_reference;

        #region Settings Menu
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

        #region Key Bindings Menu
        private bool input_create;
        private System.Windows.Forms.Label lblMess2;
        private System.Windows.Forms.Label lblPlayer2_Start;
        private System.Windows.Forms.Label lblPlayer1_Start;
        private System.Windows.Forms.Label lblPlayer1_Fire;
        private System.Windows.Forms.Label lblPlayer1_Left;
        private System.Windows.Forms.Label lblPlayer1_Right;
        private System.Windows.Forms.Label lblTilt;
        private System.Windows.Forms.Label lblPlayer2_Fire;
        private System.Windows.Forms.Label lblPlayer2_Left;
        private System.Windows.Forms.Label lblPlayer2_Right;
        private System.Windows.Forms.TextBox txtCoin;
        private System.Windows.Forms.TextBox txtPlayer2_Start;
        private System.Windows.Forms.TextBox txtPlayer1_Start;
        private System.Windows.Forms.TextBox txtPlayer1_Fire;
        private System.Windows.Forms.TextBox txtPlayer1_Left;
        private System.Windows.Forms.TextBox txtPlayer1_Right;
        private System.Windows.Forms.TextBox txtTilt;
        private System.Windows.Forms.TextBox txtPlayer2_Fire;
        private System.Windows.Forms.TextBox txtPlayer2_Left;
        private System.Windows.Forms.TextBox txtPlayer2_Right;
        private System.Windows.Forms.Button btnCoin;
        private System.Windows.Forms.Button btnPlayer2_Start;
        private System.Windows.Forms.Button btnPlayer1_Start;
        private System.Windows.Forms.Button btnPlayer1_Fire;
        private System.Windows.Forms.Button btnPlayer1_Left;
        private System.Windows.Forms.Button btnPlayer1_Right;
        private System.Windows.Forms.Button btnTilt;
        private System.Windows.Forms.Button btnPlayer2_Fire;
        private System.Windows.Forms.Button btnPlayer2_Left;
        private System.Windows.Forms.Button btnPlayer2_Right;
        #endregion
        #endregion

        #region constructor
        public Form2(Form Form1_reference, string type)
        {
            _form1_reference = Form1_reference;
            input_create = false;
            InitializeComponent();

            switch (type)
            {
                case "settings":
                    {
                        startUp_settings();
                        break;
                    }
                case "key_bindings":
                    {
                        startUp_key_bindings();
                        break;
                    }
            }
        }
        #endregion

        #region startup key bindings
        private void startUp_key_bindings()
        {
            application_title("Key Bindings Menu");
            application_resize(292, 358);
            application_misc();
            //MessageBox.Show("current monitor workarea size is: " + SystemInformation.WorkingArea.Width + "x" + SystemInformation.WorkingArea.Height);
            if (((Form1)_form1_reference).main_input == null)
            {
                ((Form1)_form1_reference).main_input = new Input(_form1_reference, this.Handle);
                input_create = true;
            }

            #region control init code
            this.lblMess = new System.Windows.Forms.Label();
            this.lblMess2 = new System.Windows.Forms.Label();
            this.lblCoin = new System.Windows.Forms.Label();
            this.lblPlayer2_Start = new System.Windows.Forms.Label();
            this.lblPlayer1_Start = new System.Windows.Forms.Label();
            this.lblPlayer1_Fire = new System.Windows.Forms.Label();
            this.lblPlayer1_Left = new System.Windows.Forms.Label();
            this.lblPlayer1_Right = new System.Windows.Forms.Label();
            this.lblTilt = new System.Windows.Forms.Label();
            this.lblPlayer2_Fire = new System.Windows.Forms.Label();
            this.lblPlayer2_Left = new System.Windows.Forms.Label();
            this.lblPlayer2_Right = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCoin = new System.Windows.Forms.TextBox();
            this.txtPlayer2_Start = new System.Windows.Forms.TextBox();
            this.txtPlayer1_Start = new System.Windows.Forms.TextBox();
            this.txtPlayer1_Fire = new System.Windows.Forms.TextBox();
            this.txtPlayer1_Left = new System.Windows.Forms.TextBox();
            this.txtPlayer1_Right = new System.Windows.Forms.TextBox();
            this.txtTilt = new System.Windows.Forms.TextBox();
            this.txtPlayer2_Fire = new System.Windows.Forms.TextBox();
            this.txtPlayer2_Left = new System.Windows.Forms.TextBox();
            this.txtPlayer2_Right = new System.Windows.Forms.TextBox();
            this.btnCoin = new System.Windows.Forms.Button();
            this.btnPlayer2_Start = new System.Windows.Forms.Button();
            this.btnPlayer1_Start = new System.Windows.Forms.Button();
            this.btnPlayer1_Fire = new System.Windows.Forms.Button();
            this.btnPlayer1_Left = new System.Windows.Forms.Button();
            this.btnPlayer1_Right = new System.Windows.Forms.Button();
            this.btnTilt = new System.Windows.Forms.Button();
            this.btnPlayer2_Fire = new System.Windows.Forms.Button();
            this.btnPlayer2_Left = new System.Windows.Forms.Button();
            this.btnPlayer2_Right = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMess
            // 
            this.lblMess.AutoSize = true;
            this.lblMess.Location = new System.Drawing.Point(37, 9);
            this.lblMess.Name = "lblMess";
            this.lblMess.Size = new System.Drawing.Size(197, 13);
            this.lblMess.TabIndex = 0;
            this.lblMess.Text = "This Screen Allows you to customize the";
            // 
            // lblMess2
            // 
            this.lblMess2.AutoSize = true;
            this.lblMess2.Location = new System.Drawing.Point(75, 22);
            this.lblMess2.Name = "lblMess2";
            this.lblMess2.Size = new System.Drawing.Size(126, 13);
            this.lblMess2.TabIndex = 1;
            this.lblMess2.Text = "Space Invaders Controls.";
            // 
            // lblCoin
            // 
            this.lblCoin.AutoSize = true;
            this.lblCoin.Location = new System.Drawing.Point(12, 47);
            this.lblCoin.Name = "lblCoin";
            this.lblCoin.Size = new System.Drawing.Size(63, 13);
            this.lblCoin.TabIndex = 1;
            this.lblCoin.Text = "Insert Coin: ";
            // 
            // lblPlayer2_Start
            // 
            this.lblPlayer2_Start.AutoSize = true;
            this.lblPlayer2_Start.Location = new System.Drawing.Point(12, 74);
            this.lblPlayer2_Start.Name = "lblPlayer2_Start";
            this.lblPlayer2_Start.Size = new System.Drawing.Size(73, 13);
            this.lblPlayer2_Start.TabIndex = 2;
            this.lblPlayer2_Start.Text = "Player2 Start: ";
            // 
            // lblPlayer1_Start
            // 
            this.lblPlayer1_Start.AutoSize = true;
            this.lblPlayer1_Start.Location = new System.Drawing.Point(12, 101);
            this.lblPlayer1_Start.Name = "lblPlayer1_Start";
            this.lblPlayer1_Start.Size = new System.Drawing.Size(73, 13);
            this.lblPlayer1_Start.TabIndex = 3;
            this.lblPlayer1_Start.Text = "Player1 Start: ";
            // 
            // lblPlayer1_Fire
            // 
            this.lblPlayer1_Fire.AutoSize = true;
            this.lblPlayer1_Fire.Location = new System.Drawing.Point(12, 128);
            this.lblPlayer1_Fire.Name = "lblPlayer1_Fire";
            this.lblPlayer1_Fire.Size = new System.Drawing.Size(68, 13);
            this.lblPlayer1_Fire.TabIndex = 4;
            this.lblPlayer1_Fire.Text = "Player1 Fire: ";
            // 
            // lblPlayer1_Left
            // 
            this.lblPlayer1_Left.AutoSize = true;
            this.lblPlayer1_Left.Location = new System.Drawing.Point(12, 155);
            this.lblPlayer1_Left.Name = "lblPlayer1_Left";
            this.lblPlayer1_Left.Size = new System.Drawing.Size(69, 13);
            this.lblPlayer1_Left.TabIndex = 5;
            this.lblPlayer1_Left.Text = "Player1 Left: ";
            // 
            // lblPlayer1_Right
            // 
            this.lblPlayer1_Right.AutoSize = true;
            this.lblPlayer1_Right.Location = new System.Drawing.Point(12, 182);
            this.lblPlayer1_Right.Name = "lblPlayer1_Right";
            this.lblPlayer1_Right.Size = new System.Drawing.Size(76, 13);
            this.lblPlayer1_Right.TabIndex = 6;
            this.lblPlayer1_Right.Text = "Player1 Right: ";
            // 
            // lblTilt
            // 
            this.lblTilt.AutoSize = true;
            this.lblTilt.Location = new System.Drawing.Point(11, 209);
            this.lblTilt.Name = "lblTilt";
            this.lblTilt.Size = new System.Drawing.Size(27, 13);
            this.lblTilt.TabIndex = 7;
            this.lblTilt.Text = "Tilt: ";
            // 
            // lblPlayer2_Fire
            // 
            this.lblPlayer2_Fire.AutoSize = true;
            this.lblPlayer2_Fire.Location = new System.Drawing.Point(11, 236);
            this.lblPlayer2_Fire.Name = "lblPlayer2_Fire";
            this.lblPlayer2_Fire.Size = new System.Drawing.Size(68, 13);
            this.lblPlayer2_Fire.TabIndex = 8;
            this.lblPlayer2_Fire.Text = "Player2 Fire: ";
            // 
            // lblPlayer2_Left
            // 
            this.lblPlayer2_Left.AutoSize = true;
            this.lblPlayer2_Left.Location = new System.Drawing.Point(11, 263);
            this.lblPlayer2_Left.Name = "lblPlayer2_Left";
            this.lblPlayer2_Left.Size = new System.Drawing.Size(69, 13);
            this.lblPlayer2_Left.TabIndex = 9;
            this.lblPlayer2_Left.Text = "Player2 Left: ";
            // 
            // lblPlayer2_Right
            // 
            this.lblPlayer2_Right.AutoSize = true;
            this.lblPlayer2_Right.Location = new System.Drawing.Point(11, 290);
            this.lblPlayer2_Right.Name = "lblPlayer2_Right";
            this.lblPlayer2_Right.Size = new System.Drawing.Size(76, 13);
            this.lblPlayer2_Right.TabIndex = 10;
            this.lblPlayer2_Right.Text = "Player2 Right: ";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(205, 323);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(124, 323);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_key_bindings_Click);
            // 
            // txtCoin
            // 
            this.txtCoin.Location = new System.Drawing.Point(94, 44);
            this.txtCoin.Name = "txtCoin";
            this.txtCoin.ReadOnly = true;
            this.txtCoin.Size = new System.Drawing.Size(100, 20);
            this.txtCoin.TabIndex = 11;
            // 
            // txtPlayer2_Start
            // 
            this.txtPlayer2_Start.Location = new System.Drawing.Point(94, 71);
            this.txtPlayer2_Start.Name = "txtPlayer2_Start";
            this.txtPlayer2_Start.ReadOnly = true;
            this.txtPlayer2_Start.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer2_Start.TabIndex = 12;
            // 
            // txtPlayer1_Start
            // 
            this.txtPlayer1_Start.Location = new System.Drawing.Point(94, 98);
            this.txtPlayer1_Start.Name = "txtPlayer1_Start";
            this.txtPlayer1_Start.ReadOnly = true;
            this.txtPlayer1_Start.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer1_Start.TabIndex = 13;
            // 
            // txtPlayer1_Fire
            // 
            this.txtPlayer1_Fire.Location = new System.Drawing.Point(94, 125);
            this.txtPlayer1_Fire.Name = "txtPlayer1_Fire";
            this.txtPlayer1_Fire.ReadOnly = true;
            this.txtPlayer1_Fire.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer1_Fire.TabIndex = 14;
            // 
            // txtPlayer1_Left
            // 
            this.txtPlayer1_Left.Location = new System.Drawing.Point(94, 152);
            this.txtPlayer1_Left.Name = "txtPlayer1_Left";
            this.txtPlayer1_Left.ReadOnly = true;
            this.txtPlayer1_Left.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer1_Left.TabIndex = 15;
            // 
            // txtPlayer1_Right
            // 
            this.txtPlayer1_Right.Location = new System.Drawing.Point(94, 179);
            this.txtPlayer1_Right.Name = "txtPlayer1_Right";
            this.txtPlayer1_Right.ReadOnly = true;
            this.txtPlayer1_Right.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer1_Right.TabIndex = 16;
            // 
            // txtTilt
            // 
            this.txtTilt.Location = new System.Drawing.Point(94, 206);
            this.txtTilt.Name = "txtTilt";
            this.txtTilt.ReadOnly = true;
            this.txtTilt.Size = new System.Drawing.Size(100, 20);
            this.txtTilt.TabIndex = 17;
            // 
            // txtPlayer2_Fire
            // 
            this.txtPlayer2_Fire.Location = new System.Drawing.Point(94, 233);
            this.txtPlayer2_Fire.Name = "txtPlayer2_Fire";
            this.txtPlayer2_Fire.ReadOnly = true;
            this.txtPlayer2_Fire.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer2_Fire.TabIndex = 18;
            // 
            // txtPlayer2_Left
            // 
            this.txtPlayer2_Left.Location = new System.Drawing.Point(94, 260);
            this.txtPlayer2_Left.Name = "txtPlayer2_Left";
            this.txtPlayer2_Left.ReadOnly = true;
            this.txtPlayer2_Left.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer2_Left.TabIndex = 19;
            // 
            // txtPlayer2_Right
            // 
            this.txtPlayer2_Right.Location = new System.Drawing.Point(94, 287);
            this.txtPlayer2_Right.Name = "txtPlayer2_Right";
            this.txtPlayer2_Right.ReadOnly = true;
            this.txtPlayer2_Right.Size = new System.Drawing.Size(100, 20);
            this.txtPlayer2_Right.TabIndex = 20;
            // 
            // btnCoin
            // 
            this.btnCoin.Location = new System.Drawing.Point(205, 42);
            this.btnCoin.Name = "btnCoin";
            this.btnCoin.Size = new System.Drawing.Size(75, 23);
            this.btnCoin.TabIndex = 21;
            this.btnCoin.Text = "Set";
            this.btnCoin.UseVisualStyleBackColor = true;
            this.btnCoin.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnCoin_PreviewKeyDown);
            this.btnCoin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCoin_MouseClick);
            // 
            // btnPlayer2_Start
            // 
            this.btnPlayer2_Start.Location = new System.Drawing.Point(205, 69);
            this.btnPlayer2_Start.Name = "btnPlayer2_Start";
            this.btnPlayer2_Start.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer2_Start.TabIndex = 22;
            this.btnPlayer2_Start.Text = "Set";
            this.btnPlayer2_Start.UseVisualStyleBackColor = true;
            this.btnPlayer2_Start.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer2_Start_PreviewKeyDown);
            this.btnPlayer2_Start.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer2_Start_MouseClick);
            // 
            // btnPlayer1_Start
            // 
            this.btnPlayer1_Start.Location = new System.Drawing.Point(205, 96);
            this.btnPlayer1_Start.Name = "btnPlayer1_Start";
            this.btnPlayer1_Start.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer1_Start.TabIndex = 23;
            this.btnPlayer1_Start.Text = "Set";
            this.btnPlayer1_Start.UseVisualStyleBackColor = true;
            this.btnPlayer1_Start.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer1_Start_PreviewKeyDown);
            this.btnPlayer1_Start.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer1_Start_MouseClick);
            // 
            // btnPlayer1_Fire
            // 
            this.btnPlayer1_Fire.Location = new System.Drawing.Point(205, 123);
            this.btnPlayer1_Fire.Name = "btnPlayer1_Fire";
            this.btnPlayer1_Fire.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer1_Fire.TabIndex = 24;
            this.btnPlayer1_Fire.Text = "Set";
            this.btnPlayer1_Fire.UseVisualStyleBackColor = true;
            this.btnPlayer1_Fire.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer1_Fire_PreviewKeyDown);
            this.btnPlayer1_Fire.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer1_Fire_MouseClick);
            // 
            // btnPlayer1_Left
            // 
            this.btnPlayer1_Left.Location = new System.Drawing.Point(205, 150);
            this.btnPlayer1_Left.Name = "btnPlayer1_Left";
            this.btnPlayer1_Left.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer1_Left.TabIndex = 25;
            this.btnPlayer1_Left.Text = "Set";
            this.btnPlayer1_Left.UseVisualStyleBackColor = true;
            this.btnPlayer1_Left.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer1_Left_PreviewKeyDown);
            this.btnPlayer1_Left.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer1_Left_MouseClick);
            // 
            // btnPlayer1_Right
            // 
            this.btnPlayer1_Right.Location = new System.Drawing.Point(205, 177);
            this.btnPlayer1_Right.Name = "btnPlayer1_Right";
            this.btnPlayer1_Right.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer1_Right.TabIndex = 26;
            this.btnPlayer1_Right.Text = "Set";
            this.btnPlayer1_Right.UseVisualStyleBackColor = true;
            this.btnPlayer1_Right.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer1_Right_PreviewKeyDown);
            this.btnPlayer1_Right.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer1_Right_MouseClick);
            // 
            // btnTilt
            // 
            this.btnTilt.Location = new System.Drawing.Point(205, 204);
            this.btnTilt.Name = "btnTilt";
            this.btnTilt.Size = new System.Drawing.Size(75, 23);
            this.btnTilt.TabIndex = 27;
            this.btnTilt.Text = "Set";
            this.btnTilt.UseVisualStyleBackColor = true;
            this.btnTilt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnTilt_PreviewKeyDown);
            this.btnTilt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnTilt_MouseClick);
            // 
            // btnPlayer2_Fire
            // 
            this.btnPlayer2_Fire.Location = new System.Drawing.Point(205, 231);
            this.btnPlayer2_Fire.Name = "btnPlayer2_Fire";
            this.btnPlayer2_Fire.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer2_Fire.TabIndex = 28;
            this.btnPlayer2_Fire.Text = "Set";
            this.btnPlayer2_Fire.UseVisualStyleBackColor = true;
            this.btnPlayer2_Fire.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer2_Fire_PreviewKeyDown);
            this.btnPlayer2_Fire.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer2_Fire_MouseClick);
            // 
            // btnPlayer2_Left
            // 
            this.btnPlayer2_Left.Location = new System.Drawing.Point(205, 258);
            this.btnPlayer2_Left.Name = "btnPlayer2_Left";
            this.btnPlayer2_Left.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer2_Left.TabIndex = 29;
            this.btnPlayer2_Left.Text = "Set";
            this.btnPlayer2_Left.UseVisualStyleBackColor = true;
            this.btnPlayer2_Left.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer2_Left_PreviewKeyDown);
            this.btnPlayer2_Left.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer2_Left_MouseClick);
            // 
            // btnPlayer2_Right
            // 
            this.btnPlayer2_Right.Location = new System.Drawing.Point(205, 285);
            this.btnPlayer2_Right.Name = "btnPlayer2_Right";
            this.btnPlayer2_Right.Size = new System.Drawing.Size(75, 23);
            this.btnPlayer2_Right.TabIndex = 30;
            this.btnPlayer2_Right.Text = "Set";
            this.btnPlayer2_Right.UseVisualStyleBackColor = true;
            this.btnPlayer2_Right.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.btnPlayer2_Right_PreviewKeyDown);
            this.btnPlayer2_Right.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlayer2_Right_MouseClick);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(292, 358);
            this.Controls.Add(this.btnPlayer2_Right);
            this.Controls.Add(this.btnPlayer2_Left);
            this.Controls.Add(this.btnPlayer2_Fire);
            this.Controls.Add(this.btnTilt);
            this.Controls.Add(this.btnPlayer1_Right);
            this.Controls.Add(this.btnPlayer1_Left);
            this.Controls.Add(this.btnPlayer1_Fire);
            this.Controls.Add(this.btnPlayer1_Start);
            this.Controls.Add(this.btnPlayer2_Start);
            this.Controls.Add(this.btnCoin);
            this.Controls.Add(this.txtPlayer2_Right);
            this.Controls.Add(this.txtPlayer2_Left);
            this.Controls.Add(this.txtPlayer2_Fire);
            this.Controls.Add(this.txtTilt);
            this.Controls.Add(this.txtPlayer1_Right);
            this.Controls.Add(this.txtPlayer1_Left);
            this.Controls.Add(this.txtPlayer1_Fire);
            this.Controls.Add(this.txtPlayer1_Start);
            this.Controls.Add(this.txtPlayer2_Start);
            this.Controls.Add(this.txtCoin);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPlayer2_Right);
            this.Controls.Add(this.lblPlayer2_Left);
            this.Controls.Add(this.lblPlayer2_Fire);
            this.Controls.Add(this.lblTilt);
            this.Controls.Add(this.lblPlayer1_Right);
            this.Controls.Add(this.lblPlayer1_Left);
            this.Controls.Add(this.lblPlayer1_Fire);
            this.Controls.Add(this.lblPlayer1_Start);
            this.Controls.Add(this.lblPlayer2_Start);
            this.Controls.Add(this.lblCoin);
            this.Controls.Add(this.lblMess2);
            this.Controls.Add(this.lblMess);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Space Invaders: Key Bindings";
            this.ResumeLayout(false);
            this.PerformLayout();
            #endregion

            load_key_bindings();
        }
        #endregion

        #region startup settings
        private void startUp_settings()
        {
            application_title("Settings Menu");
            application_resize(292, 172);
            application_misc();
            //MessageBox.Show("current monitor workarea size is: " + SystemInformation.WorkingArea.Width + "x" + SystemInformation.WorkingArea.Height);
            #region control init code
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
            this.cmbCoin.Items.AddRange(new object[] { "on", "off"});
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
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

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
            #endregion

            cmbCoin.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Game", "Coin_Info");
            cmbBonus.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Game", "Bonus_Life");
            cmbLives.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Game", "Starting_Lives");
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
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Starting_Lives", "3");

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
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Starting_Lives", "4");

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
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Starting_Lives", "5");

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
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Starting_Lives", "6");

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
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Bonus_Life", "1000");

                        if (((Temp_Dipswitch >> 3) & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x8;
                        }
                        break;
                    }
                case "1500":
                    {
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Bonus_Life", "1500");

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
                case "off":
                    {
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Coin_Info", "off");

                        if (((Temp_Dipswitch >> 7) & 0x1) != 1)
                        {
                            Temp_Dipswitch ^= 0x80;
                        }
                        break;
                    }
                case "on":
                    {
                        ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Coin_Info", "on");

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
        private void btnClose_Click(object sender, EventArgs e)
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
            ((Form1)_form1_reference).main_settings.write_config_setting("Game", "Dip_Switch_Total", Temp_Dipswitch.ToString("X2"));
            MessageBox.Show("Settings Successfully Saved.", "Success!", MessageBoxButtons.OK);
        }
        #endregion

        #region load key bindings
        void load_key_bindings()
        {
            try
            {
                txtCoin.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key0");
                txtPlayer1_Fire.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key3");
                txtPlayer1_Left.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key4");
                txtPlayer1_Right.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key5"); ;
                txtPlayer1_Start.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key2"); ;
                txtPlayer2_Fire.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key7"); ;
                txtPlayer2_Left.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key8"); ;
                txtPlayer2_Right.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key9"); ;
                txtPlayer2_Start.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key1"); ;
                txtTilt.Text = ((Form1)_form1_reference).main_settings.read_config_setting("Input", "key6"); ;
            }
            catch
            {
                MessageBox.Show("An error has been detected with the default settings file, \nplease report this error.", "Error!", MessageBoxButtons.OK);
                btnSave.Enabled = false;
            }
        }
        #endregion

        #region coin click
        private void btnCoin_MouseClick(object sender, MouseEventArgs e)
        {
            btnCoin.Text = "Press Key";
        }
        #endregion

        #region coin keydown
        private void btnCoin_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ((Form1)_form1_reference).main_input.Key_Pressed = ((Form1)_form1_reference).main_input.retrieve_keyboardstate();

            if (btnCoin.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtCoin.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnCoin.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player2 start click
        private void btnPlayer2_Start_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer2_Start.Text = "Press Key";
        }
        #endregion

        #region player2 start keydown
        private void btnPlayer2_Start_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer2_Start.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer2_Start.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer2_Start.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player1 start click
        private void btnPlayer1_Start_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer1_Start.Text = "Press Key";
        }
        #endregion

        #region player1 start keydown
        private void btnPlayer1_Start_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer1_Start.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer1_Start.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer1_Start.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player1 fire click
        private void btnPlayer1_Fire_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer1_Fire.Text = "Press Key";
        }
        #endregion

        #region player1 fire keydown
        private void btnPlayer1_Fire_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer1_Fire.Text == "Press Key")
            {
                if ((txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer1_Fire.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer1_Fire.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player1 left click
        private void btnPlayer1_Left_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer1_Left.Text = "Press Key";
        }
        #endregion

        #region player1 left keydown
        private void btnPlayer1_Left_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer1_Left.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer1_Left.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer1_Left.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player1 right click
        private void btnPlayer1_Right_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer1_Right.Text = "Press Key";
        }
        #endregion

        #region player1 right keydown
        private void btnPlayer1_Right_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer1_Right.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer1_Right.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer1_Right.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region tilt click
        private void btnTilt_MouseClick(object sender, MouseEventArgs e)
        {
            btnTilt.Text = "Press Key";
        }
        #endregion

        #region tilt keydown
        private void btnTilt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnTilt.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtTilt.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnTilt.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player2 fire click
        private void btnPlayer2_Fire_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer2_Fire.Text = "Press Key";
        }
        #endregion

        #region player2 fire keydown
        private void btnPlayer2_Fire_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer2_Fire.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer2_Fire.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer2_Fire.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player2 left click
        private void btnPlayer2_Left_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer2_Left.Text = "Press Key";
        }
        #endregion

        #region player2 left keydown
        private void btnPlayer2_Left_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer2_Left.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer2_Left.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer2_Left.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region player2 right click
        private void btnPlayer2_Right_MouseClick(object sender, MouseEventArgs e)
        {
            btnPlayer2_Right.Text = "Press Key";
        }
        #endregion

        #region player2 right keydown
        private void btnPlayer2_Right_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (btnPlayer2_Right.Text == "Press Key")
            {
                if ((txtPlayer1_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer1_Right.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer1_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtPlayer2_Fire.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Left.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtCoin.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) && (txtPlayer2_Start.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()) &&
                (txtTilt.Text != ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString()))
                {
                    txtPlayer2_Right.Text = ((Form1)_form1_reference).main_input.Key_Pressed.PressedKeys[0].ToString();
                    btnPlayer2_Right.Text = "Set";
                }
                else
                {
                    MessageBox.Show("This Button is Currently in use, please use another.", "Error!", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region save
        private void btnSave_key_bindings_Click(object sender, EventArgs e)
        {
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key0", txtCoin.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key1", txtPlayer2_Start.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key2", txtPlayer1_Start.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key3", txtPlayer1_Fire.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key4", txtPlayer1_Left.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key5", txtPlayer1_Right.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key6", txtTilt.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key7", txtPlayer2_Fire.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key8", txtPlayer2_Left.Text);
            ((Form1)_form1_reference).main_settings.write_config_setting("Input", "key9", txtPlayer2_Right.Text);
            ((Form1)_form1_reference).main_input.Reinitialize_Keyboard(this.Handle);
            ((Form1)_form1_reference).main_input.Uninitialize_Keyboard();
            MessageBox.Show("Key Bindings Successfully Saved.", "Success!", MessageBoxButtons.OK);
        }
        #endregion
    }
}
