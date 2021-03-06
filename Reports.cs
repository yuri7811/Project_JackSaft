using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class Reports : Form
    {


      //  int Selector = 1;
        int Load_Shift = 0;
        DateTime Load_Date = DateTime.Today;
        int Load_Operation = 0;
        string Load_SearchString = "1";
        int Load_ShiftCount = 3;
        int Load_Operation1 = 100;
        int Load_Operation3 = 220;

        int CalculationSelector = 0;
        string SelectedMakat = "";


        public Reports()
        {
            InitializeComponent();

        }
        private void Reports_Load(object sender, EventArgs e)
        {
            datePicker_fromDate.Value = DateTime.Now.AddDays(0);
            Load_Date = datePicker_fromDate.Value;
            datePicker_fromDate.CustomFormat = "dd/MM/yy";
            txt_Lot.Visible = false;
            datePicker_fromDate.Visible = true;
            checkBox1.Visible = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.pan_2.Width, this.pan_2.Height);
            pan_2.DrawToBitmap(bm, new Rectangle(0, 0, this.pan_2.Width, this.pan_2.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }



        private void Load_Finished_List_Archive1()  // загружаем
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            SQL_Jobs SQL_Job1 = new SQL_Jobs();

            dataGridView1.DataSource = SQL_Job1.GetData_For_Report(Load_Shift, Load_Date, Load_Operation, Load_SearchString, Load_ShiftCount); ;
            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;
            dataGridView1.Columns[7].DefaultCellStyle.Format = "HH:mm   dd/MM ";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "HH:mm   dd/MM ";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "HH:mm dd/MM ";
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersHeight = 35;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1_ChangeColor();
            label1.Text = Load_Date.ToString("dd-MM-yy");

            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            SelectCalculation();
        }
        private void Load_Finished_List_Archive2()  // загружаем
        {

            dataGridView4.DataSource = null;
            dataGridView4.Rows.Clear();

            SQL_Jobs SQL_Job1 = new SQL_Jobs();

            dataGridView4.DataSource = SQL_Job1.GetData_For_Report(Load_Shift, Load_Date, Load_Operation1, Load_SearchString, Load_ShiftCount);
            ;
            dataGridView4.DefaultCellStyle.SelectionBackColor = dataGridView4.DefaultCellStyle.BackColor;
            dataGridView4.DefaultCellStyle.SelectionForeColor = dataGridView4.DefaultCellStyle.ForeColor;
            this.dataGridView4.EnableHeadersVisualStyles = false;
            this.dataGridView4.ColumnHeadersHeight = 35;

            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dataGridView1.Columns[11].Visible = false;
        }




        private void Load_Finished_List_Archive3()  // загружаем
        {

            dataGridView3.DataSource = null;
            dataGridView3.Rows.Clear();

            SQL_Jobs SQL_Job1 = new SQL_Jobs();

            dataGridView3.DataSource = SQL_Job1.GetData_For_Report(Load_Shift, Load_Date, Load_Operation3, Load_SearchString, Load_ShiftCount); ;

           this.dataGridView3.EnableHeadersVisualStyles = false;
            this.dataGridView3.ColumnHeadersHeight = 35;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void datePicker_fromDate_ValueChanged(object sender, EventArgs e)
        {
            Load_Date = datePicker_fromDate.Value;
            Load_Finished_List_Archive1();
        }
        private void checkBox_by_Lot_CheckedChanged(object sender, EventArgs e)
        {

            if (!checkBox_by_Lot.Checked) { checkBox1.Visible = false; checkBox1.Checked = false; Load_Operation = 0; txt_Lot.Text = ""; }
            else { checkBox1.Visible = true; }

            if (checkBox_by_Lot.Checked && checkBox1.Checked)
            { txt_Lot.Visible = true; Load_Operation = 2; }
            if (checkBox_by_Lot.Checked && !checkBox1.Checked)
            { txt_Lot.Visible = true; Load_Operation = 1; }
            if (!checkBox_by_Lot.Checked)
            { txt_Lot.Visible = false; Load_Operation = 0; }

            button2.PerformClick();

            Load_Finished_List_Archive1();

        }
        private void txt_Lot_TextChanged(object sender, EventArgs e)
        {

            int aaa = Load_Operation;
            Load_SearchString = Remove_All_AB_From_Barcode(txt_Lot.Text);
            button2.PerformClick();
            Load_Finished_List_Archive1();

        }


        private string Remove_All_AB_From_Barcode(string Barcode)
        {
            string Numbers = Regex.Replace(Barcode, @"^[A-Za-z]+", "");
            return Numbers;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Load_ShiftCount = 2;
            radioButton5.Visible = false; Load_Finished_List_Archive1();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Load_ShiftCount = 3; radioButton5.Visible = true; Load_Finished_List_Archive1();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            Load_Shift = 1; Load_Finished_List_Archive1();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            Load_Shift = 2; Load_Finished_List_Archive1();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Load_Shift = 3; Load_Finished_List_Archive1();
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Load_Shift = 0; Load_Finished_List_Archive1();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button2.PerformClick();
            if (checkBox1.Checked == true)
            { panel3.Visible = false; Load_Operation = 2; }
            else { panel3.Visible = true; Load_Operation = 1; }
            Load_Finished_List_Archive1();
        }

        private void SelectCalculation()
        { if (CalculationSelector == 0)
            {
                if (!checkBox1.Checked && !checkBox_by_Lot.Checked) { Load_Operation1 = 100; }
                if (!checkBox1.Checked && checkBox_by_Lot.Checked) { Load_Operation1 = 101; }
                if (checkBox1.Checked && checkBox_by_Lot.Checked) { Load_Operation1 = 102; }
            }
            else
            {
                if (!checkBox1.Checked && !checkBox_by_Lot.Checked) { Load_Operation1 = 200; }
                if (!checkBox1.Checked && checkBox_by_Lot.Checked) { Load_Operation1 = 201; }
                if (checkBox1.Checked && checkBox_by_Lot.Checked) { Load_Operation1 = 202; }
            }

            Load_Finished_List_Archive2();
            Load_Finished_List_Archive3();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CalculationSelector = 0;
            SelectCalculation();
            button2.Enabled = false; button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CalculationSelector = 1;
            SelectCalculation();
            button2.Enabled = true; button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Load_Finished_List_Archive1();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            datePicker_fromDate.Value = datePicker_fromDate.Value.AddDays(-1);
            btnForvard.Visible = datePicker_fromDate.Value.Date >= DateTime.Today ? false : true;
        }

        private void btnForvard_Click(object sender, EventArgs e)
        {

            datePicker_fromDate.Value = datePicker_fromDate.Value.AddDays(1);
            btnForvard.Visible = datePicker_fromDate.Value.Date >= DateTime.Today ? false : true;


        }
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)    // выбрать макат из списка для подтверждения
        {
            if (dataGridView3.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView3.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView3.Rows[selectedrowindex];
                SelectedMakat = (Convert.ToString(selectedRow.Cells[0].Value));
            }
        }


        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv.Columns[e.ColumnIndex].Name.Equals("Gender"))
            {
                if (e.Value != null && e.Value.ToString().Trim() == "Male")
                {
                    dgv.Rows[e.RowIndex].Cells["name"].Style.BackColor = Color.White;
                }
                else
                {
                    dgv.Rows[e.RowIndex].Cells["name"].Style.BackColor = Color.DarkGray;
                }
            }
        }

        private void dataGridView1_ChangeColor()

           
        {

             try {

            foreach (DataGridViewRow row in dataGridView1.Rows)

                if (Convert.ToInt32(row.Cells[9].Value) == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }

            foreach (DataGridViewRow row in dataGridView1.Rows)

                if (Convert.ToInt32(row.Cells[9].Value) == 1 )
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }

            foreach (DataGridViewRow row in dataGridView1.Rows)


                if (Convert.ToInt32(row.Cells[9].Value) == 7 )
                {
                    row.DefaultCellStyle.BackColor = Color.LightSalmon;
                }

                }
            catch { }

        }



        private void button7_Click(object sender, EventArgs e)
        {
            LN_App_Discrepancy Discrepancy = new LN_App_Discrepancy();
            Discrepancy.ShowDialog(this);

        }
    }
}
