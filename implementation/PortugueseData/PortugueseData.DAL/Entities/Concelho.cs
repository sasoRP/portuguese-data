using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortugueseData.DAL
{
    public class Concelho
    {
        public virtual int Id { get; private set; }
        public virtual string CodigoConcelho { get; set; }
        public virtual String Designacao { get; set; }

        public virtual IList<Freguesia> Freguesias { get; set; }
        public virtual Distrito Distrito { get; set; }

        public Concelho()
        {
            this.Freguesias = new List<Freguesia>();
        }
    }
}
