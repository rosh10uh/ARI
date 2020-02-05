using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ARI.EVOS.Dealers.Models
{
    /// <summary>
    /// This class is used to initialize properties of Get Ready model 
    /// </summary>
    public class GetReadyModel 
    {       
        public string CountryCode { get; set; }        
        public string DealerId { get; set; }        
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
