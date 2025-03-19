using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OLM.Data;
using OLM.Helper;
using OLM.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;


namespace OLM.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper; // Khai báo IMapper

       
        private readonly OlmContext _context;

        public UserController(OlmContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult DashBoard()
        {
            return View("~/Views/User/Student/Dashboard/Index.cshtml");
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [HttpGet]
        public IActionResult Login(string? ReturnURL)
        {
            ViewBag.ReturnURL = ReturnURL;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnURL)
        {
            ViewBag.ReturnUrl = ReturnURL;
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(user => user.Username == model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("Error","Does't have this student");
                }
                else
                {
                    if (user.IsActive.HasValue && !user.IsActive.Value)
                    {
                        ModelState.AddModelError("Error", "This Account has been lock, please contact to Admin");
                    }

                    else
                    {
                        if (user.Password != model.Password.ToMd5Hash(user.RandomKey))
                        {
                            ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            var claims = new List<Claim> {
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.Name, user.FullName),
                                new Claim(MySetting.CLAIM_USERID, user.UserId.ToString()),


                                new Claim(ClaimTypes.Role, user.Role)
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);

                            if (Url.IsLocalUrl(ReturnURL))
                            {
                                return Redirect(ReturnURL);
                            }
                            else
                            {
                                return RedirectToAction("DashBoard", "User");
                            }
                        }
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _mapper.Map<User>(model);
                    user.RandomKey = MyTool.GenerateRamdomKey();
                    user.Password = model.Password.ToMd5Hash(user.RandomKey);
                    user.IsActive = true;//sẽ xử lý khi dùng Mail để active
                    user.Role = "Student";



                    _context.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "User");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound();

            var User = new UserVM
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Address = user.Address,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber?.ToString(),
                ActivationDate = user.CreatedAt,
                StudentStatus = user.IsActive == true ? "Active" : "Suspended"
            };

            return View("~/Views/User/Student/Profile.cshtml",User);
        }

        [HttpPost]
        public IActionResult Edit(int id, UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            user.Email = model.Email ?? user.Email;
            user.Address = model.Address;
            user.Username = model.Username;
            user.PhoneNumber = int.TryParse(model.PhoneNumber, out int phone) ? phone : null;

            _context.SaveChanges();

            return RedirectToAction("Dashboard", new { id = user.UserId });
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View("~/Views/User/Student/Profile.cshtml");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}