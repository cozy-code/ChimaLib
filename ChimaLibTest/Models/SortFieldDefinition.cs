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
            Assert.AreEqual("Title", sortdef.SortKey);
        }
    }
}
