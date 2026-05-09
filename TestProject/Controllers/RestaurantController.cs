using System.Linq;
using System.Web.Mvc;
using TestProject.Models;
using System.Collections.Generic;

namespace TestProject.Controllers
{
    public class RestaurantController : Controller
    {
        AssignmentEntities db = new AssignmentEntities();

        public ActionResult CreateOrder()
        {
            // Require login / session
            if (Session["Username"] == null)
            {
                TempData["Info"] = "Please sign in or register before placing an order.";
                return RedirectToAction("Login", "User");
            }

            var foodItemGroups = db.FoodItems
               .GroupBy(fi => fi.ItemType)
               .Select(group => new FoodItemGroup { ItemType = group.Key, Items = group.ToList() })
               .ToList();

            var model = new OrderModel
            {
                FoodItemGroups = foodItemGroups
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderModel model)
        {
            if (Session["Username"] == null)
            {
                TempData["Info"] = "Please sign in or register before placing an order.";
                return RedirectToAction("Login", "User");
            }

            if (ModelState.IsValid)
            {
                // create customer
                var newCustomer = new Customer
                {
                    Name = model.CustomerName,
                    Phone = model.CustomerPhone
                };

                db.Customers.Add(newCustomer);
                db.SaveChanges();
                long customerId = newCustomer.CustomerID;

                var orderedItems = new List<FoodItem>();
                if (model?.SelectedFoodItemIds != null)
                {
                    foreach (var foodItemId in model.SelectedFoodItemIds)
                    {
                        var orderItem = new Order
                        {
                            Customer_ID = customerId,
                            Item_ID = foodItemId
                        };

                        db.Orders.Add(orderItem);

                        var food = db.FoodItems.FirstOrDefault(fi => fi.Id == foodItemId);
                        if (food != null) orderedItems.Add(food);
                    }

                    db.SaveChanges();
                }

                var successModel = new OrderSuccessViewModel
                {
                    CustomerName = newCustomer.Name,
                    CustomerPhone = newCustomer.Phone,
                    CustomerId = customerId,
                    OrderedItems = orderedItems
                };

                TempData["Success"] = "Order placed successfully.";
                return View("OrderSuccess", successModel);
            }

            // rebuild list for redisplay
            var foodItemGroups = db.FoodItems
                .GroupBy(fi => fi.ItemType)
                .Select(group => new FoodItemGroup { ItemType = group.Key, Items = group.ToList() })
                .ToList();

            model.FoodItemGroups = foodItemGroups;

            TempData["Error"] = "Please correct the errors and try again.";
            return View(model);
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}