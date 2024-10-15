using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Events;

public class AddSelectedProductVariantToBasketEvent
{
    public Product Product { get; set; }
    public int UserId { get; set; }
}
