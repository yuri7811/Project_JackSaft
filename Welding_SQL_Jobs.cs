using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace JackShaft_App
{
    class Welding_SQL_Jobs
    {
        string Image_Name;
        public void Order_comand(int Station, int Operation, int FL_ID)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_FL_Order", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Station", Station));
                cmd.Parameters.Add(new SqlParameter("@Operation", Operation));
                cmd.Parameters.Add(new SqlParameter("@FL_ID", FL_ID));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable Load_List_Orders(string Zona, int Operation)  // загружаем
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {
                conn.Open();
                SqlCommand myCmd = new SqlCommand("SP_FL_Read_Orders", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add("Zona", SqlDbType.VarChar).Value = Zona;
                myCmd.Parameters.Add(new SqlParameter("@Operation", Operation));
                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public DataTable Load_Station_Status(int Station_ID)  // загружаем
        {

            DataTable dp = new DataTable();
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {
                conn.Open();
                SqlCommand myCmd = new SqlCommand("SP_FL_Station_Status", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new SqlParameter("@Station_ID", Station_ID));
                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dp);
                conn.Close();
            }
            return dp;
        }

        public void Print_Lable(string Station,string WorkerID, string Paka, string Makat, string QTY,
                                string Image_Link,string Warehouse, string Discription,  string POU,  string Printer, int Lables)

        {
            DateTime dateAndTime = DateTime.Now;
            string DateTimeNow = (dateAndTime.ToString("s"));
            string DateForPutch = (dateAndTime.ToString("MM_dd_yy__HH_mm"));
            string MyPatch = Properties.Settings.Default.Report_File_Dir + Paka + "_" + DateForPutch + "_" +  Lables + ".txt";   // ???
            string FileText;
            string s = Image_Link;
            int idx = s.LastIndexOf('/');
            if (idx != -1)
            {
             Image_Name = s.Substring(idx + 1);
            }
            Printer = Properties.Settings.Default.Printer_Lable;
            FileText = @"%BTW% /AF="  + Properties.Settings.Default.BarTenderDir_Report + " /D=" + '\u0022' + "%Trigger File Name%" + '\u0022' + " /PRN=" + '\u0022'
                      + Printer  + '\u0022' + " /R=3 /p" + System.Environment.NewLine + "%END%   " + System.Environment.NewLine +

                         ""  + Station +           //Station
                         "|" + WorkerID +          //worker_ ID
                         "|" + Makat +             // Makat
                         "|" + Paka +              // Paka
                         "|" + QTY +               // QTY
                         "|" + Properties.Settings.Default.ImageDirectory +   // ImageDirectory
                         "|" + Image_Name +        //Image
                         "|" + Warehouse +         //Warehouse
                         "|" + Discription +       //Discription
                         "|" + POU +               // POU
                         "|" + DateTimeNow +       // Print Data time
                         "";

            File.WriteAllText(MyPatch, FileText);
        }


        public void Print_QA_Lable(string Operation_ID, string Station, string QA_Status, string Paka, string Makat,
                                   string QA_WorkerID, string Note, string Printer)

        {
            DateTime dateAndTime = DateTime.Now;
            string DateTimeNow = (dateAndTime.ToString("s"));
            string DateForPutch = (dateAndTime.ToString("MM_dd_yy__HH_mm"));
            string MyPatch = Properties.Settings.Default.QA_File_Dir + Paka + "_" + DateForPutch + ".txt";   // ???
            string FileText;
            if (QA_Status == "PASS") { Printer = Properties.Settings.Default.Printer_QA_Pass; }
            if (QA_Status == "REJECT") { Printer = Properties.Settings.Default.Printer_QA_Reject; }
            FileText = @"%BTW% /AF=" + Properties.Settings.Default.BarTenderDir_QA + " /D=" + '\u0022' + "%Trigger File Name%" + '\u0022' + " /PRN=" + '\u0022'
                      + Printer + '\u0022' + " /R=3 /p" + System.Environment.NewLine + "%END%   " + System.Environment.NewLine +
                         ""  + Operation_ID +       //ID opperation
                         "|" + Station +            // Station
                         "|" + Makat  +             // Makat
                         "|" + Paka +               // Paka
                         "|" + QA_Status +          // PASS / REJECT
                         "|" + QA_WorkerID +        // QA_worker_ ID
                         "|" + Note +               // QA_Note
                         "|" + DateTimeNow +        // Print Data time
                         "";
            File.WriteAllText(MyPatch, FileText);
        }



        internal void Welding_CopyFromBaan()   // Copy data from BaanDB to local table
        {
          

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_Welding_Copy_From_Baan", conn) { CommandType = CommandType.StoredProcedure };
                    sqlCmd.CommandTimeout = 300;
                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                }

        }




    }
}
