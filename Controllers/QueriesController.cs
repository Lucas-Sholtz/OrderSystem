using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrdersSystem;
using OrdersSystem.Models;

namespace Waresoft.Controllers
{
    public class QueriesController : Controller
    {
        private const string CONNECTION_PATH = "Server=LAPTOP-P4IHNG64; Database=OrderSystemDatabase; Trusted_Connection=True; MultipleActiveResultSets=true";

        private const string Q1_PATH = @"Queries\Q1.sql";
        private const string Q2_PATH = @"Queries\Q2.sql";
        private const string Q3_PATH = @"Queries\Q3.sql";
        private const string Q4_PATH = @"Queries\Q4.sql";
        private const string Q5_PATH = @"Queries\Q5.sql";
        private const string Q6_PATH = @"Queries\Q6.sql";
        private const string Q7_PATH = @"Queries\Q7.sql";
        private const string Q8_PATH = @"Queries\Q8.sql";

        private const string ERR_AVG = "Неможливо обрахувати середню ціну, оскільки продукти відсутні.";
        private const string ERR_CUST = "Покупці, що задовольняють дану умову, відсутні.";
        private const string ERR_PROD = "Програмні продукти, що задовольняють дану умову, відсутні.";
        private const string ERR_DEV = "Розробники, що задовольняють дану умову, відсутні.";
        private const string ERR_COUNTRY = "Країни, що задовольняють дану умову, відсутні.";

        private const string PLACEHOLDER = "PLACEHOLDER1";
        private const string PLACEHOLDER1 = "PLACEHOLDER2";

        private readonly OrderSystemDatabaseContext _context;

        public QueriesController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(int errorCode)
        {
            //var customers = _context.Customers.Select(c => c.Name).Distinct().ToList();

            if (errorCode == 1)
            {
                ViewBag.ErrorFlag = 1;
                ViewBag.PriceError = "Введіть коректну вартість";
            }
            if (errorCode == 2)
            {
                ViewBag.ErrorFlag = 2;
                ViewBag.ProdNameError = "Поле необхідно заповнити";
            }

            var empty = new SelectList(new List<string> { "--Пусто--" });
            //var anyCusts = _context.Customers.Any();
            //var anyDevs = _context.Developers.Any();


            /////
            ViewBag.CouriersNames = new SelectList(_context.Couriers, "CourierName", "CourierName");
            ViewBag.TownsNames = new SelectList(_context.Towns, "TownName", "TownName");
            ViewBag.ShopsNames = new SelectList(_context.Shops, "ShopName", "ShopName");
            ViewBag.OrderIds = new SelectList(_context.Orders, "OrderId", "OrderId");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query1(Query query)
        {
            string fileString = System.IO.File.ReadAllText(Q1_PATH);
            fileString = fileString.Replace(PLACEHOLDER, $"\'{query.CourierName}\'");
            fileString = fileString.Replace("\r\n", " ");
            fileString = fileString.Replace('\t', ' ');

            query.QueryId = "1";

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(fileString, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        query.ShopName = result.ToString();
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", query);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query2(Query query)
        {
            string fileString = System.IO.File.ReadAllText(Q2_PATH);
            fileString = fileString.Replace(PLACEHOLDER, $"\'{query.CourierName}\'");
            fileString = fileString.Replace("\r\n", " ");
            fileString = fileString.Replace('\t', ' ');

            query.QueryId = "2";

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(fileString, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        query.AveragePrice = Convert.ToDecimal(result);
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", query);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query3(Query query)
        {
            string fileString = System.IO.File.ReadAllText(Q3_PATH);
            fileString = fileString.Replace(PLACEHOLDER, $"\'{query.TownName}\'");
            fileString = fileString.Replace("\r\n", " ");
            fileString = fileString.Replace('\t', ' ');

            query.QueryId = "3";
            query.ShopNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(fileString, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.ShopNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", query);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query4(Query query)
        {
            string fileString = System.IO.File.ReadAllText(Q4_PATH);
            fileString = fileString.Replace(PLACEHOLDER, $"{query.ProductPrice}");
            fileString = fileString.Replace("\r\n", " ");
            fileString = fileString.Replace('\t', ' ');

            query.QueryId = "4";
            query.ShopNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(fileString, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.ShopNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", query);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query5(Query query)
        {
            string fileString = System.IO.File.ReadAllText(Q5_PATH);
            fileString = fileString.Replace(PLACEHOLDER, query.OrderId.ToString());
            fileString = fileString.Replace("\r\n", " ");
            fileString = fileString.Replace('\t', ' ');

            query.QueryId = "5";

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(fileString, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        query.OrderPrice = Convert.ToDecimal(result);
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", query);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query6(Query query)
        {
            string fileString = System.IO.File.ReadAllText(Q6_PATH);
            fileString = fileString.Replace(PLACEHOLDER, $"\'{query.FirstShopName}\'");
            fileString = fileString.Replace(PLACEHOLDER1, $"\'{query.SecondShopName}\'");

            fileString = fileString.Replace("\r\n", " ");
            fileString = fileString.Replace('\t', ' ');

            query.QueryId = "6";
            query.SameProductNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(fileString, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.SameProductNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", query);
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery2(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S2_PATH);
            query = query.Replace("X", "N\'" + queryModel.DevName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S2";
            queryModel.CustNames = new List<string>();
            queryModel.CustSurnames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustNames.Add(reader.GetString(0));
                            queryModel.CustSurnames.Add(reader.GetString(1));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_CUST;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery3(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S3_PATH);
            query = query.Replace("K", "N\'" + queryModel.CountryName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S3";
            queryModel.ProdNames = new List<string>();
            queryModel.ProdPrices = new List<decimal>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.ProdNames.Add(reader.GetString(0));
                            queryModel.ProdPrices.Add(reader.GetDecimal(1));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_PROD;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery4(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S4_PATH);
            query = query.Replace("X", "N\'" + queryModel.CustName + "\'");
            query = query.Replace("Y", "N\'" + queryModel.CustSurname + "\'");
            query = query.Replace("Z", "N\'" + queryModel.CustEmail + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S4";
            queryModel.DevNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.DevNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_DEV;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery5(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(S5_PATH);
                query = query.Replace("P", queryModel.Price.ToString());
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');

                queryModel.QueryId = "S5";
                queryModel.DevNames = new List<string>();

                using (var connection = new SqlConnection(CONNECTION_PATH))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.DevNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = ERR_DEV;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Result", queryModel);
            }
            return RedirectToAction("Index", new { errorCode = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery6(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(S6_PATH);
                query = query.Replace("X", "N\'" + queryModel.ProdName + "\'");
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');
                queryModel.QueryId = "S6";
                queryModel.DevNames = new List<string>();

                using (var connection = new SqlConnection(CONNECTION_PATH))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.DevNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = ERR_DEV;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Result", queryModel);
            }

            return RedirectToAction("Index", new { errorCode = 2 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdvancedQuery1(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A1_PATH);
            query = query.Replace("K", queryModel.DevId.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');
            queryModel.QueryId = "A1";
            queryModel.CountryNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CountryNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_COUNTRY;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdvancedQuery2(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A2_PATH);
            query = query.Replace("Y", "N\'" + queryModel.CustEmail.ToString() + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');
            queryModel.QueryId = "A2";
            queryModel.CustSurnames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustSurnames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_CUST;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        public IActionResult AdvancedQuery3(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A3_PATH);
            query = query.Replace("Y", "N\'" + queryModel.CustName.ToString() + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');
            queryModel.QueryId = "A3";
            queryModel.CustSurnames = new List<string>();
            queryModel.CustEmails = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_PATH))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustSurnames.Add(reader.GetString(0));
                            queryModel.CustEmails.Add(reader.GetString(1));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_CUST;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        public IActionResult TeacherQuery1(Query queryModel)
        {
            throw new NotImplementedException();
        }

        public IActionResult TeacherQuery2(Query queryModel)
        {
            throw new NotImplementedException();
        }
        */
        public IActionResult Result(Query queryResult)
        {
            return View(queryResult);
        }
    }
}
