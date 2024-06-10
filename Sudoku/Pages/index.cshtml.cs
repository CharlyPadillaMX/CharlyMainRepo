using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sudoku.Pages
{
    public class indexModel : PageModel
    {
        public string Title { get; set; } = "Generador de Sudokus";
        public void OnGet()
        {
        }
        public void OnPost()
        {

        }
    }
}
