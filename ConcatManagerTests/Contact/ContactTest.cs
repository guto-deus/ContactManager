using ContactManager.Models;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Tests
{
    public class ContactTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, context, results, validateAllProperties: true);
            return results;
        }

        [Fact]
        public void Name_Should_Fail_When_Less_Than_5_Characters()
        {
            var contact = new Contact
            {
                Name = "Jose",
                ContactNumber = "123456789",
                Email = "valid@email.com"
            };

            var results = ValidateModel(contact);

            Assert.Contains(results, v => v.MemberNames.Contains("Name"));
        }

        [Fact]
        public void ContactNumber_Should_Fail_When_Not_9_Digits()
        {
            var contact = new Contact
            {
                Name = "ValidName",
                ContactNumber = "12345",
                Email = "valid@email.com"
            };

            var results = ValidateModel(contact);

            Assert.Contains(results, v => v.MemberNames.Contains("ContactNumber"));
        }

        [Fact]
        public void Email_Should_Fail_When_Invalid()
        {
            var contact = new Contact
            {
                Name = "ValidName",
                ContactNumber = "123456789",
                Email = "not-an-email"
            };

            var results = ValidateModel(contact);

            Assert.Contains(results, v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void Model_Should_Pass_With_Valid_Values()
        {
            var contact = new Contact
            {
                Name = "ValidName",
                ContactNumber = "123456789",
                Email = "email@teste.com",
                IsDeleted = false
            };

            var results = ValidateModel(contact);

            Assert.Empty(results); 
        }

        [Fact]
        public void IsDeleted_Default_Should_Be_False()
        {
            var contact = new Contact();
            Assert.False(contact.IsDeleted);
        }
    }
}