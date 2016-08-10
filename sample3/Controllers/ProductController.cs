using sample3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sample3.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        

        IProductRepository repository = new ProductRepository();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProducts(string idx, string sort, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {

            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var ProductList = repository.GetAll().Select(
                    t => new
                    {
                        t.Id,
                        t.Name,
                        t.Category,
                        t.Price
                    });
            if (_search)
            {
                switch (searchField)
                {
                    case "Name":
                        ProductList = ProductList.Where(t => t.Name.Contains(searchString));
                        break;
                    case "Category":
                        ProductList = ProductList.Where(t => t.Category.Contains(searchString));
                        break;
                    case "Price":
                        ProductList = ProductList.Where(t => t.Price.ToString() == searchString);
                        break;
                }
            }
            int totalRecords = ProductList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                ProductList = ProductList.OrderByDescending(t => t.Name);
                ProductList = ProductList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                ProductList = ProductList.OrderBy(t => t.Name);
                ProductList = ProductList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = ProductList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //public string Create([Bind(Exclude = "Id")] Product Model)
        //{

        //    string msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            repository.Add(Model);

        //            msg = "Saved Successfully";
        //        }
        //        else
        //        {
        //            msg = "Validation data not successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Error occured:" + ex.Message;
        //    }
        //    return msg;
        //}
        public string Edit(Product Model)
        {

            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Update(Model);
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        //public string Delete(string id)
        //{
        //    int IdVal = Convert.ToInt32(id);
        //    repository.Remove(IdVal);
        //    return "Deleted successfully";
        //}

    }
}
