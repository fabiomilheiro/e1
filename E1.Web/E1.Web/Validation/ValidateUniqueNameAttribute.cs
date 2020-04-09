using System;
using System.ComponentModel.DataAnnotations;
using E1.Web.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace E1.Web.Validation
{
    public class ValidateUniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value?.ToString();

            if (name == string.Empty)
            {
                return ValidationResult.Success;
            }
            
            if (DoesNameExist(validationContext, name))
            {
                return new ValidationResult($"A person named {name} already exists.");
            }

            return ValidationResult.Success;
        }

        private static bool DoesNameExist(ValidationContext validationContext, string name)
        {
            var personRepository = validationContext.GetRequiredService<IPersonRepository>();
            return personRepository.Exists(name);
        }
    }
}