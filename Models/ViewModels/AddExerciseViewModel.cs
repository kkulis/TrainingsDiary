using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class AddExerciseViewModel
    {
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
        public SelectList Categories { get; set; }
    }
}
