namespace Merchain.Web.ViewModels.Shared
{
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}