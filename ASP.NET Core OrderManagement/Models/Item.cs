﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }
    }
}
