using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sy
{
    public class LINQExample : MonoBehaviour
    {
        public class Student
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        void Start()
        {
            var students = new List<Student>()
            {
                new Student() {Name = "凉鞋", Age = 18},
                new Student() {Name = "hor", Age = 16},
                new Student() {Name = "天赐", Age = 17},
                new Student() {Name = "阿三", Age = 18}
            };

            // 1.基本的遍历
            students.ForEach(s => Debug.Log(s.Name));

            // 2.基本的条件过滤（Age > 5)
            students.Where(s => s.Age > 5)
                .ToList()
                .ForEach(s => Debug.Log(s.Name));


            // 与以上代码等价
            (from s in students
                    where s.Age > 5
                    select s)
                .ToList()
                .ForEach(s => Debug.Log(s.Name));

            // 3.基本的变换（student 转换成 name）
            students.Select(s => s.Name)
                .ToList()
                .ForEach(name => Debug.Log(name));

            // 与以上代码等价
            (from s in students
                    select s.Name)
                .ToList()
                .ForEach(name => Debug.Log(name));

            // 4.基本的分组（使用学生的名字分组）
            students.GroupBy(s => s.Name)
                .ToList()
                .ForEach(group => Debug.Log(group.Count()));

            // 与以上代码等价
            (from s in students
                    group s by s.Name)
                .ToList()
                .ForEach(group => Debug.Log(group.Count()));

            // 等等
        }
    }
}