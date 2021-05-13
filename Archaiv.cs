using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace JackShaft_App
{
    public partial class Archaiv : Form
    {

        int Set_Nr_for_Report;
        string strFilePath;

        string Lot_for_Report ;
        string Makat_for_Report;
        int Task_for_Report;
        string QTY_for_Report;
        int Lost_for_Report;

        int Prd_for_Report;
        int Prd_paint_for_Report;

        string BetweenDates_Search;
        string Lot_Search;
        string Makat_Search;
        string Lot_for_Print;
        string Makat_for_Print;
        int Blanks_for_Print;
        int Qty_in_Blank_for_Print;
        DataTable dt1 = new DataTable();
        bool Form_mode = true;
        int Mode = 0; // 0 -  report , 1  - archive
        List<int> Exit_Set_List = new List<int>();
        string s;
        int Selector = 0;
        int Oper;
        string SQL_Query;

        string BarcodeString;
        string MyPatch = @"\\fbhczcapp1\BarTender_Labels\LABELS_TYPES\Spindles\Print\Sample.txt";
        int MyPrinter = 0;
        string Lable_Print_pcs = "1";
        string ImageLink;
        string Mahsan;
        string DISCRIPTION;
        string POU;
        string Worker;
        string Station;
        string ID_Operation;
        string Waigth;
        int Multi_Print_QTY;


        private string Remove_All_AB_From_Barcode (string Barcode)
        {
            string Numbers = Regex.Replace(Barcode, @"^[A-Za-z]+", "");
            return Numbers;
        }

        public Archaiv()
        {
            InitializeComponent();
            datePicker_fromDate.Value = DateTime.Now.AddDays(-5);
            datePicker_to_Date.Value =  DateTime.Now.AddDays(1);
            datePicker_fromDate.CustomFormat = "dd/MM/yy";
            datePicker_to_Date.CustomFormat = "dd/MM/yy";
            checkBox_by_Lot.Checked = false;
            checkBox_by_Catalog.Checked = false;
            txt_Lot.Visible = false;
            panel6.Visible = true;
            cb_Station.Visible = false;

            Load_Finished_List();
        }
        private void Load_Finished_List()  // загружаем
        {
try {

            dt1.Rows.Clear();


            BetweenDates_Search = "Finish_DateTime between '" + datePicker_fromDate.Value.ToString("MM/dd/yyyy") + "'  and '" + datePicker_to_Date.Value.ToString("MM/dd/yyyy") + "'";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);
            if (checkBox_by_Lot.Checked == true)
            { SQL_Query = "select   * from [SF_Productivity].[dbo].[V_JS_PartProcessing_Report] where (" + BetweenDates_Search + ") and ((Makat LIKE  '%' + RTRIM(LTRIM( " + BarcodeString + ")) + '%') or (Lot LIKE '%' + RTRIM(LTRIM(" + BarcodeString + ")) + '%'))   order by  ID desc"; }
            else
            { SQL_Query = "select  * from [SF_Productivity].[dbo].[V_JS_PartProcessing_Report] where (" + BetweenDates_Search + ")  order by  ID desc"; }

            DataTable dt = new DataTable();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(SQL_Query, conn);
            da.Fill(dt1);
            conn.Close();


            DGV_Archaiv_Items.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 14, FontStyle.Regular);
            DGV_Archaiv_Items.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 14, FontStyle.Regular);
            DGV_Archaiv_Items.Columns[5].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 14, FontStyle.Bold);
            DGV_Archaiv_Items.Columns[6].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 14, FontStyle.Bold);
            DGV_Archaiv_Items.RowTemplate.Height = 190;



            DGV_Archaiv_Items.DataSource = null;
            DGV_Archaiv_Items.Rows.Clear();
            Lot_Search = "";
            Makat_Search = "";

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                // соединяем три поля в одну трехстрочную ячейку
                string Concat =   // dt1.Rows[i][1].ToString().Trim()       // station
                         dt1.Rows[i][2].ToString().Trim()    // lot
                        + "\r\n" + dt1.Rows[i][3].ToString().Trim();   // makat

                Image Picture = null;
                try {

                        string  Picture_Link = Reformating_ConnectionString(dt1.Rows[i][11].ToString().Trim());
                        Picture = Image.FromFile(Picture_Link); }

                catch { Picture = Image.FromFile("NoImage.png"); }

                    // строим на основе основного грида новый для  работе в форме
                    DGV_Archaiv_Items.Rows.Add(Picture,
                    dt1.Rows[i][2].ToString().Trim(),                                // лот
                    dt1.Rows[i][3].ToString().Trim(),                                // макат
                    Concat,
                    dt1.Rows[i][4].ToString().Trim(),                                // QTY
                    dt1.Rows[i][5].ToString().Trim(),                               // date
                                                                                      //dt1.Rows[i][7].ToString().Trim(),                                // Warehouse
                                                                                      //dt1.Rows[i][0].ToString().Trim(),                                // ID operation
                                                                                      //dt1.Rows[i][4].ToString().Trim(),                                // Image Link
                                                                                      //dt1.Rows[i][8].ToString().Trim(),                                // mahsan
                                                                                      //dt1.Rows[i][14].ToString().Trim(),                               // Worker
                    dt1.Rows[i][12].ToString().Trim(),                               // Discription
                    dt1.Rows[i][13].ToString().Trim());                              // POU

                }
            }

            catch { }
            DGV_Archaiv_Items.ClearSelection();

            DGV_Archaiv_Items.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DGV_Archaiv_Items.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_Archaiv_Items.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 14, FontStyle.Regular);
            DGV_Archaiv_Items.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 14, FontStyle.Regular);
            DGV_Archaiv_Items.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 14, FontStyle.Regular);
            DGV_Archaiv_Items.Columns[6].DefaultCellStyle.Format = " hh:mm dd/MM ";

            DGV_Archaiv_Items.RowTemplate.Height = 80;
            dt1.Rows.Clear();

            DGV_Archaiv_Items.ClearSelection();
        }

        private string Reformating_ConnectionString(string OriginalString)          // преобразовываем линк  в нормальный ПС читаемый  формат
        {
            if (OriginalString == "") { strFilePath = "NoImage.png"; }
            else
            {
                strFilePath = null;
                string OriginalPatch = OriginalString;
                string Cutting = OriginalPatch.Substring(32, OriginalPatch.Length - 32);
                string AfterChanging = Cutting.Replace("/", @"\");
                string AfterConnection = @"\\gfbhcapp1\\tcibd001$\" + AfterChanging;
                string AfterSpaseDeleting = AfterConnection.Replace(" ", ""); ;
                strFilePath = AfterSpaseDeleting;
            }
            return strFilePath;

        }
        private void DGV_Set_Items_SelectionChanged(object sender, EventArgs e)  // click on DATAGRIDVIEW /select
        {


            if (DGV_Archaiv_Items.SelectedRows.Count > 0)

            {
                panel1.Visible = true;
            }
            else { panel1.Visible = false; }

            foreach (DataGridViewRow row in DGV_Archaiv_Items.SelectedRows)

                    {
                Lot_for_Report = row.Cells[1].Value.ToString();
                Makat_for_Report = row.Cells[2].Value.ToString();
                QTY_for_Report = row.Cells[4].Value.ToString();
                Station = row.Cells[3].Value.ToString();
                DISCRIPTION = row.Cells[6].Value.ToString();
                Blanks_for_Print = 1;
                Waigth = row.Cells[7].Value.ToString();

            }

        }
        private void datePicker_fromDate_ValueChanged(object sender, EventArgs e)  // change datapicker FROM
        {
            Load_Finished_List();
        }
        private void datePicker_to_Date_ValueChanged(object sender, EventArgs e)   // change datapicker TO
        {
            Load_Finished_List();
        }
        private void txt_Lot_TextChanged(object sender, EventArgs e)    // change LOT text fild
        {

            BarcodeString = Remove_All_AB_From_Barcode(txt_Lot.Text);
            Load_Finished_List();
        }
        private void cb_Station_SelectedIndexChanged(object sender, EventArgs e)// change Station text fild
        {
        Load_Finished_List();
        }
        private void Btn_Print_Set_Lot_Click(object sender, EventArgs e)   // Print button
        {
            SQL_Jobs BBB = new SQL_Jobs();
            SQL_Jobs CCC = new SQL_Jobs();

         

                BBB.Print_Lable(
                Properties.Settings.Default.ID_Worker,                               // WORKER id
                Lot_for_Report,                                                      // Lot
                Makat_for_Report,                                                    // Makat
                QTY_for_Report,                                                      // Value
                ImageLink,                                                           // IMAGE PATH
                DISCRIPTION,                                                         // DISCRIPTION
                Properties.Settings.Default.Printer_Lable,                           // Printer
                Waigth,                                                              // Weight
                0);                                                                  // Printed lable
         

                CCC.Print_Lable(
                Properties.Settings.Default.ID_Worker,                               // WORKER id
                Lot_for_Report,                                                      // Lot
                Makat_for_Report.Substring(0, Makat_for_Report.Length - 3),                                                    // Makat
                QTY_for_Report,                                                      // Value
                ImageLink,                                                           // IMAGE PATH
                DISCRIPTION,                                                         // DISCRIPTION
                Properties.Settings.Default.Printer_Lable,                           // Printer
                Waigth,                                                              // Weight
                1);                                                                  // Printed lable
          


            
        }
   
        private void lbl_Nr_Documents_for_Print_Click(object sender, EventArgs e)
        {
                    Properties.Settings.Default.Print_Blank = lbl_Nr_Documents_for_Print.Text;
                    Form Key_Form = new KeyPad(1, "",1000);
                    Key_Form.ShowDialog(this);
                    lbl_Nr_Documents_for_Print.Text = Properties.Settings.Default.Print_Blank;
        } // Number lables for print
        private void lbl_Nr_Pcs_in_Printing_Document_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Items_In_Blank = lbl_Nr_Pcs_in_Printing_Document.Text;
            Form Key_Form = new KeyPad(1, "",1000);
            Key_Form.ShowDialog(this);
            lbl_Nr_Pcs_in_Printing_Document.Text = Properties.Settings.Default.Print_Items_In_Blank;
        }// Number PCS for one lable
        private void DGV_Set_Items_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
        private void CBox_Reparing_CheckedChanged(object sender, EventArgs e)  // multi print
        {


            lbl_Nr_Documents_for_Print.Text = "1"; lbl_Nr_Pcs_in_Printing_Document.Text = "1";
            if (CBox_MultiPrint.Checked){panel8.Visible = true;} else { panel8.Visible = false;  }


        }
        private void checkBox_by_Lot_CheckedChanged(object sender, EventArgs e)  // search by Lot/Makat
        {
            if (checkBox_by_Lot.Checked == false) { txt_Lot.Visible = false; txt_Lot.Text = ""; } else { txt_Lot.Visible = true; txt_Lot.Text = ""; };
        }
        private void checkBox_by_Catalog_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_by_Catalog.Checked == false) { cb_Station.Visible = false; cb_Station.Text = ""; } else { cb_Station.Visible = true; cb_Station.Text = ""; };

        }// search by by Station.
       
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
