using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SpotifyShuffler.Attributes
{
    public class SameWith : ValidationAttribute
    {
        public string SecondPropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object o = validationContext.ObjectType.GetProperties().First(x => x.Name == SecondPropertyName).GetValue(validationContext.ObjectInstance);

            if (o == value)
                return ValidationResult.Success;

            return new ValidationResult($"\"{value}\" of {validationContext.DisplayName} are not same with \"{o}\".");
        }
    }
}