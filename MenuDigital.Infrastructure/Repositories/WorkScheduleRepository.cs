
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Interfaces;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Infrastructure.Repositories
{
    public class WorkScheduleRepository : IWorkScheduleRepository
    {
        private readonly AppDbContext _context;

        public WorkScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<WorkSchedule> GetAsync()
        {
            
            var result = _context.WorkSchedules.AsQueryable();
            return result;

        }

        public async Task<WorkSchedule> GetByIdAsync(int id)
        {
            return await _context.WorkSchedules.FindAsync(id);
        }

        public async Task Create(WorkSchedule workSchedule)
        {
            try
            {
                await _context.WorkSchedules.AddAsync(workSchedule);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error to save Schedule");
            }
        }

        public async Task UpdateAsync(WorkSchedule workSchedule)
        {
            if (workSchedule == null) throw new ArgumentNullException(nameof(workSchedule));

            try
            {
                _context.Update(workSchedule);
                _context.Entry(workSchedule).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception) { throw new Exception("Failed to update"); }

        }

        public async Task DeleteAsync(WorkSchedule workSchedule)
        {
            if (workSchedule == null)
            {
                throw new NullReferenceException("Schedule not found");
            }
            _context.WorkSchedules.Remove(workSchedule);
            await _context.SaveChangesAsync();
        }
    }
}
