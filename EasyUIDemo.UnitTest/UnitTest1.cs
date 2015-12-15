using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyUIDemo.BLL;

namespace EasyUIDemo.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        UnitTestBLL bll = new UnitTestBLL();
        [TestMethod]
        public void TestMethod1()
        {
            decimal result = bll.Add(1, 2);
            Assert.AreEqual(result, 3);

            result = bll.Subtract(1, 2);
            Assert.AreEqual(result, -1);

            result = bll.Multiply(1, 2);
            Assert.AreEqual(result, 2);

            result = bll.Divide(1, 1);
            Assert.AreEqual(result, 1);


            //1、Assert类的使用

            Assert.Inconclusive();    //表示一个未验证的测试；

            Assert.AreEqual(1, 2);       //测试指定的值是否相等，如果相等，则测试通过；

            Assert.AreSame(1, 2);           //用于验证指定的两个对象变量是指向相同的对象，否则认为是错误

            Assert.AreNotSame(1, 2);       ///用于验证指定的两个对象变量是指向不同的对象，否则认为是错误

            Assert.IsTrue(1 == 2);             //测试指定的条件是否为True，如果为True，则测试通过；

            Assert.IsFalse(1 > 2);            //测试指定的条件是否为False，如果为False，则测试通过；

            Assert.IsNull(1);             //测试指定的对象是否为空引用，如果为空，则测试通过；

            Assert.IsNotNull(1);         //测试指定的对象是否为非空，如果不为空，则测试通过；

            //2、CollectionAssert类的使用

            //用于验证对象集合是否满足条件

            //3、StringAssert类的使用

            //用于比较字符串。

            StringAssert.Contains("213", "12");

            StringAssert.Matches("dfsfsd", new System.Text.RegularExpressions.Regex("aaa"));

            StringAssert.StartsWith("", "");
        }
    }
}
