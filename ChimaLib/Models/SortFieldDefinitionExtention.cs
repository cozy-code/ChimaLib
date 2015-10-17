using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChimaLib.Models
{
    public static class SortFieldDefinitionExtention
    {
        public static ISortFieldDefinition<TModel> DefineSort<TModel, TKey>(this TModel aModel, Expression<Func<TModel, TKey>> aKeySelector) {
            return new SortFieldDefinition<TModel, TKey>(aKeySelector);
        }
    }
}
