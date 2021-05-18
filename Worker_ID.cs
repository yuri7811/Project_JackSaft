using System;
using System.Data;
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

             private void button2_Click(object sender, EventArgs e)
        {
            JS_SQL_Jobs SQL_Job = new JS_SQL_Jobs();
            if (AAA == 1)
            {
                DataTable Name = SQL_Job.CheckWorkerID(txt_ID.Text.Trim());
                if (Name != null)
                {
                    if (Name.Rows.Count > 0)
                    {
                        string Name1 = Name.Rows[0][0].ToString() + " " + Name.Rows[0][1].ToString();
                        Properties.Settings.Default.ID_Worker = txt_ID.Text.Trim();
                        Properties.Settings.Default.Worker_Name = Name1;
                        Properties.Settings.Default.Save();
                        this.Close();

                        switch (Properties.Settings.Default.Line)
                        {
                            case "JS":
                                Form Loading_Form = new JS_Loading_Form();
                                Loading_Form.ShowDialog(this);
                                break;
                            case "Welding":
                                Form Loading_Form_W = new Welding_Loading_Form();
                                Loading_Form_W.ShowDialog(this);
                                break;
                            case "Warehouse":
                                //Form Loading_Form = new JS_Loading_Form();
                                //Loading_Form.ShowDialog(this);
                                break;
                            default:
                                //Form Loading_Form = new JS_Loading_Form();
                                //Loading_Form.ShowDialog(this);
                                break;
                        }
                        //Form Loading_Form = new JS_Loading_Form();
                        //Loading_Form.ShowDialog(this);




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
                    DataTable Name2 = SQL_Job.CheckWorkerID(txt_ID.Text.Trim());
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

        private void Txt_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(this, new EventArgs());
            }
        }
    }
}
