using North_DbFirst.Models;
using North_DbFirst.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace North_DbFirst
{
    public partial class Form1 : Form
    {
        private NorthwindContext _dContext = new NorthwindContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var query1 = _dContext.Categories.Select(x => new CategoryViewModel()
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
                Picture = x.Picture,
                ProductCount = x.Products.Count
            }).ToList();
            dgvNorth.DataSource = query1;


            var query2 = from cat in _dContext.Categories
                         join prod in _dContext.Products on cat.CategoryId equals prod.CategoryId
                         //where prod.UnitPrice > 20
                         select new
                         {
                             cat.CategoryName,
                             prod.ProductName,
                             prod.UnitPrice
                         };
            dgvNorth.DataSource = query2
                .OrderBy(x => x.CategoryName)
                .ThenByDescending(x => x.UnitPrice)
                .ToList();


            var query3 = _dContext.Products.Select(x => new
            {
                x.Category.CategoryName,
                x.Supplier.CompanyName,
                x.ProductName,
                x.UnitPrice
            }).OrderBy(x => x.CategoryName).ThenByDescending(x => x.UnitPrice).ToList();
            dgvNorth.DataSource = query3;


            var query4 = _dContext.Products.Where(x => x.UnitPrice >= 55 && x.UnitPrice <= 77)
                .Select(x => new
                {
                    x.ProductName,
                    x.UnitPrice
                }
                ).OrderBy(x => x.UnitPrice).ToList();
            dgvNorth.DataSource = query4;

            var query5 = _dContext.Products.Select(x => new ProductViewModel()
            {
                Productname = x.ProductName,
                UnitPrice = x.UnitPrice,
                UnitStock = x.UnitsInStock,
                Maliyet = x.UnitsInStock * x.UnitPrice

            }
            ).OrderBy(x => x.Maliyet).Take(10).ToList();
            dgvNorth.DataSource = query5;


            var query6 = _dContext.Orders.Where(x => x.Employee.FirstName == "Nancy" && x.ShipViaNavigation.CompanyName == "Federal Shipping").Select(x => new
            {
                x.OrderId
            }).ToList();
            dgvNorth.DataSource = query6;


            var query7 = _dContext.Orders.Where(x => x.OrderId == 10248).Select(x => new
            {
                x.ShipViaNavigation.CompanyName
            }
            ).ToList();
            dgvNorth.DataSource = query7;

            var query8 = from proc in _dContext.Products
                         join od in _dContext.OrderDetails on proc.ProductId equals od.ProductId
                         where proc.ProductName == "Tofu"
                         select new { od.OrderId };
            dgvNorth.DataSource = query8.ToList();

            var query9 = _dContext.Orders.Where(x => (x.CustomerId == "DUMON" || x.CustomerId == "ALFKI ") && x.EmployeeId == 1 && (x.ShipVia == 1 || x.ShipVia == 3));
            dgvNorth.DataSource = query9.ToList();

            var query10 = _dContext.Orders.Where(x => x.OrderDate > new DateTime(1998, 1, 1)).
                Select(X => new
                {
                    X.Customer.ContactName,
                    X.OrderDate
                }
                ).ToList();
            dgvNorth.DataSource = query10;
        }
    }
}
