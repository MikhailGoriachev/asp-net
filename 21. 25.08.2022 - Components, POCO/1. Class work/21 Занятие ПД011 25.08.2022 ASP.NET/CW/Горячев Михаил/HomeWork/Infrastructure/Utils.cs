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


    // коллекция ФИО
    private static List<(string surname, string name, string patronymic)>? _fullNameCollection;

    public static List<(string surname, string name, string patronymic)> FullNameCollection =>
        _fullNameCollection ??= new()
        {
            ("Игнатов", "Лев", "Протасьевич"),
            ("Афанасьев", "Тихон", "Ефимович"),
            ("Матвеев", "Константин", "Оскарович"),
            ("Михеев", "Абрам", "Романович"),
            ("Кононов", "Влас", "Витальевич"),
            ("Одинцов", "Адам", "Еремеевич"),
            ("Буров", "Модест", "Матвеевич"),
            ("Князев", "Аверьян", "Еремеевич"),
            ("Емельянов", "Лука", "Святославович"),
            ("Захаров", "Тарас", "Валерьевич"),
            ("Дмитриева", "Фрида", "Юрьевна"),
            ("Федосеева", "Аурелия", "Геннадьевна"),
            ("Филиппова", "Алира", "Семёновна"),
            ("Рожкова", "Ляля", "Лаврентьевна"),
            ("Васильева", "Алла", "Семеновна"),
            ("Белоусова", "Христина", "Степановна"),
            ("Мартынова", "Любовь", "Пантелеймоновна"),
            ("Попова", "Ирэна", "Донатовна"),
            ("Сазонова", "Эдуарда", "Пантелеймоновна"),
            ("Григорьева", "Ева", "Семёновна"),
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
