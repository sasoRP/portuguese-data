using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortugueseData.DAL
{
    public class Concelho
    {
        public int Id { get; set; }
        public string CodigoConcelho { get; set; }
        public String Designacao { get; set; }

        public IList<Freguesia> Freguesias { get; set; }
        public Distrito Distrito { get; set; }

        public Concelho()
        {
            this.Freguesias = new List<Freguesia>();
        }
    }
}
