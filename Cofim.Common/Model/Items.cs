using System;
using System.Collections.Generic;
using System.Text;

namespace Cofim.Common.Model
{   
    public class Items
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public List<Items> AdquirentesLists()
        {
            List<Items> adquirentes = new List<Items>();
            adquirentes.Add(new Items { Id = "PF"  , Name = "Persona Física" });
            adquirentes.Add(new Items { Id = "PM"  , Name = "Persona Moral" });
            adquirentes.Add(new Items { Id = "PMNC", Name = "Persona Moral No Contribuyente" });
            adquirentes.Add(new Items { Id = "SI"  , Name = "Sociedades de Inversión" });
            
            return adquirentes;
        }
    }
}
