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
            string hashedPassword = Cryptographer.Hash(password);
            if(db.Partners.Select(p => p.login).Contains(login))
            {
                string partnerPassword = db.Partners.Where(p => p.login == login).Select(p => p.password).First();
                if(partnerPassword == hashedPassword)
                {
                    return true;
                }
            }
            return false;
        }

        public Partners GetByLogin(string login)
        {
            return db.Partners.Where(p => p.login == login).First();
        }
    }
}