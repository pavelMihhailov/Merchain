namespace Merchain.Data.Models
{
    public class UserOrder
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
