using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserDetail.Data;
using Microsoft.AspNetCore.Authorization;

namespace UserDetail.Controllers
{
    public class UserController : Controller
    {
        UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            //var userId = "test-id";
            var userId = User.Claims.Where(uId => uId.Type == "User_Id").Select(c => c.Value).SingleOrDefault();
            //var user = _context.User.Where(b => b.id == userId).ToList();
            return View(userId);
        }
    }
}