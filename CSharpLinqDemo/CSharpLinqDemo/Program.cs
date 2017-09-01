using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // BasicConcept();

           // QuerySyntax();
            QueryOperations();
        }

        //Linq的基本概念
        private static void BasicConcept()
        {
            //linq:language  Intergarted Query 语言整合的查询语句
            // select * from tableName
            //IEnumerable可用
            //Linq to SQL，Linq to SQL，Linq to DataSet，Linq to object

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //1.Query syntax
            var need_value1 = from num in numbers
                              where num % 2 == 0
                              orderby num
                              select num;
            //2.Method syntax
            var need_value2 = numbers.Where(p => p % 2 == 0).OrderBy(p => p);

            foreach (var i in need_value1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            foreach (var i in need_value2)
            {
                Console.Write(i + " ");
            }
            Console.ReadLine();
        }

        //Linq Query的基本组成
        public static void QuerySyntax()
        {
            //1. Data Source 数据源
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //2.Query creation Query语句
            //这里只是创建，并没有使用(推迟执行)
            var numQuery = from num in nums
                           where num % 2 == 0
                           orderby num
                           select num;
            //强制执行
            //1.可以调用对象的任意方法
            int count = numQuery.Count();
            //2.转成List或者Array
            numQuery.ToList();
            numQuery.ToArray();
            //3.Query execution  Query的执行
            foreach (var num in numQuery)
            {
                Console.Write(  "{0,1} " , num);
            }
            Console.ReadLine();
        }

        //Linq  Query的几种基本操作
        private static void QueryOperations()
        {
            int[] number = { 1, 2, 3, 4, 5 };

            var numValue = from num in number
                           where num%2==1&&num%3==1
                           //orderby 排序，descending降序排列 ascending升序排列
                           orderby num ascending
                           select num;

            List<Customer> customer = new List<Customer>();
            customer.Add(new Customer(){Name="CY",City="hz"});
            customer.Add(new Customer() { Name = "CC", City = "hz" });
            customer.Add(new Customer() { Name = "Ch", City = "bt" });
            List<Person> Person = new List<Person>();
            Person.Add(new Person() { Name = "CY", id = 101 });
            Person.Add(new Person() { Name = "Ci", id = 102 });

            //linq中的group使用,分组
            //into 与group结合使用 打包成一个临时分组，并且可以继续做筛选
            var queryCustomer = from c in customer
                                group c by c.City into CusGroup
                                where CusGroup.Count() >= 2
                                select new { GroupName = CusGroup.Key, Count = CusGroup.Count() };
            foreach (var ct in queryCustomer)
            {
                //Console.WriteLine(ct.Key);
                 //   foreach(var c in ct)
                  //  {
                  //      Console.WriteLine(c.City);
                  //  }
                Console.WriteLine("{0},{1}", ct.GroupName, ct.Count);   
            }

            var queryCustomer1 = customer.GroupBy(p => p.City).Select(p => p);
            foreach (var j in queryCustomer1)
            {
                Console.WriteLine(j.Key);
                foreach (var q in j)
                {
                    Console.WriteLine(q.Name);
                }
            }

         //linq中join的用法，用来关联俩个数据源中数据相同的部分
            var queryJoin = from c in customer
                            join q in Person on c.Name equals q.Name
                            select new { personName = c.Name, PersonId = q.id, PersonCity = c.City };
            foreach (var a in queryJoin)
            {
                Console.WriteLine("{0},{1},{2}", a.personName, a.PersonId, a.PersonCity);
            }

            
            //Linq中let的用法，起到一个中间变量的作用
            string[ ]  strings={"sad sadas sad asdasd asdsad sda "};
            var stringQuery = from str in strings
                              let words = str.Split(' ')
                              from word in words
                              let w = word.ToUpper()
                              select w;

            foreach (var c in stringQuery)
            {
                Console.WriteLine(c);
            } 
            Console.ReadLine();            
        }

        class Customer
        {
            public string City
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }
        }

        class Person {
            public string Name
            {
                get;
                set;
            }
            public int id
            {
                get;
                set;
            }
        }

    }
}
