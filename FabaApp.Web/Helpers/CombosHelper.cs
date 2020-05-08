using Microsoft.AspNetCore.Mvc.Rendering;
using FabaApp.Web.Data;
using System.Collections.Generic;
using System.Linq;

namespace FabaApp.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboLabs()
        {
            List<SelectListItem> list = _context.Labs
                .Where (t=>t.Active)
                .Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elija un Laboratorio...]",
                Value = "0"
            });

            return list;
        }
    }
}
