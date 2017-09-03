using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VehicleEngine
{
    enum OutputType { Console, JsonFile }
    class OutputHelper
    {
        private static OutputType _configOutput = OutputType.JsonFile;

        public static OutputType ConfigOutput
        {
            get { return _configOutput; }
            set { _configOutput = value; }
        }
        public static void Output(object val)
        {
            if (ConfigOutput == OutputType.Console) Console.WriteLine(val);
            if (ConfigOutput == OutputType.JsonFile)
            {
                using (FileStream jsonFileStream = File.Create(Directory.GetCurrentDirectory() + "\\output.json"))
                using (StreamWriter sw = new StreamWriter(jsonFileStream))
                {
                    sw.WriteLine("{\"Info\":\"" + val.ToString().Replace("\r\n", "").Replace(":", "\\:") + "\"}");
                }
            }
        }

    }

    abstract class Vehicle
    {
        protected string LicenceOrRegistration { get; set; }
        public double EnginePower { get; set; }
        public abstract void Print();
        public static bool operator ==(Vehicle v1, Vehicle v2)
        {
            return v1.LicenceOrRegistration == v2.LicenceOrRegistration;
        }
        public static bool operator !=(Vehicle v1, Vehicle v2)
        {
            return v1.LicenceOrRegistration != v2.LicenceOrRegistration;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    abstract class Car : Vehicle
    {
        public double MaximalSpeed { get; set; }
        public string VehicleType { get; set; }
        public string Color { get; set; }
        public virtual Car Drive() { return this; }

        public override void Print()
        {
            StringBuilder outPut = new StringBuilder();
            outPut.AppendLine("Car Information:");
            outPut.AppendLine("LicenceNo:" + LicenceOrRegistration);
            outPut.AppendLine("Engine Power:" + EnginePower + "kW");
            outPut.AppendLine("Maximal Speed:" + MaximalSpeed + "km/h");
            outPut.AppendLine("Color:" + Color);
            outPut.AppendLine("Vehicle Type:" + VehicleType);
            OutputHelper.Output(outPut);
        }
    }
    class Car1 : Car
    {
        public Car1()
        {
            LicenceOrRegistration = "NF123456";
            EnginePower = 147;
            MaximalSpeed = 200;
            Color = "green";
            VehicleType = "personal vehicle";

        }
    }

    class Car2 : Car
    {
        public Car2()
        {
            LicenceOrRegistration = "NF654321";
            EnginePower = 150;
            MaximalSpeed = 195;
            Color = "blue";
            VehicleType = "personal vehicle";
        }
    }
    abstract class Plane : Vehicle
    {
        public string FlyingClass { get; set; }
        public double WingSpan { get; set; }
        public double LoadCapacity { get; set; }
        public double NetWeight { get; set; }
        public virtual Plane Fly() { return this; }
        public override void Print()
        {
            StringBuilder outPut = new StringBuilder();
            outPut.AppendLine("Plane Information:");
            outPut.AppendLine("Registration:" + LicenceOrRegistration);
            outPut.AppendLine("Engine Power:" + EnginePower + "kW");
            outPut.AppendLine("Wing Span:" + WingSpan + "m");
            outPut.AppendLine("Load Capacity:" + LoadCapacity + "t");
            outPut.AppendLine("Net Weight:" + NetWeight + "t");
            outPut.AppendLine("Flying Class:" + FlyingClass);
            OutputHelper.Output(outPut);
        }
    }
    class ThePlane : Plane
    {
        public ThePlane()
        {
            LicenceOrRegistration = "LN1234";
            EnginePower = 1000;
            WingSpan = 30;
            LoadCapacity = 2;
            NetWeight = 10;
            FlyingClass = "Jet Plane";
        }

    }
    abstract class Boat : Vehicle
    {
        public double MaximalSpeed { get; set; }
        public double GrossTonnage { get; set; }
        public override void Print()
        {
            StringBuilder outPut = new StringBuilder();
            outPut.AppendLine("Boat Information:");
            outPut.AppendLine("Registration:" + LicenceOrRegistration);
            outPut.AppendLine("Engine Power:" + EnginePower + "kW");
            outPut.AppendLine("Maximal Speed:" + MaximalSpeed + "know per hour");
            outPut.AppendLine("Gross Tonnage:" + GrossTonnage + "kg");
            OutputHelper.Output(outPut);
        }

    }
    class TheBoat : Boat
    {
        public TheBoat()
        {
            LicenceOrRegistration = "ABC123";
            EnginePower = 100;
            MaximalSpeed = 30;
            GrossTonnage = 500;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("press e to exit, other to continue.");
            var ter = Console.ReadLine();
            while (ter != "e")
            {
                Console.WriteLine("Choose output type: 0: json file, other. console");
                var type = Console.ReadLine();
                if (type.ToString() == "0")
                    OutputHelper.ConfigOutput = OutputType.JsonFile;
                else OutputHelper.ConfigOutput = OutputType.Console;
                var car1 = new Car1();
                car1.Drive().Print();
                var car2 = new Car2();
                car2.Drive().Print();
                Console.WriteLine("Car1 is equal to Car2?" + (car1 == car2));
                var thePlane = new ThePlane();
                thePlane.Fly().Print();
                var theBoat = new TheBoat();
                theBoat.Print();
                Console.ReadKey();
            }
        }
    }
}
