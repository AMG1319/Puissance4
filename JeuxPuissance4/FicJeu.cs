using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace JeuxPuissance4
{
    public partial class EcranJeu : Form
    {
        private Socket sServer, sClient;
        private Byte[] bBuffer;
        private bool ifserver;
        private string Out;

        private Partie partie;

        
        string[,] cases;


        Joueur joueur1 = null;
        string NJ1 = "Vide", SJ1="0";
        Color CJ1;

        Joueur joueur2 = null;
        string NJ2 = "Vide", SJ2="0";
        Color CJ2;

        Joueur joueurEnCours = null;
        string JEC = "Vide";
        string ChaineCon;
        private string ColJ = "Vide";
        public EcranJeu(bool a, string b, string NJ, Color CJ)
        {
            InitializeComponent();
            lbJ1.Text = lbJ2.Text = ""; //Initialisation des labels contenant le nom de chaque joueur
            sServer = null;
            sClient = null;
            bBuffer = new byte[256];
            ChaineCon = b;
            if (a == true)
            {
                Connecter(b);
                ifserver = false;
                NJ2 = NJ;
                CJ2 = CJ;
            }
            else
            {
                Ecouter();
                ifserver = true;
                NJ1 = NJ;
                CJ1 = CJ;
            }

        }
        
        #region SocketRegion
        private void Ecouter()
        {
            sClient = null;
            IPAddress ipServeur = AddressValide(Dns.GetHostName());
            sServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sServer.Bind(new IPEndPoint(ipServeur, 8001));
            sServer.Listen(1);
            sServer.BeginAccept(new AsyncCallback(SurDemandeConnexion), sServer);
        }
        private void Connecter(string a)
        {
            if (a.Length > 0)
            {
                sClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sClient.Blocking = false;
                IPAddress IPServeur = AddressValide(a);
                sClient.BeginConnect(new IPEndPoint(IPServeur, 8001), new AsyncCallback(Surconnexion), sClient);
            }
            else MessageBox.Show("Renseignez le serveur");
        }
        private IPAddress AddressValide(string MonPC)
        {
            IPAddress IpReponse = null;

            if (MonPC.Length > 0)
            {
                IPAddress[] ipMachine = Dns.GetHostEntry(MonPC).AddressList;
                for (int i = 0; i < ipMachine.Length; i++)
                {
                    Ping ping = new Ping();
                    PingReply pingReponse = ping.Send(ipMachine[i]);
                    if (pingReponse.Status == IPStatus.Success)
                        if (ipMachine[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            IpReponse = ipMachine[i];
                            break;
                        }
                }
            }
            return IpReponse;
        }
        private void Surconnexion(IAsyncResult iAR)
        {
            Socket Tmp = (Socket)iAR.AsyncState;
            if (Tmp.Connected)
            {
                InitialiserReception(Tmp);
                EnvoyerParam();
            }
            else
                MessageBox.Show("Serveur inaccessible");
        }
        private void SurDemandeConnexion(IAsyncResult iAR)
        {
            if (sServer != null)
            {
                Socket sTmp = (Socket)iAR.AsyncState;
                sClient = sTmp.EndAccept(iAR);
                InitialiserReception(sClient);
                EnvoyerParam();
            }
        }
        private void SurDemandeDeconnexion(IAsyncResult iAR)
        {
            Socket Tmp = (Socket)iAR.AsyncState;
            Tmp.EndDisconnect(iAR);
        }
        delegate void RenvoiVersInserer(string sTexte);
        private void InitialiserReception(Socket soc)
        {
            soc.BeginReceive(bBuffer, 0, bBuffer.Length, SocketFlags.None, new AsyncCallback(Reception), soc);
        }
        private void Reception(IAsyncResult iAR)
        {
            if (sClient != null)
            {
                Socket Tmp = (Socket)iAR.AsyncState;
                if (Tmp.EndReceive(iAR) > 0)
                {
                    InsererItem(Encoding.Unicode.GetString(bBuffer));
                    InitialiserReception(Tmp);
                }
                else
                {
                    Tmp.Disconnect(true);
                    Tmp.Close();
                    if (sServer != null)
                        sServer.BeginAccept(new AsyncCallback(SurDemandeConnexion), sServer);
                    sClient = null;
                }
            }
        }
        private void InsererItem(object oTexte)
        {
           if (lbJ1.InvokeRequired || lbJ2.InvokeRequired || clJ1.InvokeRequired || clJ2.InvokeRequired || NbVicJ1.InvokeRequired || NbVicJ2.InvokeRequired)
            {
                RenvoiVersInserer f = new RenvoiVersInserer(InsererItem);
                Invoke(f, new object[] { (string)oTexte });
            }
            else
            {
                Out = (string)oTexte;
                Recevoir(Out);
            }
        }
        public void Recevoir(string Jeu)
        {
            string[] JeuParam = Jeu.Split('/');
            string _NJ1, _CJ1, _SJ1, _NJ2 , _CJ2 , _SJ2 , _JEC ;            
            Color C1 = Color.Red, C2 = Color.Yellow;
            cases = new string[6, 7];

            switch (JeuParam[0])
            {
                case "1":
                    {
                        /*--------------------------------------EnvoyerParam----------------------------------------------------*/
                        _NJ1 = JeuParam[1];
                        _CJ1 = JeuParam[2];
                        _SJ1 = JeuParam[3];

                        _NJ2 = JeuParam[4];
                        _CJ2 = JeuParam[5];
                        _SJ2 = JeuParam[6];

                        _JEC = JeuParam[7];

                        _CJ1 = _CJ1.Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
                        _CJ2 = _CJ2.Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];

                        switch (_CJ1)
                        {
                            case "Red":
                                C1 = Color.Red;
                                break;
                            case "Yellow":
                                C1 = Color.Yellow;
                                break;
                            case "Green":
                                C1 = Color.Green;
                                break;
                            case "Blue":
                                C1 = Color.Blue;
                                break;
                        }
                        switch (_CJ2)
                        {
                            case "Red":
                                C2 = Color.Red;
                                break;
                            case "Yellow":
                                C2 = Color.Yellow;
                                break;
                            case "Green":
                                C2 = Color.Green;
                                break;
                            case "Blue":
                                C2 = Color.Blue;
                                break;
                        }
                        if (ifserver == true)
                        {
                            if (_NJ2 != "Vide")
                            {
                                NJ2 = _NJ2;
                                SJ2 = _SJ2;
                                CJ2 = C2;
                                if (NJ1 == NJ2 || CJ1 == CJ2)
                                {
                                    sServer.Close();
                                    MessageBox.Show("Le nom des joueurs ainsi que la couleur des jetons doivent être strictement différent !");                                   
                                    Close();
                                }
                                else
                                    CreerNouvellePartie();
                            }
                        }
                        else
                        {
                            if (_NJ1 != "Vide" )
                            {
                                NJ1 = _NJ1;
                                SJ1 = _SJ1;
                                CJ1 = C1;
                                if (NJ1 == NJ2 || CJ1 == CJ2)
                                {
                                    sClient.Send(Encoding.Unicode.GetBytes("Déconnexion (client)"));
                                    sClient.Shutdown(SocketShutdown.Both);
                                    sClient.BeginDisconnect(false, new AsyncCallback(SurDemandeDeconnexion), sClient);
                                    MessageBox.Show("Le nom des joueurs ainsi que la couleur des jetons doivent être strictement différent !");
                                    Close();
                                }
                                else if(_JEC != "Vide")
                                {
                                    JEC = _JEC;
                                    CreerNouvellePartie();
                                }                                
                            }
                        }
                        break;
                        /*------------------------------------------------------------------------------------------------------*/
                    }
                case "2":
                    {
                        btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = true;
                        /*--------------------------------------EnvoyerCol------------------------------------------------------*/
                        Point positionPion;             // Position du pion qui a été ajouté
                        Point positionPionTableLayout;  // Position du pion dans le table layout
                        PictureBox PBPion;              // Picture box du pion a ajouter
                        ColJ = JeuParam[1];
                        joueurEnCours = partie.GetJoueur();
                        // Joue la colonne
                        positionPion = joueurEnCours.Jouer(partie.GetPlateau(), Convert.ToInt32(ColJ));

                        // Si le pion a été ajouté correctement sur le plateau
                        if (positionPion.X != 0 && positionPion.Y != 0)
                        {
                            // Crée un nouveau pion a ajouter
                            PBPion = CreationPions(joueurEnCours.Couleur);

                            // Calcule les position dans le table layout
                            positionPionTableLayout = new Point((positionPion.Y - 1), (Plateau.nbLigne - 1) - (positionPion.X - 1));

                            // Ajoute le pion au tableLayout
                            tableLayoutPanel2.Controls.Add(PBPion, positionPionTableLayout.X, positionPionTableLayout.Y);

                            // Si la partie n'est pas finie
                            if (!VerifierFinPartie())
                            {
                                // Passe au joueur suivant
                                partie.PasserJoueurSuivant();
                            }
                            else
                            {
                                //Disable les buttons pour empecher les joueurs de continuer a mettre des jetons
                                btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = false;

                                // Si c'est un match nul
                                if (partie.GetPlateau().GetMatchNul())
                                {
                                    MessageBox.Show("Vous avez fait un match nul!");
                                }
                                else
                                {
                                    int y;
                                    MessageBox.Show(joueurEnCours.GetNom() + " a gagné");

                                    //a joute la vicotire au total
                                    joueurEnCours.SetScore();
                                    y = joueurEnCours.GetNum();

                                    //affiche la victoire au bon joueur
                                    if (y == 1)
                                    {
                                        NbVicJ1.Text = joueurEnCours.GetScore().ToString();
                                    }
                                    else
                                    {
                                        NbVicJ2.Text = joueurEnCours.GetScore().ToString();
                                    }
                                }
                            }
                        }
                        MettreAJourFenetre();
                        break;
                    }
                case "3":
                    break;

            }
                     
        }
        public void EnvoyerParam()
        {
            string Jeu = "1"+"/"+NJ1 + "/" + CJ1 + "/" + SJ1 + "/" + NJ2 + "/" + CJ2 + "/" + SJ2 + "/" + JEC + "/";
            EnvoyerSocket(Jeu);
        }
        public void EnvoyerCol()
        {
            string Jeu = "2"+"/"+ColJ+"/";
            EnvoyerSocket(Jeu);
        }
        public void EnvoyerCharger()
        {
            string Jeu = joueur1.GetNom() + "/" + joueur1.GetClr() + "/" + joueur1.GetScore() + "/" + joueur2.GetNom() + "/" + joueur2.GetClr() + "/" + joueur2.GetScore() + "/" + joueurEnCours.GetNom() + partie.GetPlateau().GetPlayerSave();
            EnvoyerSocket(Jeu);
        }
        public void EnvoyerSocket(string json)
        {
            if (sClient == null)
            {
                DialogResult r = MessageBox.Show("Client ou serveur inacessible", "Erreur de connexion",
                    MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                Environment.Exit(0);
            }
            else
            {
                sClient.Send(Encoding.Unicode.GetBytes(json));
            }

        }
        #endregion
        private PictureBox CreationPions(Color couleur)
        {
            PictureBox PBPion = new PictureBox();
            Bitmap bm = new Bitmap(32, 32);                                         // Création d'une bitmap
            Graphics g = Graphics.FromImage(bm);                                    // Création d'un objet graphics pour dessiner dessus
            //Brush b = new SolidBrush(couleur);
            LinearGradientBrush b = new LinearGradientBrush
                (new Rectangle(0, 0, 70, 30), couleur, Color.White, -45, false);    //Création d'un pinceau (dégradé)
            g.FillEllipse(b, 0, 0, bm.Size.Width, bm.Size.Height);                  // Dessinnage du pion
            PBPion.Image = bm;
            PBPion.SizeMode = PictureBoxSizeMode.StretchImage;    
            return PBPion;                                                          // Renvoie l'image
        }
        private void btnNewPartie_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous recommencer avec les mêmes joueurs ?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                RefaireUnePartie();
            else
                CreerNouvellePartie();
        }
        private void RefaireUnePartie()
        {
            Joueur joueur1 = null;
            Joueur joueur2 = null;
            Joueur joueurEnCours = null;

            //Enabled les bouton pour être pret a jouer
            btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = true;

            joueur1 = partie.GetJoueur(0);
            joueur2 = partie.GetJoueur(1);

            // Vide le plateau
            tableLayoutPanel2.Controls.Clear();

            // Met à jour le nom des joueurs
            lbJ1.Text = joueur1.GetNom();
            lbJ2.Text = joueur2.GetNom();

            // Crée la partie
            partie = new Partie(joueur1, joueur2);
            joueurEnCours = partie.tirerAuSortJoueur();
            MessageBox.Show(joueurEnCours.GetNom() + " Commence la partie");
            statusStrip1.Items[0].Text = joueurEnCours.GetNom() + " à toi de jouer";

            // Met à jour la fenetre
            MettreAJourFenetre();
        }
        
        private void CreerNouvellePartie()
        {
            joueur1 = new Joueur(NJ1, CJ1, 1);
            joueur2 = new Joueur(NJ2, CJ2, 2);

            // Vide le plateau
            tableLayoutPanel2.Controls.Clear();

            // Met à jour le nom des joueurs
            lbJ1.Text = joueur1.GetNom();
            lbJ2.Text = joueur2.GetNom();

            // Met à jour la couleur des joueurs
            clJ1.Text = joueur1.GetClr().ToString().Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
            clJ2.Text = joueur2.GetClr().ToString().Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
            // Met à Jour les scores
            NbVicJ1.Text = joueur1.GetScore().ToString();
            NbVicJ2.Text = joueur2.GetScore().ToString();

            joueur1.SetScore(int.Parse(SJ1));
            joueur2.SetScore(int.Parse(SJ2));

            // Vide le plateau
            tableLayoutPanel2.Controls.Clear();

            if (ifserver == true)
            {
                partie = new Partie(joueur1, joueur2);
                joueurEnCours = partie.tirerAuSortJoueur();
                JEC = joueurEnCours.GetNom();
                EnvoyerParam();
                if (JEC == joueur1.GetNom())
                {
                    btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = true;
                }
            }
            else
            {
                if (JEC == joueur2.GetNom())
                {
                    partie = new Partie(joueur1, joueur2, 1);
                    btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = true;
                }
                else
                {
                    partie = new Partie(joueur1, joueur2, 0);
                }
                joueurEnCours = partie.GetJoueur();
            }

            MessageBox.Show(joueurEnCours.GetNom() + " commence la partie");
            statusStrip1.Items[0].Text = " Au tour de " + joueurEnCours.GetNom() + " de jouer";

            MettreAJourFenetre();
                      
        }
        private void BtnJeu_Click(object sender, EventArgs e)
        {
            btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = false;
            Point positionPion;             // Position du pion qui a été ajouté
            Point positionPionTableLayout;  // Position du pion dans le table layout
            PictureBox PBPion;              // Picture box du pion a ajouter            
            if (partie != null)             // Si la partie est lancée
            {
                int ColonneJouee = 0;
                Joueur joueurEnCours = partie.GetJoueur();
                switch (((Button)sender).Name.ToString())
                {
                    case "btn1":
                        ColonneJouee = 1;
                        break;
                    case "btn2":
                        ColonneJouee = 2;
                        break;
                    case "btn3":
                        ColonneJouee = 3;
                        break;
                    case "btn4":
                        ColonneJouee = 4;
                        break;
                    case "btn5":
                        ColonneJouee = 5;
                        break;
                    case "btn6":
                        ColonneJouee = 6;
                        break;
                    case "btn7":
                        ColonneJouee = 7;
                        break;
                    default:
                        throw new Exception("Problème de bouton");
                }
                ColJ = Convert.ToString(ColonneJouee);
                EnvoyerCol();
                // Joue la colonne
                positionPion = joueurEnCours.Jouer(partie.GetPlateau(), ColonneJouee);

                // Si le pion a été ajouté correctement sur le plateau
                if (positionPion.X != 0 && positionPion.Y != 0)
                {
                    // Crée un nouveau pion a ajouter
                    PBPion = CreationPions(joueurEnCours.Couleur);

                    // Calcule les position dans le table layout
                    positionPionTableLayout = new Point((positionPion.Y - 1), (Plateau.nbLigne - 1) - (positionPion.X - 1));

                    // Ajoute le pion au tableLayout
                    tableLayoutPanel2.Controls.Add(PBPion, positionPionTableLayout.X, positionPionTableLayout.Y);

                    // Si la partie n'est pas finie
                    if (!VerifierFinPartie())
                    {
                        // Passe au joueur suivant
                        partie.PasserJoueurSuivant();
                    }
                    else
                    {
                        //Disable les buttons pour empecher les joueurs de continuer a mettre des jetons
                        btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = false;

                        // Si c'est un match nul
                        if (partie.GetPlateau().GetMatchNul())
                        {
                            MessageBox.Show("Vous avez fait un match nul!");
                        }
                        else
                        {
                            int y;
                            MessageBox.Show(joueurEnCours.GetNom() + " a gagné");

                            //a joute la vicotire au total
                            joueurEnCours.SetScore();
                            y = joueurEnCours.GetNum();

                            //affiche la victoire au bon joueur
                            if (y == 1)
                            {
                                NbVicJ1.Text = joueurEnCours.GetScore().ToString();
                            }
                            else
                            {
                                NbVicJ2.Text = joueurEnCours.GetScore().ToString();
                            }
                        }
                    }
                }
                MettreAJourFenetre();
            }
        }
        private bool VerifierFinPartie()
        {
            return partie.GetPlateau().VerifierFinPartie();
        }
        private void MettreAJourFenetre()
        {
            // Si la partie est lancée
            if (partie != null)
            {
                Joueur joueurEnCours = partie.GetJoueur();                                      //Joueur en cours --> GetJoueur sans argument lors de l'appel
                if (!partie.GetPlateau().VerifierFinPartie())                                   // Si la partie n'est pas finie
                {
                    statusStrip1.Items[0].Text = joueurEnCours.GetNom() + " à toi de jouer";    // Met à jour la barre d'état
                    //Change les couleurs des labels selon le tour du joueur
                    if (partie.NumeroJoueurEnCours == 0)
                    {
                        lbJ1.ForeColor = label2.ForeColor = label1.ForeColor = NbVicJ1.ForeColor = clJ1.ForeColor = partie.GetJoueur(0).GetClr();
                        lbJ2.ForeColor = label3.ForeColor = label4.ForeColor = NbVicJ2.ForeColor = clJ2.ForeColor = Color.Blue;
                    }
                    else
                    {
                        lbJ2.ForeColor = label3.ForeColor = label4.ForeColor = NbVicJ2.ForeColor = clJ2.ForeColor = partie.GetJoueur(1).GetClr();
                        lbJ1.ForeColor = label2.ForeColor = label1.ForeColor = NbVicJ1.ForeColor = clJ1.ForeColor = Color.Blue;
                    }
                }
                else                                                                            // Si la partie est finie
                {
                    if (partie.GetPlateau().GetMatchNul())                                      // Si il y a match nul
                    {
                        statusStrip1.Items[0].Text = "Match nul";
                    }
                    else
                    {
                        statusStrip1.Items[0].Text =
                            partie.GetPlateau().GetJoueurGagnant().GetNom() + " a gagné!";      // On affiche le gagnant
                    }
                }

            }
        }
        private void EcranJeu_Load(object sender, EventArgs e)
        {
        }
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            EnregistrerPartie();
        }

        private void EcranJeu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ifserver == true)
            {
                EcranAcceuil f = new EcranAcceuil(false, ChaineCon);
                f.ShowDialog();
            }
            else
            {
                EcranAcceuil f = new EcranAcceuil(true, ChaineCon);
                f.ShowDialog();
            }
        }

        private void EcranJeu_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void btnCharger_Click(object sender, EventArgs e)
        {
            ChargerPartie();
        }
        private void EnregistrerPartie()
        {
            Joueur joueur1, joueur2, joueurEnCours;

            joueur1 = partie.GetJoueur(0);
            joueur2 = partie.GetJoueur(1);
            joueurEnCours = partie.GetJoueur();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(saveFileDialog1.FileName))
                {
                    File.Create(saveFileDialog1.FileName).Close();
                    using (StreamWriter sw = File.AppendText(saveFileDialog1.FileName))
                    {
                        sw.WriteLine(joueur1.GetNom() + "/" + joueur1.GetClr() + "/" + joueur1.GetScore() + "/" + joueur2.GetNom() + "/" + joueur2.GetClr() + "/" + joueur2.GetScore() + "/" + joueurEnCours.GetNom()
                            + partie.GetPlateau().GetPlayerSave());
                    }
                }
                else
                {
                    File.WriteAllText(saveFileDialog1.FileName, String.Empty);
                    using (StreamWriter sw = File.AppendText(saveFileDialog1.FileName))
                    {
                        sw.WriteLine(joueur1.GetNom() + "/" + joueur1.GetClr() + "/" + joueur1.GetScore() + "/" + joueur2.GetNom() + "/" + joueur2.GetClr() + "/" + joueur2.GetScore() + "/" + joueurEnCours.GetNom()
                            + partie.GetPlateau().GetPlayerSave());
                    }
                }
            }
        }

        private void ChargerPartie()
        {
            // Déclare les joueurs
            Joueur joueur1 = null;
            Joueur joueur2 = null;
            Point PositionPion;             // Position du pion qui a été ajouté
            Point PositionPionTableLayout;  // Position du pion dans le table layout
            PictureBox PBPion;              // Picture box du pion a ajouter    
            string[,] cases;
            string text = "";
            string NJ1="", CJ1="", SJ1="", NJ2="", CJ2="", SJ2="", JEC="";
            cases = new string[6, 7];
            Color C1 = Color.Red, C2 = Color.Yellow;

            statusStrip1.Items[0].Text = "Chargement";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                text = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);
                NJ1 = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[0];
                CJ1 = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[1];
                SJ1 = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[2];
                NJ2 = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[3];
                CJ2 = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[4];
                SJ2 = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[5];
                JEC = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[6];

                CJ1 = CJ1.Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
                CJ2 = CJ2.Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
                
                switch (CJ1)
                {
                    case "Red":
                        C1 = Color.Red;
                        break;
                    case "Yellow":
                        C1 = Color.Yellow;
                        break;
                    case "Green":
                        C1 = Color.Green;
                        break;
                    case "Blue":
                        C1 = Color.Blue;
                        break;
                }
                switch (CJ2)
                {
                    case "Red":
                        C2 = Color.Red;
                        break;
                    case "Yellow":
                        C2 = Color.Yellow;
                        break;
                    case "Green":
                        C2 = Color.Green;
                        break;
                    case "Blue":
                        C2 = Color.Blue;
                        break;
                }
                int cpt = 7;
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 7; j++)
                    {
                        cases[i, j] = text.Split(new string[] { "/" }, 49, StringSplitOptions.None)[cpt];
                        cpt++;
                    }
            }
            //Enabled les bouton pour être pret a jouer
            btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = false;

            // Récupère le résultat et crée les joueurs
            joueur1 = new Joueur(NJ1, C1, 1);
            joueur2 = new Joueur(NJ2, C2, 2);

            joueur1.SetScore(int.Parse(SJ1));
            joueur2.SetScore(int.Parse(SJ2));

            // Vide le plateau
            tableLayoutPanel2.Controls.Clear();
            
            // Met à jour le nom des joueurs
            lbJ1.Text = joueur1.GetNom();
            lbJ2.Text = joueur2.GetNom();

            // Met à jour la couleur des joueurs
            clJ1.Text = joueur1.GetClr().ToString().Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
            clJ2.Text = joueur2.GetClr().ToString().Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];

            // Met à Jour les scores
            NbVicJ1.Text = joueur1.GetScore().ToString();
            NbVicJ2.Text = joueur2.GetScore().ToString();

            // Créé la partie
            if(JEC==joueur1.GetNom())
                partie = new Partie(joueur1, joueur2, 0);
            else
                partie = new Partie(joueur1, joueur2, 1);
            
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                {
                    if (cases[i, j] == joueur1.GetNom())
                    {
                        PositionPion = joueur1.Jouer(partie.GetPlateau(), j+1);
                        PBPion = CreationPions(joueur1.Couleur);
                        PositionPionTableLayout = new Point((PositionPion.Y - 1), (Plateau.nbLigne - 1) - (PositionPion.X - 1));
                        tableLayoutPanel2.Controls.Add(PBPion, PositionPionTableLayout.X, PositionPionTableLayout.Y);
                    }
                    else if (cases[i, j] == joueur2.GetNom())
                    {
                        PositionPion = joueur2.Jouer(partie.GetPlateau(), j+1);
                        PBPion = CreationPions(joueur2.Couleur);
                        PositionPionTableLayout = new Point((PositionPion.Y - 1), (Plateau.nbLigne - 1) - (PositionPion.X - 1));
                        tableLayoutPanel2.Controls.Add(PBPion, PositionPionTableLayout.X, PositionPionTableLayout.Y);
                    }
                }
            if (VerifierFinPartie())
            {
                btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = false;
                if (partie.GetPlateau().GetMatchNul())
                {
                    MessageBox.Show("Vous avez fait un match nul!");
                }
                else
                {
                    MessageBox.Show(partie.GetJoueur().GetNom() + " avait gagné, Recommencez pour vous vengez !");
                }
            }
            else
                btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = true;
            MettreAJourFenetre();  
        }
    }
}
