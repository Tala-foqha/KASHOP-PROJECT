using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Validations
{
    public class AllowedExctentionAttribute:ValidationAttribute
    {
        string[] _extentions = { ".jpg", ".webp" };
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var extention = Path.GetExtension(file.FileName).ToLower();
                if(!_extentions.Contains(extention))
                { //print
                    return new ValidationResult($"Allowed Extention :{string.Join(" ,",_extentions)} ");

                }

            }
            return ValidationResult.Success;

        }
    }
}
