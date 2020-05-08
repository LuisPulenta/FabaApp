using FabaApp.Common.Models;
using FabaApp.Web.Data.Entities;

namespace FabaApp.Web.Helpers
{
    public interface IConverterHelper
    {

        LabResponse ToLabResponse(LabEntity lab);

        //UserResponse ToUserResponse(UserEntity user);
    }
}
