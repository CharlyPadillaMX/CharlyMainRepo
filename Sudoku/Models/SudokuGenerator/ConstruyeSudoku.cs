using System.Text;

namespace Sudoku.Models.SudokuGenerator
{
    public class ConstruyeSudoku
    {
        public List<int[,]> _tablero = new List<int[,]>();

        private readonly ILogger<ConstruyeSudoku> _logger;

        public ConstruyeSudoku(ILogger<ConstruyeSudoku> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Metodo para limpiar todo el tablero del Sudoko anterior
        /// </summary>
        /// <param name="tablero">Lista del array tipo entero</param>
        private void LimpiaTablero(List<int[,]> tablero)
        {
            try
            {
                int[,] Celda;

                for (int i = 0; i < 9; i++)
                {
                    Celda = new int[3, 3];

                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                            Celda[j, k] = 0;
                    }

                    tablero.Add(Celda);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la inicializacion del constructor: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Funcion que valida que los numero de las celdas sean unicos
        /// </summary>
        /// <param name="numeroCelda">Numero para agregar</param>
        /// <param name="numeroAgregados">Numeros agregados previamente</param>
        /// <returns></returns>
        private bool ValidaNumeroCelda(int numeroCelda, string numeroAgregados)
        {
            bool valida = false;

            if (!string.IsNullOrEmpty(numeroAgregados))
            {
                var _secuencia = numeroAgregados.Split('|');
                if (_secuencia.Where(n => n == numeroCelda.ToString()).Count() <= 0)
                {
                    numeroAgregados = numeroCelda + "|";
                    valida = true;
                }
            }
            else
            {
                numeroAgregados = numeroCelda + "|";
                valida = true;
            }

            return valida;
        }
        /// <summary>
        /// Metodo que valida si el numero a agregar a la celda no coincide en la misma fila o columna de las otras celdas
        /// </summary>
        /// <param name="numeroCelda">Numero a agregar a la celda</param>
        /// <param name="_fila">Posicion de la fila</param>
        /// <param name="_columna">Posicion de la columna</param>
        /// <returns></returns>
        private bool ValidaFilasColumas(int numeroCelda, int _fila, int _columna)
        {
            bool valida = false;

            if (_tablero.Count == 0)
                return true;
            else
            {
                //Validar la fila de celdas del tablero, aqui no hace falta validar por columnas
                if (_tablero.Count <= 2)
                {
                    foreach (var celda in _tablero)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (celda[_fila, i] == numeroCelda)
                                return false;
                        }
                    }

                    valida = true;
                }
                else
                {
                    int celdaCount = 0;
                    //Validar fila
                    
                }
            }

            return valida;
        }

        private int[,] LlenaCelda(int[,] _celda)
        {
            try
            {
                Random _rn = new Random();
                string _numAdd = string.Empty;
                int _num = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        do
                        {
                            _num = _rn.Next(1, 9);
                        } while (ValidaNumeroCelda(_num, _numAdd));

                        _celda[i, j] = _num;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al generar la celda {_tablero.Count}: {ex.Message}");
                throw;
            }

            return _celda;
        }
        public string GeneraTablero()
        {
            StringBuilder _sb = new StringBuilder();

            LimpiaTablero(_tablero);

            int[,] Celda;

            for (int i = 0; i < 9; i++)
            {
                Celda = new int[3, 3];

            }

            return _sb.ToString();
        }
    }
}
