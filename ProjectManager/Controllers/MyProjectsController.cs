using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Helpers;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Route("api/my/projects")]
    [Authorize]
    [ApiController]
    public class MyProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MyProjectsController(AppDbContext context)
        {
            _context = context;
        }

        private bool TryGetUser([NotNullWhen(true)] out User? user)
        {
            user = (User?)HttpContext.Items["User"];
            return user != null;
        }


        // GET: api/MyProjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            if (!TryGetUser(out var user))
                return Problem("Unauthorized");

            if (_context.Projects == null)
            {
                return NotFound();
            }

            return await MyProjects(user).ToListAsync();
        }

        private IQueryable<Project> MyProjects(User user)
        {
            return _context.Projects.Where(p => p.OwnerId == user.Id);
        }

        // GET: api/MyProjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            if (!TryGetUser(out var user))
                return Problem("Unauthorized");

            if (_context.Projects == null)
            {
                return NotFound();
            }

            var project = await MyProjects(user).FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }
    }
}
