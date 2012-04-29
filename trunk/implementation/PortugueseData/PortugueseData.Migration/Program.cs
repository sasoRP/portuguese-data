using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

using PortugueseData.BLL;
using NHibernate.Cfg;


namespace PortugueseData.Migration
{
    class Program
    {
        #region Class Variables

        /// <summary>
        /// Lista de distritos guardados com sucesso.
        /// </summary>
        private static IDictionary<string, string> dicDistritos = new Dictionary<string, string>();

        /// <summary>
        /// Lista de concelhos guardados com sucesso.
        /// </summary>
        private static IDictionary<string, string> dicConcelhos = new Dictionary<string, string>();

        /// <summary>
        /// Lista de freguesias guardadas com sucesso.
        /// </summary>
        private static IDictionary<string, string> dicFreguesias = new Dictionary<string, string>();

        /// <summary>
        /// Lista de erros.
        /// </summary>
        private static IList<ExcelItem> errors = new List<ExcelItem>();

        
        #endregion


        /// <summary>
        /// Migrates data from the xls file from: http://info.portaldasfinancas.gov.pt/pt/docs/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string fileName = args[0];
            int startLine = int.Parse(args[1]);

            IList<ExcelItem> excelItems = GetExcelItems(fileName, startLine);
            SaveExcelItems(session, excelItems);
        }

        #region Helper Methods

        private static void SaveExcelItems(ISession session, IList<ExcelItem> excelItems)
        {
            foreach (ExcelItem excelItem in excelItems)
            {
                try
                {
                    if (!dicDistritos.ContainsKey(excelItem.CodigoDistrito))
                    {
                        GeneralBLL.CreateDistrito(session, excelItem.CodigoDistrito, excelItem.Distrito);
                        dicDistritos.Add(excelItem.CodigoDistrito, excelItem.Distrito);
                    }

                    if (!dicConcelhos.ContainsKey(excelItem.CodigoConcelho))
                    {
                        GeneralBLL.CreateConcelho(session, excelItem.CodigoDistrito, excelItem.CodigoConcelho, excelItem.Concelho);
                        dicConcelhos.Add(excelItem.CodigoConcelho, excelItem.Concelho);
                    }

                    if (!dicFreguesias.ContainsKey(excelItem.CodigoFreguesia))
                    {
                        GeneralBLL.CreateFreguesia(session, excelItem.CodigoConcelho, excelItem.CodigoFreguesia, excelItem.Freguesia);
                        dicFreguesias.Add(excelItem.CodigoFreguesia, excelItem.Freguesia);
                    }

                }
                catch (Exception ex)
                {
                    errors.Add(excelItem);
                    //@todo: log this.
                }
            }
        }

        private static IList<ExcelItem> GetExcelItems(string fileName, int startLine)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
