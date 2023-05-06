using Home_work.Models;

namespace Home_work.ViewModels;

public class TodoViewModel
{
    //Коллекция постов пользователей
    public List<Todo> Tasks { get; set; }

    //Общее кол-во постов
    public int TaksQuantity { get; set; }

    //Завершенные задачи
    public int Completed { get; set; }

    //Незавершенные задачи
    public int Uncompleted { get; set; }

    public TodoViewModel(List<Todo> tasks)
    {
        Tasks = tasks;

        //Вычисления и задания свойств
        
        TaksQuantity = tasks.Count();
        Completed = tasks.Where(t => t.Completed).Count();
        Uncompleted = tasks.Where(t => !t.Completed).Count();

    }
}
