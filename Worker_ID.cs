using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class Worker_ID : Form
    {

        int AAA;
        public Worker_ID(int Service)
        {
            InitializeComponent();

             AAA = Service;
        }

        private DataTable CheckWorkerID(string ID)  // загружаем
        {

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {

                conn.Open();
                SqlCommand myCmd = new SqlCommand("SP_Welding_Get_Worker_By_ID", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new SqlParameter("@Worker_ID", ID));
                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dt);
                conn.Close();
            }

            return dt;
        }

             private void button2_Click(object sender, EventArgs e)
        {

            if (AAA == 1)
            {
                DataTable Name = CheckWorkerID(txt_ID.Text.Trim());
                if (Name != null)
                {
                    if (Name.Rows.Count > 0)
                    {
                        string Name1 = Name.Rows[0][0].ToString() + " " + Name.Rows[0][1].ToString();
                        Properties.Settings.Default.ID_Worker = txt_ID.Text.Trim();
                        Properties.Settings.Default.Worker_Name = Name1;
                        Properties.Settings.Default.Save();
                        this.Close();
                        Form Loading_Form = new Loading_Form();
                        Loading_Form.ShowDialog(this);
                    }
                    else
                    {
                        label3.Visible = true;
                        txt_ID.Text = "";
                    }
                }
            }

            if (AAA == 2)
                {
                    DataTable Name2 = CheckWorkerID(txt_ID.Text.Trim());
                    if (Name2 != null)
                    {
                        if (Name2.Rows.Count > 0)
                        {
                            string Name3 = Name2.Rows[0][0].ToString() + " " + Name2.Rows[0][1].ToString();
                        Properties.Settings.Default.ID_Worker_QA = txt_ID.Text.Trim();
                        Properties.Settings.Default.Worker_Name_QA = Name3;
                        Properties.Settings.Default.Save();
                            this.Close();
                        Form QA = new QA();
                        QA.ShowDialog(this);
                    }
         else
                        {
                            label3.Visible = true;
                            txt_ID.Text = "";
                        }
                    }
                }


            }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "52548";


        }
    }
}
