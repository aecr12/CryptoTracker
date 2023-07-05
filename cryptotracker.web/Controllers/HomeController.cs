using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.web.API;
using cryptotracker.web.ViewModals;
using Microsoft.AspNetCore.Mvc;
using cryptotracker.entity;

namespace cryptotracker.web.Controllers
{
    public class HomeController:Controller
    {   
        DataAPI dataAPI = new DataAPI();
        public async Task<IActionResult> Index(int page=1)
        {
            var cryptoCurrencyDataViewModal = new CryptoCurrencyDataViewModal
            {
                DataPerPage = 20,
                CryptoCurrencies = await dataAPI.GetAll(),
                CurrentPage = page
            };
            return View(cryptoCurrencyDataViewModal);
        }

        public async Task<IActionResult> Trending()
        {
            var trendingDataViewModal = new TrendingDataViewModal();
            trendingDataViewModal.TrendingDatas = await dataAPI.GetTrending();
            return PartialView("~/Views/Shared/PartialViews/TrendingPartialView.cshtml",trendingDataViewModal);
        }

        public async Task<IActionResult> Dumping()
        {
            var dumbpingDataViewModal = new DumpingDataViewModal();
            dumbpingDataViewModal.DumpingDatas = await dataAPI.GetDumping();
            return PartialView("~/Views/Shared/PartialViews/DumpingPartialView.cshtml",dumbpingDataViewModal);
        }
    }
}