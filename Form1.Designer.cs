namespace dbManager
{
    partial class Form1
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
            this.btnDetach = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.btnAddDbNameToList = new System.Windows.Forms.Button();
            this.lblInfoMsg = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDetach02 = new System.Windows.Forms.Button();
            this.btnAttach02 = new System.Windows.Forms.Button();
            this.btnRestartSql = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnRestart02 = new System.Windows.Forms.Button();
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblInstance = new System.Windows.Forms.Label();
            this.txtInstanceName = new System.Windows.Forms.TextBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblDatabases = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDetach
            // 
            this.btnDetach.Location = new System.Drawing.Point(446, 72);
            this.btnDetach.Name = "btnDetach";
            this.btnDetach.Size = new System.Drawing.Size(64, 31);
            this.btnDetach.TabIndex = 1;
            this.btnDetach.Text = "Detach I";
            this.btnDetach.UseVisualStyleBackColor = true;
            this.btnDetach.Click += new System.EventHandler(this.btnDetach_Click);
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(376, 72);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(64, 31);
            this.btnAttach.TabIndex = 2;
            this.btnAttach.Text = "Attach I";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // btnAddDbNameToList
            // 
            this.btnAddDbNameToList.Location = new System.Drawing.Point(376, 24);
            this.btnAddDbNameToList.Name = "btnAddDbNameToList";
            this.btnAddDbNameToList.Size = new System.Drawing.Size(274, 27);
            this.btnAddDbNameToList.TabIndex = 5;
            this.btnAddDbNameToList.Text = "Add Database to List";
            this.btnAddDbNameToList.UseVisualStyleBackColor = true;
            this.btnAddDbNameToList.Click += new System.EventHandler(this.btnAddDbNameToList_Click);
            // 
            // lblInfoMsg
            // 
            this.lblInfoMsg.BackColor = System.Drawing.Color.Khaki;
            this.lblInfoMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoMsg.Location = new System.Drawing.Point(12, 274);
            this.lblInfoMsg.Name = "lblInfoMsg";
            this.lblInfoMsg.Size = new System.Drawing.Size(665, 161);
            this.lblInfoMsg.TabIndex = 7;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(717, 122);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(147, 31);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDetach02
            // 
            this.btnDetach02.Location = new System.Drawing.Point(586, 72);
            this.btnDetach02.Name = "btnDetach02";
            this.btnDetach02.Size = new System.Drawing.Size(64, 31);
            this.btnDetach02.TabIndex = 9;
            this.btnDetach02.Text = "Detach II";
            this.btnDetach02.UseVisualStyleBackColor = true;
            this.btnDetach02.Click += new System.EventHandler(this.btnDetach02_Click);
            // 
            // btnAttach02
            // 
            this.btnAttach02.Location = new System.Drawing.Point(516, 72);
            this.btnAttach02.Name = "btnAttach02";
            this.btnAttach02.Size = new System.Drawing.Size(64, 31);
            this.btnAttach02.TabIndex = 11;
            this.btnAttach02.Text = "Attach II";
            this.btnAttach02.UseVisualStyleBackColor = true;
            this.btnAttach02.Click += new System.EventHandler(this.btnAttach02_Click);
            // 
            // btnRestartSql
            // 
            this.btnRestartSql.Location = new System.Drawing.Point(717, 11);
            this.btnRestartSql.Name = "btnRestartSql";
            this.btnRestartSql.Size = new System.Drawing.Size(147, 27);
            this.btnRestartSql.TabIndex = 12;
            this.btnRestartSql.Text = "Restart SQLSERVER";
            this.btnRestartSql.UseVisualStyleBackColor = true;
            this.btnRestartSql.Click += new System.EventHandler(this.btnRestartSql_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(376, 129);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(64, 31);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemoveDb_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(516, 129);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(64, 31);
            this.btnRestore.TabIndex = 15;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnRestart02
            // 
            this.btnRestart02.Location = new System.Drawing.Point(717, 65);
            this.btnRestart02.Name = "btnRestart02";
            this.btnRestart02.Size = new System.Drawing.Size(147, 31);
            this.btnRestart02.TabIndex = 16;
            this.btnRestart02.Text = "Offline Database";
            this.btnRestart02.UseVisualStyleBackColor = true;
            this.btnRestart02.Click += new System.EventHandler(this.btnRestart02_Click);
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(6, 88);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(130, 21);
            this.cmbDatabases.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAbout);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lblInstance);
            this.panel1.Controls.Add(this.txtInstanceName);
            this.panel1.Controls.Add(this.btnAddDbNameToList);
            this.panel1.Controls.Add(this.btnBackup);
            this.panel1.Controls.Add(this.lblDatabases);
            this.panel1.Controls.Add(this.cmbDatabases);
            this.panel1.Controls.Add(this.btnAttach);
            this.panel1.Controls.Add(this.btnDetach);
            this.panel1.Controls.Add(this.btnRestore);
            this.panel1.Controls.Add(this.btnDetach02);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.btnAttach02);
            this.panel1.Location = new System.Drawing.Point(15, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 248);
            this.panel1.TabIndex = 23;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(586, 129);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(64, 31);
            this.btnAbout.TabIndex = 26;
            this.btnAbout.Text = "??";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(151, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(50, 20);
            this.btnOk.TabIndex = 25;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnGetInstance_Click);
            // 
            // lblInstance
            // 
            this.lblInstance.AutoSize = true;
            this.lblInstance.Location = new System.Drawing.Point(3, 14);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(76, 13);
            this.lblInstance.TabIndex = 24;
            this.lblInstance.Text = "InstanceName";
            // 
            // txtInstanceName
            // 
            this.txtInstanceName.Location = new System.Drawing.Point(6, 30);
            this.txtInstanceName.Name = "txtInstanceName";
            this.txtInstanceName.Size = new System.Drawing.Size(130, 20);
            this.txtInstanceName.TabIndex = 23;
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(446, 129);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(64, 31);
            this.btnBackup.TabIndex = 22;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lblDatabases
            // 
            this.lblDatabases.AutoSize = true;
            this.lblDatabases.Location = new System.Drawing.Point(3, 72);
            this.lblDatabases.Name = "lblDatabases";
            this.lblDatabases.Size = new System.Drawing.Size(77, 13);
            this.lblDatabases.TabIndex = 21;
            this.lblDatabases.Text = "Databases List";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(717, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 36);
            this.button1.TabIndex = 24;
            this.button1.Text = "New Bakup _Restore";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 444);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRestart02);
            this.Controls.Add(this.btnRestartSql);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblInfoMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dbManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDetach;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Button btnAddDbNameToList;
        private System.Windows.Forms.Label lblInfoMsg;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDetach02;
        private System.Windows.Forms.Button btnAttach02;
        private System.Windows.Forms.Button btnRestartSql;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnRestart02;
        private System.Windows.Forms.ComboBox cmbDatabases;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDatabases;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblInstance;
        private System.Windows.Forms.TextBox txtInstanceName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button button1;
    }
}

