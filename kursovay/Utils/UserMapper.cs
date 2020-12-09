using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kursovay.DTO;
using kursovay.Models;

namespace kursovay.Utils
{
    public class UserMapper
    {
        public PartnerDto GetPartnerDto(Partners partner)
        {
            PartnerDto partnerDto = new PartnerDto()
            {
                Id = partner.id_user,
                FirstName = partner.first_name,
                SecondName = partner.second_name,
                MiddleName = partner.middle_name,
                Telephone = partner.telephone,
                Address = partner.address
            };
            if(partner.Roles.title == RoleTypes.Supplier.ToString())
            {
                partnerDto.Role = RoleTypes.Supplier;
            }
            else if(partner.Roles.title == RoleTypes.Wholesaler.ToString())
            {
                partnerDto.Role = RoleTypes.Wholesaler;
            }
            return partnerDto;
        }
    }
}