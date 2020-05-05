using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Web.ViewModels;
using Infrastructure.Repositories;
using Core.Interfaces;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfolioItem> _portfolioItem;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolioItem, IHostingEnvironment hostingEnvironment)
        {
            this._portfolioItem = portfolioItem;
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {

            List<PortfolioItemCreateViewModel> portfolioItems = _portfolioItem.Entity.GetAll().Select(item => new PortfolioItemCreateViewModel
            {
                Id = item.Id,
                ProjectName = item.ProjectName,
                Description = item.Description,
                ImageUrl = item.ImageUrl
            }).ToList();

            return View(portfolioItems);
        }


        public IActionResult Create()
        {
            return View();
        }

        [Obsolete]
        private string UploadImage(IFormFile photo, string path = "img", string oldImage = "")
        {
            string uniqueFileName = null;
            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);
            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            if (!string.IsNullOrEmpty(oldImage))
            {
                if (oldImage != null)
                {
                    string oldfilePath = Path.Combine(_hostingEnvironment.WebRootPath, path, oldImage);
                    System.IO.File.Delete(oldfilePath);
                }
            }
            photo.CopyTo(new FileStream(filePath, FileMode.Create));
            return uniqueFileName;
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Create(PortfolioItemCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                PortfolioItem item = new PortfolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                };
                if (model.File != null)
                {
                    item.ImageUrl = UploadImage(model.File, "img/portfolio");
                }
                _portfolioItem.Entity.Insert(item);
                _portfolioItem.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(Guid Id)
        {
            var portfolio = _portfolioItem.Entity.GetById(Id);
            PortfolioItemEditViewModel model = new PortfolioItemEditViewModel()
            {
                Id = portfolio.Id,
                ImageUrl = portfolio.ImageUrl,
                Description = portfolio.Description,
                ProjectName = portfolio.ProjectName,
            };
            return View(model);
        }
        public IActionResult Details(Guid Id)
        {
            var portfolio = _portfolioItem.Entity.GetById(Id);

            return View(portfolio);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Edit(PortfolioItemEditViewModel model, Guid Id)
        {
            if (ModelState.IsValid)
            {
                PortfolioItem item = _portfolioItem.Entity.GetById(Id);

                item.ProjectName = model.ProjectName;
                item.Description = model.Description;

                if (model.File != null)
                {
                    item.ImageUrl = UploadImage(model.File, "img/portfolio", item.ImageUrl);
                }
                _portfolioItem.Entity.Update(item);
                _portfolioItem.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid Id)
        {
            var portfolio = _portfolioItem.Entity.GetById(Id);
            return View(portfolio);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(Guid Id)
        {
            var portfolio = _portfolioItem.Entity.GetById(Id);
            _portfolioItem.Entity.Delete(Id);
            _portfolioItem.Save();
            return RedirectToAction("Index");
        }
    }
}
