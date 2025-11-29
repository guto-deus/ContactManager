using ContactManagement.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        // Modelo que será preenchido pelo form
        [BindProperty]
        public ContactInputModel Input { get; set; } = new();

        // GET: exibe o formulário
        public void OnGet()
        {
        }

        // POST: recebe o formulário
        public async Task<IActionResult> OnPostAsync()
        {
            // Valida data annotations primeiro
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Mapear Input -> Entidade
            var contact = new Contact
            {
                Name = Input.Name,
                ContactNumber = Input.ContactNumber,
                Email = Input.Email,
                IsDeleted = false
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Depois de salvar, volta para a lista
            return RedirectToPage("/Index");
        }

        // ViewModel / InputModel com validações
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