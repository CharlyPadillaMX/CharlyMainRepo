using Sudoku.Models.SudokuGenerator;

namespace Sudoku.Models
{
    public class SudokuRepository : ISudokuRepository
    {
        private readonly ILogger<SudokuRepository> _logger;
        private readonly ConstruyeSudoku _construyeSudoku;

        public SudokuRepository(ILogger<SudokuRepository> logger, ConstruyeSudoku construyeSudoku)
        {
            _logger = logger;
            _construyeSudoku = construyeSudoku;
        }

        public Task<string> GeneraSudokuAsync()
        {
            try
            {
                return Task.FromResult(_construyeSudoku.GeneraTablero());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Generar el tablero de Sudoko: {ex.Message}");
                throw;
            }
        }
    }
}
