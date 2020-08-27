namespace eCommerceRestAPI.Dtos.Input.Orders
{
    public class ChangeOrderStatusDto
    {
        public int OrderId { get; set; }

        public string Status { get; set; }
    }
}
