namespace Merchain.Web.ViewModels.Administration.Categories
{
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
