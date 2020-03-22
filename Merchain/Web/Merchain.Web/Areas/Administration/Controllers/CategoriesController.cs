namespace Merchain.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await this.categoriesService.GetAllAsync();

            return this.View(categories);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.categoriesService.GetByIdAsync((int)id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title")] Category category)
        {
            if (this.ModelState.IsValid)
            {
                await this.categoriesService.AddCategoryAsync(category);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.categoriesService.GetByIdAsync((int)id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Category category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.categoriesService.Edit(category);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.categoriesService.GetByIdAsync((int)id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await this.categoriesService.GetByIdAsync(id);

            await this.categoriesService.Delete(category);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}