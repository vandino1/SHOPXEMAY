using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HondaVN.Data;
using HondaVN.Models;
using Microsoft.AspNetCore.Authorization;

namespace HondaVN.Controllers
{
    
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        [HttpGet("/admin/contact")]
        //[Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contact.ToListAsync());
        }

        // GET: Contacts/Details/5
        [HttpGet("/admin/contact/detail/{id}")]
        //[Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        } 

        // GET: Contacts/Create
        [HttpGet("/contact/")]
        public IActionResult SendContact()
        {
            return View();
        }
        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/contact/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContact([Bind("FullName,Email,Message,Phone")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateSent = DateTime.Now;

                _context.Add(contact);
                await _context.SaveChangesAsync();

                TempData["AlertMessage"] = "Liên hệ của bạn đã được gửi.";

                return RedirectToAction("Index", "Home");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [HttpGet("/admin/contact/delete/{id}")]
        //[Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost("/admin/contact/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Đã xóa liên hệ.";
            return RedirectToAction(nameof(Index));
        }
        
    }
}
