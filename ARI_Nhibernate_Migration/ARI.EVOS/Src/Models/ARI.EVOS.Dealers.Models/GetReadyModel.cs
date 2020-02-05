using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prism.Validation;

namespace ARI.EVOS.Dealers.Models
{
    /// <summary>
    /// This class is used to initialize properties of Get Ready model 
    /// </summary>
    public class GetReadyModel : ValidatableBindableBase
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.COUNTRYCODE_REQUIRED))]
        public string CountryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.DEALERID_REQUIRED))]
        public string DealerId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MAKECODE_REQUIRED))]
        public string MakeCode { get; set; }
        public List<string> GetReadyCategories { get; set; }
        public string GetReadyCategory { get; set; }
        public string ClientId { get; set; }
        public decimal? GetReadyAmount { get; set; }
        public DateTime? GetReadyEffectiveDate { get; set ; }
        public string LastProgram { get; set; }
        public string LastUser { get; set; }
        public DateTime? LastChange { get; set; }
    }
}
