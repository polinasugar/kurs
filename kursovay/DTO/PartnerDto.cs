using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;

namespace kursovay.DTO
{
    public class PartnerDto
    {
        public int Id { get; set; }

        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }

        public RoleTypes Role { get; set; }

        public string Telephone { get; set; }

        public string Address { get; set; }
        
        public List<int> ItemsId { get; set; }

        public PartnerDto()
        {
            ItemsId = new List<int>();
        }
    }
}