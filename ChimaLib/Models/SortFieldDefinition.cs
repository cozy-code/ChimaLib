using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChimaLib.Models
{
    public class SortFieldDefinition<TModel, TKey>
    {
        private Expression<Func<TModel, TKey>> KeySelector { get; set; }

        public SortFieldDefinition(Expression<Func<TModel, TKey>> aKeySelector){
            this.SortKey = ((MemberExpression)aKeySelector.Body).Member.Name;
            this.KeySelector = aKeySelector;
        }
        public string SortKey{ get;  private set; }

        private const string DESC_SUFFIX = " desc";
        public string GetNextSortKey(string aCurrentSortKey) {
            if (aCurrentSortKey == this.SortKey) {
                return this.SortKey + DESC_SUFFIX;
            }
            return this.SortKey;
            
        }

        public IQueryable<TModel> AddOrderBy(IQueryable<TModel> aQuery, string aCurrentSortKey) {
            if (aCurrentSortKey == this.SortKey) {
                return aQuery.OrderBy(this.KeySelector);
            } else if(aCurrentSortKey == this.SortKey + DESC_SUFFIX) {
                return aQuery.OrderByDescending(this.KeySelector);
            }
            return aQuery;
        }
    }
}
