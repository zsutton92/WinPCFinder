namespace WinPCFinder
{
    partial class frmMain
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.lblBeginIP = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtBeginIP = new System.Windows.Forms.TextBox();
            this.lblEndingIP = new System.Windows.Forms.Label();
            this.txtEndingIP = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dgvWinPCResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWinPCResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBeginIP
            // 
            this.lblBeginIP.AutoSize = true;
            this.lblBeginIP.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeginIP.ForeColor = System.Drawing.Color.Red;
            this.lblBeginIP.Location = new System.Drawing.Point(67, 51);
            this.lblBeginIP.Name = "lblBeginIP";
            this.lblBeginIP.Size = new System.Drawing.Size(147, 15);
            this.lblBeginIP.TabIndex = 0;
            this.lblBeginIP.Text = "Beginning IP Address";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(224, 114);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 32);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtBeginIP
            // 
            this.txtBeginIP.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBeginIP.Location = new System.Drawing.Point(70, 76);
            this.txtBeginIP.Name = "txtBeginIP";
            this.txtBeginIP.Size = new System.Drawing.Size(100, 20);
            this.txtBeginIP.TabIndex = 5;
            this.txtBeginIP.TextChanged += new System.EventHandler(this.txtBeginIP_TextChanged);
            // 
            // lblEndingIP
            // 
            this.lblEndingIP.AutoSize = true;
            this.lblEndingIP.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndingIP.ForeColor = System.Drawing.Color.Red;
            this.lblEndingIP.Location = new System.Drawing.Point(364, 51);
            this.lblEndingIP.Name = "lblEndingIP";
            this.lblEndingIP.Size = new System.Drawing.Size(126, 15);
            this.lblEndingIP.TabIndex = 6;
            this.lblEndingIP.Text = "Ending IP Address";
            // 
            // txtEndingIP
            // 
            this.txtEndingIP.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndingIP.Location = new System.Drawing.Point(367, 76);
            this.txtEndingIP.Name = "txtEndingIP";
            this.txtEndingIP.Size = new System.Drawing.Size(91, 20);
            this.txtEndingIP.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(12, 158);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(536, 57);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = " ";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvWinPCResults
            // 
            this.dgvWinPCResults.AllowUserToAddRows = false;
            this.dgvWinPCResults.AllowUserToDeleteRows = false;
            this.dgvWinPCResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWinPCResults.Location = new System.Drawing.Point(12, 207);
            this.dgvWinPCResults.Name = "dgvWinPCResults";
            this.dgvWinPCResults.ReadOnly = true;
            this.dgvWinPCResults.Size = new System.Drawing.Size(628, 425);
            this.dgvWinPCResults.TabIndex = 9;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(652, 694);
            this.Controls.Add(this.dgvWinPCResults);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtEndingIP);
            this.Controls.Add(this.lblEndingIP);
            this.Controls.Add(this.txtBeginIP);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblBeginIP);
            this.Name = "frmMain";
            this.Text = "WinPCFinder";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWinPCResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBeginIP;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtBeginIP;
        private System.Windows.Forms.Label lblEndingIP;
        private System.Windows.Forms.TextBox txtEndingIP;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgvWinPCResults;
    }
}

