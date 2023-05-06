using Home_work.Models;

namespace Home_work.ViewModels;

public class UsersViewModel
{
    public List<User> Users { get; set; }

    public UsersViewModel(List<User> users)
    {
        Users = users;
    }
}
