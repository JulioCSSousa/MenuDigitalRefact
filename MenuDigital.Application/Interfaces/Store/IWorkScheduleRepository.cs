using MenuDigital.Domain.Entities;

namespace MenuDigital.Domain.Interfaces
{
    public interface IWorkScheduleRepository
    {
        IQueryable<WorkSchedule> GetAsync();
        Task<WorkSchedule> GetByIdAsync(int id);
        Task Create(WorkSchedule workSchedule);
        Task UpdateAsync(WorkSchedule workSchedule);
        Task DeleteAsync(WorkSchedule workSchedule);

    }
}
