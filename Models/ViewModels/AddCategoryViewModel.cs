using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}
