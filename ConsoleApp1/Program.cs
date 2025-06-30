using System.Threading.Channels;

class Program
{
    #region Single responsibility principle

    // Single responsibility principle
    // ✔
    class PapperPrinter
    {
        public void Print(string report)
        {
            Console.WriteLine(report);
        }
    }
    class Printer3d
    {
        public void Printer3D(string report3d)
        {
            Console.WriteLine(report3d);
        }
    }
    // ❌
    class Printer
    {
        public void PapperPrinter(string report) 
        {
            Console.WriteLine(report); 
        }
        public void Printer3D(string report3d)
        {
            Console.WriteLine(report3d);
        }
    }

    #endregion
    #region Open/Closet principle
    // ✔ Open/Closed Principle
    interface IShape
    {
        double CalculateArea();
    }

    class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public double CalculateArea()
        {
            return Width * Height;
        }
    }

    class Circle : IShape
    {
        public double Radius { get; set; }

        public double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    class AreaCalculator
    {
        public double TotalArea(List<IShape> shapes)
        {
            double total = 0;
            foreach (var shape in shapes)
            {
                total += shape.CalculateArea();
            }
            return total;
        }
    }

    // ❌ Open/Closed Principle
    class ShapeBad
    {
        public int Type;
        public double Width;
        public double Height;
        public double Radius;
    }

    class AreaCalculatorBad
    {
        public double TotalArea(List<ShapeBad> shapes)
        {
            double total = 0;
            foreach (var shape in shapes)
            {
                if (shape.Type == 1)
                {
                    total += shape.Width * shape.Height;
                }
                else if (shape.Type == 2)
                {
                    total += Math.PI * shape.Radius * shape.Radius;
                }
            }
            return total;
        }
    }


    #endregion
    #region Liskov subtition principle
    // ✔ Liskov Principle
    class Bird
    {
        public virtual void Fly() { }
    }

    class Eagle : Bird
    {
        public override void Fly()
        {
            Console.WriteLine("Sahin ucur");
        }
    }

    // ❌ Liskov Principle
    class Ostrich : Bird
    {
        public override void Fly()
        {
            throw new NotImplementedException(); // bele olmaz ❌
        }
    }

    #endregion
    #region Interface sagrigation principle
    // ✔ Interface sagregation
    interface IPapperPrinter
    {
        public void Print(string report)
        {
            Console.WriteLine(report);
        }
    }
    interface IPrinter3d
    {
        public void Printer3D(string report3d)
        {
            Console.WriteLine(report3d);
        }
    }
    // ❌
    interface IPrinter
    {
        public void PapperPrinter(string report)
        {
            Console.WriteLine(report);
        }
        public void Printer3D(string report3d)
        {
            Console.WriteLine(report3d);
        }
    }

    #endregion
    #region Dependecy inversion principle
    // ✔ Dependecy inversion
    interface IMessageService
    {
        void Send(string message);
    }

    class EmailService : IMessageService
    {
        public void Send(string message)
        {
            Console.WriteLine("Email gondərildi: " + message);
        }
    }

    class Notification
    {
        private readonly IMessageService _service;

        public Notification(IMessageService service)
        {
            _service = service;
        }

        public void Alert(string message)
        {
            _service.Send(message);
        }
    }
    // ❌Dependecy inversion

    //class Notification
    //{
    //    private EmailService service = new EmailService();

    //    public void Alert(string message)
    //    {
    //        service.Send(message);
    //    }
    //}



    #endregion
    
        static void Main(string[] args)
        {
            Console.WriteLine("----- Single Responsibility Principle -----");

            var paperPrinter = new PapperPrinter();
            paperPrinter.Print("Kagiz cap olunur...");

            var printer3d = new Printer3d();
            printer3d.Printer3D("3D model cap olunur...");

            Console.WriteLine("\n----- Open/Closed Principle -----");

            var shapes = new List<IShape>
    {
        new Rectangle { Width = 5, Height = 4 },
        new Circle { Radius = 3 }
    };
            var areaCalculator = new AreaCalculator();
            Console.WriteLine("toplam sahe: " + areaCalculator.TotalArea(shapes));

            Console.WriteLine("\n----- Liskov Substitution Principle -----");

            Bird eagle = new Eagle();
            eagle.Fly();

            Bird ostrich = new Ostrich();
            try
            {
                ostrich.Fly();                    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xeta: " + ex.Message);
            }

            Console.WriteLine("\n----- Interface Segregation Principle -----");

            Console.WriteLine("bu prinsip interfeyslerin bolunmesini gosterir. Methodari class daxilinde test etmediyin ucun burada cagiris yoxdur.");

            Console.WriteLine("\n----- Dependency Inversion Principle -----");

            IMessageService service = new EmailService();
            var notification = new Notification(service);
            notification.Alert("Salam, bu mesaj Dependency Inversion ile gonderildi.");
        }


    
}