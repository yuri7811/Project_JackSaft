namespace JackShaft_App
{
    partial class Form_Password
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
            this.txtPasword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPasswordAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPasword
            // 
            this.txtPasword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtPasword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtPasword.Location = new System.Drawing.Point(70, 52);
            this.txtPasword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPasword.Name = "txtPasword";
            this.txtPasword.PasswordChar = '#';
            this.txtPasword.Size = new System.Drawing.Size(139, 32);
            this.txtPasword.TabIndex = 1;
            this.txtPasword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(36, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Please insert password";
            // 
            // BtnPasswordAccept
            // 
            this.BtnPasswordAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnPasswordAccept.Location = new System.Drawing.Point(70, 94);
            this.BtnPasswordAccept.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPasswordAccept.Name = "BtnPasswordAccept";
            this.BtnPasswordAccept.Size = new System.Drawing.Size(138, 40);
            this.BtnPasswordAccept.TabIndex = 3;
            this.BtnPasswordAccept.Text = "Accept";
            this.BtnPasswordAccept.UseVisualStyleBackColor = true;
            this.BtnPasswordAccept.Click += new System.EventHandler(this.BtnPasswordAccept_Click);
            // 
            // Form_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 165);
            this.Controls.Add(this.txtPasword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnPasswordAccept);
            this.Name = "Form_Password";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPasword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnPasswordAccept;
    }
}