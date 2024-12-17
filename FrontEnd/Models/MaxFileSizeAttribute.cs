
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    internal class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file != null)
                {
                    if (file.Length > _maxFileSize)
                    {
                        return new ValidationResult(
                            $"El tamaño máximo del archivo debe ser de {_maxFileSize / (1024 * 1024)} MB.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

    // Validación de extensiones de archivo permitidas
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    if (!_extensions.Contains(extension))
                    {
                        return new ValidationResult(
                            $"Solo se permiten archivos con las siguientes extensiones: {string.Join(", ", _extensions)}");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}