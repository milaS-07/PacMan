using System.Numerics;
using System.Runtime.CompilerServices;

namespace pekmen
{
    public partial class Form1 : Form
    {
        #region Promenljive
        System.Media.SoundPlayer pocetakPlejer = new System.Media.SoundPlayer("pocetak.wav");
        System.Media.SoundPlayer gubitakPlejer = new System.Media.SoundPlayer("gubitak.wav");
        System.Media.SoundPlayer pekmen_jede1Plejer = new System.Media.SoundPlayer("pekmen_jede1.wav");
        System.Media.SoundPlayer pekmen_jede2Plejer = new System.Media.SoundPlayer("pekmen_jede2.wav");
        System.Media.SoundPlayer pobedaPlejer = new System.Media.SoundPlayer("pobeda.wav");

        Random rnd = new Random();

        static int brojRedova = 19;
        static int brojKolona = 24;
        int[,] tabla = new int[brojRedova, brojKolona];
        bool[,] tablaNovcica = new bool[brojRedova, brojKolona];

        int dimenzijaPolja = 40;
        int prored = 15;

        bool crtanjeLavirinta = true;
        bool crtanjeNovcica = true;
        bool crtanjeJednogNovcica = false;

        string smerIgraca = "desno";
        string[] smerDuha = { "levo", "levo", "dole" };

        int kolonaIgraca = 1;
        int redIgraca = 9;

        int[] redDuha = { 1, 8, 17 };
        int[] kolonaDuha = { 5, 16, 22 };

        Brush cetkaPekmen = new SolidBrush(Color.Yellow);
        Brush cetkaPozadina = new SolidBrush(Color.Black);
        Brush cetkaNovcica = new SolidBrush(Color.FromArgb(220, 170, 35));

        Image duhSlika1 = Image.FromFile("duh1.png");
        Image duhSlika2 = Image.FromFile("duh2.png");
        Image duhSlika3 = Image.FromFile("duh3.png");
        Image duhSlikaPametan = Image.FromFile("duh_pametan.png");
        Image zidSlika = Image.FromFile("zid.png");

        string trenutnaUsta = "otvorena";

        int brojNovcica = 0;

        string[] smerovi = { "desno", "dole", "levo", "gore" };

        int brojac = 0;

        int[,] potezi =
        {
            { 1, 0 }, //desno
            { 0, 1 }, //dole
            { -1, 0 }, //levo
            { 0, -1 }, //gore
        };

        int staraKolonaDuha = 0;
        int stariRedDuha = 0;

        bool krajIgre = false;
        public static string stanjeKrajaIgre = null;

        int dodatakBrojacu = 10;

        int zvuk = 1;

        bool daLiJeUkljucenZvuk = true;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        public void NoviPocetak()
        {
            zvuk = 1;

            crtanjeLavirinta = true;
            crtanjeNovcica = true;
            smerIgraca = "desno";

            smerDuha[0] = "levo";
            smerDuha[1] = "levo";
            smerDuha[2] = "dole";

            kolonaIgraca = 1;
            redIgraca = 9;

            redDuha[0] = 1;
            redDuha[1] = 8;
            redDuha[2] = 17;

            kolonaDuha[0] = 5;
            kolonaDuha[1] = 16;
            kolonaDuha[2] = 22;

            duhSlika1 = Image.FromFile("duh1.png");
            duhSlika2 = Image.FromFile("duh2.png");
            duhSlika3 = Image.FromFile("duh3.png");

            trenutnaUsta = "otvorena";

            brojNovcica = 0;

            brojac = 0;

            staraKolonaDuha = 0;
            stariRedDuha = 0;

            krajIgre = false;

            dodatakBrojacu = 10;

            GenerisiNovcice();

            this.Refresh();
            tajmerZaOtvaranjeUsta.Interval = 250;
            tajmerZaOtvaranjeUsta.Start();

            tajmerZaPomeranjePekmena.Interval = 215;
            tajmerZaPomeranjePekmena.Start();

            tajmerZaKretanjeDuha.Interval = 450;
            tajmerZaKretanjeDuha.Start();
        }
        async private void Form1_Load(object sender, EventArgs e)
        {
            tajmerZaNoviPocetak.Interval = 10;

            tajmerZaOtvaranjeUsta.Interval = 250;

            tajmerZaPomeranjePekmena.Interval = 215;

            tajmerZaKretanjeDuha.Interval = 450;

            int sirinaForme = prored * 2 + brojKolona * dimenzijaPolja + 18;
            int visinaForme = prored * 2 + brojRedova * dimenzijaPolja + 47;
            Size = new Size(sirinaForme, visinaForme);

            GenerisiTablu();
            GenerisiNovcice();

            if (daLiJeUkljucenZvuk)
            {
                pocetakPlejer.Play();
                await Task.Run(() => { pocetakPlejer.Load(); pocetakPlejer.PlaySync(); });
            }


            tajmerZaOtvaranjeUsta.Start();
            tajmerZaPomeranjePekmena.Start();
            tajmerZaKretanjeDuha.Start();
        }
        private void GenerisiNovcice()
        {
            for (int red = 0; red < brojRedova; red++)
            {
                for (int kolona = 0; kolona < brojKolona; kolona++)
                {
                    if (tabla[red, kolona] != 1)
                    {
                        tablaNovcica[red, kolona] = true;
                        brojNovcica++;
                    }
                }
            }

            tablaNovcica[redIgraca, kolonaIgraca] = false;

            tablaNovcica[9, 19] = false;
            tablaNovcica[9, 20] = false;

            brojNovcica -= 3;
        }
        private void GenerisiTablu()
        {
            for (int red = 0; red < brojRedova; red++)
            {
                tabla[red, 0] = 1;
                tabla[red, brojKolona - 1] = 1;
            }

            for (int kolona = 0; kolona < brojKolona; kolona++)
            {
                tabla[0, kolona] = 1;
                tabla[brojRedova - 1, kolona] = 1;
            }

            tabla[8, 1] = 1;
            tabla[10, 1] = 1;

            for (int red = 1; red < 4; red++)
            {
                tabla[red, 4] = 1;
                tabla[red + 14, 4] = 1;
            }

            for (int kolona = 2; kolona < 7; kolona++)
            {
                tabla[4, kolona] = 1;
                tabla[6, kolona] = 1;

                tabla[12, kolona] = 1;
                tabla[14, kolona] = 1;
            }

            tabla[2, 2] = 1;
            tabla[16, 2] = 1;

            tabla[6, 4] = 0;
            tabla[12, 4] = 0;

            for (int kolona = 3; kolona < 7; kolona++)
            {
                tabla[8, kolona] = 1;
                tabla[10, kolona] = 1;
            }

            for (int red = 2; red < brojRedova - 2; red++)
            {
                tabla[red, 8] = 1;
            }

            for (int red = 8; red < 11; red++)
            {
                tabla[red, 8] = 0;
            }

            tabla[4, 8] = 0;
            tabla[14, 8] = 0;

            for (int kolona = 6; kolona < 8; kolona++)
            {
                tabla[2, kolona] = 1;
                tabla[16, kolona] = 1;
            }

            for (int kolona = 10; kolona < 17; kolona++)
            {
                tabla[9, kolona] = 1;
            }

            tabla[9, 8] = 1;

            for (int kolona = 18; kolona < 22; kolona++)
            {
                tabla[8, kolona] = 1;
                tabla[10, kolona] = 1;
            }

            tabla[9, 18] = 1;
            tabla[9, 21] = 1;

            for (int kolona = 10; kolona < 17; kolona++)
            {
                tabla[14, kolona] = 1;
                tabla[4, kolona] = 1;
            }

            for (int red = 12; red < 17; red++)
            {
                tabla[red, 13] = 1;
                tabla[red - 10, 13] = 1;
            }

            for (int kolona = 10; kolona < 12; kolona++)
            {
                tabla[16, kolona] = 1;
                tabla[2, kolona] = 1;
            }

            for (int red = 6; red < 8; red++)
            {
                for (int kolona = 10; kolona < 12; kolona++)
                {
                    tabla[red, kolona] = 1;
                    tabla[red + 5, kolona] = 1;
                }
            }

            for (int kolona = 15; kolona < brojKolona - 2; kolona++)
            {
                tabla[2, kolona] = 1;
                tabla[brojRedova - 3, kolona] = 1;
            }

            for (int red = 3; red < 7; red++)
            {
                tabla[red, brojKolona - 3] = 1;
                tabla[red + 9, brojKolona - 3] = 1;
            }

            for (int kolona = 15; kolona < 20; kolona++)
            {
                tabla[6, kolona] = 1;
                tabla[12, kolona] = 1;
            }

            tabla[7, 15] = 1;
            tabla[11, 15] = 1;

            for (int kolona = 18; kolona < 21; kolona++)
            {
                tabla[4, kolona] = 1;
                tabla[14, kolona] = 1;
            }

            tabla[8, 13] = 1;
            tabla[10, 13] = 1;
        }


        #region Crtanje
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (crtanjeLavirinta)
            {
                NacrtjLavirint(e.Graphics);
                crtanjeLavirinta = false;
            }

            NacrtajPekmena(e.Graphics, trenutnaUsta);

            if (crtanjeNovcica)
            {
                NacrtajNovcice(e.Graphics);
                crtanjeNovcica = false;
            }
            else if (crtanjeJednogNovcica)
            {
                //MessageBox.Show("Test");
                NacrtajJedanNovcic(e.Graphics, staraKolonaDuha, stariRedDuha);
            }

            NacrtajDuha(e.Graphics, duhSlika1, 0);
            NacrtajDuha(e.Graphics, duhSlika2, 1);
            NacrtajDuha(e.Graphics, duhSlika3, 2);
        }
        private void NacrtjLavirint(Graphics grafika)
        {
            int x = prored;
            int y = prored;

            for (int red = 0; red < brojRedova; red++)
            {
                for (int kolona = 0; kolona < brojKolona; kolona++)
                {
                    if (tabla[red, kolona] == 1)
                    {
                        grafika.DrawImage(zidSlika, x, y, dimenzijaPolja, dimenzijaPolja);
                    }

                    x += dimenzijaPolja;
                }
                x = prored;
                y += dimenzijaPolja;
            }
        }
        private void NacrtajDuha(Graphics grafika, Image duhSlika, int brojDuha)
        {
            int proredDuha = 3;
            int dimenzijaDuha = dimenzijaPolja - 2 * proredDuha;

            grafika.DrawImage(duhSlika, kolonaDuha[brojDuha] * dimenzijaPolja + prored + proredDuha, redDuha[brojDuha] * dimenzijaPolja + prored + proredDuha,
                                dimenzijaDuha, dimenzijaDuha);
        }
        private void NacrtajNovcice(Graphics grafika)
        {
            int proredNovcica = dimenzijaPolja / 5 * 2;
            int dimenzijaNovcica = dimenzijaPolja - 2 * proredNovcica;

            int x = prored + proredNovcica;
            int y = prored + proredNovcica;


            for (int red = 0; red < brojRedova; red++)
            {
                for (int kolona = 0; kolona < brojKolona; kolona++)
                {
                    if (tablaNovcica[red, kolona])
                    {
                        grafika.FillEllipse(cetkaNovcica, x, y, dimenzijaNovcica, dimenzijaNovcica);
                    }

                    x += dimenzijaPolja;
                }
                x = prored + proredNovcica;
                y += dimenzijaPolja;
            }
        }
        private void NacrtajPekmena(Graphics grafika, string usta)
        {
            int proredKruga = 3;
            int x = prored + kolonaIgraca * dimenzijaPolja + proredKruga;
            int y = prored + redIgraca * dimenzijaPolja + proredKruga;

            int dimenzijaKruga = dimenzijaPolja - 2 * proredKruga;

            grafika.FillEllipse(cetkaPekmen, x, y, dimenzijaKruga, dimenzijaKruga);

            if (usta == "otvorena")
            {
                Point centarKruga = new Point(x + dimenzijaKruga / 2, y + dimenzijaKruga / 2);

                int x1, x2, y1, y2;

                switch (smerIgraca)
                {
                    case "gore":
                        x1 = (proredKruga + prored + kolonaIgraca * dimenzijaPolja) + dimenzijaKruga / 3;
                        y1 = proredKruga + prored + redIgraca * dimenzijaPolja;

                        x2 = (proredKruga + prored + kolonaIgraca * dimenzijaPolja) + (dimenzijaKruga / 3) * 2;
                        y2 = y1;
                        break;
                    case "dole":
                        x1 = (proredKruga + prored + kolonaIgraca * dimenzijaPolja) + dimenzijaKruga / 3;
                        y1 = proredKruga + prored + redIgraca * dimenzijaPolja + dimenzijaKruga;

                        x2 = (proredKruga + prored + kolonaIgraca * dimenzijaPolja) + (dimenzijaKruga / 3) * 2;
                        y2 = y1;
                        break;
                    case "desno":
                        x1 = proredKruga + prored + kolonaIgraca * dimenzijaPolja + dimenzijaKruga;
                        y1 = (proredKruga + prored + redIgraca * dimenzijaPolja) + dimenzijaKruga / 3;

                        x2 = x1;
                        y2 = (proredKruga + prored + redIgraca * dimenzijaPolja) + (dimenzijaKruga / 3) * 2;
                        break;
                    case "levo":
                        x1 = proredKruga + prored + kolonaIgraca * dimenzijaPolja;
                        y1 = (proredKruga + prored + redIgraca * dimenzijaPolja) + dimenzijaKruga / 3;

                        x2 = x1;
                        y2 = (proredKruga + prored + redIgraca * dimenzijaPolja) + (dimenzijaKruga / 3) * 2;
                        break;
                    default:
                        x1 = -1;
                        x2 = -1;
                        y1 = -1;
                        y2 = -1;
                        break;
                }

                Point prvaTacka = new Point(x1, y1);
                Point drugaTacka = new Point(x2, y2);
                Point[] ustaCetvorougao = { centarKruga, prvaTacka, drugaTacka };

                grafika.FillPolygon(cetkaPozadina, ustaCetvorougao);
            }
        }

        #endregion
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            dimenzijaPolja = Math.Min((ClientRectangle.Width - prored * 2) / brojKolona,
                                      (ClientRectangle.Height - prored * 2) / brojRedova);

            crtanjeLavirinta = true;
            crtanjeNovcica = true;
            Refresh();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                smerIgraca = "levo";

                Rectangle obnovi = new Rectangle(prored + dimenzijaPolja * kolonaIgraca, prored + dimenzijaPolja * redIgraca, dimenzijaPolja, dimenzijaPolja);

                this.Invalidate(obnovi);
                Update();
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                smerIgraca = "desno";

                Rectangle obnovi = new Rectangle(prored + dimenzijaPolja * kolonaIgraca, prored + dimenzijaPolja * redIgraca, dimenzijaPolja, dimenzijaPolja);

                this.Invalidate(obnovi);
                Update();
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                smerIgraca = "gore";

                Rectangle obnovi = new Rectangle(prored + dimenzijaPolja * kolonaIgraca, prored + dimenzijaPolja * redIgraca, dimenzijaPolja, dimenzijaPolja);

                this.Invalidate(obnovi);
                Update();
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                smerIgraca = "dole";

                Rectangle obnovi = new Rectangle(prored + dimenzijaPolja * kolonaIgraca, prored + dimenzijaPolja * redIgraca, dimenzijaPolja, dimenzijaPolja);

                this.Invalidate(obnovi);
                Update();
            }
            else if (e.KeyCode == Keys.M)
            {
                daLiJeUkljucenZvuk = !daLiJeUkljucenZvuk;
            }
            
        }

        private void tajmerZaOtvaranjeUsta_Tick(object sender, EventArgs e)
        {
            if (trenutnaUsta == "zatvorena")
            {

                tajmerZaOtvaranjeUsta.Interval = 250;
                trenutnaUsta = "otvorena";
            }
            else
            {
                tajmerZaOtvaranjeUsta.Interval = 100;
                trenutnaUsta = "zatvorena";
            }

            Rectangle obnovi = new Rectangle(prored + dimenzijaPolja * kolonaIgraca, prored + dimenzijaPolja * redIgraca, dimenzijaPolja, dimenzijaPolja);
            this.Invalidate(obnovi);
            Update();
        }

        private void tajmerZaPomeranjePekmena_Tick(object sender, EventArgs e)
        {
            if (DaLiJePomeranjeMoguce())
            {
                Pomeri();
                this.Invalidate(OdrediObnovu(smerIgraca, kolonaIgraca, redIgraca));
                Update();

                if (DaLiJeDodirnuoDuha() && !krajIgre)
                {
                    Gubitak();
                }
            }
        }

        private bool DaLiJeDodirnuoDuha()
        {
            for (int brojDuha = 0; brojDuha < 3; brojDuha++)
            {
                if (redDuha[brojDuha] == redIgraca && kolonaDuha[brojDuha] == kolonaIgraca)
                {
                    return true;
                }
            }

            return false;
        }

        private Rectangle OdrediObnovu(string smer, int kolona, int red)
        {
            int x, y, sirina, visina;
            switch (smer)
            {
                case "gore":
                    x = prored + dimenzijaPolja * kolona;
                    y = prored + dimenzijaPolja * red;
                    sirina = dimenzijaPolja;
                    visina = dimenzijaPolja * 2;
                    break;
                case "dole":
                    x = prored + dimenzijaPolja * kolona;
                    y = prored + dimenzijaPolja * (red - 1);
                    sirina = dimenzijaPolja;
                    visina = dimenzijaPolja * 2;
                    break;
                case "desno":
                    x = prored + dimenzijaPolja * (kolona - 1);
                    y = prored + dimenzijaPolja * red;
                    sirina = dimenzijaPolja * 2;
                    visina = dimenzijaPolja;
                    break;
                case "levo":
                    x = prored + dimenzijaPolja * kolona;
                    y = prored + dimenzijaPolja * red;
                    sirina = dimenzijaPolja * 2;
                    visina = dimenzijaPolja;
                    break;
                default:
                    x = -1;
                    y = -1;
                    sirina = -1;
                    visina = -1;
                    break;
            }

            return new Rectangle(x, y, sirina, visina);
        }

        private bool DaLiJePomeranjeMoguce()
        {
            switch (smerIgraca)
            {
                case "gore":
                    return tabla[redIgraca - 1, kolonaIgraca] != 1;
                case "dole":
                    return tabla[redIgraca + 1, kolonaIgraca] != 1;
                case "desno":
                    return tabla[redIgraca, kolonaIgraca + 1] != 1;
                case "levo":
                    return tabla[redIgraca, kolonaIgraca - 1] != 1;
                default:
                    return false;

            }
        }

        private void Pomeri()
        {
            switch (smerIgraca)
            {
                case "gore":
                    redIgraca--;
                    break;
                case "dole":
                    redIgraca++;
                    break;
                case "desno":
                    kolonaIgraca++;
                    break;
                case "levo":
                    kolonaIgraca--;
                    break;
            }

            if (tablaNovcica[redIgraca, kolonaIgraca])
            {
                tablaNovcica[redIgraca, kolonaIgraca] = false;
                brojNovcica--;
                if (zvuk == 1)
                {
                    if (daLiJeUkljucenZvuk)
                    {
                        pekmen_jede1Plejer.Play();
                        zvuk = 2;
                    }
                }
                else
                {
                    if (daLiJeUkljucenZvuk)
                    {
                        pekmen_jede2Plejer.Play();
                        zvuk = 1;
                    }
                }
                
            }


            if (brojNovcica == 0 && !krajIgre)
            {
                this.Invalidate(OdrediObnovu(smerIgraca, kolonaIgraca, redIgraca));
                Update();
                Pobeda();
            }
        }

        private void tajmerZaKretanjeDuha_Tick(object sender, EventArgs e)
        {
            brojac++;

            for (int brojDuha = 0; brojDuha < kolonaDuha.Length; brojDuha++)
            {
                if (brojac < 30 + dodatakBrojacu)
                {
                    string[] validniPotezi = OdrediValidnePoteze(smerDuha[brojDuha], redDuha[brojDuha], kolonaDuha[brojDuha], out int brojValidnihPoteza, out int indexSuprotnogSmera, brojDuha);

                    if (brojValidnihPoteza != 1)
                    {
                        int n = rnd.Next(1, 11);
                        if (n == 1)
                        {
                            smerDuha[brojDuha] = validniPotezi[indexSuprotnogSmera];
                        }
                        else
                        {
                            if (brojValidnihPoteza == 2)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (i != indexSuprotnogSmera)
                                    {
                                        smerDuha[brojDuha] = validniPotezi[i];
                                        break;
                                    }
                                }
                            }
                            else if (brojValidnihPoteza == 3)
                            {
                                int indexSledecegPoteza;
                                if (n >= 2 && n <= 5)
                                {
                                    indexSledecegPoteza = 0;
                                }
                                else
                                {
                                    indexSledecegPoteza = 1;
                                }

                                if (indexSledecegPoteza >= indexSuprotnogSmera)
                                {
                                    indexSledecegPoteza++;
                                }

                                smerDuha[brojDuha] = validniPotezi[indexSledecegPoteza];
                            }
                            else if (brojValidnihPoteza == 4)
                            {
                                int indexSledecegPoteza;
                                if (n >= 2 && n <= 4)
                                {
                                    indexSledecegPoteza = 0;
                                }
                                else if (n >= 5 && n <= 7)
                                {
                                    indexSledecegPoteza = 1;
                                }
                                else
                                {
                                    indexSledecegPoteza = 2;
                                }

                                if (indexSledecegPoteza >= indexSuprotnogSmera)
                                {
                                    indexSledecegPoteza++;
                                }

                                smerDuha[brojDuha] = validniPotezi[indexSledecegPoteza];
                            }
                        }
                    }
                    else
                    {
                        smerDuha[brojDuha] = validniPotezi[0];
                    }
                }
                else
                {
                    duhSlika1 = duhSlikaPametan;
                    duhSlika2 = duhSlikaPametan;
                    duhSlika3 = duhSlikaPametan;
                    tajmerZaKretanjeDuha.Interval = 240;
                    bool DaLiJePutNadjen = false;
                    int[,] kopiranLavirint = KopirajTablu(kolonaDuha[brojDuha], redDuha[brojDuha], tabla);
                    string naredniSmerDuha = null;
                    NadjiNajkraciPut(brojDuha, kolonaDuha[brojDuha], redDuha[brojDuha], kopiranLavirint, 1, ref DaLiJePutNadjen, ref naredniSmerDuha);

                    smerDuha[brojDuha] = naredniSmerDuha;

                    if (brojac > 70 - dodatakBrojacu)
                    {
                        brojac = 0;
                        dodatakBrojacu--;
                        duhSlika1 = Image.FromFile("duh1.png");
                        duhSlika2 = Image.FromFile("duh2.png");
                        duhSlika3 = Image.FromFile("duh3.png");
                        tajmerZaKretanjeDuha.Interval = 375;
                    }

                }

                stariRedDuha = redDuha[brojDuha];
                staraKolonaDuha = kolonaDuha[brojDuha];

                switch (smerDuha[brojDuha])
                {
                    case "gore":
                        redDuha[brojDuha]--;
                        break;
                    case "dole":
                        redDuha[brojDuha]++;
                        break;
                    case "desno":
                        kolonaDuha[brojDuha]++;
                        break;
                    case "levo":
                        kolonaDuha[brojDuha]--;
                        break;
                }

                this.Invalidate(OdrediObnovu(smerDuha[brojDuha], kolonaDuha[brojDuha], redDuha[brojDuha]));
                crtanjeJednogNovcica = true;
                Update();

                if (redDuha[brojDuha] == redIgraca && kolonaDuha[brojDuha] == kolonaIgraca && !krajIgre)
                {
                    Gubitak();
                }
            }


        }

        private void NacrtajJedanNovcic(Graphics grafika, int kolona, int red)
        {
            int proredNovcica = dimenzijaPolja / 5 * 2;
            int dimenzijaNovcica = dimenzijaPolja - 2 * proredNovcica;

            int x = prored + proredNovcica + kolona * dimenzijaPolja;
            int y = prored + proredNovcica + red * dimenzijaPolja;

            if (tablaNovcica[red, kolona])
            {
                grafika.FillEllipse(cetkaNovcica, x, y, dimenzijaNovcica, dimenzijaNovcica);
            }
        }

        private void NadjiNajkraciPut(int brojDuha, int trenutnoX, int trenutnoY, int[,] kopiranLavirint, int brojac, ref bool daLiJePutNadjen, ref string naredniSmerDuha)
        {
            if (trenutnoX == kolonaIgraca && trenutnoY == redIgraca)
            {
                //IspisiMatricu(kopiranLavirint);
                daLiJePutNadjen = true;
                naredniSmerDuha = PronadjiSmerDuha(kopiranLavirint, brojDuha);
                return;
            }

            int[,] noviPotezi = IzmeniPoteze(trenutnoX, trenutnoY);

            for (int i = 0; i < noviPotezi.GetLength(0) && !daLiJePutNadjen; i++)
            {
                int novoX = trenutnoX + noviPotezi[i, 0];
                int novoY = trenutnoY + noviPotezi[i, 1];

                if (novoX >= 0 && novoX < kopiranLavirint.GetLength(1) &&
                    novoY >= 0 && novoY < kopiranLavirint.GetLength(0) &&
                    kopiranLavirint[novoY, novoX] == 0 && !daLiJePutNadjen)
                {
                    kopiranLavirint[novoY, novoX] = ++brojac;
                    NadjiNajkraciPut(brojDuha, novoX, novoY, kopiranLavirint, brojac, ref daLiJePutNadjen, ref naredniSmerDuha);
                    kopiranLavirint[novoY, novoX] = 0;
                    brojac--;
                }
            }


        }

        private string PronadjiSmerDuha(int[,] kopiranLavirint, int brojDuha)
        {
            int redDuha = this.redDuha[brojDuha];
            int kolonaDuha = this.kolonaDuha[brojDuha];

            if (redDuha > 1 && kopiranLavirint[redDuha - 1, kolonaDuha] == 2)
            {
                return "gore";
            }
            else if (redDuha < brojRedova - 1 && kopiranLavirint[redDuha + 1, kolonaDuha] == 2)
            {
                return "dole";
            }
            else if (kolonaDuha > 1 && kopiranLavirint[redDuha, kolonaDuha - 1] == 2)
            {
                return "levo";
            }
            else if (kolonaDuha < brojKolona - 1 && kopiranLavirint[redDuha, kolonaDuha + 1] == 2)
            {
                return "desno";
            }

            return null;
        }

        private int[,] IzmeniPoteze(int trenutnoX, int trenutnoY)
        {
            int[,] noviPotezi = new int[potezi.GetLength(0), potezi.GetLength(1)];

            Struktura desno;
            desno.Udaljenost = kolonaIgraca - trenutnoX;
            desno.X = 1;
            desno.Y = 0;
            Struktura levo;
            levo.Udaljenost = trenutnoX - kolonaIgraca;
            levo.X = -1;
            levo.Y = 0;
            Struktura dole;
            dole.Udaljenost = redIgraca - trenutnoY;
            dole.X = 0;
            dole.Y = 1;
            Struktura gore;
            gore.Udaljenost = trenutnoY - redIgraca;
            gore.X = 0;
            gore.Y = -1;


            Struktura[] niz = { gore, dole, desno, levo };

            Array.Sort(niz, Uporedi);


            for (int i = 0; i < potezi.GetLength(0); i++)
            {
                noviPotezi[i, 0] = niz[i].X;
                noviPotezi[i, 1] = niz[i].Y;
            }

            return noviPotezi;
        }

        private int Uporedi(Struktura struktura1, Struktura struktura2)
        {
            return struktura2.Udaljenost - struktura1.Udaljenost;
        }

        private int[,] KopirajTablu(int kolonaDuha, int redDuha, int[,] tabla)
        {
            int[,] ispis = new int[brojRedova, brojKolona];

            for (int i = 0; i < brojRedova; i++)
            {
                for (int j = 0; j < brojKolona; j++)
                {
                    ispis[i, j] = -tabla[i, j];
                }
            }

            ispis[redDuha, kolonaDuha] = 1;

            return ispis;
        }

        private string[] OdrediValidnePoteze(string smerDuha, int redDuha, int kolonaDuha, out int brojValidnihPoteza, out int indexSuprotnogSmera, int brojDuha)
        {
            string[] izlaz = new string[4];
            brojValidnihPoteza = 0;
            indexSuprotnogSmera = -1;

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (DaLiJePomeranjeMoguceDuh(smerovi[i], kolonaDuha, redDuha))
                    {
                        izlaz[brojValidnihPoteza] = smerovi[i];

                        if (i == (IndexSmeraDuha(smerDuha) + 2) % 4)
                        {
                            indexSuprotnogSmera = brojValidnihPoteza;
                        }

                        brojValidnihPoteza++;
                    }
                }
                catch
                {
                    MessageBox.Show($"smer: {smerovi[i]}, kolona: {kolonaDuha}, redDuha {redDuha} ");
                }
            }

            return izlaz;
        }

        private int IndexSmeraDuha(string smerD)
        {
            for (int i = 0; i < 4; i++)
            {
                if (smerovi[i] == smerD) return i;
            }

            return -1;
        }

        private bool DaLiJePomeranjeMoguceDuh(string smerDuha, int kolonaDuha, int redDuha)
        {
            switch (smerDuha)
            {
                case "desno":
                    return tabla[redDuha, kolonaDuha + 1] != 1;
                case "dole":
                    return tabla[redDuha + 1, kolonaDuha] != 1;
                case "levo":
                    return tabla[redDuha, kolonaDuha - 1] != 1;
                case "gore":
                    return tabla[redDuha - 1, kolonaDuha] != 1;
                default:
                    return false;

            }
        }

        private void IspisiMatricu(int[,] matrica)
        {
            string izlaz = "";

            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    izlaz += matrica[i, j];

                    if (j < matrica.GetLength(1) - 1 && (matrica[i, j + 1] == -1 || matrica[i, j + 1] > 9))
                    {
                        izlaz += " ";
                    }
                    else
                    {
                        izlaz += "  ";
                    }
                }
                izlaz += $"\n";
            }

            MessageBox.Show(izlaz);
        }

        private void Pobeda()
        {
            if (daLiJeUkljucenZvuk)
            {
                pobedaPlejer.Play();
            }
            krajIgre = true;
            tajmerZaKretanjeDuha.Stop();
            tajmerZaOtvaranjeUsta.Stop();
            tajmerZaPomeranjePekmena.Stop();

            stanjeKrajaIgre = "pobeda";

            Point lokacija = this.Location;
            Form2 krajIgreForma = new Form2(lokacija, this.Width, this.Height);

            krajIgreForma.ShowDialog();

            tajmerZaNoviPocetak.Start();

        }
        private void Gubitak()
        {
            if (daLiJeUkljucenZvuk)
            {
                gubitakPlejer.Play();
            }
            krajIgre = true;
            tajmerZaKretanjeDuha.Stop();
            tajmerZaOtvaranjeUsta.Stop();
            tajmerZaPomeranjePekmena.Stop();

            stanjeKrajaIgre = "gubitak";


            Point lokacija = this.Location;
            Form2 krajIgreForma = new Form2(lokacija, this.Width, this.Height);

            krajIgreForma.ShowDialog();
            tajmerZaNoviPocetak.Start();
        }

        private void tajmerZaNoviPocetak_Tick(object sender, EventArgs e)
        {
            if (stanjeKrajaIgre == null)
            {
                NoviPocetak();
                tajmerZaNoviPocetak.Stop();
            }
        }

        struct Struktura
        {
            public int Udaljenost;
            public int X;
            public int Y;

            public override string ToString()
            {
                return $"udaljenost: {this.Udaljenost}, x: {this.X}, y: {this.Y}";
            }
        }

    }
}