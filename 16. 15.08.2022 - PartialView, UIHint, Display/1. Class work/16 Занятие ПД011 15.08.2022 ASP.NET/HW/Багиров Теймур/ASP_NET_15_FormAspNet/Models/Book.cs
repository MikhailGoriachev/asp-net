namespace ASP_NET_15_FormAspNet.Models;

/*
  класс "Книга", с полями: 
    * идентификатор книги
    * фамилия и инициалы автора
    * название
    * год издания
    * цена одного экземпляра
    * кол-во экземпляров данной книги в библиотеке
    * имя файла и изображением обложки  
*/
public record class Book(int Id, string Fio, string Name, int Year, int Price, int Amount, string Cover);