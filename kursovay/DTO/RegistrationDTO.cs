using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kursovay.DTO
{
    public class RegistrationDTO
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public int Role { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string ITN { get; set; }
        public LoginAndPasswordDTO LoginAndPassword { get; set; }

    }
}