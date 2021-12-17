using System.Drawing;

namespace JeuxPuissance4
{
    //Représente un plateau de 7x6 case
    public class Plateau
    {
        #region Variables
        public static int nbLigne = 6;                      //Nombre de ligne 
        public static int nbColonne = 7;                    //Nombre de colonne
        Case[,] cases;                                      //Déclaration d'un tableau de case
        private Joueur joueur1, joueur2, winner;            //2 Joueurs d'une partie et le gagnant
        enum Etat : int { NonCommence, EnCours, Finie};     //Définition d'un type de variable "Etat", qui peut être non-commencé, en cours, ou finie.
        Etat etatPlateau;                                   //Déclaration d'une variable Etat, qui va contenir l'état du plateau
        bool matchNul;                                      //Booléen qui sera à true si on a à faire à un match null  
        #endregion

        #region Constructeur
        public Plateau(Joueur joueur1, Joueur joueur2)
        {
            cases = new Case[nbLigne, nbColonne];           //Initialisation du tableau à 6 lignes et 7 colonnes

            for (int i = 0; i < nbLigne; i++)
                for (int j = 0; j < nbColonne; j++)
                    cases[i, j] = new Case();               //Chaque case du tableau représentera une Case           
            this.joueur1 = joueur1;                         //Initialisation du joueur1
            this.joueur2 = joueur2;                         //Initialisation du joueur2
            etatPlateau = Etat.NonCommence;                 //Initialisation de l'état du plateau
            matchNul = false;                               //Match null est mis à false pour l'initialisation
        }
        #endregion

        #region Méthodes
        /*Ajoute le pion dans une colonne et renvoie la position de ce dernier, 
        si la position renvoyé vaut (0,0) c'est qu'il n'a pas été ajouté*/
        public Point AjouterPion(Joueur Player, int NumCol)
        {
            int i;                                              //Variable i pour le parcours des boucles
            Point PositionPion;                                 //Variable de type point qui permettra de stocker la position du pion à renvoyer     

            if (etatPlateau == Etat.NonCommence)                //Si l'état correspond à non-commencé, on le met à en cours car une partie est commencé dès lors
                etatPlateau = Etat.EnCours;                     //on le met à en cours car une partie est commencé dès lors
            /*Vérifie si la colonne n'est pas pleine*/
            if (cases[nbLigne - 1, NumCol - 1].isUsed)          //Si la colonne est pleine
            {
                PositionPion = new Point(0, 0);
                return PositionPion;                            //on retourne (0,0)
            }
            /*la colonne n'est pas pleine*/
            else
            {
                for (i = 0; cases[i, NumCol - 1].isUsed; i++)   //Boucle qui parcourt la colonne jusqu'à la ligne non remplie
                {
                }
                cases[i, NumCol - 1].OccuperCase(Player);       //Le joueur occupe la case libre de la colonne choisie
            }
            PositionPion = new Point(i + 1, NumCol);            
            return PositionPion;                                //On retourne la position du pion après qu'il a occupé une case
        }
        /*Vérifie l'alignement et retourne true si un alignement a été trouvé et false si non*/
        private bool CheckAlignement(int oCol, int oLigne, int dCol, int dLigne)       
        {
        /*o de occupé et d de déplacement dans les paramètres*/

            Joueur ancienJoueur = null;                                                 // Joueur qui occupe la case précédante
            Joueur joueurOccupant;                                                      // Joueur qui occupe la case actuel
            int compteur = 0;                                                           // Compteur de case aligner
            int Col = oCol;                                                             // Colonne Actuel
            int Row = oLigne;                                                           // Ligne Actuel

            while ((Col >= 0) && (Col < nbColonne) && (Row >= 0) && (Row < nbLigne))    // Tant qu'on est dans les limites du tableau 7x6
            {
                joueurOccupant = cases[Row, Col].GetOccupant();                         // Récupère le joueur Occupant
                if (joueurOccupant == null)                                             // Si Pas de joueur occupant 
                {
                    compteur = 1;                                                       // Le compteur est réinitialisé
                    ancienJoueur = null;                                                // Il n'y a pas de joueur ancien dans ce cas 
                }
                else                                                                    // S'il y'a un joueur occupant
                {                    
                    if (joueurOccupant != ancienJoueur)                                 // Si l'ancien joueur est différent du joueur occupant
                    {
                        compteur = 1;                                                   // Le compteur est réinitialisé
                        ancienJoueur = joueurOccupant;                                  // Le joueur occupant devient l'ancien joueur
                    }
                    else                                                                // Si le joueur occupant est le même que l'ancien occupant
                    {
                        compteur++;                                                     // Le compteur est incrémenter pour compter le nombre de fois qu'on rencontre le même joueur sur une case
                    }
                }
                if ((joueurOccupant != null) && (compteur == 4))                        // Si le joueur occupant occupe toujours, et que le compteur a compté 4 alignement
                {
                    winner = joueurOccupant;                                            // Ce joueur devient alors le joueur gagnant
                    return true;                                                        // On renvoie true car gagant et alignement trouvé
                }
                Col += dCol;                                                            // On continue le parcours en colonne selon les cases de déplacement reçues en argument (parametres)
                Row += dLigne;                                                          // On continue le parcours en ligne selon les cases de déplacement reçues en argument (parametres)
            }
            return false;                                                               // Aucun alignement trouvé, on retourne faux
        }
        /*Vérifie si le plateau de jeu est plein*/
        public bool VerifierPlateauPlein()
        {
            bool plateauPlein = true;                                       // On initialise la variable booléenne à true
            for (int i = 0; i < nbColonne; i++)                             // Pour chaque colonne
            {
                if (!(cases[nbLigne - 1, i].isUsed))                        // On vérifie Si la dernière ligne n'est pas occupée par un joueur
                {
                    plateauPlein = false;                                   // Alors Variable booléenne mise à false 
                }
            }
            return plateauPlein;                                            // On retourne le booléen et il sera True si le plateau est plein, sinon il sera false
        }
        /*Vérifie si un joueur a gagné et donc si un alignement est trouvé*/
        public bool VerifierFinPartie()
        {
            /*Vérifie les horizontales(--)
             *On commence à la colonne 0 et de la ligne 0 à la ligne 5.
             *On parcourt en avançant d'une colonne et de zéro ligne.       
             */
            for (int ligne = 0; ligne < nbLigne; ligne++)
            {
                if (CheckAlignement(0, ligne, 1, 0))                
                {
                    return true;
                }
            }
            /*Vérifie les verticales (¦)
             *On commence de la ligne 0 et de la colonne 0 à la colonne 6. 
             *On parcourt en avançant d'une ligne et de zéro colonne.       
             */
            for (int col = 0; col < nbColonne; col++)
            {
                if (CheckAlignement(col, 0, 0, 1))
                {
                    return true;
                }
            }
            /*Vérifie les diagonales depuis le bas
             *On commence de la ligne 0 et de la colonne 0 à la colonne 6.
             *Première diagonale (/) :
             *On parcourt en avançant d'un ligne et d'une colonne.
             *Deuxième diagonale (\) :
             *On parcourt en avançant d'une ligne et en reculant d'une colonne.
             */
            for (int col = 0; col < nbColonne; col++)
            {
                if (CheckAlignement(col, 0, 1, 1))       
                {
                    return true;
                }
                if (CheckAlignement(col, 0, -1, 1))
                {
                    return true;
                }
            }
            /*Vérifie les diagonales depuis les colonnes gauches et droites
             *Première diagonale (/) :
             *On commence de la colonne 0 et de la ligne 0 à la ligne 5.
             *On parcourt en avançant d'un ligne et d'une colonne.
             *Deuxième diagonale (\) :
             *On commence de la colonne 5 et de la ligne 0 à la ligne 5.
             *On parcourt en avançant d'une ligne et en reculant d'une colonne.
             */
            for (int ligne = 0; ligne < nbLigne; ligne++)
            {
                // Première diagonale ( / )
                if (CheckAlignement(0, ligne, 1, 1))
                {
                    return true;
                }
                // Deuxième diagonale ( \ )
                if (CheckAlignement(5, ligne, -1, 1))
                {
                    return true;
                }
            }
            /*Vérifie si le plateau est plein*/
            if (VerifierPlateauPlein())
            {
                matchNul = true;                        //Si plein, match null mis à true
                return true;                            //On renvoie true car jeu fini
            }
            return false;                               //Partie continue
        }
        /*renvoie le joueur gagnant*/
        public Joueur GetJoueurGagnant()
        {
            return winner;
        }
        /*Renvoie true si match nul*/
        public bool GetMatchNul()
        {
            return matchNul;
        }
        /*Réccupère l'occupant de chaque case pour l'enregistrement*/
        public string GetPlayerSave()
        {
            string chaine="";
            for (int i = 0; i < nbLigne; i++)
                for (int j = 0; j < nbColonne; j++)
                    if (cases[i, j].GetOccupant() == null)
                        chaine += "/VIDE";
                    else
                        chaine += "/"+cases[i, j].GetOccupant().GetNom();
            return chaine;
        }
        #endregion

        #region Accesseurs
        public Joueur Joueur1
        {
            get
            {
                return joueur1;
            }
        }
        public Joueur Joueur2
        {
            get
            {
                return joueur2;
            }
        }
        #endregion
    }
}
