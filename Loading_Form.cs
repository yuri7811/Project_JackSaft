using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace JackShaft_App
{

    public partial class Loading_Form : Form
    {

        SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);

        string strFilePath;
        string BarCodeString;
        string BarCodeString1;
        string Query;
        int Load_Mode = 0;
        List<string> list = new List<string>();
        int RB_Selector = 0;
        string StationSelector;
        int WhoReportedMax_APP_Or_LN;
        string Temp_Ind11;
        int counter;
        int[] Checked_Station = new int[21];
        int Distance = 2;
        int Button_Axis_X = 1200;
        int Divuah_Fild_Heith = 0;
        int delay = 0;
        int Blanks_for_Print;
        string Worker;
        string Lot_for_Report;
        string Makat_for_Report;
        string QTY_for_Report;
        string ImageLink_2;
        string DISCRIPTION;


        string Description ;
        string ImageLink_3;
        string Task_3;
        string Opr_3;
        string Option_4 ;
        string Option_5;
        string Weight;


        public Loading_Form()

        {
            InitializeComponent();

            Load_Day_Order_List_new();

            DayList_View.Columns[0].Width = 200;
            DayList_View.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

            DayList_View.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            DayList_View.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 12, FontStyle.Regular);

            DayList_View.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
            DayList_View.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);
            DayList_View.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);
            DayList_View.Columns[4].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);
            DayList_View.Columns[5].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);
            DayList_View.Columns[6].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12, FontStyle.Regular);

            DayList_View.RowHeadersVisible = false;
            BarCodeScan.Select();
            Load_Mode = 0;



             lbl_UserID.Text =  Properties.Settings.Default.ID_Worker.ToString();




        }

        private void button4_Click(object sender, EventArgs e)                      // Кнопка  -  "Reload".  Перезагружаем\обновляем  дэй лист заново
        {

            CopyFromBaan();
            Load_Mode = 0;
            DayList_View.DataSource = null;
            DayList_View.Rows.Clear();
            Load_Day_Order_List_new();

        }
         private void CopyFromBaan()   // Copy data from BaanDB to local table
                {
                        button400.Enabled = false;

                    using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                            SqlCommand sqlCmd = new SqlCommand("SP_JS_Copy_From_Baan", conn) { CommandType = CommandType.StoredProcedure };
                            sqlCmd.CommandTimeout = 300;
                            sqlCmd.ExecuteNonQuery();
                            conn.Close();
                        }

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
                            { Query = " Select  * from V_JS_Data_From_BaanDB_1  " + StationSelector + "  "; }

                  //  V_Welding_Data_From_BaanDB_Final_1

                            break;
                        case 1:
                            Query = " Select  * from V_JS_Data_From_BaanDB_1 where Makat LIKE '%" + Remove_All_AB_From_Barcode(BarCodeString) + "%'  ";
                            break;
                        default:
                            Query = " Select  * from V_JS_Data_From_BaanDB_1 ";


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
                        dataGridView2.DataSource = dt;
                        button400.Text = $"Day list ({dt.Rows.Count.ToString()}  items)";

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Concat = dt.Rows[i][1].ToString().Trim()       // лот
                            + "\r\n" + dt.Rows[i][2].ToString().Trim()

                            + "\r\n" + dt.Rows[i][11].ToString().Trim()

                            + "\r\n" + dt.Rows[i][7].ToString().Trim() + "/" + dt.Rows[i][3].ToString().Trim() + "/" + dt.Rows[i][16].ToString().Trim()
                            + "\r\n" + Convert.ToDateTime(dt.Rows[i][15]).ToString("dd/MM/yy") ;           // макат


                            string Picture_Link = null;
                            Picture_Link = Reformating_ConnectionString(dt.Rows[i][4].ToString().Trim());

                            Image Picture = null;
                            try { Picture = Image.FromFile(Picture_Link); }

                            catch  { Picture = Image.FromFile("NoImage.png");    }

                    if (String.IsNullOrEmpty(dt.Rows[i][10].ToString().Trim())) { Temp_Ind11 = "0";} else { Temp_Ind11 = dt.Rows[i][10].ToString().Trim();
            }


                            // строим на основе основного грида новый для  работе в форме
                    DayList_View.Rows.Add(
                                Picture,                                                          // picture
                                Concat,                                                           // concat
                                dt.Rows[i][1].ToString().Trim(),                                  // лот
                                dt.Rows[i][2].ToString().Trim(),                                  // макат
                                dt.Rows[i][11].ToString().Trim(),                                 // Description
                                dt.Rows[i][7].ToString().Trim(),                                  // Paka QTY 
                                dt.Rows[i][3].ToString().Trim(),                                  // LN reported
                                1,
                                dt.Rows[i][4].ToString().Trim(),                                  // Image Link      1
                                dt.Rows[i][13].ToString().Trim(),                                 // Task
                                dt.Rows[i][14].ToString().Trim(),                                 // Operation
                                dt.Rows[i][8].ToString().Trim(),                                  // Option_1
                                dt.Rows[i][8].ToString().Trim(),                                  // Option_2
                                dt.Rows[i][8].ToString().Trim(),                                  // Option_3
                                dt.Rows[i][8].ToString().Trim(),                                  // Option_4
                                dt.Rows[i][8].ToString().Trim(),                                  // Option_5
                                dt.Rows[i][15].ToString().Trim(),                                 // Daste
                                dt.Rows[i][16].ToString().Trim(),                                 // App QTY reported
                                dt.Rows[i][17].ToString().Trim()                                  // Weith

                                );
                        }
                            DayList_View.ClearSelection();
                            DayList_View.Columns[0].ReadOnly = true;
                            Load_Mode = 0;
                    }
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
                        string AfterConnection = @"\\fbhczcapp1\SFC_CLOUD\Images\tcibd001" + AfterChanging;         // Test new server  
                        //string AfterConnection = @"\\gfbhcapp1\\tcibd001$\" + AfterChanging;

                string AfterSpaseDeleting = AfterConnection.Replace(" ", ""); ;
                        strFilePath = AfterSpaseDeleting;



                    }
                    return strFilePath;

                }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)  // Двойной щелчек  -  перебрасываем  деталь из одного грида (ДЭЙ ЛИСТ)  в другой (СЕТ)
        {


                if (  (Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[5].Value.ToString())) > Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[17].Value.ToString()))
                              &&
                    (Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[5].Value.ToString())) > Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[6].Value.ToString()))))) //Проверяем  если в процессе  колличество меньше чем требуется  Должны ли мы делать еще?
            {


                pictureBox1.Image = (Image)DayList_View.CurrentRow.Cells[0].Value;
                lbl_Lot.Text = DayList_View.CurrentRow.Cells[2].Value.ToString().Trim();
                lbl_Makat.Text = DayList_View.CurrentRow.Cells[3].Value.ToString().Trim();

                if (Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[17].Value.ToString().Trim())) ==  Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[6].Value.ToString().Trim())) )
                { txt_QTY.Text = ((Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[5].Value.ToString()))) - (Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[6].Value.ToString())))).ToString(); }

                if (Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[17].Value.ToString().Trim())) == 0 && Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[6].Value.ToString().Trim())) == 0)
                { txt_QTY.Text = DayList_View.CurrentRow.Cells[5].Value.ToString().Trim(); }
                if (Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[17].Value.ToString().Trim())) > Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[6].Value.ToString().Trim())))
                { txt_QTY.Text = ((Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[5].Value.ToString()))) - (Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[17].Value.ToString())))).ToString(); }
                if (Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[17].Value.ToString().Trim())) < Convert.ToUInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[6].Value.ToString().Trim())))
                { txt_QTY.Text = ((Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[5].Value.ToString()))) - (Convert.ToInt32(ZeroIfEmpty(DayList_View.CurrentRow.Cells[6].Value.ToString())))).ToString(); }


                Description = DayList_View.CurrentRow.Cells[4].Value.ToString().Trim();
                ImageLink_3 = DayList_View.CurrentRow.Cells[8].Value.ToString().Trim();
                Task_3 = DayList_View.CurrentRow.Cells[9].Value.ToString().Trim();
                Opr_3 = DayList_View.CurrentRow.Cells[10].Value.ToString().Trim();
                Option_4 = DayList_View.CurrentRow.Cells[11].Value.ToString().Trim();
                Option_5 = DayList_View.CurrentRow.Cells[12].Value.ToString().Trim();
                Weight = DayList_View.CurrentRow.Cells[18].Value.ToString().Trim(); 






                HideButtons();
                pictureBox3.Image = null;
                lbl_Lot.Visible = true;
                lbl_Makat.Visible = true;
                txt_QTY.Visible = true;
                button22.Visible = true;
                cb_Mukti_Print.Visible = true;

                RefreshImageGrid();
                Scren_Activate();
            }
            else
            {
                HideButtons();
                pictureBox3.Image = null;
                lbl_Lot.Visible = false;
                lbl_Makat.Visible = false;
                txt_QTY.Visible = false;
                button22.Visible = false;
                cb_Mukti_Print.Visible = false;

            }


        }



        private string Remove_All_AB_From_Barcode(string Barcode)                   // В поиск вставляем заглавные и прописные буквы
        {
            string Numbers = Regex.Replace(Barcode, @"^[A-Za-z]+", "");
            return Numbers;
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





        private void txt_NoLot_Mak_Val_KeyPress(object sender, KeyPressEventArgs e) // в поле количество вводим только цифры
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }




        private void btn_Back_Click(object sender, EventArgs e)                        // Закрываем форму
        {


            Properties.Settings.Default.Save();


            this.Close(); }




        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();

            Form Worker_ID = new Worker_ID(1);
            Worker_ID.ShowDialog(this);

        }

        private void txt_QTY_MouseClick(object sender, MouseEventArgs e)
        {
                    Properties.Settings.Default.Enter_Value = txt_QTY.Text;
                    Form Key_Form = new KeyPad(0, txt_QTY.Text, Convert.ToInt16(DayList_View.CurrentRow.Cells[5].Value.ToString().Trim()));
                    Key_Form.ShowDialog(this);
                    txt_QTY.Text = Properties.Settings.Default.Enter_Value;
                    Properties.Settings.Default.Save();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button_Menagement()
        {


            button4.Location = new Point(Button_Axis_X, 10);
            button4.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button2.Location = new Point(Button_Axis_X, button4.Height + button4.Location.Y + Distance);
            button2.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button3.Location = new Point(Button_Axis_X, button2.Height + button2.Location.Y + Distance);
            button3.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button1.Location = new Point(Button_Axis_X, button3.Height + button3.Location.Y + Distance);
            button1.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button5.Location = new Point(Button_Axis_X, button400.Height + button400.Location.Y + Distance);
            button5.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button6.Location = new Point(Button_Axis_X, button5.Height + button5.Location.Y + Distance);
            button6.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button7.Location = new Point(Button_Axis_X, button6.Height + button6.Location.Y + Distance);
            button7.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button8.Location = new Point(Button_Axis_X, button7.Height + button7.Location.Y + Distance);
            button8.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith; ;
            button9.Location = new Point(Button_Axis_X, button8.Height + button8.Location.Y + Distance);
            button9.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button10.Location = new Point(Button_Axis_X, button9.Height + button9.Location.Y + Distance);
            button10.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button11.Location = new Point(Button_Axis_X, button10.Height + button10.Location.Y + Distance);
            button11.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button12.Location = new Point(Button_Axis_X, button11.Height + button11.Location.Y + Distance);
            button12.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button13.Location = new Point(Button_Axis_X, button12.Height + button12.Location.Y + Distance);
            button13.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button14.Location = new Point(Button_Axis_X, button13.Height + button13.Location.Y + Distance);
            button14.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith; ;
            button15.Location = new Point(Button_Axis_X, button14.Height + button14.Location.Y + Distance);
            button15.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button16.Location = new Point(Button_Axis_X, button15.Height + button15.Location.Y + Distance);
            button16.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button17.Location = new Point(Button_Axis_X, button16.Height + button16.Location.Y + Distance);
            button17.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button18.Location = new Point(Button_Axis_X, button17.Height + button17.Location.Y + Distance);
            button18.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button19.Location = new Point(Button_Axis_X, button18.Height + button18.Location.Y + Distance);
            button19.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button20.Location = new Point(Button_Axis_X, button19.Height + button19.Location.Y + Distance);
            button20.Height = (flowLayoutPanel1.Height / (counter + 2)) - Divuah_Fild_Heith;
            button21.Height = (flowLayoutPanel1.Height / (counter + 2)) - 20;
            button21.Location = new Point(Button_Axis_X, this.Height - button21.Height - 50);

            if (counter == 0)
            {
                button21.Height = 200;
                button21.Location = new Point(Button_Axis_X, this.Height - button21.Height - 50);
            }


        }



        private void Scren_Activate()
        {
            counter = dgvImages2.Rows.Count;
            for (int a = 1; a <= counter; a++)
            {
                Button myButton = (Button)flowLayoutPanel1.Controls["button" + a];
                myButton.BackgroundImage = null;
                myButton.Visible = true;

            }

            Button_Menagement();

            if (counter == 1) { button1.Focus(); button1.Select(); } else { button1.Focus(); button1.Select(); button21.Visible = false; }
            if (counter == 0) { btn_Back.Visible = true; btn_Back.Focus(); btn_Back.Select(); } //else { btn_Back.Visible = false; }
            button21.Visible = false;
            try
            {
                pictureBox3.Image = Image.FromFile(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[0].Cells[4].Value);
            }
            catch
            {

            }
        }


        void RefreshImageGrid()
        {

            try
            {

                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_JS_IMAGE_View_By_Station", sqlcon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Makat", lbl_Makat.Text.Trim());

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.SelectCommand = sqlCmd;
                DataTable dtblImages = new DataTable();
                sqlda.Fill(dtblImages);
                dgvImages2.DataSource = dtblImages;

                dgvImages2.Columns[0].Visible = false;
                dgvImages2.Columns[1].Visible = false;
                dgvImages2.Columns[2].Visible = false;
                dgvImages2.Columns[3].Visible = false;
                dgvImages2.Columns[4].Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message  Nr_1");
            }
            finally
            {

            }

        }


        private void All_checked()
        {

            int B = 0;

            for (int a = 0; a <= counter; a++)
            {
                B = B + Checked_Station[a];
            }

            if (B == counter) { button21.Visible = true; }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            pictureBox3.Image = null;
            try
            {

                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[0].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();




            button1.BackgroundImage = Image.FromFile( "images.png");
            Checked_Station[1] = 1; All_checked();
            Thread.Sleep(delay);
            if (counter == 1) { button21.Visible = true; button21.Focus(); button21.Select(); } else { button2.Focus(); button2.Select(); button21.Visible = false; };
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            pictureBox3.Image = null;
            try
            {

                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[1].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();


            button2.BackgroundImage = Image.FromFile( "images.png");
            Thread.Sleep(delay);
            Checked_Station[2] = 1; All_checked();
            if (counter == 2) { button21.Focus(); button21.Select(); } else { button3.Focus(); button3.Select(); };
  }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {

                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[2].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();



            button3.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[3] = 1; All_checked();
            if (counter == 3) { button21.Focus(); button21.Select(); } else { button4.Focus(); button4.Select(); };
  }
            catch { }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {

                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[3].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();



            button4.BackgroundImage = Image.FromFile( "images.png");
            Thread.Sleep(delay);
            Checked_Station[4] = 1; All_checked();
            if (counter == 4) { button21.Focus(); button21.Select(); } else { button5.Focus(); button5.Select(); };
  }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[4].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();



            button5.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[5] = 1; All_checked();
            if (counter == 5) { button21.Focus(); button21.Select(); } else { button6.Focus(); button6.Select(); };
 }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[5].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();




            button6.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[6] = 1; All_checked();
            if (counter == 6) { button21.Focus(); button21.Select(); } else { button7.Focus(); button7.Select(); };
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {

                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[6].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();




            button7.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[7] = 1; All_checked();
            if (counter == 7) { button21.Focus(); button21.Select(); } else { button8.Focus(); button8.Select(); };
 }
            catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[7].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();



            button8.BackgroundImage = Image.FromFile( "images.png");
            Thread.Sleep(delay);
            Checked_Station[8] = 1; All_checked();
            if (counter == 8) { button21.Focus(); button21.Select(); } else { button9.Focus(); button9.Select(); };
            }
            catch { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[8].Cells[4].Value, FileMode.Open, FileAccess.Read);
                pictureBox3.Image = Image.FromStream(fs);
                fs.Dispose();



            button9.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[9] = 1; All_checked();
            if (counter == 9) { button21.Focus(); button21.Select(); } else { button10.Focus(); button10.Select(); };

            }
            catch { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;

            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[9].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();



            button10.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[10] = 1; All_checked();
            if (counter == 10) { button21.Focus(); button21.Select(); } else { button11.Focus(); button11.Select(); };
            }
            catch { }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[10].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();

            button11.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[11] = 1; All_checked();
            if (counter == 11) { button21.Focus(); button21.Select(); } else { button12.Focus(); button12.Select(); };
            }
            catch { }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[11].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();


            button12.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[12] = 1; All_checked();
            if (counter == 12) { button21.Focus(); button21.Select(); } else { button13.Focus(); button13.Select(); };
            }
            catch { }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[12].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();

            button13.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[13] = 1; All_checked();
            if (counter == 13) { button21.Focus(); button21.Select(); } else { button14.Focus(); button14.Select(); };
            }
            catch { }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[13].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();


            button14.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[14] = 1; All_checked();
            if (counter == 14) { button21.Focus(); button21.Select(); } else { button15.Focus(); button15.Select(); };
            }
            catch { }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {

                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[14].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();


            button15.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[15] = 1; All_checked();
            if (counter == 15) { button21.Focus(); button21.Select(); } else { button16.Focus(); button16.Select(); };
            }
            catch { }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;

            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[15].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();



            button16.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[16] = 1; All_checked();
            if (counter == 16) { button21.Focus(); button21.Select(); } else { button17.Focus(); button17.Select(); };
            }
            catch { }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[16].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();

            button17.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[17] = 1; All_checked();
            if (counter == 17) { button21.Focus(); button21.Select(); } else { button18.Focus(); button18.Select(); };
            }
            catch { }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[17].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();



            button18.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[18] = 1; All_checked();
            if (counter == 18) { button21.Focus(); button21.Select(); } else { button19.Focus(); button19.Select(); };
            }
            catch { }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[18].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();


            button19.BackgroundImage = Image.FromFile("images.png");
            Thread.Sleep(delay);
            Checked_Station[19] = 1; All_checked();
            if (counter == 19) { button21.Focus(); button21.Select(); } else { button20.Focus(); button20.Select(); };
            }
            catch { }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            try
            {
                var fs = new FileStream(Properties.Settings.Default.OMS_Path + dgvImages2.Rows[19].Cells[4].Value, FileMode.Open, FileAccess.Read);
            pictureBox3.Image = Image.FromStream(fs);
            fs.Dispose();




            button20.BackgroundImage = Image.FromFile( "images.png");
            Thread.Sleep(delay);
            Checked_Station[20] = 1; All_checked();
            if (counter == 20) { button21.Focus(); button21.Select(); } else { button21.Focus(); button21.Select(); };
            }
            catch { }
        }



        private void HideButtons()
        {



            button1.Visible = false; button2.Visible = false; button3.Visible = false; button4.Visible = false; button5.Visible = false;
            button6.Visible = false; button7.Visible = false; button8.Visible = false; button9.Visible = false; button10.Visible = false;
            button11.Visible = false; button12.Visible = false; button13.Visible = false; button14.Visible = false; button15.Visible = false;
            button16.Visible = false; button16.Visible = false; button18.Visible = false; button19.Visible = false; button20.Visible = false;
            button21.Visible = false;

        }


        private int GetAllreadyReported_QTY(string Paka)
        {
            int new_QTY;
            Query = " Select  * from V_JS_Sum_Done where Lot LIKE '%" + Paka.Trim() + "%'  ";

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
                dataGridView2.DataSource = dt;
                new_QTY = Convert.ToInt32(dt.Rows[0][1]);

                return new_QTY;
            }


        }

        private void button21_Click(object sender, EventArgs e)
        {
            string QTY_Accumulated;

            try
            {
                 QTY_Accumulated = (GetAllreadyReported_QTY(lbl_Lot.Text) + Convert.ToInt32(txt_QTY.Text)).ToString();
            }
            catch
            { QTY_Accumulated = txt_QTY.Text; }

            Form Conformation = new Conformation(lbl_Lot.Text, lbl_Makat.Text, txt_QTY.Text, QTY_Accumulated, Task_3, Opr_3, ImageLink_3, lbl_UserID.Text,Weight);
            Conformation.ShowDialog(this);


            Load_Day_Order_List_new();

            HideButtons();
            pictureBox3.Image = null;
            pictureBox1.Image = null;
            txt_QTY.Visible = false;
            button22.Visible = false;
            lbl_Lot.Visible = false;
            lbl_Makat.Visible = false;
            cb_Mukti_Print.Visible = false;
        }


    





            private void button22_Click(object sender, EventArgs e)
        {
            SQL_Jobs BBB = new SQL_Jobs();
            SQL_Jobs CCC = new SQL_Jobs();


            for (int i = 0; i < Convert.ToInt32(txt_QTY.Text); i++)
                {
                    BBB.Print_Lable(
                    Properties.Settings.Default.ID_Worker,                                                              // WORKER id
                    lbl_Lot.Text,                                                      // Lot
                    lbl_Makat.Text.Substring(0,lbl_Makat.Text.Length - 3),                                                  // Makat
                    txt_PCS_Nr.Text,                                                   // Value
                    ImageLink_3,                                                       // IMAGE PATH
                    Description,                                                       // DISCRIPTION
                    Properties.Settings.Default.Printer_Lable,                         // Printer
                    Weight,                                                            // Weight
                    i);                                                                // Printed lable
                }


            for (int i = 0; i < Convert.ToInt32(txt_QTY.Text); i++)

            {
                CCC.Print_Lable(
                Properties.Settings.Default.ID_Worker,                             // WORKER id
                lbl_Lot.Text,                                                      // Lot
                lbl_Makat.Text,                                                    // Makat + HX1
                txt_PCS_Nr.Text,                                                   // Value
                ImageLink_3,                                                       // IMAGE PATH
                Description,                                                       // DISCRIPTION
                Properties.Settings.Default.Printer_Lable,                         // Printer
                Weight,                                                            // Weight
                i + 20);                                                                // Printed lable
            }



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (cb_Mukti_Print.Checked == true) { panel_MultiLable.Visible = true; }
            else { panel_MultiLable.Visible = false; }
        }


        public string ZeroIfEmpty( string s)
        {
            return string.IsNullOrEmpty(s) ? "0" : s;
        }


        private void DayList_View_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {



            foreach (DataGridViewRow row in DayList_View.Rows)




                if (Convert.ToInt32(ZeroIfEmpty(row.Cells[5].Value.ToString())) <= Convert.ToInt32(ZeroIfEmpty(row.Cells[17].Value.ToString()))
                    || Convert.ToInt32(ZeroIfEmpty(row.Cells[5].Value.ToString())) <= Convert.ToInt32(ZeroIfEmpty(row.Cells[6].Value.ToString())))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
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

        private void Txt_QTY_TextChanged(object sender, EventArgs e)
        {
            if ((Convert.ToInt16 (txt_QTY.Text) >
                Convert.ToInt16(DayList_View.CurrentRow.Cells[5].Value) - 
                Convert.ToInt16(DayList_View.CurrentRow.Cells[17].Value)) ||

                (Convert.ToInt16(txt_QTY.Text) >
                Convert.ToInt16(DayList_View.CurrentRow.Cells[5].Value) -
                Convert.ToInt16(DayList_View.CurrentRow.Cells[6].Value)))
            { txt_QTY.Text = "0"; Hide_All_Buttons(); }
            else { button22.Visible = true; Scren_Activate(); }
          
        }

        private void Hide_All_Buttons()
        {
            button22.Visible = false;
            button21.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;









        }
    }
}


