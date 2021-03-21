using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrdersSystem;
using ClosedXML.Excel;
using System.IO;
using ClosedXML.Utils;
using System.Linq.Expressions;

namespace OrdersSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OrderSystemDatabaseContext _context;

        public ProductsController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var orderSystemDatabaseContext = _context.Products.Include(p => p.Shop);
            return View(await orderSystemDatabaseContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductRemainingQuantity,ShopId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName", product.ShopId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName", product.ShopId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductRemainingQuantity,ShopId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName", product.ShopId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {                    
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Product newProduct = new Product();
                                        newProduct.ProductName = row.Cell(1).Value.ToString();
                                        newProduct.ProductPrice = double.Parse(row.Cell(2).Value.ToString());
                                        newProduct.ProductRemainingQuantity = (int)double.Parse(row.Cell(3).Value.ToString());

                                        Shop shop = new Shop();
                                        //находим магазин, у каждого уникальное имя!
                                        var s = (from sh in _context.Shops
                                                 where sh.ShopName.Contains(row.Cell(4).Value.ToString())
                                                 select sh).ToList();
                                        if (s.Count > 0) //если такой магазин есть, добавляем его и прыгаем на 236 строку
                                        {
                                            shop = s[0];
                                        }
                                        else //если такого магазина нет, то надо добавить его
                                        {
                                            shop.ShopName = row.Cell(4).Value.ToString(); //присваиваем имя
                                            //у каждого магазина есть уникальный адрес, поэтому сразу создадим его
                                            Adress adress = new Adress();
                                            //и улицу тоже
                                            Street street = new Street();
                                            //найдём улицу с таким же именем что написано, они уникальны))
                                            var str = (from st in _context.Streets
                                                       where st.StreetName.Contains(row.Cell(7).Value.ToString())
                                                       select st).ToList();
                                            //если такая улица есть
                                            if (str.Count > 0) //запишем её в адрес, но всё равно нужен номер поэтому нам на 234 строку
                                            {
                                                street = str[0];
                                            }
                                            else //если улицы с таким именем нету, то надо создать улицу
                                            {
                                                //запишем её имя
                                                street.StreetName = row.Cell(7).Value.ToString();
                                                //создадим город
                                                Town town = new Town();
                                                //проверим существует ли указанный город
                                                var twn = (from t in _context.Towns
                                                           where t.TownName.Contains(row.Cell(8).Value.ToString())
                                                           select t).ToList();
                                                if (twn.Count > 0) //если он есть то запишем его и прыгнем на????
                                                {
                                                    town = twn[0];
                                                }
                                                else //если такого города нет то надо создать его и добавить в бд
                                                {
                                                    town.TownName = row.Cell(8).Value.ToString();
                                                    town.TownPostCode= (int)double.Parse(row.Cell(9).Value.ToString());
                                                    _context.Towns.Add(town);
                                                }
                                                //запишем город в лицу и добавим улицу
                                                street.Town = town;
                                                _context.Streets.Add(street);
                                            }
                                            //запишем в адрес данные и добавим его
                                            adress.Street = street;
                                            adress.AddressNotes = row.Cell(6).Value.ToString();
                                            adress.AddressStreetNumber = (int)double.Parse(row.Cell(5).Value.ToString());
                                            _context.Adresses.Add(adress);
                                            //после того как сделали адрес, запишем его в магазин и добавим магазин в БД
                                            shop.Address = adress;
                                            _context.Shops.Add(shop);
                                        }
                                        //присвоим продукту магазин и добавим его
                                        newProduct.Shop = shop;
                                        _context.Products.Add(newProduct);
                                    }
                                    catch (Exception e)
                                    {
                                        //logging самостійно :)
                                        Town town = new Town();
                                    }
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var products = _context.Products.ToList();

                var worksheet = workbook.Worksheets.Add();
                //Name	Price	Quantity	Shop name	Street number	Notes	Street name	City name	Postal code

                worksheet.Cell("A1").Value = "Name";
                worksheet.Cell("B1").Value = "Price";
                worksheet.Cell("C1").Value = "Quantity";
                worksheet.Cell("D1").Value = "Shop name";
                worksheet.Cell("E1").Value = "Street number";
                worksheet.Cell("F1").Value = "Notes";
                worksheet.Cell("G1").Value = "Street name";
                worksheet.Cell("H1").Value = "City name";
                worksheet.Cell("I1").Value = "Postal code";
                worksheet.Row(1).Style.Font.Bold = true;

                for (int i = 0; i < products.Count; i++)
                {
                    try
                    {
                        worksheet.Cell(i + 2, 1).Value = products[i].ProductName;
                        worksheet.Cell(i + 2, 2).Value = products[i].ProductPrice;
                        worksheet.Cell(i + 2, 3).Value = products[i].ProductRemainingQuantity;
                        var shop = (from sh in _context.Shops
                                    where sh.ShopId == products[i].ShopId
                                    select sh).ToList();

                        worksheet.Cell(i + 2, 4).Value = shop[0].ShopName;
                        var address = (from ad in _context.Adresses
                                       where ad.AddressId == shop[0].AddressId
                                       select ad).ToList();
                        worksheet.Cell(i + 2, 5).Value = address[0].AddressStreetNumber;
                        worksheet.Cell(i + 2, 6).Value = address[0].AddressNotes;
                        var street = (from st in _context.Streets
                                      where st.StreetId == address[0].StreetId
                                      select st).ToList();
                        worksheet.Cell(i + 2, 7).Value = street[0].StreetName;
                        var town = (from tw in _context.Towns
                                    where tw.TownId == street[0].TownId
                                    select tw).ToList();
                        worksheet.Cell(i + 2, 8).Value = town[0].TownName;
                        worksheet.Cell(i + 2, 9).Value = town[0].TownPostCode;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"library_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
    }
}
