using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortugueseData.DAL
{
    public class Distrito
    {
        public int Id { get; set; }
        public string CodigoDistrito { get; set; }
        public String Designacao { get; set; }

        public IList<Concelho> Concelhos { get; set; }

        public Distrito()
        {
            this.Concelhos = new List<Concelho>();
        }
    }
}
