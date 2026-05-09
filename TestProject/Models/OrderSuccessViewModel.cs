using System.Collections.Generic;

namespace TestProject.Models
{
    public class OrderSuccessViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public long CustomerId { get; set; }
        public List<FoodItem> OrderedItems { get; set; }
    }
}