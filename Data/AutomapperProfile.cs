using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data.POCO;
using TrainingDiary.Models.ViewModels;

namespace TrainingDiary.Data
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Exercise, ExerciseViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Training, CreateTrainingViewModel>().ReverseMap();
            CreateMap<ExerciseTraining, ExerciseViewModel>()
                .ForMember(et => et.Name, opt => opt.MapFrom(src => src.Exercise.Name))
                .ReverseMap();
        }

    }
}
