namespace JeuxPuissance4
{
    //Représente une case sur un plateau 7x6
    public class Case
    {
        #region Variables
        public bool isUsed;     //Case occupé par un joueur
        Joueur Player;          //Joueur qui occupe la case
        #endregion

        #region Constructeur
        //Constructeur d'une case vide sans occupant
        public Case()
        {
            isUsed = false;
            Player = null;
        }
        #endregion

        #region Méthodes
        /*Occuppation d'une case*/
        public bool OccuperCase(Joueur Player)
        {
            /*Si case est libre le joueur occupe la case et on renvoie true*/
            if(!isUsed)
            {
                this.Player = Player;
                isUsed = true;
                return true;
            }
            /*Si la case est occupé on renvoie false*/
            else
            {
                return false;
            }
        }
        /* Connaitre l'occupant de la case*/
        public Joueur GetOccupant()
        {
            return this.Player;
        }
        #endregion
    }
}
