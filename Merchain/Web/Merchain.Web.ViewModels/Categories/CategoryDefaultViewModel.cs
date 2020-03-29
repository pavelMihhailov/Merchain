namespace Merchain.Web.ViewModels.Categories
{
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class CategoryDefaultViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
