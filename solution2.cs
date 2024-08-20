﻿using System;
using Google.OrTools.LinearSolver;

namespace TDK1_remake
{
    public class SolarPanelPlacement2
    {
        public static void Solution()
        {
            // Adatok inicializálása
            int N = 5; // Sorok maximális száma
            int M = 5; // Oszlopok maximális száma
            double w = 1.0; // Panel szélessége
            double h = 1.0; // Panel magassága
            int P = 9; // Panelek maximális száma

            // Solver létrehozása
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
    }
}