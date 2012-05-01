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
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;


namespace PortugueseData.Migration
{
    class Program
    {
        #region Class Variables

        private static string SQL_FILE = @"C:\distritos-schema.sql";

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

        #region nHibernate Class Variables

        private static ISessionFactory sessionFactory;
        private static ISession currentSession;

        #endregion

        #endregion

        /// <summary>
        /// Migrates data from the xls file from: http://info.portaldasfinancas.gov.pt/pt/docs/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Initialize();

            string fileName = args[0];
            int startLine = int.Parse(args[1]);

            IList<ExcelItem> excelItems = GetExcelItems(fileName, startLine);
            SaveExcelItems(currentSession, excelItems);

            currentSession.Flush();
            currentSession.Close();
        }

        #region Helper Methods

        #region nHibernate Initialization

        private static void Initialize()
        {
            sessionFactory = CreateSessionFactory();
            currentSession = sessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(
                            MySQLConfiguration.Standard.ConnectionString(
                            c => c.FromConnectionStringWithKey("DistritosConnectionString")
                        )
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Distrito>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).SetOutputFile(SQL_FILE).Create(false,false);
        }

        #endregion

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

                    if (!dicConcelhos.ContainsKey(excelItem.CodigoDistrito + "-" + excelItem.CodigoConcelho))
                    {
                        GeneralBLL.CreateConcelho(session, excelItem.CodigoDistrito, excelItem.CodigoConcelho, excelItem.Concelho);
                        dicConcelhos.Add(excelItem.CodigoDistrito + "-" + excelItem.CodigoConcelho, excelItem.Concelho);
                    }

                    if (!dicFreguesias.ContainsKey(excelItem.CodigoDistrito + "-" + excelItem.CodigoConcelho + "-" + excelItem.CodigoFreguesia))
                    {
                        GeneralBLL.CreateFreguesia(session, excelItem.CodigoConcelho, excelItem.CodigoFreguesia, excelItem.Freguesia);
                        dicFreguesias.Add(excelItem.CodigoDistrito + "-" + excelItem.CodigoConcelho + "-" + excelItem.CodigoFreguesia, excelItem.Freguesia);
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
                    excelItem.CodigoConcelho = excelReader.GetString(1) + "-" + excelReader.GetString(3);
                    excelItem.Concelho = excelReader.GetString(4);
                    excelItem.CodigoFreguesia = excelReader.GetString(1) + "-" + excelReader.GetString(3) + "-" + excelReader.GetString(5);
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
