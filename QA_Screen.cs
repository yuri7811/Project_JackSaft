using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class QA_Screen : Form
    {

        string strFilePath;

        DataTable dt1 = new DataTable();
        List<int> Exit_Set_List = new List<int>();
        string SQL_Query;





        public QA_Screen()
        {
            InitializeComponent();

            Load_Finished_List();
        }




        private void Load_Finished_List()  // загружаем
        {
            try
            {
                dt1.Rows.Clear();
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.Aplication_ConnectionString);
                SQL_Query = "select   * from [SF_Productivity].[dbo].[V_JS_QA]   order by  ID desc";
                DataTable dt = new DataTable();
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQL_Query, conn);
                da.Fill(dt1);
                conn.Close();
                DGV_Archaiv_Items.DataSource = null;
                DGV_Archaiv_Items.Rows.Clear();
                DGV_Archaiv_Items.Columns[5].DefaultCellStyle.Format = "hh:mm dd/MM";
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    // соединяем три поля в одну трехстрочную ячейку
                    string Concat =    dt1.Rows[i][2].ToString().Trim()    // lot
                            + "\r\n" + dt1.Rows[i][3].ToString().Trim();   // makat

                    string DateTime = dt1.Rows[i][6].ToString();
                    Image Picture = null;
                    try
                    {
                        string Picture_Link = Reformating_ConnectionString(dt1.Rows[i][4].ToString().Trim());
                        Picture = Image.FromFile(Picture_Link);
                    }
                    catch { Picture = Image.FromFile("NoImage.png"); }
                    // строим на основе основного грида новый для  работе в форме
                    DGV_Archaiv_Items.Rows.Add(
                    dt1.Rows[i][0].ToString().Trim(),                                // ID    
                    Picture,                                                         // Image 
                    Concat,
                    dt1.Rows[i][5].ToString().Trim(),                                // количество
                    DateTime.ToString(),                                             // data
                    dt1.Rows[i][8].ToString().Trim(),                                // Status QA
                    dt1.Rows[i][2].ToString().Trim(),                                // Lot
                    dt1.Rows[i][3].ToString().Trim(),                                // макат
                    dt1.Rows[i][9].ToString().Trim(),                                // QA_Worker
                    dt1.Rows[i][10].ToString().Trim()                               // QA_Resume
                    //dt1.Rows[i][11].ToString().Trim()                               // name qa worker
                    );
                }
            }
            catch { }
            DGV_Archaiv_Items.RowTemplate.Height = 80;
            var dgv = new DataGridView();
            DGV_Archaiv_Items.RowTemplate.Height = 150;
            dgv.ClearSelection();

            Colloring();



        }
        private string Reformating_ConnectionString(string OriginalString)          // преобразовываем линк  в нормальный ПС читаемый  формат
        {
            if (OriginalString == "") { strFilePath = "NoImage.png"; }
            else
            {
                strFilePath = null;
                string OriginalPatch = OriginalString;
                string Cutting = OriginalPatch.Substring(32, OriginalPatch.Length - 32);
                string AfterChanging = Cutting.Replace("/", @"\");
                string AfterConnection = @"\\gfbhcapp1\\tcibd001$\" + AfterChanging;
                string AfterSpaseDeleting = AfterConnection.Replace(" ", ""); ;
                strFilePath = AfterSpaseDeleting;
            }
            return strFilePath;

        }

        private void DGV_Archaiv_Items_SelectionChanged(object sender, EventArgs e)
        {

            Colloring();


        }





        private void Colloring()
        {
            DGV_Archaiv_Items.RowTemplate.Height = 150;

            try {


                foreach (DataGridViewRow row in DGV_Archaiv_Items.Rows)

                    switch (Convert.ToInt32(row.Cells[5].Value))
                    {
                        case 1:
                            row.DefaultCellStyle.BackColor = Color.Tomato; ;
                            break;
                        case 2:
                            row.DefaultCellStyle.BackColor = Color.PaleGreen; ;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }
            }
            catch { }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Load_Finished_List();
            Colloring();
        }
    }
}
