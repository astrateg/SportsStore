﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System.ComponentModel;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository productsRepository;
        public int PageSize = 4;

        public ProductsController(IProductsRepository productsRepository) 
        {
            this.productsRepository = productsRepository;
            // Тут было примечание
        }

        public ViewResult List(string category, int page = 1)
        {
            var productsToShow = (category == null)
                ? productsRepository.Products
                : productsRepository.Products.Where(x => x.Category == category);
            var viewModel = new ProductsListViewModel {
                Products = productsToShow.Skip((page-1)*PageSize).Take(PageSize).ToList(),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productsToShow.Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);  // Passed to view as ViewData.Model (or simply Model)
        }

        public FileContentResult GetImage(int productId)
        {
            var product = productsRepository.Products.First(x => x.ProductID == productId);
            return File(product.ImageData, product.ImageMimeType);
        }
    }
}
