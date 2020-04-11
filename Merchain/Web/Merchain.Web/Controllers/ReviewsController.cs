namespace Merchain.Web.Controllers
{
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.Products;
    using Merchain.Web.ViewModels.Reviews;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<ReviewsController> logger;

        public ReviewsController(
            IReviewsService reviewService,
            IProductsService productsService,
            UserManager<ApplicationUser> userManager,
            ILogger<ReviewsController> logger)
        {
            this.reviewService = reviewService;
            this.productsService = productsService;
            this.userManager = userManager;
            this.logger = logger;
        }

        [Authorize]
        public IActionResult AddReview(int id)
        {
            var product = this.productsService.GetById<ProductDefaultViewModel>(id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Products");
            }

            var viewModel = new AddReviewInputModel()
            {
                Product = product,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(AddReviewInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

            var reviewId = await this.reviewService
                .AddReview(user.Id, inputModel.ProductId, inputModel.Stars, inputModel.Title, inputModel.Content);

            if (reviewId == -1)
            {
                return this.View(inputModel.ProductId);
            }

            return this.RedirectToAction("ProductReviews", new { productId = inputModel.ProductId });
        }

        public IActionResult ProductReviews(int productId)
        {
            var product = this.productsService.GetById<ProductDefaultViewModel>(productId);
            var reviews = this.reviewService.GetReviewsForProduct<ReviewDefaultViewModel>(productId);

            var viewModel = new ProductReviewsViewModel()
            {
                Product = product,
                Reviews = reviews,
            };

            return this.View(viewModel);
        }
    }
}
