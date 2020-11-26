using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Matrix
    {
        private int rows, cols;
        private int[,] mass;

        public int ROWS
        {
            get { return rows; }
            set { if (value > 0) rows = value; }
        }

        public int COLS
        {
            get { return cols; }
            set { if (value > 0) cols = value; }
        }

        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            mass = new int[rows, cols];
        }

        public void EnterMatrix()
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException();
            //Random rn = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.WriteLine("Введите элемент матрицы {0}:{1}", i + 1, j + 1);
                    mass[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
        }

        /*ЗАМЕНА ЭЛЕМЕНТА В МАТРИЦЕ*/

        public static Matrix Change(Matrix a, int ChangeRows, int ChangeCols, int Changable)
        {
            if ((ChangeRows > a.ROWS) || (ChangeCols > a.COLS))
                throw new IndexOutOfRangeException();
            else
            {
                for (int i = 1; i <= a.ROWS; i++)
                {
                    for (int j = 1; j <= a.COLS; j++)
                    {
                        if (ChangeRows == i && ChangeCols == j)
                        {
                            a.mass[i - 1, j - 1] = Changable;
                            break;
                        }
                    }
                }
            }
            return a;
        }

        /*УМНОЖЕНИЕ МАТРИЦЫ НА СКАЛЯР*/

        public static Matrix NUM(Matrix a, int num)
        {
            Matrix array = new Matrix(a.ROWS, a.COLS);
            for (int i = 0; i < a.ROWS; i++)
            {
                for (int j = 0; j < a.COLS; j++)
                {
                    array.mass[i, j] = a.mass[i, j] * num;
                }
            }
            return array;
        }

        /*ВЫЧИТАНИЕ МАТРИЦ*/

        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix array = new Matrix(a.ROWS, a.COLS);
            if ((a.ROWS != b.ROWS) || (a.COLS != b.COLS))
                throw new InvalidOperationException();
            else
            {
                for (int i = 0; i < a.ROWS; i++)
                {
                    for (int j = 0; j < b.COLS; j++)
                    {
                        array.mass[i, j] = a.mass[i, j] - b.mass[i, j];
                    }
                }
            }
            return array;
        }

        /*СЛОЖЕНИЕ МАТРИЦ*/

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix array = new Matrix(a.ROWS, a.COLS);

            if ((a.ROWS != b.ROWS) || (a.COLS != b.COLS))
                throw new InvalidOperationException();
            else
            {
                for (int i = 0; i < a.ROWS; i++)
                {
                    for (int j = 0; j < b.COLS; j++)
                    {
                        array.mass[i, j] = a.mass[i, j] + b.mass[i, j];
                    }
                }
            }
            return array;
        }

        /*УМНОЖЕНИЕ МАТРИЦ*/

        public static Matrix Multiply(Matrix MatrixA, Matrix MatrixB)
        {
            Matrix MatrixC = new Matrix(MatrixA.ROWS, MatrixB.COLS);

            if (MatrixA.COLS != MatrixB.ROWS)
                throw new InvalidOperationException();
            else
            {
                for (int i = 0; i < MatrixA.ROWS; i++)
                    for (int j = 0; j < MatrixB.COLS; j++)
                    {
                        for (int r = 0; r < MatrixA.COLS; r++)
                            MatrixC.mass[i, j] += MatrixA.mass[i, r] * MatrixB.mass[r, j];
                    }
            }
            return MatrixC;
        }

        public static int Determinant(Matrix a)
        {
            int rows = a.COLS, determ = 0, minor_determ, parity;

            if (a.ROWS != a.COLS)
                throw new InvalidOperationException();
            else
            {
                if (rows == 2)
                    return a.mass[0, 0] * a.mass[1, 1] - a.mass[0, 1] * a.mass[1, 0];
                for (int i = 0; i < rows; i++)
                {
                    minor_determ = Determinant(Create_minor(a, i));
                    parity = ((i & 1) == 0 ? 1 : -1);
                    determ += parity * a.mass[0, i] * minor_determ;
                }
            }
            return determ;
        }

        public static Matrix Create_minor(Matrix a, int coloumn)
        {
            int rows = a.COLS - 1;
            int count_coloumn = 0;
            Matrix Minor = new Matrix(rows, rows);
            for (int i = 1; i < rows + 1; i++)
            {
                for (int j = 0; j < rows + 1; j++)
                    if (j != coloumn)
                        Minor.mass[i - 1, count_coloumn++] = a.mass[i, j];
                count_coloumn = 0;
            }
            return Minor;
        }

        ~Matrix()
        {
            Console.WriteLine("Очистка");
        }

        public override string ToString()
        {
            string buf = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    buf += (mass[i, j] + "\t");
                }
                buf += "\n";
            }
            return buf;
        }
    }
}