using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace JackShaft_App
{
    public partial class Conformation : Form
    {

        Byte Code = 0;

        string Lot_;
        string Makat_;
        string QTY_;
        string Task_;
        string OPR_;
        string Image_;
        string UserID_;
        string Weight_;
        string Accumulated_QTY_;


        public Conformation(string Lot, string Makat, string QTY, string Accumulated_QTY, string Task, string OPR, string Image , string UserID, string Weight)
        {
            InitializeComponent();
            button2.GotFocus += Button2_GotFocus;
            button1.GotFocus += Button1_GotFocus;
             Lot_ = Lot;
             Makat_ = Makat;
             QTY_ = QTY;
             Task_ =Task;
             OPR_ = OPR;
            Image_ = Image;
            UserID_ = UserID;
            Weight_ = Weight;
            Accumulated_QTY_ = Accumulated_QTY;
        }


        private void Button1_GotFocus(object sender, EventArgs e)
        {
            button1.BackColor = Color.Green;
            button2.BackColor = Color.Gray;
        }

        private void Button2_GotFocus(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Green;
        }


        private void button2_Click(object sender, EventArgs e)
        {


            int result = Int32.Parse(QTY_);


            Report_To_LN(Lot_, Makat_, QTY_, Task_, OPR_, Image_, UserID_, Weight_);

            // we generate XML code

            Bod XML_BOD = new Bod();
            XML_BOD.Create_Bod(Properties.Settings.Default.XML_Template.Trim(),
            Properties.Settings.Default.New_XML_Destination.Trim(),
            Lot_.Trim(),
            OPR_,
            Task_,
            Accumulated_QTY_);

            Code = 0;
           Properties.Settings.Default.ConformationCode  = Code;
           Properties.Settings.Default.Save();
           this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Code = 1;
            Properties.Settings.Default.ConformationCode = Code;
            Properties.Settings.Default.Save();
            this.Hide();
        }


        private void Report_To_LN( string Lot, string Makat, string Report_QTY,string Task, string Opr, string Image_Link, string User_ID, string Weight)   // Записываем СЕТ в таблицу.
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_JS_Report_To_LN", conn) { CommandType = CommandType.StoredProcedure };
                    sqlCmd.Parameters.AddWithValue("@Lot", Lot);
                    sqlCmd.Parameters.AddWithValue("@Makat", Makat);
                    sqlCmd.Parameters.AddWithValue("@Value", Report_QTY);
                    sqlCmd.Parameters.AddWithValue("@Task", Task);
                    sqlCmd.Parameters.AddWithValue("@Opr", Opr);
                    sqlCmd.Parameters.AddWithValue("@Image", Image_Link);
                    sqlCmd.Parameters.AddWithValue("@UserID", User_ID);
                    sqlCmd.Parameters.AddWithValue("@Weight", Weight);


                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                }
        }







    }
}
