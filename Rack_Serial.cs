using System;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class Rack_Serial : Form
    {
        public Rack_Serial()
        {
            InitializeComponent();
            txt_RACK_NUMBER.Select();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RACK_NUMBER = "0";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.RACK_NUMBER = txt_RACK_NUMBER.Text;
            Properties.Settings.Default.Save(); 
            this.Hide();
        }
    }
}
