using ContactManagement.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages.Contacts
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Só para exibir os dados na tela de confirmação
        [BindProperty]
        public Contact Contact { get; set; } = null!;

        // GET /Contacts/Delete/5
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Contact = await _context.Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Contact == null)
            {
                return NotFound();
            }

            return Page();
        }

        // POST /Contacts/Delete/5
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            // Soft delete
            contact.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}