using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChimaLib.Models
{
    public class SortFieldDefinition<TModel, TKey> : ISortFieldDefinition<TModel>
    {
        private Expression<Func<TModel, TKey>> KeySelector { get; set; }

        /// <summary>
        /// Objectのフィールドに対するソートを定義
        /// </summary>
        /// <param name="aKeySelector">要素からキーを抽出する関数(ラムダ式)</param>
        public SortFieldDefinition(Expression<Func<TModel, TKey>> aKeySelector){
            this.SortKey = ((MemberExpression)aKeySelector.Body).Member.Name;
            this.KeySelector = aKeySelector;
        }
        public string SortKey{ get;  private set; }

        private const string DESC_SUFFIX = " desc";
        /// <summary>
        /// UI上で選択した値によって、次に使うべきソートキーを決定
        /// </summary>
        /// <param name="aCurrentSortKey">ユーザー選択値</param>
        /// <returns></returns>
        public string GetNextSortKey(string aCurrentSortKey) {
            if (aCurrentSortKey == this.SortKey) {
                return this.SortKey + DESC_SUFFIX;
            }
            return this.SortKey;
            
        }

        /// <summary>
        /// ソートキーとユーザー選択値を元に、クエリ(IQueryable)にOrderByを追加します。
        /// </summary>
        /// <param name="aQuery">クエリ</param>
        /// <param name="aCurrentSortKey">ユーザー選択値</param>
        /// <returns></returns>
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
