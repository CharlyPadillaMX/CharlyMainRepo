using Sudoku.Models.SudokuGenerator;

namespace Sudoku.Models
{
    public interface ISudokuRepository
    {
        Task<string> GeneraSudoku();
    }
}
