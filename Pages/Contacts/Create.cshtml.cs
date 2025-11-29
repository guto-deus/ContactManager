using ContactManagement.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public ContactInputModel Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contact = new Contact
            {
                Name = Input.Name,
                ContactNumber = Input.ContactNumber,
                Email = Input.Email,
                IsDeleted = false
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public class ContactInputModel
        {
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