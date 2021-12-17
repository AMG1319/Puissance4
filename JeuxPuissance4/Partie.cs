using System;

namespace JeuxPuissance4
{
    //Représente une partie qui réunit toutes les informations
    public class Partie
    {
        #region Variables
        Joueur[] joueur;                //Tableau qui contiendra les deux joueurs
        Plateau plateau;                //Plateau de jeu
        int NumPlayer;                  //Numéro du joueur qui est en train de jouer (0 ou 1)
        bool matchNul;                  //Booléen qui passe à true au cas d'un match nul
        #endregion

        #region Constructeur
        public Partie(Joueur joueur1, Joueur joueur2)
        {
            joueur = new Joueur[2];                         // Deux joueurs dans le tableau
            joueur[0] = joueur1;                            // Initialisatoin joueur 1
            joueur[1] = joueur2;                            // Initialisation joueur 2
            matchNul = false;                               // Initialisation du booléen à false
            plateau = new Plateau(joueur1, joueur2);        // Initialise le plateau
        }
        public Partie(Joueur joueur1, Joueur joueur2, int Num)
        {
            joueur = new Joueur[2];                         // Deux joueurs dans le tableau
            joueur[0] = joueur1;                            // Initialisatoin joueur 1
            joueur[1] = joueur2;                            // Initialisation joueur 2
            NumPlayer = Num;
            matchNul = false;                               // Initialisation du booléen à false
            plateau = new Plateau(joueur1, joueur2);        // Initialise le plateau
        }
        #endregion

        #region Méthodes
        /*Tire au sort le joueur en cours*/
        public Joueur tirerAuSortJoueur()
        {
            Random theRandom = new Random();
            int number = theRandom.Next();
            NumPlayer = number % 2;
            return GetJoueur();
        }
        public Joueur GetJoueur()
        {
            return joueur[NumPlayer];
        }
    /*Retourne un joueur du tableau*/
        public Joueur GetJoueur(int i)
        {
            return joueur[i];
        }
    /*Passe au joueur suivant*/
        public void PasserJoueurSuivant()
        {
            NumPlayer = (++NumPlayer) % 2;
        }
    /*Revoie le plateau*/
        public Plateau GetPlateau()
        {
            return plateau;
        }
    /*Renvoie le match nul*/
        public bool GetMatchNul()
        {
            return matchNul;
        }
        #endregion

        #region Accesseurs
        public int NumeroJoueurEnCours
        {
            get
            {
                return NumPlayer;
            }
        }
        #endregion
    }
}
