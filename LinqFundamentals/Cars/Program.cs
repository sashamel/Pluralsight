using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Scott Allen	- LINQ Fundamentals
/// https://www.pluralsight.com/courses/linq-fundamentals-csharp-6
/// </summary>

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            //var cars = ProcessCars("fuel.csv");
            //var manufacturers = ProcessManufacturers("manufacturers.csv");

            //var query =
            //    from car in cars
            //    join manufacturer in manufacturers
            //        on new { car.Manufacturer, car.Year } equals new { Manufacturer = manufacturer.Name, manufacturer.Year}
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };

            //var query2 =
            //    cars.Join(manufacturers,
            //            c => new { c.Manufacturer, c.Year },
            //            m => new { Manufacturer = m.Name, m.Year },
            //            (c, m) => new
            //            {
            //                m.Headquarters,
            //                c.Name,
            //                c.Combined
            //            })
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name);

            //foreach (var car in query2.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}

            //var query =
            //    from car in cars
            //    group car by car.Manufacturer.ToUpper() into manufacturer
            //    orderby manufacturer.Key
            //    select manufacturer;

            //var query2 =
            //    cars.GroupBy(c => c.Manufacturer.ToUpper())
            //        .OrderBy(g => g.Key);

            //foreach (var group in query2)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))   
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}

            //var query =
            //    from manufacturer in manufacturers
            //    join car in cars on manufacturer.Name equals car.Manufacturer
            //        into carGroup
            //    orderby manufacturer.Name
            //    select new
            //    {
            //        Manufacturer = manufacturer,
            //        Cars = carGroup
            //    };

            //var query2 =
            //    manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
            //        (m, g) =>
            //            new
            //            {
            //                Manufacturer = m,
            //                Cars = g
            //            })
            //    .OrderBy(m => m.Manufacturer.Name);

            //foreach (var group in query2)
            //{
            //    Console.WriteLine($"{group.Manufacturer.Name}:{group.Manufacturer.Headquarters}");
            //    foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}

            //var query =
            //    from car in cars
            //    join manufacturer in manufacturers
            //        on car.Manufacturer equals manufacturer.Name
            //    orderby manufacturer.Headquarters, car.Combined
            //    select new
            //    {
            //        Car = car,
            //        Manufacturer = manufacturer,
            //    } into result
            //    group result by result.Manufacturer.Headquarters;

            //var query2 =
            //    cars.Join(manufacturers, c => c.Manufacturer, m => m.Name,
            //        (c, m) =>
            //            new
            //            {
            //                Car = c,
            //                Manufacturer = m,
            //            })
            //    .OrderBy(c => c.Manufacturer.Headquarters)
            //    .ThenBy(c => c.Car.Combined)
            //    .GroupBy(c => c.Manufacturer.Headquarters);

            //foreach (var group in query2)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach (var car in group.OrderByDescending(c => c.Car.Combined).Take(3))
            //    {
            //        Console.WriteLine($"\t{car.Car.Name} : {car.Car.Combined}");
            //    }
            //}

            //var query =
            //    from manufacturer in manufacturers
            //    join car in cars on manufacturer.Name equals car.Manufacturer
            //        into carGroup
            //    select new
            //    {
            //        Manufacturer = manufacturer,
            //        Cars = carGroup
            //    } into result
            //    group result by result.Manufacturer.Headquarters;

            //var query2 =
            //    manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
            //        (m, g) =>
            //            new
            //            {
            //                Manufacturer = m,
            //                Cars = g
            //            })
            //    .GroupBy(m => m.Manufacturer.Headquarters);

            //foreach (var group in query2)
            //{
            //    Console.WriteLine($"{group.Key}");
            //    foreach (var car in group.SelectMany(c => c.Cars).OrderByDescending(c => c.Combined).Take(3))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}

            //var query =
            //    from car in cars
            //    group car by car.Manufacturer into carGroup
            //    select new
            //    {
            //        Name = carGroup.Key,
            //        Max = carGroup.Max(c => c.Combined),
            //        Min = carGroup.Min(c => c.Combined),
            //        Avg = carGroup.Average(c => c.Combined),
            //    } into result
            //    orderby result.Max descending
            //    select result;

            //var query2 =
            //    cars.GroupBy(m => m.Manufacturer)
            //        .Select(g =>
            //        {
            //            var results = g.Aggregate(new CarStatistics(),
            //                                (acc, c) => acc.Accumulate(c),
            //                                acc => acc.Compute());
            //            return new
            //            {
            //                Name = g.Key,
            //                Max = results.Max,
            //                Min = results.Min,
            //                Avg  = results.Average,
            //            };
            //        })
            //        .OrderByDescending(r => r.Max);


            //foreach (var result in query2)
            //{
            //    Console.WriteLine($"{result.Name}");
            //    Console.WriteLine($"\t Max: {result.Max}");
            //    Console.WriteLine($"\t Min: {result.Min}");
            //    Console.WriteLine($"\t Avg: {result.Avg}");
            //}

            //CreateXml();
            //QueryXml();

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData();
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();
            //db.Database.Log = Console.WriteLine;

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryData()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            //var query = from car in db.Cars
            //            orderby car.Combined descending, car.Name ascending
            //            select car;

            //var query2 = db.Cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            //foreach (var car in query2.Take(10))
            //{
            //    Console.WriteLine($"{car.Name}: {car.Combined}");
            //}

            var query = from car in db.Cars
                        group car by car.Manufacturer into manufacturer
                        select new
                        {
                            Name = manufacturer.Key,
                            Cars = (from car in manufacturer
                                   orderby car.Combined descending
                                   select car).Take(2)
                        };

            var query2 = db.Cars.GroupBy(c => c.Manufacturer)
                                .Select(g => new
                                {
                                    Name = g.Key,
                                    Cars = g.OrderByDescending(c => c.Combined).Take(2)
                                });

            foreach (var group in query)
            {
                Console.WriteLine(group.Name);
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }
        }

        private static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = new XDocument();
            //var cars = new XElement("Cars");

            //foreach (var record in records)
            //{
            //    var car = new XElement("Car",
            //        new XAttribute("Name", record.Name),
            //        new XAttribute("Combined", record.Combined),
            //        new XAttribute("Manufacturer", record.Manufacturer));

            //    cars.Add(car);
            //}

            var cars = new XElement(ns + "Cars",
                           from record in records
                           select new XElement(ex + "Car",
                                            new XAttribute("Name", record.Name),
                                            new XAttribute("Combined", record.Combined),
                                            new XAttribute("Manufacturer", record.Manufacturer)));
            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static void QueryXml()
        {
            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = XDocument.Load("fuel.xml");

            var query =
                from element in document.Element(ns + "Cars")?.Elements(ex + "Car")
                ?? Enumerable.Empty<XElement>()
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static List<Car> ProcessCars(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            return query.ToList();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });

            return query.ToList();
        }
    }

    public class CarStatistics
    {
        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[02],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7]),
                };
            }
        }
    }
}
