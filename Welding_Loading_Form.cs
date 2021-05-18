using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace JackShaft_App
{

    public partial class Welding_Loading_Form : Form
    {
        string strFilePath;
        string BarCodeString;
        string BarCodeString1;
        string Query;
        string Query_1;
        int Load_Mode = 0;
        List<string> list = new List<string>();
        int RB_Selector = 0;
        string StationSelector;
        int WhoReportedMax_APP_Or_LN;
        string Temp_Ind11;
        Welding_SQL_Jobs W_SQL_Jobs  =  new Welding_SQL_Jobs();

        public Welding_Loading_Form()

        {
            InitializeComponent();
            GetStationList();
            Load_Day_Order_List_new();



            DGV_Set.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_Set.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_Set.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_Set.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
            DGV_Set.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DayList_View.Columns[0].Width = 170;
            DayList_View.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DayList_View.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DayList_View.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            DayList_View.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 12, FontStyle.Regular);
            DayList_View.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);
            DayList_View.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);
            DayList_View.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);
            DayList_View.Columns[4].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
            DayList_View.Columns[5].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
            DayList_View.Columns[6].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
            DayList_View.RowHeadersVisible = false;
            DGV_Set.RowHeadersVisible = false;
            BarCodeScan.Select();
            Load_Mode = 0;
            btn_ReportToLN.Visible = false;
            label1.Text = "Operator:   " + Properties.Settings.Default.Worker_Name.ToString();
            lbl_StationNr.Text = Properties.Settings.Default.DefaultStationNr;
        }

         private void button4_Click(object sender, EventArgs e)                      // Кнопка  -  "Reload".  Перезагружаем\обновляем  дэй лист заново
        {

            W_SQL_Jobs.Welding_CopyFromBaan();
            button4.Enabled = false;
            Load_Mode = 0;
            DayList_View.DataSource = null;
            DayList_View.Rows.Clear();
            Load_Day_Order_List_new();
        }








         //private void Welding_CopyFromBaan()   // Copy data from BaanDB to local table
         //       {
         //               button4.Enabled = false;

         //           using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))

         //               if (conn.State == ConnectionState.Closed)
         //               {
         //                   conn.Open();
         //                   SqlCommand sqlCmd = new SqlCommand("SP_Welding_Copy_From_Baan", conn) { CommandType = CommandType.StoredProcedure };
         //                   sqlCmd.CommandTimeout = 300;
         //                   sqlCmd.ExecuteNonQuery();
         //                   conn.Close();
         //               }

         //       }



         private void GetStationList()                                               // Получаем лист станций для выбора.
                {
                    StationComboBax.Items.Add("All station");
                    SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);
                    SqlCommand sqlCmd = new SqlCommand(" SELECT  DISTINCT [Welding_Station] FROM    T_Welding_Baandb_Copy  ", conn);
                    conn.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        StationComboBax.Items.Add(sqlReader["Welding_Station"].ToString());
                    }
                    sqlReader.Close();
                    StationComboBax.SelectedItem = "All station";
                }


        private string Last_Update_fromBaan()
        {
            string LastUpdateTime;
            Query_1 = " Select  * from [T_Welding_LastUpdateLog] where Id = 1";

            using (SqlConnection conn1 = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            using (SqlCommand command1 = new SqlCommand(Query_1, conn1))
            {
                conn1.Open();
                DataTable dt1 = new DataTable();
                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
                adapter1.Fill(dt1);
                conn1.Close();
                LastUpdateTime = string.Format("{0:HH:mm dd/MM}", Convert.ToDateTime(dt1.Rows[0][1]));
            }
            return LastUpdateTime;
        }




         private void Load_Day_Order_List_new()                                      // загружаем лист с заданиями на день (или общий лист) и на его основании строим ДЭЙ ЛИСТ
                {
                    // в зависимости от статуса  лист  меняет цвета  и перестраивается
                    // в зависимости от количества лист меняет цвет
                    DayList_View.Rows.Clear();
                    DayList_View.Refresh();
                    switch (Load_Mode)
                    {
                        case 0:
                            if (RB_Selector == 0)
                            { Query = " Select  * from V_Welding_Data_From_BaanDB_Final_1  " + StationSelector + "  order by CONVERT(DATETIME,finish_date,114) asc"; }
                            break;
                        case 1:
                            Query = " Select  * from V_Welding_Data_From_BaanDB_Final_1 where Makat LIKE '%" + Remove_All_AB_From_Barcode(BarCodeString) + "%'  order by CONVERT(DATETIME,finish_date,114) asc";
                            break;
                        default:
                            Query = " Select  * from V_Welding_Data_From_BaanDB_Final_1 order by CONVERT(DATETIME,finish_date,114) asc";
                            break;
                    }
                    using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
                    using (SqlCommand command = new SqlCommand(Query, conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();

                try
                {

                    button4.Text = $"Day list ({dt.Rows.Count.ToString()}  items)   updated  at  {Last_Update_fromBaan()} ";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Concat = dt.Rows[i][1].ToString().Trim()       // лот
                        + "\r\n" + dt.Rows[i][2].ToString().Trim();           // макат
                        string Picture_Link = null;
                        Picture_Link = Reformating_ConnectionString(dt.Rows[i][4].ToString().Trim());
                        Image Picture = null;
                        try { Picture = Image.FromFile(Picture_Link); }
                        catch { Picture = Image.FromFile("C:\\NoImage.png"); }
                        if (String.IsNullOrEmpty(dt.Rows[i][10].ToString().Trim())) { Temp_Ind11 = "0"; }
                        else
                        {
                            Temp_Ind11 = dt.Rows[i][10].ToString().Trim();
                        }

                        double totalHours;
                        double TimeForOnePCS = Convert.ToDouble(dt.Rows[i][15]) / Convert.ToInt32(dt.Rows[i][3]);
                        int RemainQTY = Convert.ToInt32(dt.Rows[i][3]) - Convert.ToInt32(Temp_Ind11);
                        if (RemainQTY != 0) { totalHours = TimeForOnePCS * RemainQTY; }
                        else { totalHours = 0; }
                        TimeSpan time = TimeSpan.FromHours(totalHours * 1.25);


                        // строим на основе основного грида новый для  работе в форме
                        DayList_View.Rows.Add(
                                    Picture,                                                          // picture
                                    Concat,                                                           // Paka/Maket
                                    string.Format("{0:dd/MM/yy}", Convert.ToDateTime(dt.Rows[i][5])), // Only date
                                    dt.Rows[i][3].ToString().Trim(),                                  // QTY
                                    dt.Rows[i][7].ToString().Trim(),                                  // in procces
                                    dt.Rows[i][4].ToString().Trim(),                                  // Image Link
                                    dt.Rows[i][6].ToString().Trim(),                                  // PakaStatus
                                    dt.Rows[i][8].ToString().Trim(),                                  // If need QA
                                    dt.Rows[i][1].ToString().Trim(),                                  // Lot
                                    dt.Rows[i][2].ToString().Trim(),                                  // Makat
                                    dt.Rows[i][0].ToString().Trim(),                                  // Station
                                    Temp_Ind11,
                                    dt.Rows[i][9].ToString().Trim(),                                  // Mahsan
                                    dt.Rows[i][11].ToString().Trim(),                                 // Description
                                    dt.Rows[i][12].ToString().Trim(),                                 // POU
                                    dt.Rows[i][13].ToString().Trim(),                                 // Task
                                    dt.Rows[i][14].ToString().Trim(),                                 // Opr
                                                                                                      // dt.Rows[i][15].ToString().Trim()
                                    time.ToString("dd'd 'hh'h 'mm'm'")
                                    );
                    }
                    DayList_View.ClearSelection();
                    DayList_View.Columns[0].ReadOnly = true;
                    Load_Mode = 0;

                }
                catch { }
            }
                }
         private string Reformating_ConnectionString(string OriginalString)          // преобразовываем линк  в нормальный ПС читаемый  формат
                {
                    if (OriginalString == "") { strFilePath = "C:\\NoImage.png"; }
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


        private int Check_Discrepancy(int ReportQTY, string Lot_1)  // загружаем
        {
           
            DataTable dt25 = new DataTable();
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {
                conn.Open();
                SqlCommand myCmd = new SqlCommand("SP_Welding_Check_if_Allow_Report", conn);
                myCmd.Parameters.AddWithValue("@ReportQTY", ReportQTY);
                myCmd.Parameters.AddWithValue("@Lot", Lot_1);
                myCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dt25);
                conn.Close();
            }
            return Convert.ToInt16(dt25.Rows[0][0]);
        }


        private void ReportSet( string WeldingStation,string WorkerID, string Lot, string Makat, string Report_QTY,  string Picture, string QA_Status, string Warehouse, string Task, string Opr)   // Записываем СЕТ в таблицу.
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_Welding_Report", conn) { CommandType = CommandType.StoredProcedure };
                    sqlCmd.Parameters.AddWithValue("@WeldingStation", WeldingStation);
                    sqlCmd.Parameters.AddWithValue("@WorkerID", WorkerID);
                    sqlCmd.Parameters.AddWithValue("@Lot", Lot);
                    sqlCmd.Parameters.AddWithValue("@Makat", Makat);
                    sqlCmd.Parameters.AddWithValue("@Value", Report_QTY);
                    sqlCmd.Parameters.AddWithValue("@Image_link", Picture);
                    sqlCmd.Parameters.AddWithValue("@QA_Status", QA_Status);
                    sqlCmd.Parameters.AddWithValue("@Warehouse", Warehouse);
                    sqlCmd.Parameters.AddWithValue("@Task", Task);
                    sqlCmd.Parameters.AddWithValue("@Opr", Opr);
                    DataTable dt7 = new DataTable();
                    SqlDataAdapter da7 = new SqlDataAdapter(sqlCmd);
                    da7.Fill(dt7);
                    conn.Close();

                        Bod XML_BOD = new Bod();
                        XML_BOD.Create_Bod(Properties.Settings.Default.XML_Template.Trim(),
                            Properties.Settings.Default.New_XML_Destination.Trim(),
                            Lot.Trim(),
                            Opr.ToString(),
                            Task.Trim(),
                            dt7.Rows[0][0].ToString());
                }
        }
        private void SendReportToLN()
        {
           //  SQL_Jobs Z = new SQL_Jobs();
            if (DGV_Set.RowCount > 0)
            {
                for (int i = 0; i < DGV_Set.RowCount; i++)
                {
                    ReportSet(
                    DGV_Set.Rows[i].Cells[7].Value.ToString().Trim(), //  Station
                    Properties.Settings.Default.ID_Worker.ToString().Trim(),             //  ID_Worker
                    DGV_Set.Rows[i].Cells[3].Value.ToString().Trim(),                    //  Lot
                    DGV_Set.Rows[i].Cells[4].Value.ToString().Trim(),                    //  Makat
                    DGV_Set.Rows[i].Cells[2].Value.ToString(),                           //  Value
                    DGV_Set.Rows[i].Cells[5].Value.ToString().Trim(),                    //  IMAGE PATH
                    "0",                                                                 //  QA_Status
                    DGV_Set.Rows[i].Cells[8].Value.ToString().Trim(),                   // Mahsan
                    DGV_Set.Rows[i].Cells[11].Value.ToString().Trim(),                   // Task
                    DGV_Set.Rows[i].Cells[12].Value.ToString().Trim());                   // Opr


                    W_SQL_Jobs.Print_Lable(
                    DGV_Set.Rows[i].Cells[7].Value.ToString().Trim(),                    // STATION
                    Properties.Settings.Default.ID_Worker.ToString().Trim(),             // WORKER id
                    DGV_Set.Rows[i].Cells[3].Value.ToString().Trim(),                    // Lot
                    DGV_Set.Rows[i].Cells[4].Value.ToString().Trim(),                    // Makat
                    DGV_Set.Rows[i].Cells[2].Value.ToString(),                           // Value
                    DGV_Set.Rows[i].Cells[5].Value.ToString().Trim(),                    // IMAGE PATH
                    DGV_Set.Rows[i].Cells[8].Value.ToString().Trim(),                    // Mahsan
                    DGV_Set.Rows[i].Cells[9].Value.ToString().Trim(),                    // DISCRIPTION
                    DGV_Set.Rows[i].Cells[10].Value.ToString().Trim(),                   // POU
                    "Printer_ABC",1);                                                   // Printer
                }
                DGV_Set.DataSource = null;
                DGV_Set.Rows.Clear();
                DayList_View.DataSource = null;
                DayList_View.Rows.Clear();
                Load_Day_Order_List_new();
            }
        }


        private void DGV_Set_DoubleClick(object sender, EventArgs e)                // Двойной щелчек по  детале    -  удаляем  деталь из списка
        {
            try
            {
                DGV_Set.Rows.RemoveAt(DGV_Set.SelectedCells[0].RowIndex);
            }
            catch { }
        }



        private void DGV_Set_CellClick(object sender, DataGridViewCellEventArgs e)  // Двойной щелчек на СЕТ (на поле колличества) вызов клавы
        {
            if (DGV_Set.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
            {
                if (DGV_Set.CurrentCell != null && DGV_Set.CurrentCell.Value != null)
                {
                    Properties.Settings.Default.Enter_Value = DGV_Set.CurrentCell.Value.ToString();
                    Form Key_Form = new KeyPad(e.RowIndex, DGV_Set.CurrentCell.Value.ToString(), Convert.ToInt16(DGV_Set.Rows[e.RowIndex].Cells[6].Value));
                    Key_Form.ShowDialog(this);
                    DGV_Set.CurrentCell.Value = Properties.Settings.Default.Enter_Value;
                }
            }
            DGV_Set.ClearSelection();
            DGV_Set.Columns[0].ReadOnly = true;
            Properties.Settings.Default.OMS_Image = DayList_View.CurrentRow.Cells[2].Value.ToString().Trim();
            btn_ReportToLN.Visible = true;

        }


        private void OpenKeyboard()  // Двойной щелчек на СЕТ (на поле колличества) вызов клавы
        {
           // //if (DGV_Set.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
           // //{
           //     if (DGV_Set.CurrentCell != null && DGV_Set.CurrentCell.Value != null)

           //     {
           //         Properties.Settings.Default.Enter_Value = DGV_Set.CurrentCell.Value.ToString();
           //         Form Key_Form = new KeyPad(1, DGV_Set.CurrentCell.Value.ToString(), Convert.ToInt16(DGV_Set.Rows[0].Cells[6].Value));
           //         Key_Form.ShowDialog(this);

           //         DGV_Set.CurrentCell.Value = Properties.Settings.Default.Enter_Value;
           //     }
           // //}
           // DGV_Set.ClearSelection();
           // DGV_Set.Columns[0].ReadOnly = true;

           //// Properties.Settings.Default.OMS_Image = DayList_View.CurrentRow.Cells[2].Value.ToString().Trim();



        }
        private string Remove_All_AB_From_Barcode(string Barcode)                   // В поиск вставляем заглавные и прописные буквы
        {
            string Numbers = Regex.Replace(Barcode, @"^[A-Za-z]+", "");
            return Numbers;
        }
        private void DGV_Set_SelectionChanged(object sender, EventArgs e)           //  СЕТ  запрещаем выделение  строки
        {
            try {
                if (DGV_Set.Columns[DGV_Set.CurrentCell.ColumnIndex].Name == Lot.Name)
                    DGV_Set.CurrentCell.Selected = false;
            }
            catch { }
        }
        private void BarCodeScan_TextChanged(object sender, EventArgs e)            // Cканируем код, запускаем два таймера
        {
            if (BarCodeScan.Text != "")
            {   timer1.Enabled = true;
                timer2.Enabled = true;
                BarCodeString = BarCodeScan.Text;
                BarCodeString1 = BarCodeString;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)                        // Даем время  для полного сканирования и обрезаем лишнее
        {
            if (BarCodeScan.Text != "")
            {   BarCodeString = BarCodeScan.Text;
                timer1.Enabled = false;
                int commaPos = BarCodeString.IndexOf('/');
                if (commaPos != -1)
                BarCodeString = BarCodeString.Substring(0, commaPos);
                Load_Mode = 1;
                DayList_View.DataSource = null;
                DayList_View.Rows.Clear();
                Load_Day_Order_List_new();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)                        // Очищаем поля для повторного сканирования
        {
            BarCodeScan.Text = "";
            BarCodeString = "";
            timer2.Enabled = false;
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)  // Подкрашиваем серым ЛОТы которые уже в работе
        {
                foreach (DataGridViewRow row in DayList_View.Rows)

                       if (Convert.ToInt32(row.Cells[3].Value) <= Convert.ToInt32(row.Cells[4].Value) || Convert.ToInt32(row.Cells[3].Value) <= Convert.ToInt32(row.Cells[11].Value))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        DayList_View.Columns[2].DefaultCellStyle.BackColor = Color.White;
                        DayList_View.Columns[0].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }

            DayList_View.ClearSelection();
            DayList_View.Columns[0].ReadOnly = true;

            for (int i = 0; i < DayList_View.Rows.Count; i++)
            {
                DayList_View.Rows[i].Cells[0].Style.BackColor = Color.White;
            }
        }



        private void btn_ADD_NoLotMakat_Click(object sender, EventArgs e)           // посылаем в сет макаты у которых нет лотов
        {
            if (!string.IsNullOrEmpty(txt_NoLot_Mak_Val.Text) && !string.IsNullOrEmpty(txt_NoLot_Makat.Text))

            {
                Image pictureBox1 = Image.FromFile("NoImage.png");
                DGV_Set.Rows.Add(pictureBox1, txt_NoLot_Makat.Text, Convert.ToInt16(txt_NoLot_Mak_Val.Text), "0", txt_NoLot_Makat.Text, "Unknown", "NoImage.png", lbl_StationNr.Text,  "0",     (Convert.ToInt16(txt_NoLot_Mak_Val.Text)));
                DGV_Set.RowHeadersVisible = false;
                DGV_Set.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_Set.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
                DGV_Set.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 20, FontStyle.Bold);
                DayList_View.ClearSelection();
                DayList_View.Columns[0].ReadOnly = true;
                DGV_Set.ClearSelection();
                DGV_Set.Columns[0].ReadOnly = true;
            }
            txt_NoLot_Makat.Text = "";
            txt_NoLot_Mak_Val.Text = "";
        }

        private void txt_NoLot_Mak_Val_KeyPress(object sender, KeyPressEventArgs e) // в поле количество вводим только цифры
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }


        private void StationComboBax_SelectedIndexChanged(object sender, EventArgs e)  //  Выбираеь комбобоксом станцию
        {
            DayList_View.Rows.Clear();
            DayList_View.Refresh();
            if (StationComboBax.SelectedItem.ToString() == "All station")
            { StationSelector = ""; }
            else { StationSelector = " where [Welding_Station] = '" + StationComboBax.SelectedItem.ToString() + "'" ;}
            Load_Day_Order_List_new();
        }


        private void btn_Back_Click(object sender, EventArgs e)                        // Закрываем форму
        {
            Properties.Settings.Default.DefaultStationNr = lbl_StationNr.Text;
            Properties.Settings.Default.Save();
            this.Close(); }

        private void btn_ReportToLN_Click(object sender, EventArgs e)
        {
           if (Check_Discrepancy(Convert.ToInt16(DGV_Set.Rows[0].Cells[2].Value), DGV_Set.Rows[0].Cells[3].Value.ToString().Trim()) == 1)
            {
                SendReportToLN();
                Load_Day_Order_List_new();
            }
            else
            {
                MessageBox.Show("Report rejected. LN - Application  paka discrepancy");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form Worker_ID = new Worker_ID(1);
            Worker_ID.ShowDialog(this);
        }

        private void lbl_StationNr_DoubleClick(object sender, EventArgs e)
        {
            //Form Select_Station = new Select_Station();
            //Select_Station.ShowDialog(this);
            //lbl_StationNr.Text = Properties.Settings.Default.DefaultStationNr;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_Set.Rows.Count == 0)

            {
                bool IfLotRepeat = false;
                foreach (DataGridViewRow row in DGV_Set.Rows) //We check if  DGW already contain this PAKA, not allowed put to DGW few same PAKA rows
                {
                    if (row.Cells[1].Value.ToString().Contains(DayList_View.CurrentRow.Cells[1].Value.ToString().Trim())
                        )
                    { IfLotRepeat = true; }
                }
                if ((Convert.ToInt32(DayList_View.CurrentRow.Cells[3].Value) > Convert.ToInt32(DayList_View.CurrentRow.Cells[4].Value) && IfLotRepeat == false)
                      &&
                      (Convert.ToInt32(DayList_View.CurrentRow.Cells[3].Value) > Convert.ToInt32(DayList_View.CurrentRow.Cells[11].Value) && IfLotRepeat == false))   //Проверяем  если в процессе  колличество меньше чем требуется  Должны ли мы делать еще?
                {
                    if (Convert.ToInt32(DayList_View.CurrentRow.Cells[4].Value) > Convert.ToInt32(DayList_View.CurrentRow.Cells[11].Value))
                    {
                        WhoReportedMax_APP_Or_LN = Convert.ToInt32(DayList_View.CurrentRow.Cells[4].Value);
                    }
                    else { WhoReportedMax_APP_Or_LN = Convert.ToInt32(DayList_View.CurrentRow.Cells[11].Value); }
                    Image pictureBox1 = (Image)DayList_View.CurrentRow.Cells[0].Value;
                    string txt_Lot = DayList_View.CurrentRow.Cells[8].Value.ToString().Trim();
                    string txt_Makat = DayList_View.CurrentRow.Cells[9].Value.ToString().Trim();
                    string LotMakat = txt_Lot + "\r\n" + txt_Makat;  // три строки в одну ячейку
                    string txt_Count = DayList_View.CurrentRow.Cells[3].Value.ToString().Trim();
                    string txt_in_Process = DayList_View.CurrentRow.Cells[4].Value.ToString().Trim();
                    string ImagePath = DayList_View.CurrentRow.Cells[5].Value.ToString().Trim();
                    string WeldingStation = DayList_View.CurrentRow.Cells[10].Value.ToString().Trim();
                    string Warehouse1 = DayList_View.CurrentRow.Cells[12].Value.ToString().Trim();
                    string Discription = DayList_View.CurrentRow.Cells[13].Value.ToString().Trim();
                    string POU = DayList_View.CurrentRow.Cells[14].Value.ToString().Trim();
                    string Task = DayList_View.CurrentRow.Cells[15].Value.ToString().Trim();
                    string Opr = DayList_View.CurrentRow.Cells[16].Value.ToString().Trim();
                    DGV_Set.Rows.Add(pictureBox1, LotMakat, Convert.ToInt16(txt_Count) - WhoReportedMax_APP_Or_LN, txt_Lot, txt_Makat, ImagePath, Convert.ToInt16(txt_Count) - WhoReportedMax_APP_Or_LN, WeldingStation, Warehouse1, Discription, POU, Task, Opr);
                    DGV_Set.RowHeadersVisible = false;
                    DGV_Set.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_Set.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
                    DGV_Set.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 20, FontStyle.Bold);
                    DayList_View.ClearSelection();
                    DayList_View.Columns[0].ReadOnly = true;
                    DGV_Set.ClearSelection();
                    DGV_Set.Columns[0].ReadOnly = true;
                }
                IfLotRepeat = false;
                btn_ReportToLN.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenKeyboard();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            Load_Day_Order_List_new();
        }
    }
}


