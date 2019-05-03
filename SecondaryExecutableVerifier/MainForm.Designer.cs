namespace SecondaryExecutableVerifier
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtHardwareID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOemPKHash = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblCertificateStoreStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblImageSignatureStatus = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRootCertificateStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSoftwareID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File to verify:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(108, 108);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(269, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // txtHardwareID
            // 
            this.txtHardwareID.Location = new System.Drawing.Point(108, 6);
            this.txtHardwareID.Name = "txtHardwareID";
            this.txtHardwareID.Size = new System.Drawing.Size(129, 20);
            this.txtHardwareID.TabIndex = 2;
            this.txtHardwareID.Text = "00 80 00 E1 00 C8 00 00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "HW_ID:";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(464, 106);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 4;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "OEM_PK_HASH:";
            // 
            // txtOemPKHash
            // 
            this.txtOemPKHash.Location = new System.Drawing.Point(108, 74);
            this.txtOemPKHash.Name = "txtOemPKHash";
            this.txtOemPKHash.Size = new System.Drawing.Size(512, 20);
            this.txtOemPKHash.TabIndex = 6;
            this.txtOemPKHash.Text = "2D E2 B8 86 D7 7F 39 5C 2E 82 EF 78 C1 48 D1 2E B4 99 3B C2 AA 3A CC 92 5B 65 F9 " +
                "EC 58 40 D7 F8";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Certificate Store Status:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(383, 106);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblCertificateStoreStatus
            // 
            this.lblCertificateStoreStatus.AutoSize = true;
            this.lblCertificateStoreStatus.Location = new System.Drawing.Point(136, 186);
            this.lblCertificateStoreStatus.Name = "lblCertificateStoreStatus";
            this.lblCertificateStoreStatus.Size = new System.Drawing.Size(50, 13);
            this.lblCertificateStoreStatus.TabIndex = 9;
            this.lblCertificateStoreStatus.Text = "STATUS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Image Signature Status:";
            // 
            // lblImageSignatureStatus
            // 
            this.lblImageSignatureStatus.AutoSize = true;
            this.lblImageSignatureStatus.Location = new System.Drawing.Point(136, 221);
            this.lblImageSignatureStatus.Name = "lblImageSignatureStatus";
            this.lblImageSignatureStatus.Size = new System.Drawing.Size(50, 13);
            this.lblImageSignatureStatus.TabIndex = 11;
            this.lblImageSignatureStatus.Text = "STATUS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Root Certificate Status:";
            // 
            // lblRootCertificateStatus
            // 
            this.lblRootCertificateStatus.AutoSize = true;
            this.lblRootCertificateStatus.Location = new System.Drawing.Point(136, 151);
            this.lblRootCertificateStatus.Name = "lblRootCertificateStatus";
            this.lblRootCertificateStatus.Size = new System.Drawing.Size(50, 13);
            this.lblRootCertificateStatus.TabIndex = 13;
            this.lblRootCertificateStatus.Text = "STATUS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(243, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(265, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "(The QFPROM stores this value in reversed byte order)";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(545, 106);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 15;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "SW_ID:";
            // 
            // txtSoftwareID
            // 
            this.txtSoftwareID.Location = new System.Drawing.Point(108, 40);
            this.txtSoftwareID.Name = "txtSoftwareID";
            this.txtSoftwareID.Size = new System.Drawing.Size(129, 20);
            this.txtSoftwareID.TabIndex = 16;
            this.txtSoftwareID.Text = "00 00 00 00 00 00 00 00";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 253);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSoftwareID);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblRootCertificateStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblImageSignatureStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCertificateStoreStatus);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOemPKHash);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHardwareID);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(630, 280);
            this.MinimumSize = new System.Drawing.Size(630, 280);
            this.Name = "MainForm";
            this.Text = "Secondary Executable Verifier";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtHardwareID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOemPKHash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblCertificateStoreStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblImageSignatureStatus;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRootCertificateStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSoftwareID;
    }
}

