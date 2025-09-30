
using MenuDigital.Domain.Entities;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace appointmentApp.ApplicationSeed
{
    public class SeedWorkSchedule
    {
        public static async Task SeedWorkSchedules(AppDbContext context, ILogger<SeedWorkSchedule> logger)
        {
            try
            {
                if (!await context.WorkSchedules.AnyAsync())
                {
                    DateTime today = DateTime.Today;

                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        DateTime dayDate = today.AddDays(((int)day - (int)today.DayOfWeek + 7) % 7);

                        bool isSelected = true;
                        TimeSpan start = new TimeSpan(8, 0, 0);
                        TimeSpan end = new TimeSpan(17, 0, 0);

                        var workSchedule = new WorkSchedule(dayDate, isSelected, start, end);
                        context.WorkSchedules.Add(workSchedule);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (MySqlConnector.MySqlException ex)
            {
                logger.LogError($"Erro ao conectar ao MySQL: {ex.Message}");
                throw new Exception("Falha ao inicializar os WorkSchedules. Verifique a conexão com o banco de dados.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro inesperado: {ex.Message}");
                throw;
            }
        }
    }
}
