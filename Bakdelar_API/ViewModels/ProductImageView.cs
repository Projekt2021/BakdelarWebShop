using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar_API.ViewModels
{
    public class ProductImageView
    {
        public int ImageId { get; set; }
        public string ImageURL { get; set; }
    }
}
