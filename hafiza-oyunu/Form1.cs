using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace hafiza_oyunu
{
    public partial class oyunEkrani : Form
    {
        List<string> icons = new List<string>()
        {
            "!",",","b","k","v","w","z","N","@","#",
            "!",",","b","k","v","w","z","N","@","#"
        };

        Random rnd = new Random();
        Timer t = new Timer();
        Timer t2 = new Timer();
        Button first, second;

        int player1 = 0;
        int player2 = 0;
        bool isPlayer1Turn = true;

        public oyunEkrani()
        {
            InitializeComponent();
            t.Tick += T_Tick;
            t.Interval = 5000;
            arayuz();
            t.Start();
            t2.Tick += T2_Tick;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            t.Stop();

            foreach (Control control in Controls)
            {
                if (control is Button item)
                {
                    item.ForeColor = item.BackColor;
                }
            }
        }

        private void T2_Tick(object sender, EventArgs e)
        {
            t2.Stop();

            if (first != null && second == null)
            {
 
                first.ForeColor = first.BackColor;
                first = null;
                isPlayer1Turn = !isPlayer1Turn;
            }
            else if (first != null && second != null && first.Text != second.Text)
            {

                first.ForeColor = first.BackColor;
                second.ForeColor = second.BackColor;
                first = null;
                second = null;
                isPlayer1Turn = !isPlayer1Turn;
            }
        }

        private void arayuz()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.Name != "button1" && icons.Count > 0)
                {
                    int randomindex = rnd.Next(icons.Count);
                    btn.Text = icons[randomindex];
                    btn.ForeColor = Color.Black;
                    icons.RemoveAt(randomindex);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (first != null && second != null) return;

            if (first == null)
            {
                first = btn;
                first.ForeColor = Color.Black;
                t2.Interval = 5000;
                t2.Start();
                return;
            }

            second = btn;
            second.ForeColor = Color.Black;

            t2.Stop();

            if (first.Text == second.Text)
            {
                if (isPlayer1Turn)
                {
                    player1++;
                    label8.Text = player1.ToString();
                }
                else
                {
                    player2++;
                    label10.Text = player2.ToString();
                }

                first = null;
                second = null;

                if (CheckForCompletion() || player1 >= 11 || player2 >= 11)
                {
                    ShowWinner();
                    ResetGame();
                }
            }
            else
            {
                t2.Start();
                t2.Interval = 1000;
            }
        }

        private bool CheckForCompletion()
        {
            foreach (Control control in Controls)
            {
                if (control is Button btn && btn.Name != "button1" && btn.ForeColor == btn.BackColor)
                {
                    return false;
                }
            }
            return true;
        }

        private void ShowWinner()
        {
            string winnerMessage;
            if (player1 > player2)
            {
                winnerMessage = "Birinci oyuncu kazandı!";
            }
            else if (player1 < player2)
            {
                winnerMessage = "İkinci oyuncu kazandı!";
            }
            else
            {
                winnerMessage = "Beraberlik!";
            }

            MessageBox.Show(winnerMessage);
        }

        private void ResetGame()
        {
            icons = new List<string>()
            {
                "!",",","b","k","v","w","z","N","@","#",
                "!",",","b","k","v","w","z","N","@","#"
            };
            MessageBox.Show("Yeni bölüm başlatılıyor...");
            arayuz();
            player1 = 0;
            player2 = 0;
            isPlayer1Turn = true;
            label8.Text = "0";
            label10.Text = "0";
            t.Start();
        }
    }
}

