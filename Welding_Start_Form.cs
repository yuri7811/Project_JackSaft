using System;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class Welding_Start_Form : Form
    {
        public Welding_Start_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form Worker_ID = new Worker_ID(1);
            Worker_ID.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form Arhaiv_form = new Archaiv();
            Arhaiv_form.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form Report1 = new Reports();
            Report1.ShowDialog(this);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form Worker_ID = new Worker_ID(2);
            Worker_ID.ShowDialog(this);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form QA_Screen = new QA_Screen();
            QA_Screen.Show(this);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
        }


    }
}
