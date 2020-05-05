using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class PortfolioItemEditViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }
        [MinLength(50)]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
    }
}
