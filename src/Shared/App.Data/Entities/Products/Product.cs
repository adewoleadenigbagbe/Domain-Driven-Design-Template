using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Product : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get; set ; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Vat { get; set; }

        public Category Category { get; set; }

        public DateTime? CreatedOn { get; set ; }

        public DateTime? ModifiedOn { get ; set; }

        public bool IsDeprecated { get ; set ; }
    }

    [Flags]
    public enum Category
    {
        Electronics = 1 << 1,
        Clothings = 1 << 2,
        Grocery = 1 << 3,
        Automobile = 1 << 4,
        Books = 1 << 5,
        Beauty = 1 << 6,
        Softwares = 1 << 7,
        FineArt = 1 << 8,
        Sports = 1 << 9,
        Fashion = 1 << 10
    }
}
