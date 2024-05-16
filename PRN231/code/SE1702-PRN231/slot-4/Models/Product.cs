using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace slot_4.Models
{
    public partial class Product
    {
        [JsonIgnore]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
    }
}
