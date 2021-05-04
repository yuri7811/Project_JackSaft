namespace JackShaft_App
{
    partial class Image_Editor
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
            this.txtPDF_ID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_New = new System.Windows.Forms.Button();
            this.txt_Makat = new System.Windows.Forms.TextBox();
            this.Txt_Op_Order = new System.Windows.Forms.TextBox();
            this.txt_ImageTitle = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvImages = new System.Windows.Forms.DataGridView();
            this.cmb_Makat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Copy = new System.Windows.Forms.Button();
            this.btn_Paste = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.IMAGE_SCREEN = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_FileSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMAGE_SCREEN)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPDF_ID
            // 
            this.txtPDF_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPDF_ID.Location = new System.Drawing.Point(1276, 70);
            this.txtPDF_ID.Name = "txtPDF_ID";
            this.txtPDF_ID.Size = new System.Drawing.Size(49, 35);
            this.txtPDF_ID.TabIndex = 27;
            this.txtPDF_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPDF_ID.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(455, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "Order";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(643, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 18);
            this.label3.TabIndex = 25;
            this.label3.Text = "Image";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(995, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 23;
            this.label1.Text = "Catalog Nr.";
            // 
            // btn_New
            // 
            this.btn_New.BackColor = System.Drawing.Color.LightGray;
            this.btn_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_New.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_New.Location = new System.Drawing.Point(500, 109);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(89, 32);
            this.btn_New.TabIndex = 22;
            this.btn_New.Text = "New";
            this.btn_New.UseVisualStyleBackColor = false;
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click_1);
            // 
            // txt_Makat
            // 
            this.txt_Makat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Makat.ForeColor = System.Drawing.Color.Blue;
            this.txt_Makat.Location = new System.Drawing.Point(234, 73);
            this.txt_Makat.Name = "txt_Makat";
            this.txt_Makat.Size = new System.Drawing.Size(177, 29);
            this.txt_Makat.TabIndex = 21;
            this.txt_Makat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Txt_Op_Order
            // 
            this.Txt_Op_Order.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Op_Order.ForeColor = System.Drawing.Color.Blue;
            this.Txt_Op_Order.Location = new System.Drawing.Point(443, 73);
            this.Txt_Op_Order.Name = "Txt_Op_Order";
            this.Txt_Op_Order.Size = new System.Drawing.Size(58, 29);
            this.Txt_Op_Order.TabIndex = 19;
            this.Txt_Op_Order.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_ImageTitle
            // 
            this.txt_ImageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ImageTitle.ForeColor = System.Drawing.Color.Blue;
            this.txt_ImageTitle.Location = new System.Drawing.Point(543, 73);
            this.txt_ImageTitle.Name = "txt_ImageTitle";
            this.txt_ImageTitle.Size = new System.Drawing.Size(265, 29);
            this.txt_ImageTitle.TabIndex = 18;
            this.txt_ImageTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(702, 109);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 32);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Delete";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(601, 109);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 32);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // dgvImages
            // 
            this.dgvImages.AllowUserToAddRows = false;
            this.dgvImages.AllowUserToDeleteRows = false;
            this.dgvImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImages.Location = new System.Drawing.Point(840, 158);
            this.dgvImages.Name = "dgvImages";
            this.dgvImages.ReadOnly = true;
            this.dgvImages.RowTemplate.Height = 30;
            this.dgvImages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImages.Size = new System.Drawing.Size(485, 559);
            this.dgvImages.TabIndex = 28;
            this.dgvImages.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvImages_CellDoubleClick);
            // 
            // cmb_Makat
            // 
            this.cmb_Makat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmb_Makat.ForeColor = System.Drawing.Color.Blue;
            this.cmb_Makat.FormattingEnabled = true;
            this.cmb_Makat.Location = new System.Drawing.Point(963, 73);
            this.cmb_Makat.Name = "cmb_Makat";
            this.cmb_Makat.Size = new System.Drawing.Size(166, 32);
            this.cmb_Makat.TabIndex = 30;
            this.cmb_Makat.SelectedIndexChanged += new System.EventHandler(this.cmb_Makat_SelectedIndexChanged);
            this.cmb_Makat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmb_Makat_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(282, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 18);
            this.label6.TabIndex = 32;
            this.label6.Text = "Catalog Nr.";
            // 
            // btn_Copy
            // 
            this.btn_Copy.BackColor = System.Drawing.Color.LightGray;
            this.btn_Copy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Copy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Copy.Location = new System.Drawing.Point(298, 109);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Size = new System.Drawing.Size(89, 32);
            this.btn_Copy.TabIndex = 34;
            this.btn_Copy.Text = "Copy";
            this.btn_Copy.UseVisualStyleBackColor = false;
            this.btn_Copy.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Paste
            // 
            this.btn_Paste.BackColor = System.Drawing.Color.LightGray;
            this.btn_Paste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Paste.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Paste.Location = new System.Drawing.Point(399, 109);
            this.btn_Paste.Name = "btn_Paste";
            this.btn_Paste.Size = new System.Drawing.Size(89, 32);
            this.btn_Paste.TabIndex = 35;
            this.btn_Paste.Text = "Paste";
            this.btn_Paste.UseVisualStyleBackColor = false;
            this.btn_Paste.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(227, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 53;
            this.pictureBox2.TabStop = false;
            // 
            // IMAGE_SCREEN
            // 
            this.IMAGE_SCREEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IMAGE_SCREEN.Location = new System.Drawing.Point(25, 158);
            this.IMAGE_SCREEN.Name = "IMAGE_SCREEN";
            this.IMAGE_SCREEN.Size = new System.Drawing.Size(790, 559);
            this.IMAGE_SCREEN.TabIndex = 29;
            this.IMAGE_SCREEN.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Location = new System.Drawing.Point(25, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 33);
            this.button1.TabIndex = 54;
            this.button1.Text = "Statistic";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(840, 123);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(485, 29);
            this.textBox1.TabIndex = 55;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_FileSelect
            // 
            this.btn_FileSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_FileSelect.Location = new System.Drawing.Point(840, 72);
            this.btn_FileSelect.Name = "btn_FileSelect";
            this.btn_FileSelect.Size = new System.Drawing.Size(75, 30);
            this.btn_FileSelect.TabIndex = 56;
            this.btn_FileSelect.Text = ". . .";
            this.btn_FileSelect.UseVisualStyleBackColor = true;
            this.btn_FileSelect.Click += new System.EventHandler(this.btn_FileSelect_Click);
            // 
            // Image_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 729);
            this.Controls.Add(this.btn_FileSelect);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_Paste);
            this.Controls.Add(this.btn_Copy);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmb_Makat);
            this.Controls.Add(this.IMAGE_SCREEN);
            this.Controls.Add(this.dgvImages);
            this.Controls.Add(this.txtPDF_ID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_New);
            this.Controls.Add(this.txt_Makat);
            this.Controls.Add(this.Txt_Op_Order);
            this.Controls.Add(this.txt_ImageTitle);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Name = "Image_Editor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMAGE_SCREEN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPDF_ID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_New;
        private System.Windows.Forms.TextBox txt_Makat;
        private System.Windows.Forms.TextBox Txt_Op_Order;
        private System.Windows.Forms.TextBox txt_ImageTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvImages;
        private System.Windows.Forms.PictureBox IMAGE_SCREEN;
        private System.Windows.Forms.ComboBox cmb_Makat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Copy;
        private System.Windows.Forms.Button btn_Paste;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_FileSelect;
    }
}