using System;
using System.Collections.Generic;

namespace eCommerceRestAPI.Dtos.Output
{
    public class OrderOutputDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public List<string> ProductNames { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public string CreatedAt { get; set; }
    }
}
