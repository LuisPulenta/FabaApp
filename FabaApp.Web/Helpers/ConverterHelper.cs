using FabaApp.Common.Models;
using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;

namespace FabaApp.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;


        public ConverterHelper(DataContext context)
        {
            _context = context;
        }

        public LabResponse ToLabResponse(LabEntity lab)
        {
            return new LabResponse
            {
                Active = lab.Active,
                Address = lab.Address,
                Contact = lab.Contact,
                CUIT = lab.CUIT,
                DischargeDate = lab.DischargeDate,
                Email = lab.Email,
                Id = lab.Id,
                Name = lab.Name,
                Phone = lab.Phone
            };
        }


        //public UserResponse ToUserResponse(UserEntity user)
        //{

        //    return new UserResponse
        //    {
        //        Active = user.Active,
        //        DischargeDate = user.DischargeDate,
        //        Document = user.Document,
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        Id = user.Id,
        //        Lab = ToLabResponse(user.Lab),
        //        LastName = user.LastName,
        //        PhoneNumber = user.PhoneNumber,
        //        UserType = user.UserType
        //    };
        //}
    }
}