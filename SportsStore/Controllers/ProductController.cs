﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Domain;
using SportsStore.Models.ProductViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.GetAll().OrderBy(b => b.Name).ToList();
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetById(id);
            ViewData["IsEdit"] = true;
            ViewData["Categories"] = GetCategoriesSelectList();
            return View(new EditViewModel(product));
        }

        [HttpPost]
        public IActionResult Edit(int id, EditViewModel editViewModel)
        {
            Product product = _productRepository.GetById(id);
            MapToProduct(editViewModel, product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var product = new Product();
            ViewData["IsEdit"] = false;
            ViewData["Categories"] = GetCategoriesSelectList();
            return View(nameof(Edit), new EditViewModel(product));
        }

        [HttpPost]
        public IActionResult Create(EditViewModel editViewModel)
        {
            var product = new Product();
            MapToProduct(editViewModel, product);
            _productRepository.Add(product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            ViewData["ProductName"] = _productRepository.GetById(id).Name;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = _productRepository.GetById(id);
            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetCategoriesSelectList(int selected = 0)
        {
            return new SelectList(_categoryRepository.GetAll().OrderBy(g => g.Name).ToList(),
                nameof(Category.CategoryId), nameof(Category.Name), selected);
        }

        private void MapToProduct(EditViewModel editViewModel, Product product)
        {
            product.Name = editViewModel.Name;
            product.Description = editViewModel.Description;
            product.Price = editViewModel.Price;
            product.InStock = editViewModel.InStock;
            product.Category = _categoryRepository.GetById(editViewModel.CategoryId);
        }
    }
}