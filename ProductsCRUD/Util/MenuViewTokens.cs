using System.ComponentModel;

namespace ProductsCRUD.Util
{
    public enum MenuViewTokens
    {
        [Description("ProductsMain")]
        PRODUCTS_MAIN,
        [Description("CustomersMain")]
        CUSTOMERS_MAIN,
        [Description("OrdersMain")]
        ORDERS_MAIN,
        [Description("SalesMain")]
        SALES_MAIN,
        [Description("CustomersReport")]
        CUSTOMERS_REPORT,
        [Description("OrdersReport")]
        ORDERS_REPORT,
        [Description("SalesReport")]
        SALES_REPORT,        
        [Description("UsersRegister")]
        USER_REGISTER,        
        [Description("Login")]
        LOGIN,
        [Description("ManageUsers")]
        MANAGE_USERS
    }
}
