namespace H_WASP_NET.Models
{/// <summary>
 /// Информация о книге в библиотеке по заданию
 /// Сведения о книге в библиотеке: идентификатор книги, фамилия и инициалы автора,
 /// название, год издания, количество экземпляров данной книги в библиотеке,
 /// цена одного экземпляра, имя файла с изображением обложки.
 /// </summary>
 /// <param name="Id">идентифкатор</param>
 /// <param name="Autor"></param>
 /// <param name="Title"></param>
 /// <param name="PubYear"></param>
 /// <param name="Quantity"></param>
 /// <param name="Price"></param>
 /// <param name="FileName"></param>
    public record Book(int Id, string Autor, string Title, int PubYear, int Quantity, int Price, string FileName);
}
