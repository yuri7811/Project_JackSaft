using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

namespace JackShaft_App
{
    public partial class QA : Form
    {


        int Set_Nr_for_Report;
        string Station;
        string strFilePath;
        string Operation_ID;
        string QA_Status;
        string QA_Worker;
        string QA_Resume;
        string Search_QA_Status  = " = 0";

        string Lot_for_Report;
        string Makat_for_Report;
        string QA_Status_Print;
        int Task_for_Report;
        int QTY_for_Report;
        int Lost_for_Report;

        int Prd_for_Report;
        int Prd_paint_for_Report;

        string BetweenDates_Search;
        string Lot_Search;
        string Makat_Search;
        string Lot_for_Print;
        string Note_for_Print;

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
        string QA_printer;



        string BarcodeString;

        string MyPatch = @"\\fbhczcapp1\BarTender_Labels\LABELS_TYPES\Spindles\Print\Sample.txt";
        int MyPrinter = 0;
        string Lable_Print_pcs = "1";
        bool Permitions = false;



        private string Remove_All_AB_From_Barcode(string Barcode)
        {
            string Numbers = Regex.Replace(Barcode, @"^[A-Za-z]+", "");
            return Numbers;
        }

        public QA()
        {
            InitializeComponent();
            datePicker_fromDate.Value = DateTime.Now.AddDays(-5);
            datePicker_to_Date.Value = DateTime.Now.AddDays(1);
            datePicker_fromDate.CustomFormat = "dd/MM/yy";
            datePicker_to_Date.CustomFormat = "dd/MM/yy";
            checkBox_by_Lot.Checked = false;
            checkBox_by_Catalog.Checked = false;
            txt_Lot.Visible = false;
            panel6.Visible = true;
            cb_Station.Visible = false;
            //label3.Text = "Operator QA:   " + Properties.Settings.Default.Worker_Name_QA.ToString();
            btn_Call_QA.Visible = false;
            CheckPermition();

            Search_QA_Status = "=  0  or  [QA_Status]  = 3 ";
            Load_Finished_List();
        }
        private void Load_Finished_List()  // загружаем
        {

                dt1.Rows.Clear();

                BetweenDates_Search = "Finish_DateTime between '" + datePicker_fromDate.Value.ToString("MM/dd/yyyy") + "'  and '" + datePicker_to_Date.Value.ToString("MM/dd/yyyy") + "'";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);
                if (checkBox_by_Lot.Checked == true)
                { SQL_Query = "select   * from [SF_Productivity].[dbo].[V_JS_QA] where (" + BetweenDates_Search + ") and (Makat LIKE  '%' + RTRIM(LTRIM( " + BarcodeString + ")) + '%' or Lot LIKE '%' + RTRIM(LTRIM(" + BarcodeString + ")) + '%') and ([QA_Status]  " + Search_QA_Status + ") order by ID desc"; }
                else
                { SQL_Query = "select  * from [SF_Productivity].[dbo].[V_JS_QA] where (" + BetweenDates_Search + ")  and ([QA_Status] " + Search_QA_Status + ") order by  ID desc"; }


                DataTable dt = new DataTable();
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQL_Query, conn);
                da.Fill(dt1);
                conn.Close();




                DGV_Archaiv_Items.DataSource = null;
                DGV_Archaiv_Items.Rows.Clear();
                Lot_Search = "";
                Makat_Search = "";


                DGV_Archaiv_Items.Columns[5].DefaultCellStyle.Format = "hh:mm dd/MM";



                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    // соединяем три поля в одну трехстрочную ячейку
                    string Concat = //dt1.Rows[i][1].ToString().Trim()       // station
                                dt1.Rows[i][2].ToString().Trim()    // lot
                            + "\r\n" + dt1.Rows[i][3].ToString().Trim();   // makat

                   // (DateTime)row.Cells[4].Value).ToString("MM-dd-yyyy")
                    string DateTime = dt1.Rows[i][6].ToString();
                    Image Picture = null;
                    try
                    {

                        string Picture_Link = Reformating_ConnectionString(dt1.Rows[i][4].ToString().Trim());
                        Picture = Image.FromFile(Picture_Link);
                    }

                    catch { Picture = Image.FromFile("NoImage.png"); }

                    // строим на основе основного грида новый для  работе в форме
                    DGV_Archaiv_Items.Rows.Add(
                    dt1.Rows[i][0].ToString().Trim(),                                // ID
                    Picture,                                                         // Image
                    dt1.Rows[i][1].ToString().Trim(),                                // Station
                    Concat,
                    dt1.Rows[i][5].ToString().Trim(),                                // количество
                    DateTime.ToString(),                                             // data
                    dt1.Rows[i][7].ToString().Trim(),                                // warehouse
                    dt1.Rows[i][8].ToString().Trim(),                                // Status QA

                    dt1.Rows[i][2].ToString().Trim(),                                // Lot
                    dt1.Rows[i][3].ToString().Trim(),                                // макат
                    dt1.Rows[i][9].ToString().Trim(),                                // QA_Worker
                    dt1.Rows[i][10].ToString().Trim()                               // QA_Resume

                    //dt1.Rows[i][11].ToString().Trim(),                               // name qa worker
                    //dt1.Rows[i][12].ToString().Trim()                                // second name qa worker
                    );

                }
            //}

            //catch { }
          //  DGV_Archaiv_Items.ClearSelection();
        //    dataGridView1.DataSource = dt1;
            DGV_Archaiv_Items.RowTemplate.Height = 80;


            DGV_Archaiv_Items.ClearSelection();

           // DGV_Archaiv_Items.CurrentCell = null;


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

        private void DGV_Archaiv_Items_SelectionChanged(object sender, EventArgs e)
        {


            if (DGV_Archaiv_Items.SelectedRows.Count > 0 && Permitions == true)

            {
                panel1.Visible = true; btn_Call_QA.Visible = false;
            }
            else { panel1.Visible = false; btn_Call_QA.Visible = true; }

                foreach (DataGridViewRow row in DGV_Archaiv_Items.SelectedRows)
            {


                Lot_for_Print =     row.Cells[8].Value.ToString().Trim();                              // Lot
                Makat_for_Print =   row.Cells[9].Value.ToString().Trim();                              // макат
                Note_for_Print =    row.Cells[11].Value.ToString().Trim();                             // QA_Resume
                Operation_ID =      row.Cells[0].Value.ToString().Trim();                              // Operation ID
                QA_Status =         row.Cells[7].Value.ToString().Trim();                              //QA status     PASS / REJECT
                                                                                                       //     Station =           row.Cells[2].Value.ToString().Trim();                              //Station





                //    if (row.Cells[12].Value.ToString().Trim() != "")
                //    { QA_Worker = "QA checker :   " + row.Cells[12].Value.ToString().Trim() + " " + row.Cells[13].Value.ToString().Trim(); } else { QA_Worker = ""; }
                QA_Resume = row.Cells[11].Value.ToString();
            }
            if (QA_Status == "0") { CBox_Pass.Checked = false; CBox_Reject.Checked = false; QA_Status_Print = ""; }
                if (QA_Status == "1") { CBox_Pass.Checked = false; CBox_Reject.Checked = true;  QA_Status_Print = "PASS";}

                if (QA_Status == "2") { CBox_Pass.Checked = true; CBox_Reject.Checked = false; QA_Status_Print = "REJECT"; }

                tbox_QA_Note.Text = QA_Resume;
                Last_QA_Worker.Text = QA_Worker;

            if (DGV_Archaiv_Items.SelectedRows.Count == 0) { panel1.Visible = false; btn_Call_QA.Visible = false; }



            try
            {

                foreach (DataGridViewRow row in DGV_Archaiv_Items.Rows)

                    switch (Convert.ToInt32(row.Cells[7].Value))
                    {
                        case 1:
                            row.DefaultCellStyle.BackColor = Color.Tomato; ;
                            break;
                        case 2:
                            row.DefaultCellStyle.BackColor = Color.PaleGreen; ;
                            break;
                        case 3:
                            row.DefaultCellStyle.BackColor = Color.Lavender; ;
                            break;

                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }

            }
            catch { }
        }



        private void QA_Report(int Operation_ID, string QA_Status, string QA_Worker, string QA_Resume)   // Записываем СЕТ в таблицу.
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_JS_QA_Report", conn) { CommandType = CommandType.StoredProcedure };
                    sqlCmd.Parameters.AddWithValue("@Operation_ID", Operation_ID);
                    sqlCmd.Parameters.AddWithValue("@QA_Status", QA_Status);
                    sqlCmd.Parameters.AddWithValue("@QA_Worker", QA_Worker);
                    sqlCmd.Parameters.AddWithValue("@QA_Resume", QA_Resume);


                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                }
        }


        private void Repeated_QA_Call(int Operation_ID)   // Записываем СЕТ в таблицу.
        {
            //using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        conn.Open();
            //        SqlCommand sqlCmd = new SqlCommand("SP_Reapit_QA_Call", conn) { CommandType = CommandType.StoredProcedure };
            //        sqlCmd.Parameters.AddWithValue("@Operation_ID", Operation_ID);
            //        sqlCmd.ExecuteNonQuery();
            //        conn.Close();
            //    }
        }


        private void datePicker_fromDate_ValueChanged_1(object sender, EventArgs e)
        {
            Load_Finished_List();
        }
        private void datePicker_to_Date_ValueChanged_1(object sender, EventArgs e)
        {
            Load_Finished_List();
        }
        private void txt_Lot_TextChanged_1(object sender, EventArgs e)
        {

            BarcodeString = Remove_All_AB_From_Barcode(txt_Lot.Text);
            Load_Finished_List();
        }

        private void checkBox_by_Lot_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox_by_Lot.Checked == false) { txt_Lot.Visible = false; txt_Lot.Text = ""; } else { txt_Lot.Visible = true; txt_Lot.Text = ""; };
        }
        private void checkBox_by_Catalog_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox_by_Catalog.Checked == false) { cb_Station.Visible = false; cb_Station.Text = ""; } else { cb_Station.Visible = true; cb_Station.Text = ""; };
        }
         private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CBox_MultiPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (CBox_Pass.Checked == true) { CBox_Reject.Checked = false;  QA_Status = "2";    }



            //
        }
        private void myCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CBox_Reject.Checked == true) { CBox_Pass.Checked = false; QA_Status = "1";  }




            //
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            //if (QA_Status == "0") { QA_Status_Print = ""; QA_printer = ""; }
            //if (QA_Status == "1") { QA_Status_Print = "REJECT"; QA_printer = Properties.Settings.Default.Printer_QA_Reject; }
            //if (QA_Status == "2") { QA_Status_Print = "PASS"; QA_printer = Properties.Settings.Default.Printer_QA_Pass; }

           // SQL_Jobs Print = new SQL_Jobs();
            QA_Report(Convert.ToInt32(Operation_ID), QA_Status, Properties.Settings.Default.ID_Worker_QA.ToString(), tbox_QA_Note.Text);

            //Print.Print_QA_Lable(Operation_ID, Station, QA_Status_Print, Lot_for_Print, Makat_for_Print, Properties.Settings.Default.ID_Worker_QA.ToString(), tbox_QA_Note.Text, QA_printer);


            Load_Finished_List();



        }







        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked ) { Search_QA_Status = "=  0  or  [QA_Status]  = 3 " ;       Load_Finished_List(); }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) { Search_QA_Status = "=   2"; Load_Finished_List(); }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) { Search_QA_Status = " =  1 or  [QA_Status]  = 3 "; Load_Finished_List(); }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) { Search_QA_Status = " <>  10"; Load_Finished_List(); }
        }

        private void CheckPermition()
        {
            DataTable dt7 = new DataTable();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {

                conn.Open();
                SqlCommand myCmd = new SqlCommand("SP_Wellder_Or_QA", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new SqlParameter("@Worker_ID", Properties.Settings.Default.ID_Worker_QA.ToString()));
                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dt7);
                conn.Close();
            }

            if (dt7.Rows[0][0].ToString().Trim() == "2002") { Permitions = true; } else { Permitions = false; }

        }

        private void btn_Call_QA_Click(object sender, EventArgs e)
        {
            Repeated_QA_Call(Convert.ToInt32(Operation_ID));
            Load_Finished_List();
        }
    }
}
