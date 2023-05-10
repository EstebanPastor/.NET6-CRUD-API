using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? BarCode { get; set; }

    public string? ProdDescription { get; set; }

    public string? ProdLabel { get; set; }

    public int? IdCategory { get; set; }

    public decimal? Price { get; set; }

    public virtual Category? oCategory { get; set; }
}
