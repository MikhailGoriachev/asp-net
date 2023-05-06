namespace ViewComponentsFirst.Model;

// Сведения о пользователях, прочитанные из ресурса по запросу
// https://jsonplaceholder.typicode.com/users/.
// Сохранять поля: id, name, username, email, phone, website. 
// 
public class User
{
    public int    Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }


    public User():this(1, "", "", "", "", "") { } // User

    public User(int id, string name, string userName, string email, string phone, string website) {
        Id = id;
        Name = name;
        UserName = userName;
        Email = email;
        Phone = phone;
        Website = website;
    } // User
} // class User

