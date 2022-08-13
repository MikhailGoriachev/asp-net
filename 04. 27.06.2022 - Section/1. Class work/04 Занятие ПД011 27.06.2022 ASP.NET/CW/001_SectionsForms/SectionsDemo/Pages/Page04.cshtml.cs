using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SectionsDemo.Models;

namespace SectionsDemo.Pages
{
    // ��������� ������ submit �� �����, ����������� �������� POST �� ��������
    public class Page04Model : PageModel
    {
        // �������� ������
        IEnumerable<Person> _people = new List<Person>
        {
            new ("����", 37),
            new ("����", 28),
            new ("����", 41),
            new ("�����", 25),
            new ("����", 37)
        };
        public IEnumerable<Person> DisplayedPeople { get; private set; } = new List<Person>();


        // ��������� GET-�������, ����� ��������
        public void OnGet() {
            DisplayedPeople = _people;
        } // OnGet


        // ���������� ������ ������ submit
        public void OnPostGreaterThan(int age) {
            DisplayedPeople = _people.Where(p => p.Age > age);
        } // OnPostGreaterThan


        // ���������� ������ ������ submit
        public void OnPostLessThan(int age) {
            DisplayedPeople = _people.Where(p => p.Age < age);
        } // OnPostLessThan
    } // class Page04Model

}
