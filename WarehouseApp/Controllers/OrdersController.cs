    using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data;
using WarehouseApp.Models;

namespace WarehouseApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            if (User.IsInRole("Admin"))
            {
                
                var orders = await _context.Orders.Include(o => o.OrderItems)
                                                  .ThenInclude(oi => oi.Product)
                                                  .ToListAsync();
                return View(orders);
            }
            else
            {
                
                var orders = await _context.Orders.Where(o => o.UserId == userId)
                                                  .Include(o => o.OrderItems)
                                                  .ThenInclude(oi => oi.Product)
                                                  .ToListAsync();
                return View(orders);
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            
                var order = await _context.Orders
                    .Include(o => o.OrderItems)  
                    .ThenInclude(oi => oi.Product) 
                    .Include(o => o.IdentityUser)  
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }

            decimal totalPrice = 0;
            foreach (var orderItem in order.OrderItems)
            {
                
                orderItem.UnitPrice = orderItem.Product.Price;
                totalPrice += orderItem.UnitPrice * orderItem.Quantity;
            }

            
            ViewData["TotalPrice"] = totalPrice;

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            
            var products = await _context.Products.ToListAsync();

            
            if (products == null || !products.Any())
            {
                return View("Error", "No products available");
            }

            
            ViewBag.Products = new SelectList(products, "Id", "Name");
            ViewBag.Statuses = new List<string> { "Pending", "Completed", "Cancelled" };
            
            var model = new Order
            {
                OrderDate = DateTime.Now,
                Status = Order.OrderStatus.Pending.ToString(),
                OrderItems = new List<OrderItem>
            {
                new OrderItem()  
            }
            };

            return View(model);
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order model)
        {
           
            var userId = _userManager.GetUserId(User);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

           
            model.UserId = userId;
            model.IdentityUser = user; 
            model.OrderDate = DateTime.Now;
            model.Status = Order.OrderStatus.Pending.ToString();
            decimal totalAmount = 0;
            
            foreach (var orderItem in model.OrderItems)
            {
                orderItem.Order = model; 
                var product = await _context.Products.FindAsync(orderItem.ProductId);
                if (product != null)
                {
                    orderItem.UnitPrice = product.Price;  
                }
                orderItem.Order = model;
                if (product.Stock < orderItem.Quantity)
                {
                    ModelState.AddModelError(string.Empty, "Not enough stock for " + product.Name);
                    return View(model);
                }

                
                product.Stock -= orderItem.Quantity;
                totalAmount += orderItem.Quantity * product.Price;
                model.TotalAmount = totalAmount;
            }

            
            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Customers, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,Status,UserId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Customers, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.IdentityUser)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
