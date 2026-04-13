namespace CanopyViewer.Services
{
    public static class RecurrenceCalculator
    {
        public static DateTime AdvanceOccurrence(DateTime current, string interval)
        {
            return interval switch
            {
                "Daily" => current.AddDays(1),
                "Weekly" => current.AddDays(7),
                "Monthly" => current.AddMonths(1),
                "Yearly" => current.AddYears(1),
                _ => current.AddDays(1)
            };
        }
    }
}
