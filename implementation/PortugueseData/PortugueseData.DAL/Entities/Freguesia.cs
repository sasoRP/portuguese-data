using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortugueseData.DAL
{
    public class Freguesia
    {
        public int Id { get; set; }
        public String CodigoFreguesia { get; set; }
        public String Designacao { get; set; }

        public Concelho Concelho { get; set; }
    }
}
