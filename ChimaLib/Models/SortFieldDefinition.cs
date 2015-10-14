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
        public SortFieldDefinition(Expression<Func<TModel, TKey>> aKeySelector){
            this.SortKey = ((MemberExpression)aKeySelector.Body).Member.Name;
        }

        public string SortKey{ get;  private set; }

        private const string DESC_SUFFIX = " desc";
        public string GetNextSortKey(string aCurrentSortKey) {
            if (aCurrentSortKey == this.SortKey) {
                return this.SortKey + DESC_SUFFIX;
            }
            return this.SortKey;
            
        }
    }
}
