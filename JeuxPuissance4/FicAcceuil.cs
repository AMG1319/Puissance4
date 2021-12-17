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
        private Color CouleurJ1, CouleurJ2;
        public EcranAcceuil()
        {
            InitializeComponent();
        }
        private void EcranAcceuil_Load(object sender, EventArgs e)
        {
            InitialisationPictureBox();                             // Initialisation des picturebox
        }
        /* Méthode pour récupérer le pseudo des joueurs*/
        public string GetPseudoJ1()
        {
            return tbNomJ1.Text;
        }
        public string GetPseudoJ2()
        {
            return tbNomJ2.Text;
        }

    /* Méthode pour récupérer les couleurs des joueurs*/
        public Color GetCouleurJ1()
        {
            return CouleurJ1;
        }
        public Color GetCouleurJ2()
        {
            return CouleurJ2;
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
            pbPionRouge2.Image = PionRouge;
            pbPionJaune2.Image = PionJaune;
            pbPionVert2.Image  = PionVert;
            pbPionBleu2.Image  = PionBleu;
            CouleurJ1 = Color.Red;              // Met la couleur Rouge par defaut au joueur 1
            CouleurJ2 = Color.Yellow;           // Met la couleur Jaune par défaut au joueur 2
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
    /*Choix de la couleur du joueur 2 via radiobutton*/
        private void R2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ2 = Color.Red;
        }
        private void J2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ2 = Color.Yellow;
        }
        private void V2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ2 = Color.Green;
        }
        private void B2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJ2 = Color.Blue;
        }
        #endregion

    }
}
