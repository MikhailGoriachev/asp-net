namespace ViewComponentsFirst.Model;

// фотография из альбома пользователя
public class Photo
{
    // Идентификатор фотографии в альбоме
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }

    
    public Photo():this(1, "", "", "") { }

    public Photo(int id, string title, string url, string thumbnailUrl) {
        Id = id;
        Title = title;
        Url = url;
        ThumbnailUrl = thumbnailUrl;
    }
}

