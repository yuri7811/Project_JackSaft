namespace JackShaft_App
{
    partial class Worker_ID
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
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(229, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(826, 73);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please  insert  your Work ID";
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txt_ID.Location = new System.Drawing.Point(356, 179);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.PasswordChar = '*';
            this.txt_ID.Size = new System.Drawing.Size(409, 80);
            this.txt_ID.TabIndex = 2;
            this.txt_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_ID_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(107, 344);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1081, 73);
            this.label3.TabIndex = 4;
            this.label3.Text = "You ID  is\'t  correct. Please try again.";
            this.label3.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(800, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 80);
            this.button2.TabIndex = 6;
            this.button2.Text = "GO !";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(593, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "52548";
            this.label2.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 67);
            this.button1.TabIndex = 8;
            this.button1.Text = "QA";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Worker_ID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "Worker_ID";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

