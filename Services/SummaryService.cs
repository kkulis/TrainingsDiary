using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data;
using TrainingDiary.Models.ViewModels;

namespace TrainingDiary.Services
{
    public interface ISummaryService
    {
        public Task<TrainingSummaryViewModel> GetDoneTraining(int trainingNumber);
    }
    public class SummaryService : ISummaryService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public SummaryService(ApplicationDbContext applicationDbContext, IMapper mapper )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<TrainingSummaryViewModel> GetDoneTraining(int trainingNumber)
        {
            var training = await _applicationDbContext.Trainings.Include(t => t.ExerciseTraining)
                                                                .ThenInclude(et => et.Series)
                                                                .Include(et => et.ExerciseTraining)
                                                                .ThenInclude(s => s.Exercise)
                                                                .ThenInclude(c => c.Category)
                                                                .FirstOrDefaultAsync(t => t.TrainingNumber == trainingNumber);

            return _mapper.Map<TrainingSummaryViewModel>(training);
        }
    }
}
