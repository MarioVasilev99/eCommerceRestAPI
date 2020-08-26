namespace eCommerceRestAPI.Dtos.Input.Orders
{
    using System.Collections.Generic;

    public class CreateOrderDto
    {
        public List<int> Products { get; set; }
    }
}
