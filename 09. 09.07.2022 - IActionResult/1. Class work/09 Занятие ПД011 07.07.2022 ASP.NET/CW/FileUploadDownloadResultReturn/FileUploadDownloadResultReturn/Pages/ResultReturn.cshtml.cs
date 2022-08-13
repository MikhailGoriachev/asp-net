using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileUploadDownloadResultReturn.Pages
{
    public class ResultReturnModel : PageModel
    {
        // �� ��������� ������ �� ����������
        public void OnGet()
        {
            ViewData["Message"] = "��������� GET";
        }
        

        // ����������� ContentResult - �����
        public IActionResult OnGetContent() {
            return Content($"������ �� �������: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        }

        // ������� ������� �������� - �� ��,��� � void OnGet()
        public IActionResult OnGetPage()
        {
            ViewData["Message"] = "������ �� Page()";
            return Page();
        }

        // ������������� �� ������ ��������
        public IActionResult OnGetRedirect()
        {
            // ������������� �� /TestPage/John/23000
            return RedirectToPage("/TestPage", new {name="John", salary=23_000});
        }

        // ������� ���������� ���� ������� - StatusCodeResult
        public IActionResult? OnGetStatusCode()
        {
            // return NotFound();
            // return NotFound(new {username="john"});
            return StatusCode(503);
        }

    }
}
