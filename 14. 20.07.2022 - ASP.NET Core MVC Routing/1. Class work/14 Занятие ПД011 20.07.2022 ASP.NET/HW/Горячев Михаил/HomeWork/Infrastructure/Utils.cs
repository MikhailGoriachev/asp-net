using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWork.Models;

namespace HomeWork.Infrastructure;

// Класс Утилиты
public static class Utils
{
    // объект для получения случайных числовых значений
    private static Random _rand;

    public static Random Rand
    {
        get => _rand;
        set => _rand = value;
    }

    // виды издания
    public static List<string> TypesEditionList { get; private set; } = new()
    {
        "газета",
        "каталог",
        "журнал"
    };

    #region Конструкторы

    // статический конструктор
    static Utils()
    {
        // установка значений
        _rand = new Random();
    }

    #endregion

    #region Утилиты

    // получение случайного целого числа
    public static int GetInt(int min, int max) => _rand.Next(min, max);


    // получение слуйчного дробного числа
    public static double GetDouble(double min, double max)
    {
        // если границы диапазона неверны
        if (min >= max)
            throw new InvalidDataException($"Utils.GetDouble: Указан непрваильный диапазон ({min} - {max})");

        double value;

        do
        {
            value = GetInt((int)min, (int)max - 1) + _rand.NextDouble();
        } while (value <= min && value >= max);

        return value;
    }


    // генератор случайных дат
    public static DateTime GetDate(DateTime minDate, DateTime maxDate)
    {
        // разница в днях между датами
        int days = (int)(maxDate - minDate).TotalDays;

        return maxDate.AddDays(GetInt(0, days));
    }

    // сохранить файл на сервер
    public static void SaveFile(string path, IFormFile file)
    {
        using Stream stream = File.Create(Path.Combine(path, file.FileName));

        file.CopyTo(stream);
    }

    #endregion
}
