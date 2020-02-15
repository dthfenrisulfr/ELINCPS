namespace Elin_Course_Project_Service.Models
{
    /// <summary>
    ///  Модель отвечающая за свзяь многие-ко-многим между таблицами Заказы и продукты
    /// </summary>
    public class ProductsToOrders
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
    }
}
