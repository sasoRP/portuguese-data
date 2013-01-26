using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace PortugueseData.DAL.Mappings
{
    public class ConcelhoMap : ClassMap<Concelho>
    {
        public ConcelhoMap ()
	    {
            Table("concelhos");
            Id(x => x.Id, "id").GeneratedBy.Identity();
            Map(x => x.CodigoConcelho, "codigo").Length(20).Not.Nullable();
            Map(x => x.Designacao, "designacao").Length(150).Not.Nullable();

            HasMany<Freguesia>(x => x.Freguesias).Cascade.All();
            References<Distrito>(x => x.Distrito, "distrito_id");
	    }
    }
}
