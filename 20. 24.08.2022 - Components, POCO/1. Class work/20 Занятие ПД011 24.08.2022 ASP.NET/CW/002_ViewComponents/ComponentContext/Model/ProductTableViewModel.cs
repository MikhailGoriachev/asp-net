using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentContext.Model;

// ViewModel для отображения итогов
public class ProductTableViewModel
{
    public List<Product> Products { get; set; }

    public bool ShowId { get; set; }
    public bool ShowTotal { get; set; }
    public double Total { get; set; }
}

