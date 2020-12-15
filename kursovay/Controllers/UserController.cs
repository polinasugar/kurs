using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kursovay.DTO;
using kursovay.Models;
using kursovay.Services;

namespace kursovay.Controllers
{
    public class UserController : Controller
    {
        //Для регистрации
        private UserService userSerivice = new UserService();
       
        public ActionResult RegistrationForm()
        {
            List<Roles> roles = userSerivice.GetAllRoles();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach(Roles role in roles)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = role.title,
                    Value = role.id_role.ToString()
                };
                items.Add(item);
            }
            SelectList selectList = new SelectList(items, "id_role", "title");
            ViewData["selectList"] = selectList;
            return View();
        }

        public ActionResult LogOut()
        {
            Session["session"] = null;
            return RedirectToAction("LoginForm");
        }

        [HttpPost]
        public ActionResult RegistrationForm(RegistrationDTO newUser)
        {
            userSerivice.AddPartner(newUser);
            Session["user"] = userSerivice.GetUserIdByLogin(newUser.LoginAndPassword.Login);
            Session["login"] = newUser.LoginAndPassword.Login;
            return RedirectToAction("PrivatePage", "User", new { login = newUser.LoginAndPassword.Login });
        }

        //Для авторизации
        public ActionResult LoginForm()
        {
            return View();
        }

        public ActionResult PrivatePage(string login)
        {
            PartnerDto partnerDto = userSerivice.LogIn(login);
            return View(partnerDto);
        }

        public ActionResult LogIn(string login, string password)
        {
            if (userSerivice.CheckLoginAndPassword(login, password))
            {
                Session["session"] = userSerivice.LogIn(login);
                Session["login"] = login;
                return RedirectToAction("PrivatePage", new { login = login });
            }
            else
            {
                //После поменять
                return View("LoginForm");
            }
        }
    }
}