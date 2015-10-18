using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChimaLib.Models
{
    public class SortDefinition<TModel>
    {
        public IEnumerable<ISortFieldDefinition<TModel>> Fields { get; set; }
        public string SortKey { get; set; }

        public IQueryable<TModel> AddOrderBy(IQueryable<TModel> aQuery) {
            foreach (var field in this.Fields) { //ソート適用
                aQuery = field.AddOrderBy(aQuery, this.SortKey);
            }
            return aQuery;
        }
    }
}
