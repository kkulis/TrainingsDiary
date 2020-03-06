using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrainingDiary.Data.POCO
{
    public class ExerciseTraining
    {
        public Guid Id { get; set; }
        public virtual Exercise Exercise { get; set; }
        public Guid ExerciseID { get; set; }
        public virtual Training Training { get; set; }
        public Guid TrainingId { get; set; }
        [JsonIgnore]
        public ICollection<Series> Series { get; set; }

    }
}
