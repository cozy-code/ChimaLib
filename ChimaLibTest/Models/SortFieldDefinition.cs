using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using ChimaLib.Models;

namespace ChimaLibTest.Models
{
    /// <summary>
    /// テスト用データクラス
    /// Class for test data
    /// </summary>
    public class Article
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Published { get; set; }
        public long Viewcount { get; set; }
    }

    [TestClass]
    public class SortFieldDefinition
    {
        [TestMethod]
        public void SortKey_Test1()
        {
            var sortdef = new ChimaLib.Models.SortFieldDefinition<Article, string>(obj => obj.Title);
            Assert.AreEqual("Title", sortdef.SortKey);  //キーを抽出する関数からソートキー文字列生成
        }

        [TestMethod]
        public void NextSortKey_Test1()
        {
            var sortdef = new ChimaLib.Models.SortFieldDefinition<Article, string>(obj => obj.Title);
            
            var next=sortdef.GetNextSortKey("Title");
            Assert.AreEqual("Title desc", next);    //昇順の次は降順

            next = sortdef.GetNextSortKey("Title desc");
            Assert.AreEqual("Title", next);         //降順の次は昇順

            next = sortdef.GetNextSortKey("unknown");
            Assert.AreEqual("Title", next);         //別のフィールドがソートキーの場合は昇順

        }

        [TestMethod]
        public void AddOrderBy_Test1() {
            var dummy_list = new Article[] {
                new Article() {Title="Z"},
                new Article() {Title="A"},
                new Article() {Title="M"}
            };
            var sortdef = new ChimaLib.Models.SortFieldDefinition<Article, string>(obj => obj.Title);

            var articles = (from a in dummy_list select a).AsQueryable();

            articles = sortdef.AddOrderBy(articles, "Title");  //ソートキー"Title"順に並べ替え
            var result = articles.ToArray();

            Assert.AreEqual("A", result[0].Title);
            Assert.AreEqual("M", result[1].Title);
            Assert.AreEqual("Z", result[2].Title);

        }

        [TestMethod]
        public void AddOrderBy_Test2() {
            var dummy_list = new Article[] {
                new Article() {Title="Z"},
                new Article() {Title="A"},
                new Article() {Title="M"}
            };
            var sortdef = new ChimaLib.Models.SortFieldDefinition<Article, string>(obj => obj.Title);

            var articles = (from a in dummy_list select a).AsQueryable();

            articles = sortdef.AddOrderBy(articles, "Title desc");  //ソートキー"Title"順に並べ替え
            var result = articles.ToArray();

            Assert.AreEqual("Z", result[0].Title);
            Assert.AreEqual("M", result[1].Title);
            Assert.AreEqual("A", result[2].Title);

        }

        [TestMethod]
        public void SortDef_New_Test1() {
            Article article=null;
            //理想的には Article.DefineSort(obj => obj.Title); みたいに書きたいけど無理っぽい。
            var sortdef = article.DefineSort(obj => obj.Title);
            Assert.AreEqual("Title", sortdef.SortKey);  //キーを抽出する関数からソートキー文字列生成
        }

        [TestMethod]
        public void SortDefInterface_New_Test1() {
            Article article = null;
            ISortFieldDefinition<Article> sortdef = article.DefineSort(obj => obj.Title);
            Assert.AreEqual("Title", sortdef.SortKey);
        }

        /// <summary>
        /// create dummy data
        /// </summary>
        /// <param name="num">num of data. must be an even number.</param>
        /// <returns></returns>
        private IQueryable<Article> TestData1(int num) {
            Article[] data = new Article[num];
            for(var i = 0; i < num; i++) {
                /* mixed index. if num=10 then 0,9,2,7,4,5,6,3,8,1 */
                var idx = (i % 2 == 0) ? i: num - i;
                System.Diagnostics.Debug.WriteLine(idx);
                data[i] = new Article() {
                    Title = "Title" + idx.ToString(),
                    Category = "Category" + idx.ToString(),
                    Published = new DateTime(2015, 10, idx+1),
                    Viewcount = idx
                };
            }
            return data.AsQueryable();
        }

        [TestMethod]
        public void MultiSortField_Asc_Test1() {
            var data = this.TestData1(10);
            Article article = null;
            ISortFieldDefinition<Article>[] sort_fields = new[] {   //ソート列定義
                article.DefineSort(a=>a.Title),
                article.DefineSort(a=>a.Viewcount),
                article.DefineSort(a=>a.Published),
            };

            foreach (var field in sort_fields) { //ソート適用
                data = field.AddOrderBy(data, "Title");
            }
            var result = data.ToArray();
            for(var i = 0; i < 10; i++) {
                Assert.AreEqual(i, result[i].Viewcount);
            }
        }

        [TestMethod]
        public void MultiSortField_Desc_Test1() {
            var data = this.TestData1(10);
            Article article = null;
            ISortFieldDefinition<Article>[] sort_fields = new[] {   //ソート列定義
                article.DefineSort(a=>a.Title),
                article.DefineSort(a=>a.Viewcount),
                article.DefineSort(a=>a.Published),
            };

            foreach (var field in sort_fields) { //ソート適用
                data = field.AddOrderBy(data, "Title desc");
            }
            var result = data.ToArray();
            for (var i = 0; i < 10; i++) {
                Assert.AreEqual(9-i, result[i].Viewcount);
            }
        }

    }
}
