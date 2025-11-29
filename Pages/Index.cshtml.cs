using ContactManagement.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista que será exibida na página
        public List<Contact> Contacts { get; set; } = new();

        public async Task OnGetAsync()
        {
            var contato = await _context.Contacts.Where(p => p.IsDeleted == false).ToListAsync();

            Contacts = contato;
        }
    }
}