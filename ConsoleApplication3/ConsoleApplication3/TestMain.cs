using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication1.Enity;
using ConsoleApplication1.Helper;
using ConsoleApplication1.Model;

namespace ConsoleApplication3
{
    public class TestMain
    {
        static IList<int> GetPage(IList<int> list, int pageNumber, int pageSize = 10)
        {
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
        
        static IList<Transaction> GetPageHistory(IList<Transaction> list, int pageNumber, int pageSize = 10)
        {
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
        
        static void Main2()
        {
            // Console.WriteLine(Guid.NewGuid().ToString());
            // List<Transaction> transaction= new UserModel().transactionHisory();
            // IList<Transaction> iList= new UserModel().transactionHisory();
            var pageSize = 3;
            int pageItem = 0;
            // if (iList  is IList)
            // {
            //     pageItem = ((IList)iList).Count;
            // }
            var Pages = pageItem / pageSize;
            Console.WriteLine(Pages);    // Số trang
            Console.WriteLine(pageSize); // Số item trong 1 trang 
            Console.WriteLine(pageItem); // Tổng số item
            // for (int i = 0 ; i < pageItem; )
            // {
            //     Console.WriteLine("---------");
            //     Console.WriteLine(iList[i].Message);
            //     Console.WriteLine(iList[i+1].Message);
            //     Console.WriteLine(iList[i+2].Message);
            //     i += pageSize;
            //     Console.WriteLine(i);
            // }
            
            // int y = 3;
            // Console.WriteLine("---------");
            // Console.WriteLine(iList[y].Message);
            // Console.WriteLine(iList[y+1].Message);
            // Console.WriteLine(iList[y+2].Message);
            
            
            // IList<int> list = Enumerable.Range(1, 100).ToList();
            // // Console.WriteLine(Enumerable.Range(1, 100).ToList());
            // Console.WriteLine(String.Join(", ", GetPage(list, 1, 10)));
            // Console.WriteLine(String.Join(", ", GetPage(list, 2, 10)));
            // Console.WriteLine(String.Join(", ", GetPage(list, 3, 10)));
        }
    }
}