
using System;
using Google.OrTools.LinearSolver;


namespace TDK1_remake
{
    public class Models
    {
        public static void Solution1(int N, int M, double w, double h, int P)
        {
            // Solver létrehozása
            Solver solver = Solver.CreateSolver("GLOP");
            if (solver is null)
            {
                Console.WriteLine("Nem sikerült létrehozni a solver-t.");
                return;
            }
            // Döntési változók létrehozása
            // x * y méretü mátrix
            Variable[,] x = new Variable[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    x[i, j] = solver.MakeIntVar(0, 1, $"x_{i}_{j}");
                }
            }

            //Korlátok hozzáadása
            Constraint panelConstraint = solver.MakeConstraint(P, P, "panelConstraint");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    panelConstraint.SetCoefficient(x[i, j], 1);
                }
            }


            //Célfüggvény beállítása
            Objective objective = solver.Objective();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    objective.SetCoefficient(x[i, j], w * j + h * i);
                }
            }
            objective.SetMinimization();

            //Megoldás kiszámítása
            solver.Solve();

            //Eredmények kiírása mátrix formában
            Console.WriteLine("Megoldás mátrix formában:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write(x[i, j].SolutionValue() + " ");
                }
                Console.WriteLine();
            }

        }


        public static void Solution2(int N, int M, double w, double h, int P)
        {
            Solver solver = Solver.CreateSolver("SCIP");
            if (solver is null)
            {
                Console.WriteLine("Nem sikerült létrehozni a solver-t.");
                return;
            }

            // Döntési változók létrehozása
            Variable[,] x = new Variable[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    x[i, j] = solver.MakeIntVar(0, 1, $"x_{i}_{j}");
                }
            }

            // Korlátok hozzáadása
            for (int i = 0; i < N; i++)
            {
                for (int j = 1; j < M; j++)
                {
                    solver.Add(x[i, j] - x[i, j - 1] <= 0);
                }
            }

            for (int j = 0; j < M; j++)
            {
                for (int i = 1; i < N; i++)
                {
                    solver.Add(x[i, j] - x[i - 1, j] <= 0);
                }
            }

            Constraint panelConstraint = solver.MakeConstraint(P, P, "panelConstraint");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    panelConstraint.SetCoefficient(x[i, j], 1);
                }
            }

            // Célfüggvény beállítása
            Objective objective = solver.Objective();
            for (int i = 0; i < N; i++)
            {
                objective.SetCoefficient(x[i, 0], w);
            }
            for (int j = 0; j < M; j++)
            {
                objective.SetCoefficient(x[0, j], h);
            }
            objective.SetMinimization();

            // Megoldás kiszámítása
            solver.Solve();

            // Eredmények kiírása mátrix formában
            Console.WriteLine("Megoldás mátrix formában:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write(x[i, j].SolutionValue() + " ");
                }
                Console.WriteLine();
            }
        }


        public static void Solution1_with_symmetry(int N, int M, double w, double h, int P)
        {
            // Solver létrehozása
            Solver solver = Solver.CreateSolver("GLOP");
            if (solver is null)
            {
                Console.WriteLine("Nem sikerült létrehozni a solver-t.");
                return;
            }

            // Döntési változók létrehozása
            Variable[,] x = new Variable[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    x[i, j] = solver.MakeIntVar(0, 1, $"x_{i}_{j}");
                }
            }

            // Korlátok hozzáadása
            // Korlát a panelek számához
            Constraint panelConstraint = solver.MakeConstraint(P, P, "panelConstraint");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    panelConstraint.SetCoefficient(x[i, j], 1);
                }
            }

            // Szimmetria korlátok (függőleges tengelyes szimmetria)
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M / 2; j++)
                {
                    solver.Add(x[i, j] == x[i, M - j - 1]);
                }
            }

            // Sarokba szorítási korlát (két szemközti sarok)
            for (int i = 0; i < N / 2; i++)
            {
                for (int j = 0; j < M / 2; j++)
                {
                    // Bal felső sarok és jobb alsó sarok közötti kapcsolat
                    solver.Add(x[i, j] == x[N - i - 1, M - j - 1]);
                }
            }

            // Célfüggvény beállítása
            Objective objective = solver.Objective();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M / 2; j++)
                {
                    objective.SetCoefficient(x[i, j], w * j + h * i);
                }
            }
            objective.SetMinimization();

            // Megoldás kiszámítása
            solver.Solve();

            // Eredmények kiírása mátrix formában
            Console.WriteLine("Megoldás mátrix formában:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write(x[i, j].SolutionValue() + " ");
                }
                Console.WriteLine();
            }
        }



        public static void Model_with_rectangular_symmetry(int N, int M, double w, double h, int P)
        {
            // Solver létrehozása
            Solver solver = Solver.CreateSolver("GLOP");
            if (solver is null)
            {
                Console.WriteLine("Nem sikerült létrehozni a solver-t.");
                return;
            }

            // Döntési változók létrehozása
            Variable[,] x = new Variable[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    x[i, j] = solver.MakeIntVar(0, 1, $"x_{i}_{j}");
                }
            }

            // Korlátok hozzáadása
            // Szimmetria korlát (függőleges tengelyes szimmetria)
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M / 2; j++)
                {
                    solver.Add(x[i, j] == x[i, M - j - 1]);
                }
            }

            // Korlátok a téglalapokba szorításhoz
            for (int i = 0; i < N; i++)
            {
                for (int j = 1; j < M / 2; j++)
                {
                    // x_(i,j) - x_(i,j-1) <= 0
                    solver.Add(x[i, j] - x[i, j - 1] <= 0);
                }
            }

            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    // x_(i,j) - x_(i-1,j) <= 0
                    solver.Add(x[i, j] - x[i - 1, j] <= 0);
                }
            }

            // Korlát a maximális P számú panelhez
            Constraint panelConstraint = solver.MakeConstraint(0, P, "panelConstraint");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    panelConstraint.SetCoefficient(x[i, j], 1);
                }
            }

            // Célfüggvény beállítása (csak az egyik oldalt kell figyelni)
            Objective objective = solver.Objective();

            // Első oszlop súlyozott összege
            for (int i = 0; i < N; i++)
            {
                objective.SetCoefficient(x[i, 0], i * h);
            }

            // Első sor súlyozott összege
            for (int j = 0; j < M / 2; j++)
            {
                objective.SetCoefficient(x[0, j], j * w);
            }

            objective.SetMinimization();

            // Megoldás kiszámítása
            solver.Solve();

            // Eredmények kiírása mátrix formában
            Console.WriteLine("Megoldás mátrix formában:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write(x[i, j].SolutionValue() + " ");
                }
                Console.WriteLine();
            }
        }

    }







}