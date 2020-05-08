using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FabaApp.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboLabs();
    }
}
