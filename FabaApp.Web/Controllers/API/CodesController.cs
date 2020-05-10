using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;
using FabaApp.Web.Helpers;

namespace FabaApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public CodesController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/Codes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCodeEntity([FromRoute] int id)
        {
            List<CodeEntity> codes = await _context.Codes
                .Include (t=>t.SocialWork)
                .Where (t=>t.SocialWork.Id==id && t.Active)
                .OrderBy(t=>t.Code)
                .ToListAsync();

            return Ok(_converterHelper.ToCodeResponse(codes));
        }
    }
}