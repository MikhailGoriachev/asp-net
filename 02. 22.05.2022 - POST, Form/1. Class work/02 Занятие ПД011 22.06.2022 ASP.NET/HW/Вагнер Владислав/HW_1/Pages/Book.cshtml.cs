using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HW_1.Models;

namespace HW_1.Pages
{
    public class BookModel : PageModel
    {
        public Book Book { get; private set; }
        public void OnGet()
        {
            Book = new Book("Python ��� �����. ����������� �� ����������������", "������ ��.",2017);
        }
    }
}
