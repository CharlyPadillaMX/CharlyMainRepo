using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sudoku.Models;

namespace Sudoku.Pages
{
    public class indexModel : PageModel
    {
        private readonly ISudokuRepository _repository;

        public indexModel(ISudokuRepository repository)
        {
            _repository = repository;
        }

        public string Title { get; set; } = "Generador de Sudokus";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            _repository.GeneraSudoku();
        }
    }
}
