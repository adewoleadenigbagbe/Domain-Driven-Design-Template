using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App.Data.Entities;
using App.Infastructure.Models;
using AutoMapper;

namespace App.Infastructure.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        { 
            CreateMap<Product,ProductModel>();
        }
    }
}
