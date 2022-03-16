using System;
namespace shape
{
   
    public interface Shape
    {
        double calcuArea();
        bool isLegal();
    }
    class Square : Shape
    {
        double Side { set; get; }
        public double Area { get{ return calcuArea(); } }

        public Square(double side)
        {
            Side = side;        
        }
        public bool isLegal()
        {
            return Side>0;
        }
        public double calcuArea()
        {
            if (!isLegal())
            {
                Console.WriteLine("形状不正确！");
            }
            return Side * Side;
        }
    }
    class Triangle : Shape
    {
        double A { set; get; }
        double B { set; get; }
        double C { set; get; }
        public Triangle(double a,double b,double c)
        {
            A = a; B = b; C = c; 
        }
        public bool isLegal()
        {
            return A > 0 && B > 0 && C > 0 && A + B > C && A + C > B && B + C > A;
        }
        public double calcuArea()
        {
            if (!isLegal()) { Console.WriteLine("形状不正确！"); }
            return System.Math.Sqrt((A + B + C) * (A + B - C) * (A + C - B) * (B + C - A))/4;
        }
        public double Area()
        {
                return calcuArea(); 
        }
    }
    class Rectangle : Shape
    {
        double Length { set; get; }
        double Width { set; get; }

        public double Area
        {
            get { return calcuArea(); }
        }

        public Rectangle(double l, double w)
        {
            Length = l; Width = w;
        }
        public double calcuArea()
        {
            if (!isLegal())
            {
                Console.WriteLine("形状不正确");
            }
            return Length * Width;
        }
        public bool isLegal()
        {
            return Length > 0 && Width > 0;
        }
    }
    class ShapeFactory
    {
        public static Shape generateRandomShape(int seed)
        {
            Random rd = new Random(seed);
            int key = rd.Next(0, 3);
            Shape shape = null;
            Console.Write(key);
            switch (key)
            {
                case 0:
                    shape = new Square(rd.NextDouble() + rd.Next(0, 10));
                    Console.WriteLine("正方形"); break;
                case 1:
                    shape = new Triangle(rd.NextDouble() + rd.Next(2, 5), rd.NextDouble() + rd.Next(2, 5), rd.NextDouble() + rd.Next(2, 5));
                    Console.WriteLine("三角形"); break;
                case 2:
                    shape = new Rectangle(rd.NextDouble() + rd.Next(0, 10), rd.NextDouble() + rd.Next(0, 10));
                    Console.WriteLine("矩形"); break;
            }
            return shape;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            double allArea = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.Write("第" + (i + 1) + "个图形：");
                Shape sp = ShapeFactory.generateRandomShape(i);
                allArea += sp.calcuArea();
                Console.WriteLine("this area=" + sp.calcuArea());
                Console.WriteLine("allArea=" + allArea);
            }
            Console.WriteLine("十个图形总面积（含不正确图形）为：" + allArea);
        }
    }
}