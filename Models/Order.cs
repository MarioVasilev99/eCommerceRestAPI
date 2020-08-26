namespace eCommerceRestAPI.Models
{
    using eCommerceRestAPI.Models.Enums;
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Order()
        {
            this.Products = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }

        public decimal TotalPrice { get; set; }

        public StatusEnum Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
