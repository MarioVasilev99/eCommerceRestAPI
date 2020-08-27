namespace eCommerceRestAPI.Helpers
{
    public static class RoutesHelper
    {
        // USERS CONTROLLER
        public const string UserController = "api/[controller]";

        // USERS CONTROLLER - ACTIONS
        public const string UserLogin = "Login";
        public const string UserRegister = "Register";

        // PRODUCTS CONTROLLER
        public const string ProductsController = "api/[controller]";

        // PRODUCTS CONTROLLER - ACTIONS
        public const string GetAllProducts = "All";
        public const string GetProductById = "byId/{id}";
        public const string CreateProduct = "Create";
        public const string DeleteProduct = "Delete/{productId}";

        // ORDERS CONTROLLER
        public const string OrdersController = "api/[controller]";

        // ORDERS CONTROLLER - ACTIONS
        public const string OrderCreate = "Create";
        public const string OrderGetUserOrders = "all/{userId}";
        public const string OrderChangeStatus = "status/change";
    }
}
