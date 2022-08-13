using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Post.Models;

namespace RazorPages_Post.Pages
{
    [IgnoreAntiforgeryToken]
    public class SailingFleetModel : PageModel
    {
        // ��������� �������� ��������� �����
        private List<Sail> _fleet = new List<Sail>();
        public List<Sail> Fleet {
            get => _fleet; 
            private set => _fleet = value;
        } // Fleet

        // ��������� GET-������� /SailingFleet
        public void OnGet() {
            _fleet.Clear();
            _fleet.AddRange(new[]{
                new Sail("�������� �������", "��������", 69.34, 15.8, 3556, 1765, "victory.jpg"),
                new Sail("������ �������", "����� ����", 85.4, 10.97, 2133, 1869, "cuttysark.jpg"),
                new Sail("����", "�����������", 114.5, 14, 1645, 1926, "kruzenstern.jpg"),
            });

            // ������������ ����� ���������� ��������
            _fleet.ForEach(s => s.Image = "/images/task02/" + s.Image);
        } // OnGet


        // POST-������ �� �����, ������ ��� �����������
        public void OnPost(Sail[] fleet) {
            _fleet = fleet.ToList();
        } // OnPost
    } // class SailingFleetModel
}
