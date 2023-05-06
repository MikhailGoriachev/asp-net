namespace PartialViews02.Model;

public class ContactsViewModel
{
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public ContactsViewModel() {
        Phone = "212 85 06";
        Email = "abc@def.org";
        Address = "Заречье, ул. Приозерная, 1";
    }

    public ContactsViewModel(string phone, string email, string address) {
        Phone = phone;
        Email = email;
        Address = address;
    }
}

