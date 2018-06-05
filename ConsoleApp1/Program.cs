using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp1
{
    class Rand
    {
        static public Random rander = new Random();
    }

    class A
    {
        int id;
        public A()
        {
            id = Rand.rander.Next();
        }
    }
    class B
    {
        A a = new A();
    }
    class AB
    {
        A a = new A();
        B b = new B();
    }
    class Hoge
    {
        AB ab = new AB();
        B b = new B();
    }

    class Program
    {
        static void TypeLog(object obj, int depth)
        {
            var t = obj.GetType();
            var fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (!fields.Any()) return;
            List<string> memberNames = new List<string>();
            foreach (var field in fields)
            {
                memberNames.Add(field.Name);
            }
            foreach (var name in memberNames)
            {
                var member = t.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                var v = member.GetValue(obj);
                if (obj.Equals(v)) return;
                for(int i = 0; i < depth; ++i) Console.Write("\t");
                Console.WriteLine(v.ToString());
                TypeLog(v, depth + 1);
            }
        }

        static void Main(string[] args)
        {
            Hoge hoge = new Hoge();

            TypeLog(hoge, 0);

            Console.Read();
        }
    }
}
