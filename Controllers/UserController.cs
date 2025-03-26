using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index()
        {
            // Return the Index view with the user list
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Find the user with the specified ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // Return a NotFound result if the user is not found
                return NotFound();
            }

            // Return the Details view with the user data
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Return the Create view
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (user == null)
            {
                // Return a BadRequest result if the user is null
                return BadRequest();
            }

            // Add the user to the user list
            userlist.Add(user);

            // Redirect to the Index action after successful creation
            return RedirectToAction(nameof(Index));
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.

            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // Return a NotFound result if the user is not found
                return NotFound();
            }

            // Return the Edit view with the user data
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // Find the user with the specified ID
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                // Return a NotFound result if the user is not found
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the user's properties
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                // Redirect to the Index action after successful update
                return RedirectToAction(nameof(Index));
            }

            // Return the Edit view with the user data if the model state is invalid
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Find the user with the specified ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // Return a NotFound result if the user is not found
                return NotFound();
            }

            // Return the Delete view with the user data
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // Find the user with the specified ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // Return a NotFound result if the user is not found
                return NotFound();
            }

            // Remove the user from the user list
            userlist.Remove(user);

            // Redirect to the Index action after successful deletion
            return RedirectToAction(nameof(Index));
        }

        public ActionResult FindByName(string name)
        {
            // Find the user(s) with the specified name (case-insensitive)
            var users = userlist.Where(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (users == null || users.Count == 0)
            {
                // Return a NotFound result if no users are found
                return NotFound();
            }

            // Return the view with the list of users
            return View(users);
        }

        public IActionResult SearchByName(string name)
        {
            // Filtrar usuarios cuyo nombre contenga el término de búsqueda (ignorando mayúsculas/minúsculas)
            var users = userlist
                .Where(u => u.Name.Contains(name ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Devolver una vista parcial con los usuarios filtrados
            return PartialView("_UserTable", users);
        }
}
