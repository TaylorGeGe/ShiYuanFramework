using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sy.Tests
{
    public class TableTests
    {
        public class TestDataItem
        {
            public string Name { get; set; }

            public int Age { get; set; } 
        }


        public class TestDataTable : Table<TestDataItem>
        {


            public TableIndex<string, TestDataItem> NameIndex { get; private set; }
            public TableIndex<int, TestDataItem> AgeIndex { get; private set; }
            public TestDataTable()
            {
                NameIndex = new TableIndex<string, TestDataItem>(testdata => testdata.Name);
                AgeIndex = new TableIndex<int, TestDataItem>(testdata => testdata.Age);

            }
            protected override void OnAdd(TestDataItem item)
            {
                NameIndex.Add(item);
                AgeIndex.Add(item);
            }

            protected override void OnRemove(TestDataItem item)
            {
                NameIndex.Remove(item);
                AgeIndex.Remove(item);
            }

            protected override void OnClear()
            {
                NameIndex.Clear();
                AgeIndex.Clear();
            }
        }

        [Test]
        public void TableAddGetTest()
        {
            var table = new TestDataTable();

            for (var i = 0; i < 10; i++)
            {
                table.Add(new TestDataItem()
                {
                    Name = "名字" + i,
                    Age = i
                });
            }

            var result = table.Get(item => item.Age < 5);

            Assert.AreEqual(5, result.Count());
        }
        [Test]
        public void TableQuerySpeedTest()
        {
            var table = new TestDataTable();

            // 生成 300 个数据项
            for (var i = 0; i < 300; i++)
            {
                table.Add(new TestDataItem
                {
                    Name = string.Format("名字:{0}", i),
                    Age = i
                });
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            // 查询 10000 次
            for (var i = 0; i < 10000; i++)
            {
                foreach (var testDataItem in table.Get(item => item.Age == 150 && item.Name == "名字:150"))
                {

                }
            }

            var oldTime = stopWatch.ElapsedMilliseconds;

            UnityEngine.Debug.Log(oldTime);
            // 追加代码
            stopWatch.Reset();
            stopWatch.Start();

            // 查询 10000 次
            for (var i = 0; i < 10000; i++)
            {
                foreach (var testDataItem in table.AgeIndex.Get(150).Where(item => item.Name == "名字:150"))
                {

                }
            }

            var newTime = stopWatch.ElapsedMilliseconds;

            UnityEngine.Debug.Log(newTime);
            // ? 
        }
    }
}
