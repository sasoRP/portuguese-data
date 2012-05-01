using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortugueseData.DAL
{
    public class Distrito
    {
        public virtual int Id { get; set; }
        public virtual string CodigoDistrito { get; set; }
        public virtual String Designacao { get; set; }

        public virtual IList<Concelho> Concelhos { get; set; }

        public Distrito()
        {
            this.Concelhos = new List<Concelho>();
        }
    }
}
