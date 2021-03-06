﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrainingDiary.Data.POCO
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
