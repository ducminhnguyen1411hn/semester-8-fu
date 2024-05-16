using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace slot_4.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [JsonIgnore]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
