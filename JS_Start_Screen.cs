using System;
using System.Windows.Forms;





namespace JackShaft_App
{
    public partial class JS_Start_Screen : Form
    {
        public JS_Start_Screen()
        {
            InitializeComponent();

            // загружаем  путь из  файла и сохраняем его в параметрах программы
            IniFile MyIni = new IniFile("JackShaft_App.ini");
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

            Properties.Settings.Default.Save();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form Worker_ID = new Worker_ID(1);
            Worker_ID.ShowDialog(this);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form Report1 = new Reports();
            Report1.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form OMS = new Image_Editor();
            OMS.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Worker_ID = new Worker_ID(2);
            Worker_ID.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {

            Form QA_Screen = new QA_Screen();
            QA_Screen.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form Label = new Archaiv();
            Label.ShowDialog(this);
        }
    }
}
