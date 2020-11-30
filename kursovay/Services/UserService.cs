using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.Models;
using kursovay.DTO;
using kursovay.Utils;
using kursovay.DAO;

namespace kursovay.Services
{
    public class UserService
    {
        //Для регистрации
        private PartnersDAO partnersDAO = new PartnersDAO();
        private RolesDAO rolesDAO = new RolesDAO();
        private Mapper mapper = new Mapper();
        private ProductManager productManager = new ProductManager();
        public void AddPartner(RegistrationDTO registrationDTO)
        {
            Partners partner = mapper.RegistrationDTOToPartner(registrationDTO);
            partnersDAO.AddNewPartner(partner);
        }

        //Для авторизации
        public bool CheckLoginAndPassword(LoginAndPasswordDTO loginAndPassword)
        {
            return partnersDAO.CheckLoginAndPassword(loginAndPassword.Login, loginAndPassword.Password);
        }

        public List<Roles> GetAllRoles()
        {
            return rolesDAO.GetAll();
        }

        public int GetUserIdByLogin(string login)
        {
            return partnersDAO.GetByLogin(login).id_user;
        }

        public void MakeOrder(List<ProductDto> productDtos, int partnerId)
        {
            List<Products> products = mapper.MapProductDtoToList(productDtos);
            productManager.AddOrder(products, partnerId);
        }
    }
}