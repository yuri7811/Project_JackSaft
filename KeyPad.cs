using System;
using System.Windows.Forms;

namespace JackShaft_App
{
    public partial class KeyPad : Form
    {

        int Multiplaer = 0;
        int number = 0;
        int Hundreds = 0;
        int Result;
        int current_number;


        public KeyPad(int RowIndex, string Volume, int AlowedValue)
        {
            InitializeComponent();
            label3.Text = Volume;
            label4.Text = RowIndex.ToString();
            label5.Text = AlowedValue.ToString();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }





        private void Calculete()

        {

            if (Hundreds != 0) { Result = Hundreds + Multiplaer + number; }
            else
            {
                if (Multiplaer != 0) { Result = Multiplaer + number; }
                else { Result = number; }

            }

         if (Hundreds == 0 && Multiplaer == 0 && number == 00) { Result = current_number; }

         if ( Result > Convert.ToInt16(label5.Text)) { Result = Convert.ToInt16(label5.Text); }


            lbl_Val.Text = Result.ToString();
            Properties.Settings.Default.Enter_Value_After_Proccess = Result.ToString();
            Properties.Settings.Default.Enter_Value = Result.ToString();
            Properties.Settings.Default.Print_Blank = Result.ToString();
            Properties.Settings.Default.Print_Items_In_Blank = Result.ToString();
            Properties.Settings.Default.Save();


            this.Hide();

        }
        private void btn_1_Click(object sender, EventArgs e)
        {
            number = 1;
            Calculete();
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            number = 2;
            Calculete();
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            number = 3;
            Calculete();
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            number = 4;
            Calculete();
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            number = 5;
            Calculete();
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            number = 6;
            Calculete();
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            number = 7;
            Calculete();
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            number = 8;
            Calculete();
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            number = 9;
            Calculete();
        }
  private void btn_10_Click(object sender, EventArgs e)
        {
            Multiplaer = 10;
        }

        private void btn_20_Click(object sender, EventArgs e)
        {
            Multiplaer = 20;
        }

        private void btn_30_Click(object sender, EventArgs e)
        {
            Multiplaer = 30;
        }
        private void btn_90_Click(object sender, EventArgs e)
        {
            Multiplaer = 90;
        }

        private void btn_80_Click(object sender, EventArgs e)
        {
            Multiplaer = 80;
        }

        private void btn_70_Click(object sender, EventArgs e)
        {
            Multiplaer = 70;
        }

        private void btn_60_Click(object sender, EventArgs e)
        {
            Multiplaer = 60;
        }

        private void btn_50_Click(object sender, EventArgs e)
        {
            Multiplaer = 50;
        }

        private void btn_40_Click(object sender, EventArgs e)
        {
            Multiplaer = 40;
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            number = 0; Calculete();
        }


        private void btn_100_Click(object sender, EventArgs e)
        {
            Hundreds = 100;
        }

        private void btn_200_Click(object sender, EventArgs e)
        {
            Hundreds = 200;
        }

        private void btn_300_Click(object sender, EventArgs e)
        {
            Hundreds = 300;
        }

        private void btn_400_Click(object sender, EventArgs e)
        {
            Hundreds = 400;
        }

        private void btn_500_Click(object sender, EventArgs e)
        {
            Hundreds = 500;
        }
    }
}
