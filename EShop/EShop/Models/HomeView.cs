using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;
using Content.Models;

namespace EShop.Models
{
    public class HomeView
    {
        public List<SlideBanner> BannerList = new List<SlideBanner>();
        public List<Item> NewList = new List<Item>();
        public List<Item> FeaturedList = new List<Item>();
    }
}
