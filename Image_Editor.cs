using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class Image_Editor : Form
    {

        int PDF_ID = 0;
        String strFilePath = "";
        Image DefaultImage;
        Byte[] ImageByteArray;




        SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);

        string Copy_Macat ="";
        string Copy_Station ="";
        string Copy_Order ="";
        string Copy_Picture ="";
        int Paste_counter = 0;

        public Image_Editor()
        {
            InitializeComponent();
            Makat_Loading();
           // Station_Nr_Loading();
            RefreshImageGrid();
        }
        void RefreshImageGrid()
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();

            string st1;
            int st2;
            if (cmb_Makat.Text == "") { st1 = ""; }   else { st1 = cmb_Makat.Text.Trim(); }
         //   if (cmb_Station.Text == "") { st2 = 555; } else { st2 = Convert.ToInt32(cmb_Station.Text.Trim()); }



            SqlDataAdapter sqlda = new SqlDataAdapter("SP_JS_IMAGE_ViewAll", sqlcon);

            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlda.SelectCommand.Parameters.Add("@Makat", st1);
           // sqlda.SelectCommand.Parameters.Add("@Station_Nr", st2);

            DataTable dtblImages = new DataTable();
            sqlda.Fill(dtblImages);
            dgvImages.DataSource = dtblImages;
            dgvImages.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvImages.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvImages.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvImages.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dgvImages.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dgvImages.Columns[5].Visible = false;
            dgvImages.Columns[0].Visible = false;
            sqlcon.Close();
        }
        void Clear()
        {

            PDF_ID = 0;
            txtPDF_ID.Text = PDF_ID.ToString();
            txt_ImageTitle.Clear();
            txt_Makat.Clear();
            Txt_Op_Order.Clear();
       //     txt_Station_Nr.Clear();
            txt_ImageTitle.Clear();
        //    axAcroPDF1.src = "C:\\PDF_NotFound.pdf";
            strFilePath = "";
            if ((Copy_Macat != "") &  (Copy_Order != "") & (Copy_Picture != "")) { btn_Paste.Visible = true; } else { btn_Paste.Visible = false; }
            if ((txt_Makat.Text != "") & (Txt_Op_Order.Text != "") & (txt_ImageTitle.Text != "")) { btn_Copy.Visible = true; } else { btn_Copy.Visible = false; }
            btnSave.Text = "Save";
        }
   
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if ((txt_Makat.Text.Trim() != "") &  (Txt_Op_Order.Text.Trim() != "") & (txt_ImageTitle.Text.Trim() != ""))
            {

                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_JS_ImageAddOrEdit", sqlcon) { CommandType = CommandType.StoredProcedure };
                sqlCmd.CommandTimeout = 120;
                sqlCmd.Parameters.Add("@IMAGE_ID", PDF_ID);
                sqlCmd.Parameters.Add("@Makat", txt_Makat.Text.Trim());
                sqlCmd.Parameters.Add("@Op_Order", Txt_Op_Order.Text.Trim());
                sqlCmd.Parameters.Add("@Title", txt_ImageTitle.Text.Trim());
              //  sqlCmd.Parameters.Add("@ImageLink", txt_ImageTitle.Text.Trim());
             //   sqlCmd.Parameters.Add("@QTY", Convert.ToInt32(txt_Station_Nr.Text.Trim()));

                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("Saved successfuly");
                Clear();
                RefreshImageGrid();
            }
            else
            {
                MessageBox.Show("Please feel all filds");
            }
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_JS_IMAGE_Delite", sqlcon) { CommandType = CommandType.StoredProcedure };
            sqlCmd.CommandTimeout = 120;
            sqlCmd.Parameters.Add("@IMAGE_ID", PDF_ID);
            sqlCmd.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Deleted");
            RefreshImageGrid();
            Clear();
        }
        private void btn_New_Click_1(object sender, EventArgs e)
        {
            Clear();
            btnSave.Text = "Save";
        }
        private void dgvImages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            try
            {
                txt_Makat.Text = dgvImages.CurrentRow.Cells[1].Value.ToString();
                Txt_Op_Order.Text = dgvImages.CurrentRow.Cells[2].Value.ToString();
                txt_ImageTitle.Text = dgvImages.CurrentRow.Cells[3].Value.ToString();

                strFilePath = txt_ImageTitle.Text;
                textBox1.Text = Properties.Settings.Default.Images_store_path + strFilePath;


                IMAGE_SCREEN.Image = null;
                var fs = new FileStream(Properties.Settings.Default.Images_store_path + strFilePath, FileMode.Open, FileAccess.Read);
                IMAGE_SCREEN.Image = Image.FromStream(fs);
                fs.Dispose();



                IMAGE_SCREEN.SizeMode = PictureBoxSizeMode.StretchImage;


             }

            catch (Exception ex) { MessageBox.Show(ex.Message); }


            PDF_ID = Convert.ToInt32(dgvImages.CurrentRow.Cells[0].Value);
            txtPDF_ID.Text = PDF_ID.ToString();

            if ((txt_Makat.Text != "") &  (Txt_Op_Order.Text != "") & (txt_ImageTitle.Text != "")) { btn_Copy.Visible = true; } else { btn_Copy.Visible = false; }

            btnSave.Text = "Update";




        }
        public void Makat_Loading()
        {
            using (SqlCommand command = new SqlCommand("Select distinct (Makat) from T_JS_IMAGE_TABLE", sqlcon))


                try
                {
                    sqlcon.Open();
                    SqlDataAdapter sqladap1 = new SqlDataAdapter(command);
                    System.Data.DataTable dt2 = new System.Data.DataTable();
                    sqladap1.Fill(dt2);
                    cmb_Makat.DisplayMember = "Makat";
                    cmb_Makat.DataSource = dt2;
                    sqladap1.Update(dt2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
                finally
                {
                    sqlcon.Close();
                }
        }
        private void cmb_Station_MouseClick(object sender, MouseEventArgs e)
        {
           //  Station_Nr_Loading();
        }
        private void cmb_Makat_MouseClick(object sender, MouseEventArgs e)
        {
           Makat_Loading();
        }
        private void cmb_Makat_SelectedIndexChanged(object sender, EventArgs e)
        {
            IMAGE_SCREEN.Image = null;
            Clear();
            RefreshImageGrid();
        }
        private void cmb_Station_SelectedIndexChanged(object sender, EventArgs e)
        {

            IMAGE_SCREEN.Image = null;
            Clear();
            RefreshImageGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Copy_Macat = txt_Makat.Text;
            Copy_Order = Txt_Op_Order.Text;
            Copy_Picture = txt_ImageTitle.Text;

            if ((Copy_Macat != "") &  (Copy_Order != "") & (Copy_Picture != "")) { btn_Paste.Visible = true;  } else { btn_Paste.Visible = false; }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Paste_counter = Paste_counter + 1;
            Clear();
            txt_Makat.Text = Copy_Macat  + "_" + Paste_counter.ToString();
            Txt_Op_Order.Text = Copy_Order;
            txt_ImageTitle.Text = Copy_Picture;
        }

        private void btn_FileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG(.png)|*.*;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFilePath = ofd.FileName;


                IMAGE_SCREEN.Image = null;
                var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                IMAGE_SCREEN.Image = Image.FromStream(fs);
                fs.Dispose();



               // IMAGE_SCREEN.Image = Image.FromFile(ofd.FileName);
                IMAGE_SCREEN.SizeMode = PictureBoxSizeMode.StretchImage;
                txt_ImageTitle.Clear();





                txt_ImageTitle.Text = System.IO.Path.GetFileName(strFilePath);
            }
        }
    }
}
