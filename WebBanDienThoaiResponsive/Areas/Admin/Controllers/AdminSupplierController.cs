using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminSupplierController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                List<Supplier> supplierList = context.Suppliers.ToList();
                List<SupplierViewModel> supplierViewModelList = new List<SupplierViewModel>();
                foreach (var item in supplierList)
                {
                    SupplierViewModel viewModel = new SupplierViewModel
                    {
                        ID = item.ID,
                        SupplierName = item.SupplierName,
                        Address = item.Address,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber,
                        Status = item.Status
                    };
                    supplierViewModelList.Add(viewModel);
                }
                return View(supplierViewModelList);
            }
        }

        [HttpGet]
        public ActionResult Update(Guid Id)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                if (!context.Suppliers.Any(p => p.ID == Id))
                {
                    return View();
                }
                else
                {
                    Supplier supplier = context.Suppliers.Single(p => p.ID == Id);
                    SupplierViewModel supplierViewModel = new SupplierViewModel
                    {
                        ID = supplier.ID,
                        SupplierName = supplier.SupplierName,
                        Address = supplier.Address,
                        Email = supplier.Email,
                        PhoneNumber = supplier.PhoneNumber,
                        Status = supplier.Status
                    };
                    return View(supplierViewModel);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SupplierViewModel viewModel)
        {
            using (var context = new Context())
            {
                Supplier supplier = context.Suppliers.Single(p => p.ID == viewModel.ID);
                supplier.SupplierName = viewModel.SupplierName;
                supplier.Address = viewModel.Address;
                supplier.Email = viewModel.Email;
                supplier.PhoneNumber = viewModel.PhoneNumber;
                context.SaveChanges();
                return RedirectToAction("Index", "AdminSupplier");
            }
        } 

        public ActionResult Disable(Guid Id, string CurrentURL)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                Supplier supplier = context.Suppliers.Single(p => p.ID == Id);
                supplier.Status = !supplier.Status;
                context.SaveChanges();
                return Redirect(CurrentURL);
            }
        }
    }
}