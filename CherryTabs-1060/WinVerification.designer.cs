namespace BreakTheBankTabs1063
{
    partial class WinVerification
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
            this.txbLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.lblWinAmount = new System.Windows.Forms.Label();
            this.onScreenKeyboard1 = new BreakTheBankTabs1063.OnScreenKeyboard();
            this.lblResultInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Login";
            // 
            // txbLogin
            // 
            this.txbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLogin.Location = new System.Drawing.Point(152, 20);
            this.txbLogin.Name = "txbLogin";
            this.txbLogin.Size = new System.Drawing.Size(176, 22);
            this.txbLogin.TabIndex = 2;
            this.txbLogin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbLogin.Click += new System.EventHandler(this.controlSelect);
            this.txbLogin.Enter += new System.EventHandler(this.controlSelect);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(207, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // txbPassword
            // 
            this.txbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPassword.Location = new System.Drawing.Point(152, 61);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(176, 22);
            this.txbPassword.TabIndex = 2;
            this.txbPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbPassword.Click += new System.EventHandler(this.controlSelect);
            this.txbPassword.Enter += new System.EventHandler(this.controlSelect);
            // 
            // btnVerify
            // 
            this.btnVerify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerify.Location = new System.Drawing.Point(403, 29);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 50);
            this.btnVerify.TabIndex = 3;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // lblWinAmount
            // 
            this.lblWinAmount.BackColor = System.Drawing.Color.Black;
            this.lblWinAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinAmount.ForeColor = System.Drawing.Color.Red;
            this.lblWinAmount.Location = new System.Drawing.Point(9, 20);
            this.lblWinAmount.Name = "lblWinAmount";
            this.lblWinAmount.Size = new System.Drawing.Size(134, 63);
            this.lblWinAmount.TabIndex = 5;
            this.lblWinAmount.Text = "$$$$$$";
            this.lblWinAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // onScreenKeyboard1
            // 
            this.onScreenKeyboard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onScreenKeyboard1.Location = new System.Drawing.Point(-1, 168);
            this.onScreenKeyboard1.Name = "onScreenKeyboard1";
            this.onScreenKeyboard1.Size = new System.Drawing.Size(494, 197);
            this.onScreenKeyboard1.TabIndex = 0;
            this.onScreenKeyboard1.TabStop = false;
            // 
            // lblResultInfo
            // 
            this.lblResultInfo.BackColor = System.Drawing.Color.Black;
            this.lblResultInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultInfo.ForeColor = System.Drawing.Color.Red;
            this.lblResultInfo.Location = new System.Drawing.Point(25, 90);
            this.lblResultInfo.Name = "lblResultInfo";
            this.lblResultInfo.Size = new System.Drawing.Size(438, 25);
            this.lblResultInfo.TabIndex = 8;
            this.lblResultInfo.Text = "Invalid Login Name!";
            this.lblResultInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(79, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 50);
            this.label3.TabIndex = 7;
            this.label3.Text = "Your winnings amount requires verification from a Clerk..\r\nPlease acquire a clerk" +
                "s attention to claim \r\nyour winnings and continue your game play";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WinVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 371);
            this.ControlBox = false;
            this.Controls.Add(this.lblResultInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbLogin);
            this.Controls.Add(this.lblWinAmount);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.onScreenKeyboard1);
            this.Controls.Add(this.txbPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "WinVerification";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Winner Verification";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.WinVerification_Activated);
            this.Load += new System.EventHandler(this.WinVerification_Load);
            this.VisibleChanged += new System.EventHandler(this.WinVerification_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OnScreenKeyboard onScreenKeyboard1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Label lblWinAmount;
        private System.Windows.Forms.Label lblResultInfo;
        private System.Windows.Forms.Label label3;
    }
}