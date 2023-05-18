using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPamyatajka.Models;
using WebPamyatajka.Services;

namespace WebPamyatajka.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
        // GET: Category

        [HttpGet]
        public List<Category> Index()
        {
            return _categoryService.GetAllByCreatorIdOrDefault();
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody] Category category)
        {

            var addedCategory = _categoryService.Create(category);
            return Ok(addedCategory);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteById(id);
            return Ok();
        }

        [HttpPatch]
        public ActionResult<Category> Rename([FromBody] Category category)
        {
            return Ok(_categoryService.Rename(category));
        }
    }
}