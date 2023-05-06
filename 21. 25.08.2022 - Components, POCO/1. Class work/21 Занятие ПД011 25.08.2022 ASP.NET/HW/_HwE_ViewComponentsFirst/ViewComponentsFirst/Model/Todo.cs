namespace ViewComponentsFirst.Model;

// дело из списка дел пользователя
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }


    public Todo() : this(1, "", false) { }

    public Todo(int id, string title, bool completed) {
        Id = id;
        Title = title;
        Completed = completed;
    } // Post
}

