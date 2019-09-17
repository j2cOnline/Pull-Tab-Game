namespace PlayerAudit1063
{
    partial class PlayData
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
            this.lblGameTitle = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.prtPlayDoc = new System.Drawing.Printing.PrintDocument();
            this.lblGameType = new System.Windows.Forms.Label();
            this.pnlTab = new System.Windows.Forms.Panel();
            this.lblTabNo = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblWinnings = new System.Windows.Forms.Label();
            this.pnlWonPatterns = new System.Windows.Forms.Panel();
            this.lblTabPrice = new System.Windows.Forms.Label();
            this.lblBonus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGameTitle
            // 
            this.lblGameTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblGameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameTitle.Location = new System.Drawing.Point(168, 12);
            this.lblGameTitle.Name = "lblGameTitle";
            this.lblGameTitle.Size = new System.Drawing.Size(264, 24);
            this.lblGameTitle.TabIndex = 0;
            this.lblGameTitle.Text = "Game Title";
            this.lblGameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(527, 250);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 40);
            this.btnPrint.TabIndex = 30;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // prtPlayDoc
            // 
            this.prtPlayDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prtPlayDoc_PrintPage);
            // 
            // lblGameType
            // 
            this.lblGameType.BackColor = System.Drawing.Color.Transparent;
            this.lblGameType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameType.Location = new System.Drawing.Point(168, 52);
            this.lblGameType.Name = "lblGameType";
            this.lblGameType.Size = new System.Drawing.Size(264, 24);
            this.lblGameType.TabIndex = 0;
            this.lblGameType.Text = "Game Type Title";
            this.lblGameType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTab
            // 
            this.pnlTab.Location = new System.Drawing.Point(13, 117);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Size = new System.Drawing.Size(324, 194);
            this.pnlTab.TabIndex = 31;
            // 
            // lblTabNo
            // 
            this.lblTabNo.BackColor = System.Drawing.Color.Transparent;
            this.lblTabNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTabNo.Location = new System.Drawing.Point(168, 90);
            this.lblTabNo.Name = "lblTabNo";
            this.lblTabNo.Size = new System.Drawing.Size(264, 24);
            this.lblTabNo.TabIndex = 0;
            this.lblTabNo.Text = "Tab No.";
            this.lblTabNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(343, 152);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(259, 24);
            this.lblBalance.TabIndex = 0;
            this.lblBalance.Text = "End Balance:";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWinnings
            // 
            this.lblWinnings.BackColor = System.Drawing.Color.Transparent;
            this.lblWinnings.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinnings.Location = new System.Drawing.Point(343, 116);
            this.lblWinnings.Name = "lblWinnings";
            this.lblWinnings.Size = new System.Drawing.Size(259, 24);
            this.lblWinnings.TabIndex = 0;
            this.lblWinnings.Text = "Winnings:";
            this.lblWinnings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlWonPatterns
            // 
            this.pnlWonPatterns.Location = new System.Drawing.Point(13, 317);
            this.pnlWonPatterns.Name = "pnlWonPatterns";
            this.pnlWonPatterns.Size = new System.Drawing.Size(589, 250);
            this.pnlWonPatterns.TabIndex = 32;
            // 
            // lblTabPrice
            // 
            this.lblTabPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblTabPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTabPrice.Location = new System.Drawing.Point(343, 188);
            this.lblTabPrice.Name = "lblTabPrice";
            this.lblTabPrice.Size = new System.Drawing.Size(259, 24);
            this.lblTabPrice.TabIndex = 0;
            this.lblTabPrice.Text = "Tab Price:";
            this.lblTabPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBonus
            // 
            this.lblBonus.BackColor = System.Drawing.Color.Transparent;
            this.lblBonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus.Location = new System.Drawing.Point(12, 570);
            this.lblBonus.Name = "lblBonus";
            this.lblBonus.Size = new System.Drawing.Size(590, 24);
            this.lblBonus.TabIndex = 0;
            this.lblBonus.Text = "Bonus";
            this.lblBonus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(614, 612);
            this.Controls.Add(this.pnlWonPatterns);
            this.Controls.Add(this.pnlTab);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblBonus);
            this.Controls.Add(this.lblTabPrice);
            this.Controls.Add(this.lblWinnings);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblTabNo);
            this.Controls.Add(this.lblGameType);
            this.Controls.Add(this.lblGameTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(3, 144);
            this.Name = "PlayData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PlayData";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PlayData_Load);
            this.VisibleChanged += new System.EventHandler(this.PlayData_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlayData_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGameTitle;
        private System.Windows.Forms.Button btnPrint;
        private System.Drawing.Printing.PrintDocument prtPlayDoc;
        private System.Windows.Forms.Label lblGameType;
        private System.Windows.Forms.Panel pnlTab;
        private System.Windows.Forms.Label lblTabNo;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblWinnings;
        private System.Windows.Forms.Panel pnlWonPatterns;
        private System.Windows.Forms.Label lblTabPrice;
        private System.Windows.Forms.Label lblBonus;
    }
}