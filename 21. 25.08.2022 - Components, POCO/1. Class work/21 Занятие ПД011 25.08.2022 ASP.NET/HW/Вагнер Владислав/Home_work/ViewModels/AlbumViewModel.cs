using Home_work.Models;

namespace Home_work.ViewModels;

public class AlbumViewModel
{
    //Коллекция постов пользователей
    public List<Photo> Album { get; set; }

    //Общее кол-во фотографий в альбоме
    public int AlbumSize { get; set; }

    public AlbumViewModel(List<Photo> photos)
    {
        Album = photos;

        //Вычисления и задания свойств
        
        AlbumSize = photos.Count();
    }
}
