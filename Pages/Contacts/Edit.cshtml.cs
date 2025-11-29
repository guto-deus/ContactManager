using ContactManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Pages.Contacts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContactInputModel Input { get; set; } = new();

        // GET /Contacts/Edit/5
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var contact = await _context.Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (contact == null)
            {
                return NotFound();
            }

            // Preenche o Input com os dados do banco
            Input = new ContactInputModel
            {
                Id = contact.Id,
                Name = contact.Name,
                ContactNumber = contact.ContactNumber,
                Email = contact.Email
            };

            return Page();
        }

        // POST /Contacts/Edit/5
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Verificar se o contato ainda existe
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == Input.Id && !c.IsDeleted);

            if (contact == null)
            {
                return NotFound();
            }

            // Atualiza os dados
            contact.Name = Input.Name;
            contact.ContactNumber = Input.ContactNumber;
            contact.Email = Input.Email;

            await _context.SaveChangesAsync();

            // Volta para a lista
            return RedirectToPage("/Index");
        }

        public class ContactInputModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "O nome é obrigatório.")]
            [MinLength(6, ErrorMessage = "O nome deve ter mais de 5 caracteres.")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "O contato é obrigatório.")]
            [RegularExpression(@"^\d{9}$", ErrorMessage = "O contato deve possuir exatamente 9 dígitos.")]
            [Display(Name = "Contato")]
            public string ContactNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "O e-mail é obrigatório.")]
            [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
            public string Email { get; set; } = string.Empty;
        }
    }
}