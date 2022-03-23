using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderService a = new OrderService();
            bool judge_ = true;
            while (judge_)
            {
                Console.WriteLine("请选择你要进行的操作：\n1.增加订单  2.删除订单  3.修改订单  4.查询订单  5.退出");
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1": a.addOrder(); break;
                    case "2": a.removeOrder(); break;
                    case "3": a.changeOrder(); break;
                    case "4": a.queryOrder(); break;
                    case "5": judge_ = false; break;
                    default: Console.WriteLine("输入错误"); break;
                }
            }
        }
    }
    public class OrderDetails
    {
        public int Id { get; set; }
        public double Mount { get; set; }
        public double Price { get; set; }
        public OrderDetails()//无参构造函数
        {
            this.Id = 0;
            this.Mount = 0;
            this.Price = 0;
        }
        public OrderDetails(int id, double price, double mount)
        {
            this.Id = id;
            this.Mount = price;
            this.Price = mount;
        }
    }
    public class Order
    {
        public int Id { get; set; }//订单号
        public string Name { get; set; }//客户
        public string Good { get; set; }//商品名称
        public double Price { get; set; }//价格
        public List<OrderDetails> orderItem = new List<OrderDetails>();//订单详细list
        public void RemoveOrderItem(int index1) //删除订单详细
        {
            this.orderItem.RemoveAt(index1);
        }

        public Order()//无参构造函数
        {
            this.Id = 0;
            this.Name = string.Empty;
            this.Good = string.Empty;
            this.Price = 0;
        }

        public Order(int id, string good, string customer, double money)
        {
            this.Id = id;
            this.Good = good;
            this.Name = customer;
            this.Price = money;
        }

        public int CompareTo(object obj)//重写排序函数
        {
            Order a = obj as Order;
            return this.Id.CompareTo(a.Id);
        }
        public override bool Equals(object obj)//重写比较函数
        {
            Order a = obj as Order;
            return this.Id == a.Id;
        }

        public override int GetHashCode()//重写哈希码函数
        {
            return Convert.ToInt32(Id);
        }

        public void addOrderItem(int id, double price, double mount)  //添加订单明细
        {
            OrderDetails a = new OrderDetails(id, price, mount);
            this.orderItem.Add(a);
        }

        public void showOrder()//显示订单
        {
            Console.WriteLine("订单号:{0}  商品名称:{1} 客户:{2} 订单金额:{3}", this.Id, this.Good, this.Name, this.Price);
        }

        public void showOrderItem()  //显示订单项
        {
            foreach (OrderDetails a in this.orderItem)
            {
                Console.WriteLine("订单号:{0} 单价:{1} 数量:{2}", a.Id, a.Price, a.Mount);
            }
        }

        public void changeOrderItem(int aa, int id, double price, double mount)//修改订单详细
        {
            int index = 0;
            bool find = false;
            foreach (OrderDetails m in this.orderItem)
            {
                if (m.Id == id)
                {
                    index = this.orderItem.IndexOf(m);
                    find = true;
                }

            }

            if (find)
            {
                this.orderItem[index] = new OrderDetails(id, price, mount);

            }
        }
        
       

    }
    public class OrderService : IOrderService    //所有订单
    {
        public List<Order> order = new List<Order>();//存储订单的List

        public OrderService()
        {

        }
        public void addOrder()          //增加订单
        {
            try
            {
                Console.WriteLine("请输入订单编号：");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("请输入商品名称：");
                string name = Console.ReadLine();

                Console.WriteLine("请输入客户名称：");
                string customer = Console.ReadLine();

                Console.WriteLine("请输入订单总金额：");
                double money = Convert.ToDouble(Console.ReadLine());

                Order a = new Order(id, name, customer, money);


                bool judge = true;
                bool same = false;
                foreach (Order m in this.order)//查找订单是否已经存在
                {
                    if (m.Equals(a)) same = true;
                }
                if (same) Console.WriteLine("订单重复");
                else//新订单继续添加订单项
                {
                    if (judge && !same)
                    {

                        Console.WriteLine("订单明细：");

                        Console.WriteLine("订单号：");
                        int id1 = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("请输入单价：");
                        double price1 = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("请输入数量：");
                        double mount1 = Convert.ToDouble(Console.ReadLine());

                        order.Add(a);
                        a.addOrderItem(id1, price1, mount1);
                        Console.WriteLine("添加订单成功");
                        Console.WriteLine("-------------------------");

                        bool judge_ = true;
                        while (judge_)
                        {
                            Console.WriteLine("是否继续添加订单：1.是  2.否");
                            string putin = Console.ReadLine();
                            switch (putin)
                            {
                                case "1": addOrder(); break;
                                case "2": judge_ = false; break;
                                default: Console.WriteLine("输入错误"); break;
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }

        }
        public void changeOrder()//修改订单
        {
            Console.WriteLine("请输入你要修改的订单号：");
            int id = Convert.ToInt32(Console.ReadLine());
            Order a = new Order(id, "", "", 0);

            bool same = false;
            foreach (Order m in this.order)
            {
                if (m.Equals(a)) same = true;
            }
            if (same)
            {
                Console.WriteLine("请修改您的订单：");

                Console.WriteLine("请输入商品名称：");
                string str0 = Console.ReadLine();

                Console.WriteLine("请输入客户名字：");
                string str1 = Console.ReadLine();

                Console.WriteLine("请输入单价：");
                double price1 = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("请输入数量：");
                double mount1 = Convert.ToDouble(Console.ReadLine());

                int index = 0;
                bool find = false;
                foreach (Order m in this.order)
                {
                    if (m.Id == id)
                    {
                        index = this.order.IndexOf(m);
                        find = true;
                    }
                }
                if (find)
                {
                    this.order[index] = new Order(id, str0, str1, price1 * mount1);
                    a.changeOrderItem(index, id, price1, mount1);//修改订单详细
                    Console.WriteLine("修改成功！");

                    bool judge_ = true;
                    while (judge_)
                    {
                        Console.WriteLine("是否继续修改订单：1.是  2.否");
                        string putin = Console.ReadLine();
                        switch (putin)
                        {
                            case "1": changeOrder(); break;
                            case "2": judge_ = false; break;
                            default: Console.WriteLine("输入错误"); break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("不存在你要修改的订单！");
                }
            }

        }

        public void removeOrder() //删除订单
        {
            Console.WriteLine("请输入订单号删除订单：");
            int id = Convert.ToInt32(Console.ReadLine());
            bool find = false;
            int index = 0;
            foreach (Order a in this.order)
            {
                if (a.Id == id)
                {
                    index = this.order.IndexOf(a);
                    find = true;
                }
            }
            if (find)
            {
                this.order.RemoveAt(index);
                this.order[index].RemoveOrderItem(index);
                Console.WriteLine("删除订单成功!");
                Console.WriteLine("-----------------");
            }
            else
            {
                Console.WriteLine("没有该订单!");
            }


        }

        public void queryOrder()  //查询订单
        {
            bool judge_ = true;
            while (judge_)
            {
                Console.WriteLine("请选择你要查询的方式：\n1.订单号  2.商品名称  3.客户  4.订单金额  5.退出");
                string choose1 = Console.ReadLine();
                switch (choose1)
                {
                    case "1":
                        Console.WriteLine("请输入你要查询的订单号：");
                        int id1 = Convert.ToInt32(Console.ReadLine());
                        var query1 = from s1 in order
                                     where s1.Id == id1
                                     orderby s1.Price
                                     select s1;
                        List<Order> a1 = query1.ToList();
                        if (query1.FirstOrDefault() != null)
                        {
                            foreach (Order b1 in a1)
                            {
                                b1.showOrder();//显示订单
                                b1.showOrderItem();//显示订单详细
                            }

                        }
                        else
                            Console.WriteLine("没有找到订单!");
                        break;
                    case "2":
                        Console.WriteLine("请输入你要查询的商品名称：");
                        string str1 = Console.ReadLine();
                        var query2 = from s1 in order
                                     where s1.Good == str1
                                     orderby s1.Price
                                     select s1;
                        List<Order> a2 = query2.ToList();
                        if (query2.FirstOrDefault() != null)
                        {
                            foreach (Order b2 in a2)
                            {
                                b2.showOrder();
                                b2.showOrderItem();
                            }

                        }
                        else
                            Console.WriteLine("没有找到订单!");
                        break;
                    case "3":
                        Console.WriteLine("请输入你要查询的客户：");
                        string str2 = Console.ReadLine();
                        var query3 = from s1 in order
                                     where s1.Name == str2
                                     orderby s1.Price
                                     select s1;
                        List<Order> a3 = query3.ToList();
                        if (query3.FirstOrDefault() != null)
                        {
                            foreach (Order b3 in a3)
                            {
                                b3.showOrder();
                                b3.showOrderItem();
                            }

                        }
                        else
                            Console.WriteLine("没有找到订单!");
                        break;
                    case "4":
                        Console.WriteLine("请输入你要查询的订单金额：");
                        double money = Convert.ToDouble(Console.ReadLine());
                        var query4 = from s1 in order
                                     where s1.Price == money
                                     orderby s1.Id
                                     select s1;
                        List<Order> a4 = query4.ToList();
                        if (query4.FirstOrDefault() != null)
                        {
                            foreach (Order b4 in a4)
                            {
                                b4.showOrder();
                                b4.showOrderItem();
                            }

                        }
                        else
                            Console.WriteLine("没有找到订单");
                        break;
                    case "5":
                        judge_ = false;
                        break;
                    default:
                        Console.WriteLine("输入错误");
                        break;
                }
            }

        }



    }

    public interface IOrderService
    {
        void addOrder();
        void removeOrder();
        void queryOrder();
    }
}
