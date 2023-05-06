﻿namespace HomeWork.Models;

// состояние сортировки поездок
// сортировка данных о поездке: по первичному ключу, по дате начала, по длительности, по стране пребывания – и по убыванию, и по
// возрастанию (по примеру, рассмотренному в классной работе, но выводить коллекцию в исходном и
// отсортированном виде надо на одну страницу, в одну и ту же таблицу).
public enum SortStateVisits
{
    // по первичному ключу (Id)
    IdAsc,
    IdDesc,

    // по дате начала
    DateStartAsc,
    DateStartDesc,

    // по длительности
    DurationAsc,
    DurationDesc,

    // по стране пребывания
    CountryAsc,
    CountryDesc
}
