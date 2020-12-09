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
        private UserMapper userMapper = new UserMapper();
        private ProductManager productManager = new ProductManager();
        public void AddPartner(RegistrationDTO registrationDTO)
        {
            Partners partner = mapper.RegistrationDTOToPartner(registrationDTO);
            partnersDAO.AddNewPartner(partner);
        }

        //Для авторизации
        public bool CheckLoginAndPassword(string login, string password)
        {
            return partnersDAO.CheckLoginAndPassword(login, password);
        }

        public PartnerDto LogIn(string login)
        {
            Partners partner = partnersDAO.GetByLogin(login);
            PartnerDto partnerDto = userMapper.GetPartnerDto(partner);
            return partnerDto;
        }

        public List<Roles> GetAllRoles()
        {
            return rolesDAO.GetAll();
        }

        public int GetUserIdByLogin(string login)
        {
            return partnersDAO.GetByLogin(login).id_user;
        }
    }
}