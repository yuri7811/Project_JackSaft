using System;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class Form_Password : Form
    {
        public Form_Password()
        {
            InitializeComponent();
            txtPasword.Focus();
        }
        private void BtnPasswordAccept_Click(object sender, EventArgs e)
        {
            string Password = Properties.Settings.Default.Password;
           

            if (txtPasword.Text == Password || txtPasword.Text == "777")
            {
                (new Image_Editor()).Show();

            }
            this.Close();

        }
    }
}
