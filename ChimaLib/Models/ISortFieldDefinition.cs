using System.Linq;

namespace ChimaLib.Models
{
    public interface ISortFieldDefinition<TModel>
    {
        string SortKey { get; }

        IQueryable<TModel> AddOrderBy(IQueryable<TModel> aQuery, string aCurrentSortKey);
        string GetNextSortKey(string aCurrentSortKey);
    }
}