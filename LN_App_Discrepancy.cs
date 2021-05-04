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


namespace JackShaft_App
{
    public partial class LN_App_Discrepancy : Form
    {
        public LN_App_Discrepancy()
        {
            InitializeComponent();
            Load_LN_App_Discrepancy();
        }

        private void Load_LN_App_Discrepancy()  // загружаем
        {

            dataGridView100.DataSource = null;
            dataGridView100.Rows.Clear();
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {
                conn.Open();
                SqlCommand myCmd = new SqlCommand("SP_JS_LN_ErrorReport", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dt);
                conn.Close();
            }


            dataGridView100.DataSource = dt;
            dataGridView100.DefaultCellStyle.SelectionBackColor = dataGridView100.DefaultCellStyle.BackColor;
            dataGridView100.DefaultCellStyle.SelectionForeColor = dataGridView100.DefaultCellStyle.ForeColor;
            //dataGridView1.Columns[4].DefaultCellStyle.Format = "HH:mm   dd/MM/yy ";
            //dataGridView1.Columns[5].DefaultCellStyle.Format = "HH:mm   dd/MM/yy ";
            //dataGridView1.Columns[6].DefaultCellStyle.Format = "HH:mm   dd/MM/yy ";
            dataGridView100.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Load_LN_App_Discrepancy();
        }
    }
}
