using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class OrderModel
    {

        [Required]
        [Display(Name ="Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string CustomerPhone { get; set; }
        public List<int> SelectedFoodItemIds { get; set; }
        public List<FoodItemGroup> FoodItemGroups { get; set; }
    }
}