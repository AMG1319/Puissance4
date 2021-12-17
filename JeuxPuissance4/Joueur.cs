using System.Drawing;

namespace JeuxPuissance4
{
    //Représente un joueur
    public class Joueur
    {
        #region Variables
        private string nom;     //Nom d'un joueur
        private int score;      //Score d'un joueur
        private int num;        //Numéro d'un joueur
        private Color Clr;      //Couleur du jeton d'un joueur
        #endregion

        #region Constructeur
        public Joueur(string Name, Color Color, int num)
        {
            nom = Name;
            this.num = num;
            Clr = Color;
            score = 0;
        }
        #endregion

        #region Méthodes
        /*Renvoie la position du pion joué*/
        public Point Jouer(Plateau Plat, int NumCol)
        {
            Point PositionPion;
            PositionPion = Plat.AjouterPion(this, NumCol);
            return PositionPion;
        }
        /*Retourne le numéro du joueur*/
        public int GetNum()
        {
            return num;
        }
        /*Retourne le nom du joueur*/
        public string GetNom()
        {
            return nom;
        }
        /*Retourne le score*/
        public int GetScore()
        {
            return score;
        }
        /*Affecte le score*/
        public void SetScore()
        {
            score += 1;
        }
        public void SetScore(int i)
        {
            score += i;
        }

        public Color GetClr()
        {
            return Couleur;
        }
        #endregion

        #region Accesseurs
        public Color Couleur
        {
            get
            {
                return Clr;
            }
            set
            {
                Clr = value;
            }
        }
        #endregion

    }
}
