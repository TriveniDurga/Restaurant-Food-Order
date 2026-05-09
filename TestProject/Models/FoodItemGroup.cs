using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class FoodItemGroup
    {
        public string ItemType { get; set; }
        public List<FoodItem> Items { get; set; }
    }
}