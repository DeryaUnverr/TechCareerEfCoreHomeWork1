using System;
using System.Collections.Generic;
using System.Linq;
using Odev1.Models;

namespace Odev1
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindContext context = new NorthwindContext();

            Soru1(context);
            Soru2(context);
            Soru3(context);
            Soru4(context);
            Soru5(context);
            Soru6(context);
            Soru7(context);
            Soru8(context);
            Soru9(context);
            Soru10(context);
            Soru11(context);
            Soru12(context);

            Console.ReadKey();
        }


        #region 1.	Londra’da ikamet eden çalışanlar kimlerdir. Ad | Soyad
        private static void Soru1(NorthwindContext context)
        {
            //SQL-->SELECT * FROM Employees where City='London'
            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Employees.Where(e => e.City == "London")
                .Select(x => new { Ad = x.FirstName, Soyad = x.LastName }).ToList();
            method.ForEach(x => Console.WriteLine(x.Ad + " " + x.Soyad));
            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = from e in context.Employees
                        where e.City == "London"
                        select new
                        {
                            Ad = e.FirstName,
                            Soyad = e.LastName
                        };

            foreach (var item in query)
            {
                Console.WriteLine(item.Ad + " " + item.Soyad);
            }
        }
        #endregion

        #region  2.	Hangi çalışanlar Almanca biliyor? ID | AdSoyad
        private static void Soru2(NorthwindContext context)
        {
            //SQL -->     select EmployeeId,FirstName,LastName  from Employees where Notes like '%German%'
            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Employees.Where(e => e.Notes.Contains("German"))
                .Select(e => new { ID = e.EmployeeId, AdSoyad = e.FirstName + " " + e.LastName }).ToList();
            method.ForEach(x => Console.WriteLine(x.ID + " " + x.AdSoyad));
            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = from e in context.Employees
                        where e.Notes.Contains("German")
                        select new
                        {
                            ID = e.EmployeeId,
                            AdSoyad = e.FirstName + " " + e.LastName
                        };
            foreach (var item in query)
            {
                Console.WriteLine(item.ID + " " + item.AdSoyad);
            }
        }
        #endregion

        #region 3.	Henüz teslimatı gerçekleşmemiş siparişler hangileridir? SiparisNo

        private static void Soru3(NorthwindContext context)
        {
            //SQL-->select OrderID from Orders where ShippedDate IS NULL
            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Orders
                .Where(o => (o.ShippedDate == null)).Select(o => new { SiparisNo = o.OrderId }).ToList();
            method.ForEach(o => Console.WriteLine(o.SiparisNo));
            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = from o in context.Orders
                        where o.ShippedDate == null
                        select new
                        {
                            SiparisNo = o.OrderId
                        };
            foreach (var item in query)
            {
                Console.WriteLine(item.SiparisNo);
            }
        }
        #endregion

        #region 4.	Birim fiyatı 50 ile 80 arasında olan ürünler hangileridir? ProductName | UnitPrice

        private static void Soru4(NorthwindContext context)
        {
            //SELECT ProductName,UnitPrice FROM Products WHERE UnitPrice>50 AND UnitPrice<80
            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Products
                .Where(p => ((p.UnitPrice > 50) && (p.UnitPrice < 80)))
                .Select(p => new { ProductName = p.ProductName, UnitPrice = p.UnitPrice }).ToList();
            method.ForEach(p => Console.WriteLine(p.ProductName + " " + p.UnitPrice));

            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = from p in context.Products
                        where
                            p.UnitPrice > 50 &&
                            p.UnitPrice < 80
                        select new
                        {
                            UrunAdı = p.ProductName,
                            Fiyat = p.UnitPrice
                        };
            foreach (var item in query)
            {
                Console.WriteLine(item.UrunAdı + " " + item.Fiyat);
            }
        }
        #endregion

        #region 5.	Şişede satılan ürünler hangileridir? ProductName | Stock
        private static void Soru5(NorthwindContext context)
        {
            //SQL-->select * from Products where QuantityPerUnit LIKE '%bottle%'  
            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Products.Where(p => p.QuantityPerUnit.Contains("bottle"))
                .Select(p => new { UrunAdi = p.ProductName, Stok = p.UnitsInStock }).ToList();
            method.ForEach(p => Console.WriteLine(p.UrunAdi + " " + p.Stok));

            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = from p in context.Products
                        where
                            p.QuantityPerUnit.Contains("bottle")
                        select new
                        {
                            UrunAdi = p.ProductName,
                            Stok = p.UnitsInStock
                        };
            foreach (var item in query)
            {
                Console.WriteLine(item.UrunAdi + " " + item.Stok);
            }
        }
        #endregion

        #region 6.	Teslim edilen siparişler kaç günde teslim edilmiştir? SiparisNo | SiparisTarihi | TeslimTarihi | Gun
        private static void Soru6(NorthwindContext context)
        {
            //Sql-->DateDıff
            var query = (from Orders in context.Orders
                         where Orders.ShippedDate.HasValue
                         select new
                         {
                             Orders.OrderId,
                             ShippedDate = (Convert.ToString(Orders.ShippedDate.Value.Day) + "-" +
                                            Convert.ToString(Orders.ShippedDate.Value.Month) + "-" +
                                            Convert.ToString(Orders.ShippedDate.Value.Year)),

                             OrderDate = (Convert.ToString(Orders.OrderDate.Value.Day) + "-" +
                                          Convert.ToString(Orders.OrderDate.Value.Month) + "-" +
                                          Convert.ToString(Orders.OrderDate.Value.Year)),
                             Fark = (Orders.ShippedDate - Orders.OrderDate).Value.Days
                         }).ToList();
            query.ForEach(a => Console.WriteLine("SiparisNO:" + a.OrderId + " Order Date:" + a.OrderDate + " Shipped Date:" +
                                                 a.ShippedDate + " Fark:" + a.Fark));
        }

        #endregion

        #region 7.	Stok miktarı kritik seviyede olan ürünler hangileridir? ProductName | Stock

        private static void Soru7(NorthwindContext context)
        {
            //SQL--> select ProductName,UnitsInStock from Products where UnitsInStock<=ReorderLevel
            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Products.Where(p => (p.UnitsInStock) <= (p.ReorderLevel))
                .Select(p => new { UrunAdi = p.ProductName, Stok = p.UnitsInStock }).ToList();
            method.ForEach(p => Console.WriteLine(p.UrunAdi + " " + p.Stok));

            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = from p in context.Products
                        where
                            p.UnitsInStock <= p.ReorderLevel
                        select new
                        {
                            UrunAdi = p.ProductName,
                            Stok = p.UnitsInStock
                        };
            foreach (var item in query)
            {
                Console.WriteLine(item.UrunAdi + " " + item.Stok);
            }
        }
        #endregion

        #region 8.	Seafood kategorisinden ürün alan müşteriler hangileridir? MusteriAdi
        private static void Soru8(NorthwindContext context)
        {
            //SQL-->sub Query
            //select ContactName from Customers
            //where CustomerID in(select CustomerID from Orders
            //where  OrderId   in(select OrderId from[Order Details]
            //where ProductID in(select ProductId from Products
            //where  CategoryID in(select CategoryID from Categories where CategoryName = 'Seafood'))))

            //SQL-->jOİN 

            //select DISTINCT c.ContactName from Customers c
            //INNER JOIN Orders O
            //on o.CustomerID = c.CustomerID
            //INNER JOIN[Order Details] od
            //    on od.OrderID = o.OrderID
            //INNER JOIN Products p
            //on p.ProductID = od.ProductID
            //INNER JOIN Categories ct
            //on ct.CategoryID = P.CategoryID
            //where ct.CategoryName = 'Seafood'


            Console.WriteLine("-------------Query Yöntemi--------------------");
            var queryJoin = (from cu in context.Customers
                             join o in context.Orders on cu.CustomerId equals o.CustomerId
                             join od in context.OrderDetails on o.OrderId equals od.OrderId
                             join p in context.Products on od.ProductId equals p.ProductId
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             where c.CategoryName == "Seafood"
                             select new { cu.ContactName }).Distinct();
            foreach (var item in queryJoin)
            {
                Console.WriteLine(item.ContactName);
            }
        }
        #endregion

        #region 9.	Hangi kargo şirketine ne kadar ödeme yapılmış? KargoSirketi | OdemeTutari

        private static void Soru9(NorthwindContext context)
        {
            //SQL-->
            //select sum(Freight) as Tutar,CompanyName as Firmalar from Orders join Shippers
            //on Orders.ShipVia = Shippers.ShipperID Group by CompanyName
            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = (from o in context.Orders
                           join s in context.Shippers on o.ShipVia equals s.ShipperId
                           group o by s.CompanyName
                           into yeni
                           select new
                           {
                               Sirket = yeni.Key,
                               Odenen = yeni.Sum(a => a.Freight)
                           });


            foreach (var item in query)
            {
                Console.WriteLine(item.Sirket + ":" + item.Sirket + "  " + item.Odenen + ":" +
                                  item.Odenen);
            }
            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Orders
                .Join(context.Shippers, order => order.ShipVia, shipper => (shipper.ShipperId),
                    (order, shipper) =>
                        new
                        {
                            o = order,
                            s = shipper
                        }
                )
                .GroupBy(
                    gCompanyName => gCompanyName.s.CompanyName,
                    gCompanyName => gCompanyName.o
                )
                .Select(
                    yeni =>
                        new
                        {
                            Sirket = yeni.Key,
                            Odenen = yeni.Sum(a => a.Freight)
                        }
                ).ToList();
            method.ForEach(item => Console.WriteLine(item.Sirket + ":" + item.Sirket + "  " + item.Odenen + ":" +
                                                  item.Odenen));

        }
        #endregion

        #region 10.	Her yıl hangi ülkeye kaç adet sipariş gönderilmiştir? Year | Country | OrderCount

        private static void Soru10(NorthwindContext context)
        {
            //select distinct ShipCountry, Year(OrderDate),Count(OrderId) from Orders Group by Year(OrderDate),ShipCountry
            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = (from o in context.Orders
                         group o by new
                         {
                             Yil = o.OrderDate.Value.Year,
                             Ulke=o.ShipCountry
                         }
                into yeni
                         select new
                         {
                             Country = yeni.Key.Ulke,
                             Year = yeni.Key.Yil,
                             OrderCount = yeni.Count(p => p.OrderId != null)
                         }).Distinct();
     
      
            foreach (var q in query)
            {
                Console.WriteLine(q.Year + "  " + q.Country + "  " + q.OrderCount + " ");
            }

            Console.WriteLine("-------------Method Yöntemi--------------------");
            var method = context.Orders
                .GroupBy(Orders => new
                        {
                            Yil = (Orders.OrderDate.Value.Year),
                            Ulke = Orders.ShipCountry
                        }
                ).Select(yeni =>
                        new
                        {
                            Country = yeni.Key.Ulke,
                            Year = yeni.Key.Yil,
                            OrderCount = yeni.Count(p => ((p.OrderId) != null))
                        }
                ).Distinct().ToList();
                method.ForEach(q =>
                Console.WriteLine(q.Year + "  " + q.Country + "  " + q.OrderCount + " "));
        }

        #endregion

        #region 11.	Dörtten az sipariş veren müşteriler hangileridir?
        private static void Soru11(NorthwindContext context)
        {
            //SQL
            //select COUNT(Orders.CustomerID) ,Customers.ContactName,Customers.CompanyName from Orders
            //    INNER JOIN Customers on Orders.CustomerID = Customers.CustomerID
            //group by Customers.CustomerID,ContactName,CompanyName HAVING COUNT(*) < 4
      
         
            var query = from o in context.Orders
                        join c in context.Customers on o.CustomerId equals c.CustomerId into yeni
                        from c in yeni
                        group new { c, o } by new
                        {
                            c.CustomerId,
                            c.ContactName,
                            c.CompanyName
                        }
                into g
                        where g.Count() < 4
                        select new
                        {
                            Column1 = g.Count(p => p.o.CustomerId != null),
                            ContactName = g.Key.ContactName,
                            CompanyName = g.Key.CompanyName
                        };


            foreach (var item in query)
            {
                Console.WriteLine(item.ContactName);
            }
        }

        #endregion

        #region 12.	New York şehrinden sorumlu çalışan(lar) kim?
        private static void Soru12(NorthwindContext context)
        {
            //SQL-- >
            ////select DISTINCT(FirstName)  from Employees INNER JOIN  EmployeeTerritories
            // on Employees.EmployeeID = EmployeeTerritories.EmployeeID
            //INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
            //where TerritoryDescription = 'New York'
            Console.WriteLine("-------------Query Yöntemi--------------------");
            var query = (from e in context.Employees
                join et in context.EmployeeTerritories
                    on e.EmployeeId equals et.EmployeeId
                join t in context.Territories
                    on et.TerritoryId equals t.TerritoryId
                where t.TerritoryDescription == "New York"
                select new { e.FirstName, e.LastName }).Distinct();


            foreach (var q in query)
            {
                Console.WriteLine( q.FirstName + " " + q.LastName);
            }

            Console.WriteLine();


        }

        #endregion

    }
}
