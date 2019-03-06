using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "card")]
    public sealed class Card : IValidatableObject
    {
        [StringLength(19, MinimumLength = 15, ErrorMessage = "The card number is not the proper length")]
        [RegularExpression("[0-9]{15,19}", ErrorMessage = "The card number must be 15-19 numeric characters.")]
        [DataMember(Name = "card_number")]
        public string Pan { get; set; }

        [Required]
        [DataMember(Name = "card_present")]
        public bool CardPresent { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "The expiration month must be in the form \"MM\"")]
        [RegularExpression("(0[1-9]|1[0-2])", ErrorMessage = "The expiration month must be two numerical characters within the range 01 to 12")]
        [DataMember(Name = "expiration_month")]
        public string ExpirationMonth { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "The expiration year must be in the form \"YY\"")]
        [RegularExpression("[0-9]{2}", ErrorMessage = "The expiration year must be two numerical characters")]
        [DataMember(Name = "expiration_year")]
        public string ExpirationYear { get; set; }

        [DataMember(Name = "cvv")]
        public string Cvv { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!string.IsNullOrWhiteSpace(Pan))
            {
                if (int.TryParse(ExpirationYear, out int year) && int.TryParse(ExpirationMonth, out int month))
                {
                    var now = DateTime.Now;
                    var thisMonth = now.Month;
                    var thisYear = now.Year;
                    year += 2000; // Place into this century

                    var totalMonthsToday = thisYear * 12 + thisMonth;
                    var totalMonthsCard = year * 12 + month;

                    if (totalMonthsToday > totalMonthsCard)
                    {
                        errors.Add(new ValidationResult("The card is expired",
                            new List<string> { nameof(ExpirationMonth), nameof(ExpirationYear) }));
                    }
                }
                else
                {
                    errors.Add(new ValidationResult("the expiration dates were not valid integers or are not present in the request",
                        new List<string> { nameof(ExpirationMonth), nameof(ExpirationYear) }));
                }
            }

            if (!string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace(Pan))
            {
                errors.Add(new ValidationResult("a token and PAN cannot both be provided",
                    new List<string> { nameof(Token), nameof(Pan) }));
            }
            else if (string.IsNullOrWhiteSpace(Token) && string.IsNullOrWhiteSpace(Pan))
            {
                errors.Add(new ValidationResult("you must provide a token or PAN",
                    new List<string> { nameof(Token), nameof(Pan) }));
            }

            return errors;
        }
    }
}
