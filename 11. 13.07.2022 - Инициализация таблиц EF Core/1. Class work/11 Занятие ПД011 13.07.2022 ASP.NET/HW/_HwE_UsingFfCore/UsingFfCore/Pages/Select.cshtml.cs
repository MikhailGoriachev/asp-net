using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UsingFfCore.Models;

namespace UsingFfCore.Pages
{
    public class SelectModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public IQueryable<Publication> Publications { get; private set; }

        // ������������� �������
        public string? Task { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public SelectModel(ApplicationContext db) {
            _context = db;
        } // SelectModel

        public void OnGet() { }

        // �������� ���������� �� ������� � �������� ��������.
        public void OnGetQuery01(string pubIndex = "464885") {
            Task = $"������� ���������� �� ������� � �������� {pubIndex}";
            Publications = _context.Publications.Where(p => p.PubIndex == pubIndex);
        } // OnGetQuery01


        // �������� ���������� ��� ���� ��������, ��� ������� ���� 1 ����������
        // ���� �������� �� ���������� ���������.
        public void OnGetQuery02(int costFrom = 300, int costTo = 1200) {
            Task = $"������� ���������� �� �������� � ����� �� ��������� {costFrom}, �, {costTo}";
            Publications = _context.Publications.Where(p => costFrom <= p.Cost && p.Cost <= costTo);
        } // OnGetQuery02


        // ������ �� ������� �������� ���������� �� ��������, ��� �������
        // ������������ �������� ���� �������� �� ���������� ���������.
        public void OnGetQuery03(int durationFrom = 6, int durationTo = 12) {
            Task = $"������� ���������� �� �������� � ������������� �������� �� ��������� {durationFrom}, �, {durationTo}";
            Publications = _context.Publications.Where(p => durationFrom <= p.Duration && p.Duration <= durationTo);
        } // OnGetQuery03
    } // class QueriesModel
}
