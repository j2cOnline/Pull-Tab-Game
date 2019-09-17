namespace GameMenuBase1063
{
    partial class GameSetup
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbx2 = new System.Windows.Forms.GroupBox();
            this.btnRemoveDenomination = new System.Windows.Forms.Button();
            this.btnAddDenomination = new System.Windows.Forms.Button();
            this.lbDenominations = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSpin = new System.Windows.Forms.CheckBox();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(2, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1021, 51);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Break The Bank Tabs Settings";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Lime;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(60, 621);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 100);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(844, 621);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 100);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.chkSpin);
            this.gbx2.Controls.Add(this.btnRemoveDenomination);
            this.gbx2.Controls.Add(this.btnAddDenomination);
            this.gbx2.Controls.Add(this.lbDenominations);
            this.gbx2.Controls.Add(this.label1);
            this.gbx2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbx2.Location = new System.Drawing.Point(36, 70);
            this.gbx2.Name = "gbx2";
            this.gbx2.Size = new System.Drawing.Size(950, 670);
            this.gbx2.TabIndex = 7;
            this.gbx2.TabStop = false;
            this.gbx2.Text = "Standard";
            // 
            // btnRemoveDenomination
            // 
            this.btnRemoveDenomination.Enabled = false;
            this.btnRemoveDenomination.Location = new System.Drawing.Point(24, 475);
            this.btnRemoveDenomination.Name = "btnRemoveDenomination";
            this.btnRemoveDenomination.Size = new System.Drawing.Size(148, 36);
            this.btnRemoveDenomination.TabIndex = 2;
            this.btnRemoveDenomination.Text = "Remove";
            this.btnRemoveDenomination.UseVisualStyleBackColor = true;
            this.btnRemoveDenomination.Click += new System.EventHandler(this.btnRemoveDenomination_Click);
            // 
            // btnAddDenomination
            // 
            this.btnAddDenomination.Location = new System.Drawing.Point(24, 433);
            this.btnAddDenomination.Name = "btnAddDenomination";
            this.btnAddDenomination.Size = new System.Drawing.Size(148, 36);
            this.btnAddDenomination.TabIndex = 2;
            this.btnAddDenomination.Text = "Add";
            this.btnAddDenomination.UseVisualStyleBackColor = true;
            this.btnAddDenomination.Click += new System.EventHandler(this.btnAddDenomination_Click);
            // 
            // lbDenominations
            // 
            this.lbDenominations.FormattingEnabled = true;
            this.lbDenominations.ItemHeight = 24;
            this.lbDenominations.Location = new System.Drawing.Point(24, 99);
            this.lbDenominations.Name = "lbDenominations";
            this.lbDenominations.Size = new System.Drawing.Size(148, 316);
            this.lbDenominations.TabIndex = 1;
            this.lbDenominations.SelectedIndexChanged += new System.EventHandler(this.lbDenominations_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tab Prices";
            // 
            // chkSpin
            // 
            this.chkSpin.AutoSize = true;
            this.chkSpin.Location = new System.Drawing.Point(367, 229);
            this.chkSpin.Name = "chkSpin";
            this.chkSpin.Size = new System.Drawing.Size(330, 28);
            this.chkSpin.TabIndex = 5;
            this.chkSpin.Text = "Use Slot style spin instead of pull tab";
            this.chkSpin.UseVisualStyleBackColor = true;
            this.chkSpin.CheckedChanged += new System.EventHandler(this.modified_Changed);
            // 
            // GameSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.gbx2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSetup";
            this.Load += new System.EventHandler(this.GameSetup_Load);
            this.VisibleChanged += new System.EventHandler(this.GameSetup_VisibleChanged);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbx2;
        private System.Windows.Forms.ListBox lbDenominations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemoveDenomination;
        private System.Windows.Forms.Button btnAddDenomination;
        private System.Windows.Forms.CheckBox chkSpin;
    }
}