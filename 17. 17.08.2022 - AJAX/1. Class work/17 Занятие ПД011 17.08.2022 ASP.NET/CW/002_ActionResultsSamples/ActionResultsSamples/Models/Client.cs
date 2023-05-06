using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionResultsSamples.Models;

public class Client
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }

    public Client():this(1, "tic", "tic@tac.toe") { } // Client

    public Client(int id, string login, string email) {
        Id = id;
        Login = login;
        Email = email;
    } // Client
} // class Client

