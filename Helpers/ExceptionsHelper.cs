namespace eCommerceRestAPI.Helpers
{
    public static class ExceptionsHelper
    {
        // USERS CONTROLLER EXCEPTIONS
        // ---------------------------
        // REGISTER ACTION
        public const string CurrencyCodeNotValid = "Currency code not valid.";
        public const string UsernamePasswordIncorrect = "The username or password is incorrect.";


        // PRODUCTS CONTROLLER EXCEPTIONS
        // ---------------------------
        // GetProductById ACTION
        public const string ProductNotExist = "Product does not exist.";


        // ORDERS CONTROLLER EXCEPTIONS
        // ---------------------------
        // CreateOrder ACTION
        public const string ProductIdNotExist = "Product does not exist.";

        // ChangeOrderStatus ACTION
        public const string OrderNotExist = "An order with this id does not exist.";
        public const string NotAbleToModify = "You don't have permission to modify this order.";
        public const string OrderStatusNotValid = "Order status is not valid.";
    }
}
