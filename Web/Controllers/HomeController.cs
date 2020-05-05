using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfolioItem> _portfolio;

        public HomeController(IUnitOfWork<Owner> owner,IUnitOfWork<PortfolioItem> portfolio)
        {
            this._owner = owner;
            this._portfolio = portfolio;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().FirstOrDefault(),
                PortfolioItems = _portfolio.Entity.GetAll().ToList(),
            };
            return View(model);
        }  
      

    }
}