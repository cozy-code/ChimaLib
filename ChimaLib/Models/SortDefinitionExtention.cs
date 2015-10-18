using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChimaLib.Models
{
    public static class SortDefinitionExtention
    {
        public static string GetNextSortKey<TModel,TKey>(this SortDefinition<TModel> aSortDef, Expression<Func<TModel, TKey>> aSelector) {
            var targetKey = ((MemberExpression)aSelector.Body).Member.Name;
            var field = aSortDef.Fields.First((def) => def.SortKey == targetKey);
            return field.GetNextSortKey(aSortDef.SortKey);
        }
    }
}
