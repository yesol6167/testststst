using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//절차지향
//함수지향
//---------------------------------- C
//객체지향
//---------------------------------- C++ ( class, struct )
namespace StudyCs9F
{    
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for(int i = 1; i < 10; ++i)
            {
                list.Add(i);
            }

            list.Remove(3);
            list.RemoveAt(1);
            list.RemoveAll(n => n >= 5 && n <= 7);

            if(list.Contains(9))
            {
                list.Add(900);
            }

            list.AddRange(new int[]{2,3,4,5,6});
            //list.AddRange(list);
            list.RemoveRange(0, 3);
            list.Insert(2, 200);
            list.InsertRange(1, new int[] { 8, 7 });
            //list.Clear();

            foreach(int n in list)
            {
                Console.Write($"[{n}]");
            }
            Console.WriteLine();
        }                               
    }
}
