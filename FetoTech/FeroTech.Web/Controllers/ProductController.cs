using FeroTech.Infrastructure.Application.DTOs;
using FeroTech.Infrastructure.Application.Interfaces;
using FeroTech.Infrastructure.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeroTech.Web.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _repo.GetAllAsync();
            var dtos = products.Select(p => new ProductDto
            {
                Id = p.MemberId,
                Name = p.Name,
                Price = p.Price
            });
            return View(dtos);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price
                };
                await _repo.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }
    }
}
