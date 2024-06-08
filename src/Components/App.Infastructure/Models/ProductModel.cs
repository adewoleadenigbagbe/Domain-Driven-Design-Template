using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infastructure.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Vat { get; set; }
    }

}
