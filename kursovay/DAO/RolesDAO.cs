using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;
namespace kursovay.DAO
{
    public class RolesDAO
    {
        private CargoDataBase db = new CargoDataBase();

        public List<Roles> GetAll()
        {
            return db.Roles.ToList();
        }
    }
}