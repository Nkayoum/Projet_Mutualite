﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutualite.BO
{
   public class Caisse
    {
        public string Compte { get; set; }
        public int Montant { get; set; }
        public DateTime DateVersement { get; set; }
        public string ModePayement { get; set; }
        public string Commentaire { get; set; }

        public Caisse(string compte, int montant, DateTime dateVersement, string modePayement, string commentaire)
        {
            Compte = compte;
            Montant = montant;
            DateVersement = dateVersement;
            ModePayement = modePayement;
            Commentaire = commentaire;
        }

        public Caisse()
        {

        }

        
    }
}
