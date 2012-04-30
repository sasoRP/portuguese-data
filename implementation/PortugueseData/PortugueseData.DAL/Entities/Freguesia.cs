using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortugueseData.DAL
{
    public class Freguesia
    {
        public virtual int Id { get; private set; }
        public virtual String CodigoFreguesia { get; set; }
        public virtual String Designacao { get; set; }

        public virtual Concelho Concelho { get; set; }
    }
}
