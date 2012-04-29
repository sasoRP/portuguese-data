using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortugueseData.DAL;
using NHibernate;
using NHibernate.Criterion;

namespace PortugueseData.BLL
{
    public class GeneralBLL
    {
        public static Distrito GetDistrito(ISession session, string codigoDistrito)
        {
            ICriteria criteria = session.CreateCriteria<Distrito>();
            criteria.Add(Restrictions.Eq(Projections.Property<Distrito>(x => x.CodigoDistrito), codigoDistrito));

            return criteria.UniqueResult<Distrito>();
        }

        public static Concelho GetConcelho(ISession session, string codigoConcelho)
        {
            ICriteria criteria = session.CreateCriteria<Concelho>();
            criteria.Add(Restrictions.Eq(Projections.Property<Concelho>(x => x.CodigoConcelho), codigoConcelho));

            return criteria.UniqueResult<Concelho>();
        }

        public static Freguesia GetFreguesia(ISession session, string codigoFreguesia)
        {
            ICriteria criteria = session.CreateCriteria<Freguesia>();
            criteria.Add(Restrictions.Eq(Projections.Property<Freguesia>(x => x.CodigoFreguesia), codigoFreguesia));

            return criteria.UniqueResult<Freguesia>();
        }


        public static void CreateDistrito(ISession session, string codigoDistrito, string designacao)
        {
            Distrito distrito = new Distrito();
            distrito.CodigoDistrito = codigoDistrito;
            distrito.Designacao = designacao;

            session.Save(distrito);
            session.Flush();
        }

        public static void CreateConcelho(ISession session, string codigoDistrito, string codigoConcelho, string designacao)
        {
            Concelho concelho = new Concelho();
            concelho.Distrito = GetDistrito(session, codigoDistrito);
            concelho.CodigoConcelho = codigoConcelho;
            concelho.Designacao = designacao;

            session.Save(concelho);
            session.Flush();
        }

        public static void CreateFreguesia(ISession session, string codigoConcelho, string codigoFreguesia, string designacao)
        {
            Freguesia freguesia = new Freguesia();
            freguesia.CodigoFreguesia = codigoFreguesia;
            freguesia.Designacao = designacao;
            freguesia.Concelho = GetConcelho(session, codigoConcelho);

            session.Save(freguesia);
            session.Flush();
        }
    }
}
