using System;
using System.Text;
using System.Threading;
using Home_work.Models;
using Newtonsoft.Json;

namespace Home_work.Infrastructure;

public static class Utils
{

    // объект для получения случайных чисел
    public static Random Random = new Random();

    // формирование случайных чисел в диапазоне от lo до hi
    public static double GetRandom(double lo, double hi)
        => lo + (hi - lo) * Random.NextDouble();
    public static int GetRandom(int lo, int hi)
        => Random.Next(lo, hi);

   
    //Закодировать/Декодировать строку 
    public static string EncodeString(string str, int key = 42)
    {
        byte[] strBytes = Encoding.UTF8.GetBytes(str);

        byte[] result = new byte[strBytes.Length];

        for (int i = 0; i < strBytes.Length; i++)
        {

            result[i] = (byte)(strBytes[i] ^ key);
        }

        return Encoding.UTF8.GetString(result);
    }


    #region Сереализация/Десериализация

    //Сериализация 
    public static void Serialize(Object obj,string filePath, char devider = '\\')
    {
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);

        //Получение имени папки
        string directory = filePath.Replace(filePath.Substring(filePath.LastIndexOf($"{devider}")), "");

        //Есил каталога нет, то создать 
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(filePath, json);
    }

    //Десериализация
    public static T Deserialize<T>(string filePath) where T : new()
    {
        //Если файла не существует - создать объект по умолчанию (например пустой List<>)
        if (!File.Exists(filePath))
            return new();

        string json = File.ReadAllText(filePath);

        return JsonConvert.DeserializeObject<T>(json);
    }

    //Опасная десериализация - 
    public static T Deserialization<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);

        return JsonConvert.DeserializeObject<T>(json);
    }

    #endregion

    //Сохранение файла 
    public static bool SaveFile(string path, IFormFile formFile)
    {
        string fullPath = Path.Combine(path, formFile.FileName);

        try{
            //Запись файла по нужному пути
            using (FileStream stream = File.Create(fullPath))
            {
                formFile.CopyTo(stream);
            }
        } 
        catch (Exception){
            return false;
        }

        return true;

    }



} // class Utils