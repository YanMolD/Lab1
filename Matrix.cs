using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    partial class MainProgram
    {
        static public int[,] EnterMatrix()

        {
            private int[,] mass;

        private int rows, cols;
        Console.WriteLine("Введите количество строк матрицы\n");
            rows = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество столбцов матрицы\n");
            cols = Convert.ToInt32(Console.ReadLine());
            if (rows <= 0 || cols <= 0)

                throw private new ArgumentException();

        mass = new int[rows, cols];
            for (int i = 0; i<rows; i++)
            {
                for (int j = 0; j<cols; j++)
                {
                    Console.WriteLine("Введите элемент матрицы {0}:{1}", i + 1, j + 1);
                    mass[i, j] = Convert.ToInt32(Console.ReadLine());
                }
}

return mass;
        }

        internal class Matrix
{
    public readonly int rows, cols;
    public readonly int[,] mass;

    public Matrix(int[,] mass)
    {
        this.rows = mass.GetLength(0);
        this.cols = mass.GetLength(1);
        this.mass = mass;
    }

    public Matrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        mass = new int[rows, cols];
    }

    /*ЗАМЕНА ЭЛЕМЕНТА В МАТРИЦЕ*/

    public Matrix Change(int ChangeRows, int ChangeCols, int Changable)
    {
        Matrix matrix = this;
        if ((ChangeRows > rows) || (ChangeCols > cols))
            throw new IndexOutOfRangeException();
        else
        {
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    if (ChangeRows == i && ChangeCols == j)
                    {
                        matrix.mass[i - 1, j - 1] = Changable;
                        break;
                    }
                }
            }
        }
        return matrix;
    }

    /*УМНОЖЕНИЕ МАТРИЦЫ НА СКАЛЯР*/

    public Matrix NUM(int num)
    {
        Matrix array = new Matrix(rows, cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array.mass[i, j] = mass[i, j] * num;
            }
        }
    }

    /*ВЫЧИТАНИЕ МАТРИЦ*/

    public static Matrix operator -(Matrix a, Matrix b)
    {
        Matrix array = new Matrix(a.rows, a.cols);
        if ((a.rows != b.rows) || (a.cols != b.cols))
            throw new InvalidOperationException();
        else
        {
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < b.cols; j++)
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
        Matrix array = new Matrix(a.rows, a.cols);

        if ((a.rows != b.rows) || (a.cols != b.cols))
            throw new InvalidOperationException();
        else
        {
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < b.cols; j++)
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
        Matrix MatrixC = new Matrix(MatrixA.rows, MatrixB.cols);

        if (MatrixA.cols != MatrixB.rows)
            throw new InvalidOperationException();
        else
        {
            for (int i = 0; i < MatrixA.rows; i++)
                for (int j = 0; j < MatrixB.cols; j++)
                {
                    for (int r = 0; r < MatrixA.cols; r++)
                        MatrixC.mass[i, j] += MatrixA.mass[i, r] * MatrixB.mass[r, j];
                }
        }
        return MatrixC;
    }

    public int Determinant()
    {
        int rows = cols, determ = 0, minor_determ, parity;

        if (rows != cols)
            throw new InvalidOperationException();
        else
        {
            if (rows == 2)
                return mass[0, 0] * mass[1, 1] - mass[0, 1] * mass[1, 0];
            for (int i = 0; i < rows; i++)
            {
                minor_determ = Determinant(Create_minor(this, i));
                parity = ((i & 1) == 0 ? 1 : -1);
                determ += parity * mass[0, i] * minor_determ;
            }
        }
        return determ;
    }

    public int Determinant(Matrix a)
    {
        int rows = a.cols, determ = 0, minor_determ, parity;

        if (a.rows != a.cols)
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

    private Matrix Create_minor(Matrix a, int coloumn)
    {
        int rows = a.cols - 1;
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

    public bool Comparison(Matrix a)
    {
        if ((a == null) || ((a.rows == 0) || (a.cols == 0)))
            throw new ArgumentException("Передана пустая матрица");
        if ((a.cols != cols) || (a.rows != rows))
            return false;
        for (int i = 0; i < cols; i++)
            for (int j = 0; j < cols; j++)
                if (a.mass[i, j] == mass[i, j])
                    return false;
        return true;
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
}
