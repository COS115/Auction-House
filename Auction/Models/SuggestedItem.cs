using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.Models
{
    public class SuggestedItem
    {
        public int ID { set; get; }

        [Required]
        [Display(Name = "Name")]
        public String Name { set; get; }

        [Display(Name = "Description")]
        public String Description { set; get; }

        [Display(Name = "Rating")]
        public string Rating { set; get; }

        [Display(Name = "Image")]
        public byte[] PhotoData { set; get; }
    }
}