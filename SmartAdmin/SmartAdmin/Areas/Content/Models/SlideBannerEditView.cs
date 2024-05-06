using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Content.Models;
using Microsoft.AspNetCore.Http;

namespace SmartAdmin.Models
{
    public class SlideBannerEditView
    {
        public SlideBanner Data { get; set; }

        [Display(Name = "Image")] 
        public IFormFile FileUpload { get; set; }
    }
}
