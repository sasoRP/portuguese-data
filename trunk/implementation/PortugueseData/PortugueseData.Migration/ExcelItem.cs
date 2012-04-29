using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortugueseData.Migration
{
    public class ExcelItem
    {
        /// <summary>
        /// Coluna A.
        /// </summary>
        public string CodigoFinancas { get; set; }

        /// <summary>
        /// Coluna B.
        /// </summary>
        public string CodigoDistrito { get; set; }

        /// <summary>
        /// Coluna C.
        /// </summary>
        public string Distrito { get; set; }

        /// <summary>
        /// Coluna D.
        /// </summary>
        public string CodigoConcelho { get; set; }

        /// <summary>
        /// Coluna E.
        /// </summary>
        public string Concelho { get; set; }

        /// <summary>
        /// Coluna F.
        /// </summary>
        public string CodigoFreguesia { get; set; }
        
        /// <summary>
        /// Coluna G.
        /// </summary>
        public string Freguesia { get; set; }
    }
}
