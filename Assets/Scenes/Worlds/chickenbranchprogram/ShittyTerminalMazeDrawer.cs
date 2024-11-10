using System;
using System.Collections.Generic;

public class ShittyTerminalMazeDrawer {
    public static void Main(string[] args) {
        int row = 15;
        int col = 15;
        Random rng = new Random();
        Cell st = new Cell(1, 1);
        MazeGen mgen = new MazeGen();
        Maze aMAZEing = mgen.Gen(rng, row, col, st);
        aMAZEing.PrintVals();
        for (int i = 0; i < row; ++i) {
            PrintUpDown(aMAZEing, i, Direction.Up, col);
            PrintLeftRight(aMAZEing, i, col);
        }
        PrintUpDown(aMAZEing, row - 1, Direction.Down, col);
    }

    private static void PrintUpDown(Maze maze, int row_num, Direction dir, int max_col) {
        Cell cell = new Cell(row_num, 0);
        Console.Write("-");
        for (int i = 0; i < max_col; ++i) {
            cell.SetCol(i);
            if (maze.CellHasWall(cell, dir)) {
                Console.Write("----");
            }
            else {
                Console.Write("    ");
            }
        }
        Console.Write("\n");
    }
    private static void PrintLeftRight(Maze maze, int row_num, int max_col) {
        Cell cell = new Cell(row_num, 0);
        if (maze.CellHasWall(cell, Direction.Left)) {
            Console.Write("|");
        }
        else {
            Console.Write(" ");
        }
        for (int i = 1; i < max_col; ++i) {
            cell.SetCol(i);
            if (maze.CellHasWall(cell, Direction.Left)) {
                Console.Write("   |");
            }
            else {
                Console.Write("    ");
            }
        }
        if (maze.CellHasWall(cell, Direction.Right)) {
            Console.Write("   |");
        }
        else {
            Console.Write("    ");
        }
        Console.Write("\n");
    }
}