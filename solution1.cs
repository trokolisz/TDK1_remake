using System;
using Google.OrTools.LinearSolver;

namespace TDK1_remake
{
    public class SolarPanelPlacement1
    {
        public static void Solution(int N, int M, double w, double h, int P)
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
                for (int j = 0; j < M; j++)
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
    }
}