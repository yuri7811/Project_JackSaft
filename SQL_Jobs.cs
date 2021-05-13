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



    }
}
