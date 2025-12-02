using ContactManagement.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages.Contacts
{
    [Authorize]
    public class ListModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public ListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Contact> Contacts { get; set; } = new();

        public async Task OnGetAsync()
        {
            var contato = await _context.Contacts.ToListAsync();

            Contacts = contato;
        }
    }
}