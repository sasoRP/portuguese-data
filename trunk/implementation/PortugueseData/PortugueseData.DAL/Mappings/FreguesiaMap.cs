using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace PortugueseData.DAL.Mappings
{
    public class FreguesiaMap : ClassMap<Freguesia>
    {
        public FreguesiaMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.CodigoFreguesia, "codigo").Length(20).Not.Nullable();
            Map(x => x.Designacao, "designacao").Length(150).Not.Nullable();

            References<Concelho>(x => x.Concelho);
        }
    }
}
