using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;


namespace JeuxPuissance4
{
    public partial class EcranJeu : Form
    {
        private Partie partie;
        public EcranJeu()
        {
            InitializeComponent();
            lbJ1.Text = lbJ2.Text = ""; //Initialisation des labels contenant le nom de chaque joueur
        }
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
            // Déclare les joueurs
            Joueur joueur1 = null;
            Joueur joueur2 = null;
            Joueur joueurEnCours = null;
            //Enabled les bouton pour être pret a jouer
            btn1.Enabled = btn2.Enabled = btn3.Enabled = btn4.Enabled = btn5.Enabled = btn6.Enabled = btn7.Enabled = true;

            // Crée une boite de dialogue pour demander le nom des joueurs
            EcranAcceuil DiagNom = new EcranAcceuil();
            do
            {
                // Lance la boite de dialogue
                if (DiagNom.ShowDialog() == DialogResult.OK)
                {
                    if (DiagNom.GetPseudoJ1() == "" || DiagNom.GetPseudoJ2() == "" || DiagNom.GetPseudoJ2() == DiagNom.GetPseudoJ1())
                        MessageBox.Show("Les nom doivent être différent et non nuls");
                    else
                    {
                        // Récupère le résultat et crée les joueurs
                        joueur1 = new Joueur(DiagNom.GetPseudoJ1(), DiagNom.GetCouleurJ1(), 1);
                        joueur2 = new Joueur(DiagNom.GetPseudoJ2(), DiagNom.GetCouleurJ2(), 2);

                        // Vide le plateau
                        tableLayoutPanel2.Controls.Clear();

                        // Met à jour le nom des joueurs
                        lbJ1.Text = joueur1.GetNom();
                        lbJ2.Text = joueur2.GetNom();

                        // Met à jour la couleur des joueurs
                        clJ1.Text = joueur1.GetClr().ToString().Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
                        clJ2.Text = joueur2.GetClr().ToString().Split(new string[] { "[", "]" }, 3, StringSplitOptions.None)[1];
                        // Met à Jour les scores
                        NbVicJ1.Text = "0";
                        NbVicJ2.Text = "0";

                        // Créé la partie
                        partie = new Partie(joueur1, joueur2);
                        joueurEnCours = partie.tirerAuSortJoueur();
                        MessageBox.Show(joueurEnCours.GetNom() + " commence la partie");
                        statusStrip1.Items[0].Text = joueurEnCours.GetNom() + " à toi de jouer";

                        // Met à jour la fenetre
                        MettreAJourFenetre();
                    }
                }
                else
                {
                    Application.Exit();
                }
            } while (DiagNom.GetPseudoJ1() == "" || DiagNom.GetPseudoJ2() == ""||DiagNom.GetPseudoJ2()==DiagNom.GetPseudoJ1());
        }
        private void BtnJeu_Click(object sender, EventArgs e)
        {
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
            CreerNouvellePartie();
        }
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            EnregistrerPartie();
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
