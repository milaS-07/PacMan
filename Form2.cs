using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pekmen
{
    public partial class Form2 : Form
    {
        int proredX = 10;
        int proredTeksta = 15;

        Point pozicijaForme1;
        Point pozicijaForme2;
        int sirinaForme1, visinaForme1;
        Image pozadina = Image.FromFile("pozadina.jpg");
        Image pobeda = Image.FromFile("pobeda.png");
        Image gubitak = Image.FromFile("gubitak.png");
        public Form2(Point p, int sirinaForme1, int visinaForme1)
        {
            this.sirinaForme1 = sirinaForme1;
            this.visinaForme1 = visinaForme1;
            pozicijaForme1 = p;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            bIgrajOpet.TabIndex = 0;
            bX.TabIndex = 1;

            pozicijaForme2 = new Point();
            pozicijaForme2.X = pozicijaForme1.X + sirinaForme1 / 4;
            pozicijaForme2.Y = pozicijaForme1.Y + visinaForme1 / 4;

            Size = new Size(sirinaForme1 / 2, visinaForme1 / 2);

            this.Location = pozicijaForme2;

            bIgrajOpet.BackColor = Color.FromArgb(90, 120, 130);
            bIgrajOpet.Height = this.Size.Height / 8 + proredTeksta;
            bIgrajOpet.Width = this.Size.Width / 3 + proredTeksta;

            bIgrajOpet.Location = new Point((this.Size.Width - bIgrajOpet.Width) / 2, this.Size.Height / 8 * 6 - proredTeksta);

            bIgrajOpet.Font = new Font("Century Gothic", bIgrajOpet.Size.Height / 4, FontStyle.Bold);


            bX.BackColor = Color.FromArgb(85, 5, 5);
            bX.ForeColor = Color.White;

            bX.Width = this.Size.Height / 16;
            bX.Height = bX.Width;

            bX.Location = new Point(this.Size.Width - proredX - bX.Width, proredX);

            bX.Font = new Font("Century Gothic", bX.Size.Height / 3, FontStyle.Bold);
            bX.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(pozadina, 0, 0, this.Size.Width, this.Size.Height);


            int sirinaTeksta = this.Size.Width - 2 * proredTeksta;
            double visinaTeksta = (double)sirinaTeksta / 4.705882;

            Rectangle tekstPravougaonik = new Rectangle(proredTeksta, this.Size.Height / 4, sirinaTeksta, (int)visinaTeksta);

            if (Form1.stanjeKrajaIgre == "pobeda")
            {
                e.Graphics.DrawImage(pobeda, tekstPravougaonik);
            }
            else if (Form1.stanjeKrajaIgre == "gubitak")
            {
                e.Graphics.DrawImage(gubitak, tekstPravougaonik);
            }
        }

        private void bX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bIgrajOpet_Click(object sender, EventArgs e)
        {
            Form1.stanjeKrajaIgre = null;
            this.Close();
        }
    }
}
