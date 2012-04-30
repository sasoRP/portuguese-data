using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace PortugueseData.DAL.Mappings
{
    public class DistritoMap : ClassMap<Distrito>
    {
        public DistritoMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.CodigoDistrito, "codigo").Length(20).Not.Nullable();
            Map(x => x.Designacao, "designacao").Length(150).Not.Nullable();

            HasMany<Concelho>(x => x.Concelhos).Cascade.All();
        }
    }
}
