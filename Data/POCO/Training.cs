using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrainingDiary.Data.POCO
{
    public class Training
    {
        public int TrainingNumber { get; set; }
        public Guid Id { get; set; }
        public DateTime TrainingStart { get; set; }
        public DateTime TrainingEnd { get; set; }
        public TimeSpan TrainingTime { get; set; }
       // [JsonIgnore]
        public virtual ICollection<ExerciseTraining> ExerciseTraining { get; set; }



    }
}
