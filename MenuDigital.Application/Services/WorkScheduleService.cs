using MenuDigital.Application.Interfaces;
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Application.Services
{
    public class WorkScheduleService : IWorkScheduleService
    {
        private readonly IWorkScheduleRepository _repository;
        public WorkScheduleService(IWorkScheduleRepository repository) 
        {
            _repository = repository;
        }

        async Task<IEnumerable<WorkSchedule>> IWorkScheduleService.GetAsync()
        {
            return await _repository.GetAsync().ToListAsync();
        }
        async Task IWorkScheduleService.Create(WorkSchedule workSchedule)
        {

             await _repository.Create(workSchedule);

        }

        async Task IWorkScheduleService.DeleteAsync(WorkSchedule workSchedule)
        {
            await _repository.DeleteAsync(workSchedule);
        }

        
        async Task<WorkSchedule> IWorkScheduleService.GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        async Task IWorkScheduleService.UpdateAsync(WorkSchedule workSchedule)
        {
           await _repository.UpdateAsync(workSchedule);
        }

        
    }
}
