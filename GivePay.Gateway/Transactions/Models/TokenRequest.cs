using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "token_request")]
    public sealed class TokenRequest : IValidatableObject
    {
        [Required]
        [DataMember(Name = "terminal")]
        public Terminal Terminal { get; set; }

        /// <summary>
        /// The card information to exchange for a token
        /// </summary>
        [Required]
        [DataMember(Name = "card")]
        public Card Card { get; set; }

        [Required]
        [DataMember(Name = "mid")]
        public string Mid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (int.TryParse(Card.ExpirationYear, out int year) && int.TryParse(Card.ExpirationMonth, out int month))
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
                        new List<string> { nameof(Card.ExpirationMonth), nameof(Card.ExpirationYear) }));
                }
            }
            else
            {
                errors.Add(new ValidationResult("the expiration dates were not valid integers or are not present in the request",
                    new List<string> { nameof(Card.ExpirationMonth), nameof(Card.ExpirationYear) }));
            }

            return errors;
        }
    }
}
