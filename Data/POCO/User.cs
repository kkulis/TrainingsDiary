using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Data.POCO
{
    public class User : IdentityUser
    {
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public ICollection<Category> Categories { get; set;  }
    }
}
