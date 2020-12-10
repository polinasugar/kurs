using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;
using kursovay.Utils;

namespace kursovay.DAO
{
    public class PartnersDAO
    {
        //Для регистрации
        private CargoDataBase db = new CargoDataBase();
        public void AddNewPartner(Partners partners)
        {
            db.Partners.Add(partners);
            db.SaveChanges();
        }

        //Для авторризации
        public bool CheckLoginAndPassword(string login, string password)
        {
            try
            {
                Partners partner = GetByLogin(login);
                if(partner.login == login && partner.password == password)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Partners GetByLogin(string login)
        {
            return db.Partners.Where(p => p.login == login).First();
        }

        public string GetLoginById(int id)
        {
            return db.Partners.Where(u => u.id_user == id).First().login;
        }
    }
}