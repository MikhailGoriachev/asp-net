namespace UsingTagHelpers2.Models;

// Возмодные режимы сортировки для Sales
//     Идентификатор
//     Дата продажи
//     Цена продажи
public enum SortState
{
    IdAsc,         // Идентификатор по возрастанию
    IdDesc,        // Идентификатор по убыванию
    SaleDateAsc,   // Дата продажи по возрастанию
    SaleDateDesc,  // Дата продажи по убыванию
    SalePriceAsc,  // Цена продажи по возрастанию
    SalePriceDesc  // Цена продажи по убыванию
}

