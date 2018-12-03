using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIsleAdminHelp.enumObj;

namespace TheIsleAdminHelp.classe
{
    public class Dino
    {
        public Race Race { get; set; }
        public bool Genre { get; set; }
        public bool Carni { get; set; }
        public bool Bebe { get; set; }
        public bool Juvie { get; set; }
        public bool Subs { get; set; }
        public bool Adult { get; set; }

        public Dino(Race race, bool genre, string id)
        {
            Race = race;
            Genre = genre;
            Carni = race.isCarni();
            Grandeur(id);
        }

        private void Grandeur(string id)
        {
            if (id.Contains("Hatch"))
            {
                Bebe = true;
            }
            else if (id.Contains("Juv"))
            {
                Juvie = true;
            }
            else if (id.Contains("Sub"))
            {
                Subs = true;
            }
            else
            {
                Adult = true;
            }
        }
    }
}
