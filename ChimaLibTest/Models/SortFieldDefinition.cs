using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    }
}
