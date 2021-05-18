using System;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace JackShaft_App
{
    class JS_SQL_Jobs
    {
        string Image_Name;

        //  Printing lable job
        public void Print_Lable( string WorkerID, string Paka, string Makat, string QTY,
                                string Image_Link,  string Discription,  string Printer, string Weight, int Lables)

        {

            DateTime dateAndTime = DateTime.Now;
            string DateTimeNow = (dateAndTime.ToString("s"));
            string DateForPutch = (dateAndTime.ToString("MM_dd_yy"));

            string MyPatch = Properties.Settings.Default.Report_File_Dir + Paka + "_" + DateForPutch + "_" + Lables + ".txt";   // ???
            string FileText;


            Printer = Properties.Settings.Default.Printer_Lable;
            FileText = @"%BTW% /AF=" + Properties.Settings.Default.BarTenderDir_Report + " /D=" + '\u0022' + "%Trigger File Name%" + '\u0022' + " /PRN=" + '\u0022'
                      + Printer + '\u0022' + " /R=3 /p" + System.Environment.NewLine + "%END%   " + System.Environment.NewLine +


                         "" +  Paka +              // Paka
                         "|" + Makat +             // Makat
                         "|" + "1"  +               // QTY  -   1 
                         "|" + WorkerID +           // Worker
                         "|" + Discription +       //Discription
                         "|" + Image_Link +         // Image
                         "|" + ""   +               //
                         "|" + Weight +             //Discription
                         "|" + DateTimeNow +       // Print Data time
                         "";

            File.WriteAllText(MyPatch, FileText);
        }

        // Copy data from BaanDB to local table
        internal void JS_CopyFromBaan()   
        {
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

        // JS we get set of images  according makat
        internal DataTable JS_RefreshImageGrid(string Makat)
        {
               SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);
              
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_JS_IMAGE_View_By_Station", conn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Makat", Makat.Trim());

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.SelectCommand = sqlCmd;
                DataTable dtblImages = new DataTable();
                sqlda.Fill(dtblImages);
                return dtblImages;

          

        }

        // JS when we make report to ln we have  put to xml file accumulated QTY and we asc already reported QTY
        internal int GetAllreadyReported_QTY(string Paka)  
        {
            int new_QTY;
            string  Query = " Select  * from V_JS_Sum_Done where Lot LIKE '%" + Paka.Trim() + "%'  ";
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
                new_QTY = Convert.ToInt32(dt.Rows[0][1]);

                return new_QTY;
            }


        }

        // Worker is ???  QA  - 1  , Production - 0
        internal DataTable CheckWorkerID(string ID)  // загружаем
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

        // working with Report  get data according  Load Operation  key.
        internal DataTable GetData_For_Report(int Load_Shift, DateTime Load_Date, int Load_Operation1, string Load_SearchString, int Load_ShiftCount)
        {

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString))
            {

                conn.Open();
                SqlCommand myCmd = new SqlCommand("SP_JS_Select_By_Shift", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new SqlParameter("@Shift", Load_Shift));
                myCmd.Parameters.Add(new SqlParameter("@Date", Load_Date));
                myCmd.Parameters.Add(new SqlParameter("@Operation", Load_Operation1));
                myCmd.Parameters.Add(new SqlParameter("@SearchString", Load_SearchString));
                myCmd.Parameters.Add(new SqlParameter("@ShiftCount", Load_ShiftCount));

                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dt);
                conn.Close();
            }

            return dt;

        }

        internal DataTable GetData_For_QA_Screen()
        {
            DataTable dt1 = new DataTable();
            dt1.Rows.Clear();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);
            string SQL_Query = "select   * from [SF_Productivity].[dbo].[V_JS_QA]   order by  ID desc";
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(SQL_Query, conn);
            da.Fill(dt1);
            conn.Close();
            return dt1;
        }

        internal void QA_Report(int Operation_ID, string QA_Status, string QA_Worker, string QA_Resume)   // Записываем СЕТ в таблицу.
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
    }
}
