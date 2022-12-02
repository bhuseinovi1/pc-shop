using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;
using Core.Specifications;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        /* private readonly IProductRepository _repo;
       
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        } */
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;


        private readonly IProductRepository _nonGenericRepo;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IProductRepository nonGenericRepo) {
            _nonGenericRepo = nonGenericRepo;

            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() // ActionResult znaci da će povratna vrijednost biti neki oblik HTTP odgovora (200 ili 400...)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
            /* var products = await _productsRepo.ListAllAsync(); */
           /*  var products = await _repo.GetProductsAsync(); */
            /* var products = await _context.Products.ToListAsync(); */
            return Ok(products); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productsRepo.GetByIdAsync(id);
            /* var product = await _repo.GetProductByIdAsync(id); */
            return Ok(product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() // ActionResult znaci da će povratna vrijednost biti neki oblik HTTP odgovora (200 ili 400...)
        {
            var productsT = await _productTypeRepo.ListAllAsync();
            /* var productsT = await _repo.GetProductTypesAsync(); */
            /* var products = await _context.Products.ToListAsync(); */
            return Ok(productsT);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductBrands() // ActionResult znaci da će povratna vrijednost biti neki oblik HTTP odgovora (200 ili 400...)
        {
            var productsB = await _productBrandRepo.ListAllAsync();
            /* var productsB = await _repo.GetProductBrandsAsync(); */
            /* var products = await _context.Products.ToListAsync(); */
            return Ok(productsB);
        }


        // BEZ GENERIČKIH KLASA

        [HttpGet("asus")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetASUSProducts() // ActionResult znaci da će povratna vrijednost biti neki oblik HTTP odgovora (200 ili 400...)
        {
            var productsASUS = _nonGenericRepo.GetASUSProducts();
            /* var productsB = await _repo.GetProductBrandsAsync(); */
            /* var products = await _context.Products.ToListAsync(); */
            return Ok(productsASUS);
        }
    }
}