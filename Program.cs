using System;
using System.Windows.Forms;

namespace JackShaft_App
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // JS _ загружаем  путь из  файла и сохраняем его в параметрах программы
            IniFile MyIni = new IniFile("JackShaft_App.ini");

            Properties.Settings.Default.Line = MyIni.Read("Line");
            Properties.Settings.Default.Aplication_ConnectionString = MyIni.Read("Aplication_ConnectionString");
            Properties.Settings.Default.Images_store_path = MyIni.Read("Images_Global_store_path");
            Properties.Settings.Default.ConnectionString_LN_Linker = MyIni.Read("ConnectionString_LN_Linker");
            Properties.Settings.Default.Current_Station = MyIni.Read("Current_Station");
            Properties.Settings.Default.OMS_Path = MyIni.Read("OMS_Path");
            Properties.Settings.Default.Report_File_Dir = MyIni.Read("Report_File_Dir");
            Properties.Settings.Default.QA_File_Dir = MyIni.Read("QA_File_Dir");
            Properties.Settings.Default.BarTenderDir_Report = MyIni.Read("BarTenderDir_Report");
            Properties.Settings.Default.BarTenderDir_QA = MyIni.Read("BarTenderDir_QA");
            Properties.Settings.Default.ImageDirectory = MyIni.Read("ImageDirectory");
            Properties.Settings.Default.Printer_Lable = MyIni.Read("Printer_Lable");
            Properties.Settings.Default.XML_Template = MyIni.Read("XML_Template");
            Properties.Settings.Default.New_XML_Destination = MyIni.Read("New_XML_Destination");

          // Welding _ загружаем  путь из  файла и сохраняем его в параметрах программы
            Properties.Settings.Default.Aplication_ConnectionString_2 = MyIni.Read("Aplication_ConnectionString_2");
          //  Properties.Settings.Default.Baan_ConnectionString = MyIni.Read("Baan_ConnectionString");
            Properties.Settings.Default.DefaultStationNr = MyIni.Read("DefaultStationNr");
          //  Properties.Settings.Default.Print_File_Dir = MyIni.Read("Print_File_Dir");
            Properties.Settings.Default.Printer_QA_Pass = MyIni.Read("Printer_QA_Pass");
            Properties.Settings.Default.Printer_QA_Reject = MyIni.Read("Printer_QA_Reject");

            Properties.Settings.Default.Save();

            switch (Properties.Settings.Default.Line)
            {
                case "JS":
                    Application.Run(new JS_Start_Screen());
                    break;
                case "Welding":
                    Application.Run(new Welding_Start_Form());
                    break;
                case "Warehouse":
                    Application.Run(new JS_Start_Screen());
                    break;
                default:
                    Application.Run(new JS_Start_Screen());
                    break;
            }

          //  Application.Run(new JS_Start_Screen());
        }
    }
}
