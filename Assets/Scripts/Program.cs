using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _220906004
{
    class Test
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            /* 기술면접에서 나올수도있음 ★
            //boxing unboxing => cpu의 일이 증가함 >> 최대한 사용하지 않게끔

            Test a = default; // 스택에 생성
            //boxing
            object o = a; // object는 부모라서 업캐스팅 가능 / 참조형 / o는 스택에 생성 / 힙에 int형 100의 값을 가진 a가 생성 후 o가 a를 참조
            //unboxing
            int b = (int)o;
        */
            //boxing unboxing
            ArrayList list = new ArrayList();
            list.Add(100);//boxing
            list.Add("Hi");//boxing

            foreach (object o in list)
            {
                Console.WriteLine(o.ToString()); //unboxing
            }
        }
    }
}
