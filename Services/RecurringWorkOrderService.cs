using Microsoft.EntityFrameworkCore;
using CanopyViewer.Data;
using CanopyViewer.Models;

namespace CanopyViewer.Services
{
    //This service tracks recurrence as a background task.
    public class RecurringWorkOrderService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RecurringWorkOrderService> _logger;

        public RecurringWorkOrderService(
            IServiceScopeFactory scopeFactory,
            ILogger<RecurringWorkOrderService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Recurring work order service started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessRecurringWorkOrders();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing recurring work orders.");
                }

                //Check every ten minutes
                await Task.Delay(TimeSpan.FromMinutes(10));
            }
        }

        private async Task ProcessRecurringWorkOrders()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var now = DateTime.UtcNow;
            //Find all recurring work orders that are due
            var dueWorkOrders = await db.WorkOrders
                .Where(w =>
                    w.RecurrenceType == "Recurring" &&
                    w.NextOccurrence.HasValue &&
                    w.NextOccurrence.Value <= now &&
                    (w.RecurCount == null || w.RecurCount > 0))
                .ToListAsync();

            _logger.LogInformation(
                "Found {Count} recurring work orders to process.", dueWorkOrders.Count);
            foreach (var template in dueWorkOrders)
            {
                var newWorkOrder = new WorkOrder
                {
                    Title = template.Title,
                    Description = template.Description,
                    Status = "New",
                    CreatedDate = now,
                    CreatedBy = "System",
                    AssignedById = template.AssignedById,
                    AssignedToId = template.AssignedToId,
                    AssetId = template.AssetId,

                    RecurrenceType = "One-Time"
                };

                db.WorkOrders.Add(newWorkOrder);

                template.NextOccurrence = RecurrenceCalculator.AdvanceOccurrence(
                    template.NextOccurrence!.Value,
                    template.RecurrenceInterval ?? "Daily");

                if (template.RecurCount.HasValue)
                    template.RecurCount--;
            }

            await db.SaveChangesAsync();
        }
    }
}
