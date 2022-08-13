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


    // получить фигуру
    public static IFigure GetFigure(int? type = null)
    {
        type ??= GetInt(0, 3);

        return type switch
        {
            0 => new Square { Id = ++IFigure.CurrentMaxId, Side = Math.Round(GetDouble(12d, 20d), 5) },
            1 => new Triangle
            {
                Id = ++IFigure.CurrentMaxId,
                Sides = (Math.Round(GetDouble(10d, 13d), 5), Math.Round(GetDouble(10d, 13d), 5),
                    Math.Round(GetDouble(10d, 13d), 5))
            },
            _ => new Rectangle
            {
                Id = ++IFigure.CurrentMaxId, SideA = Math.Round(GetDouble(10d, 20d), 5),
                SideB = Math.Round(GetDouble(10d, 20d), 5)
            }
        };
    }


    // получить коллекцию фигур
    public static List<IFigure> GetFigurList(int count) => Enumerable.Repeat(0, count)
        .Select(i => GetFigure())
        .ToList();

    #endregion
}
