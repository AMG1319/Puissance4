using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JeuxPuissance4
{
    public partial class EcranAcceuil : Form
    {
        private Color CouleurJ1;
        private bool a;
        private string b;
        bool Capteur = false;

        private EcranJeu _RefJeu;
        public EcranJeu RefJeu
        {
            get { return _RefJeu; }
            set { _RefJeu = value; }
        }

        public EcranAcceuil(bool _a, string _b)
        {
            InitializeComponent();
            if (_a == false)
            {
                this.Text += " - Serveur";
                groupBox1.Text += " 1";
            }
                
            else
            {
                this.Text += " - Client";
                groupBox1.Text += " 2";
            }
                
            a = _a;
            b = _b;
        }
        private void EcranAcceuil_Load(object sender, EventArgs e)
        {
            InitialisationPictureBox();                             // Initialisation des picturebox
        }

    /* Initialise les picture box de la fenêtre*/
        private void InitialisationPictureBox()
        {
        /* Définition de l'image (pointeurs) de chaque pion*/
            Image PionRouge;
            Image PionJaune;
            Image PionVert;
            Image PionBleu;
        /*Création des images de chaque pions de couleur différente*/
            PionRouge = CreationPions(Color.Red);
            PionJaune = CreationPions(Color.Yellow);
            PionVert  = CreationPions(Color.Green);
            PionBleu  = CreationPions(Color.Blue);
        /*Attribution des images aux picturebox de la fenetre*/
            pbPionRouge1.Image = PionRouge;
            pbPionJaune1.Image = PionJaune;
            pbPionVert1.Image  = PionVert;
            pbPionBleu1.Image  = PionBleu;

        }
    /*Renvoie l'image d'un pion en fonction de sa couleur*/
        private Image CreationPions(Color couleur)
        {
            Bitmap bm = new Bitmap(32, 32);                                     // Création d'une bitmap
            Graphics g = Graphics.FromImage(bm);                                // Création d'un objet graphics pour dessiner dessus
            // Brush b = new SolidBrush(couleur);
            LinearGradientBrush b = new LinearGradientBrush
            (new Rectangle(0, 0, 70, 30), couleur, Color.White, -45, false);    //Création d'un pinceau (dégradé)
            g.FillEllipse(b, 0, 0, bm.Size.Width, bm.Size.Height);              // Dessinnage du pion
            return bm;                                                          // Renvoie l'image

        }

        #region RadioButton
    /*Choix de la couleur du joueur 1 via radiobutton*/
        private void R1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ1 = Color.Red;
        }
        private void J1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ1 = Color.Yellow;
        }
        private void V1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ1 = Color.Green;
        }
        private void B1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ1 = Color.Blue;
        }

        #endregion

        private void btnValider_Click(object sender, EventArgs e)
        {
            
            if((CouleurJ1 != Color.Blue && CouleurJ1 != Color.Green && CouleurJ1 != Color.Yellow && CouleurJ1 != Color.Red ) || tbNomJ1.Text=="")
            {
                MessageBox.Show("Veuillez insérez un nom et choisir une couleur svp");
            }
            else if (Capteur ==false)
            {
                Capteur = true;
                Hide();
                EcranJeu f = new EcranJeu(a, b,tbNomJ1.Text,CouleurJ1);
                f.RefAccueil = this;
               // RefJeu = f;
                f.ShowDialog();
            }
            else if(Capteur==true)
            {
                if(a==false)
                {
                    RefJeu.NJ1 = tbNomJ1.Text;
                    RefJeu.CJ1 = CouleurJ1;
                }
                else
                {
                    RefJeu.NJ2 = tbNomJ1.Text;
                    RefJeu.CJ2 = CouleurJ1;
                }                
                Hide();
                RefJeu.RefAccueil = this;
                RefJeu.Show();                
            }

        }
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
