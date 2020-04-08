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
            CreateMap<Training, CreateTrainingViewModel>()
                .ForMember(ctv => ctv.TrainigTime, opt => opt.MapFrom(src => src.TrainingTime))
                .ForMember(ctv => ctv.DoneExerciseViewModels, opt => opt.MapFrom(src=>src.ExerciseTraining))
                .ReverseMap();
            CreateMap<ExerciseTraining, ExerciseViewModel>()
                .ForMember(et => et.Name, opt => opt.MapFrom(src => src.Exercise.Name))
                .ForMember(et => et.Category, opt => opt.MapFrom(src => src.Exercise.Category))
                .ForMember(et => et.SeriesViewModels, opt => opt.MapFrom(src => src.Series))
                .ReverseMap();
            CreateMap<Series, SeriesViewModel>().ReverseMap();
        }

    }
}
