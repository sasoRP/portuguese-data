using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

using PortugueseData.BLL;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PortugueseData.DAL;
using Excel;
using System.IO;


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
            IList<ExcelItem> excelItems = new List<ExcelItem>();

            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            for (int i = 1; i < startLine; i++)
            {
                excelReader.Read();
            }

            while (excelReader.Read())
            {
                try
                {
                    ExcelItem excelItem = new ExcelItem();

                    excelItem.CodigoFinancas = excelReader.GetString(0);
                    excelItem.CodigoDistrito = excelReader.GetString(1);
                    excelItem.Distrito = excelReader.GetString(2);
                    excelItem.CodigoConcelho = excelReader.GetString(3);
                    excelItem.Concelho = excelReader.GetString(4);
                    excelItem.CodigoFreguesia = excelReader.GetString(5);
                    excelItem.Freguesia = excelReader.GetString(6);
                    excelItems.Add(excelItem);
                }
                catch (Exception ex)
                {
                    //@todo: log
                }
            }

            return excelItems;
        }

        #endregion
    }
}
