namespace eCommerceRestAPI.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            this.Orders = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<OrderProduct> Orders { get; set; }
    }
}
