namespace Homework.Common;

// Класс валидации загружаемого файла на предмет поддерживаемых типов изображений
public static class ImageValidator
{
    // Проверка на допустимость расширения
    public static bool IsValidExtension(string ext) =>
        !string.IsNullOrEmpty(ext) && FileSignatures.ContainsKey(ext);

    // Проверка на допустимость сигнатуры типа файла
    public static bool IsValidSignature(IFormFile file)
    {
        var signatures = FileSignatures[Path.GetExtension(file.FileName).ToLowerInvariant()];

        using var reader = new BinaryReader(file.OpenReadStream());
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature)) ;
    }
    
    // Список сигнатур поддерживаемых типов изображений (https://filesignatures.net/)
    private static readonly Dictionary<string, List<byte[]>> FileSignatures = new()
    {
        [".jpeg"] = new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
        },
        [".jpg"] = new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
        },
        [".png"] = new List<byte[]>
        {
            new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
        }
    };
}