﻿using System;

class MatrixSearch
{
    public static int CountSubmatrixOccurrences(int[,] A, int[,] B)
    {
        int rowCountA = A.GetLength(0);
        int colCountA = A.GetLength(1);
        int rowCountB = B.GetLength(0);
        int colCountB = B.GetLength(1);

        if (rowCountB > rowCountA || colCountB > colCountA)
        {
            throw new ArgumentException("Submatrix B must have smaller or equal dimensions than matrix A.");
        }

        int occurrences = 0;
        int chaveMatriz = 0;
        Dictionary<int, List<int[,]>> listM = new Dictionary<int, List<int[,]>>();

        for (int i = 0; i <= rowCountA - rowCountB; i++)
        {
            for (int j = 0; j <= colCountA - colCountB; j++)
            {
                for (int x = 0; x < rowCountB; x++)
                {
                    for (int y = 0; y < colCountB; y++)
                    {
                        int ce = A[i + x, j + y];

                        if (ce == B[x, y])
                        {
                            if (listM.Count() > 0)
                            {
                                if (VerificarNumeroNaMatriz(ce, listM))
                                {
                                    //tem que instanciar novo C, pois achou mais um candidato de submatriz
                                    int[,] C = new int[rowCountB, colCountB];
                                    C[x, y] = ce;
                                    List<int[,]> lista = new List<int[,]>
                                    {
                                        C
                                    };
                                    listM.Add(chaveMatriz, lista);
                                    chaveMatriz++;
                                }
                                //adiciona numero ce em todos os C no dicionario
                                InsereNumeroMatriz(ref listM, ce, x, y);

                            }
                            else
                            {
                                int[,] C = new int[rowCountB, colCountB];
                                C[x, y] = ce;
                                List<int[,]> lista = new List<int[,]>
                                {
                                    C
                                };
                                listM.Add(chaveMatriz, lista);
                                chaveMatriz++;
                            }
                            
                        }
                    }
                }
            }
        }
        VerificarMatrizPreenchida(ref occurrences, listM);
        return occurrences;
    }

    static void VerificarMatrizPreenchida(ref int occurrences, Dictionary<int, List<int[,]>> listM)
    {
        bool lvalid = true;
        foreach (var matrizes in listM.Values)
        {
            foreach (var matriz in matrizes)
            {
                // Obtém o número de linhas e colunas da matriz
                int linhas = matriz.GetLength(0);
                int colunas = matriz.GetLength(1);
                lvalid = true;
                // Percorre todos os elementos da matriz
                for (int i = 0; i < linhas; i++)
                {
                    for (int j = 0; j < colunas; j++)
                    {
                        // Se encontrar um elemento com valor 0, retorna false
                        if (matriz[i, j] == 0)
                        {
                            lvalid = false;

                        }
                    }
                }

                if (lvalid)
                {
                    occurrences++;
                }
            }
        }
    }

    static bool VerificarNumeroNaMatriz(int numeroProcurado, Dictionary<int, List<int[,]>> listM)
    {
        bool lValid = false;
        foreach (var matrizes in listM.Values)
        {
            foreach (var matriz in matrizes)
            {
                // Obtém o número de linhas e colunas da matriz
                int linhas = matriz.GetLength(0);
                int colunas = matriz.GetLength(1);

                // Percorre todos os elementos da matriz
                for (int i = 0; i < linhas; i++)
                {
                    for (int j = 0; j < colunas; j++)
                    {
                        // Se encontrar o número na matriz, retorna verdadeiro
                        if (matriz[i, j] == numeroProcurado)
                        {
                            lValid = true;
                            break;
                        }
                    }

                    if (lValid)
                    {
                        break;
                    }
                }

                if (lValid)
                {
                    break;
                }
            }

            if (lValid)
            {
                break;
            }
        }
        // Se não encontrou o número na matriz, retorna falso
        return lValid;
    }

    static void InsereNumeroMatriz(ref Dictionary<int, List<int[,]>> listM, int num, int a, int b)
    {
        foreach (var item in listM)
        {
            foreach (var it in item.Value)
            {
                if (it[a, b] == 0)
                {
                    it[a, b] = num;
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        int[,] A = {
            {6, 2, 3, 7},
            {5, 6, 7, 8},
            {9, 8, 7, 6},
            {8, 4, 3, 7}
        };

        int[,] B = {
            {6, 7},
            {8, 7}
        };

        int occurrences = MatrixSearch.CountSubmatrixOccurrences(A, B);
        Console.WriteLine("Number of occurrences of submatrix B in matrix A: " + occurrences);
    }
}