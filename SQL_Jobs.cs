using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace JackShaft_App
{
    class SQL_Jobs
    {
        string Image_Name;






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


        public void Print_QA_Lable(string Operation_ID, string Station, string QA_Status, string Paka, string Makat,
                                   string QA_WorkerID, string Note, string Printer)

        {
            //DateTime dateAndTime = DateTime.Now;
            //// string DateTimeNow = (dateAndTime.ToString("MM_dd_yy__HH_mm"));
            //string DateTimeNow = (dateAndTime.ToString("s"));
            //string DateForPutch = (dateAndTime.ToString("MM_dd_yy__HH_mm"));


            ////string MyPatch = @"C:\Users\kk27955\" + Paka + "_" + DateForPutch + ".txt";   // ???
            //string FileText;

            //if (QA_Status == "PASS") { Printer = Properties.Settings.Default.Printer_QA_Pass; }
            //if (QA_Status == "REJECT") { Printer = Properties.Settings.Default.Printer_QA_Reject; }



            //FileText = @"%BTW%  /AF=" + Properties.Settings.Default.BarTenderDir + " /D = %Trigger File Name% /PRN = " + '\u0022'
            //           + Printer + '\u0022' + "/R = 3/p" + 1 + System.Environment.NewLine + "%END%   " + System.Environment.NewLine +
            //             "" + Operation_ID +       //ID opperation
            //             "|" + Station +            // Station
            //             "|" + Makat +             // Makat
            //             "|" + Paka +               // Paka
            //             "|" + QA_Status +          // PASS / REJECT
            //             "|" + QA_WorkerID +        // QA_worker_ ID
            //             "|" + Note +               // QA_Note
            //             "|" + DateTimeNow +        // Print Data time
            //             "";

            //File.WriteAllText(MyPatch, FileText);
        }




    }
}
