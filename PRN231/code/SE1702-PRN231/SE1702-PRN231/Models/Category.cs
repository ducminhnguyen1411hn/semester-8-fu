﻿using System;
using System.Collections.Generic;

namespace SE1702_PRN231.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
