namespace TouristicAgencyMvcCore.Models.Reports;

// Тип записи коллекции итогового запроса для отчета 1 - позиционный record
public record Result01(string Purpose, int MinCost, double AvgCost, int MaxCost, int Amount);
