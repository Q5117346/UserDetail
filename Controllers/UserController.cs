using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserDetail.Data;
using Microsoft.AspNetCore.Authorization;
using UserDetail.Models;
using UserDetail.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace UserDetail.Controllers
{
    public class UserController : Controller
    {
        IConfiguration configuration;
        UserContext _context;

        public UserController(IConfiguration configuration, UserContext context)
        {
            this.configuration = configuration;
            this._context = context;
        }

        public UserController(UserContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.User.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,email,phoneNumber,address,canBuy")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexAsync));
            }
            return View(user);
        }
    }
}