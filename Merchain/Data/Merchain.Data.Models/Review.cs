namespace Merchain.Data.Models
{
    using Merchain.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public int Stars { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
