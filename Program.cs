﻿using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    partial class MainProgram
    {
        private static void Main(string[] args)
        {
            int num = 10;
            int ChangeRows = 1, ChangeCols = 1, Changable = 10;

            Matrix Res;
            Matrix A = null;
            Matrix B = null;
        Enter:
            try
            {
                Console.WriteLine("ввод Матрица А: ");
                A = new Matrix(EnterMatrix());
                Console.WriteLine("Ввод Матрица B: ");
                B = new Matrix(EnterMatrix());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Матрица не существует: {ex.Message}");
                Environment.Exit(0);
            }
            catch
            {
                Console.WriteLine("Недопустимое значение элемента");
                goto Enter;
            }

            Console.WriteLine("Матрица А: ");
            Console.Write(A);
            Console.WriteLine();
            Console.WriteLine("Матрица В: ");
            Console.Write(B);
            Console.WriteLine();

            Console.WriteLine("Замена элемента:");
            try
            {
                Console.Write(A.Change(ChangeRows, ChangeCols, Changable));
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Такого элемента не существует: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Данный формат не inetger");
            }
            Console.WriteLine();
            Console.WriteLine("Вычитание:");
            try
            {
                Res = (A - B);
                Console.Write(Res);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Вычитание недопустимо: {ex.Message}");
            }
            Console.WriteLine();
            Console.WriteLine("Сложение:");
            try
            {
                Res = (A + B);
                Console.Write(Res);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Сложение недопустимо: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("Умножение на скаляр:");
            Console.Write(A.NUM(num));
            Console.WriteLine();

            Console.WriteLine("Умножение матриц:");
            try
            {
                Console.Write(Matrix.Multiply(A, B));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Умножение недопустимо: {ex.Message}");
            }

            try
            {
                Console.WriteLine("Детерминант матрицы А равен {0}\nДетерминант матрицы B равен {1}", A.Determinant(), B.Determinant());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Невозможно посчитать определитель: {ex.Message}");
            }
            try
            {
                if (A.Comparison(B))
                    Console.WriteLine("Матрицы А и В равны\n");
                else
                    Console.WriteLine("Матрицы А и В не равны\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}