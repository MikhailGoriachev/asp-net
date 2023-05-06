using Newtonsoft.Json;

namespace Homework.Common;

public static class Utils
{
    // Случайная дата в указанном промежутке
    public static DateTime RandomDate(DateTime from, DateTime? to = null) => 
        from.AddDays(Random.Shared.Next(((to ?? DateTime.Today) - from).Days));
    
    // Сериализация/десерализация данных в JSON-файл
    public static void Serialize<T>(List<T> data, string dataPath) => 
        File.WriteAllText(dataPath, JsonConvert.SerializeObject(data, Formatting.Indented));
    public static List<T> Deserialize<T>(string dataPath) => 
        JsonConvert.DeserializeObject<List<T>>(System.IO.File.ReadAllText(dataPath))!;
    
    public static string EncryptDecrypt(string data, int key)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data).Select(x => (byte)(x ^ key)).ToArray();
        /*for(int i = 0; i < bytes.Length; i++){
            bytes[i] = (byte)(bytes[i] ^ key);
        }*/
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}