using System;
using System.Collections.Generic;

#nullable disable

namespace Odev1.Models
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
