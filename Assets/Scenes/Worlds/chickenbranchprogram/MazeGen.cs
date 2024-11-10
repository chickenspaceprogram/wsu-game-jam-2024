using System;
using System.Collections.Generic;

public class MazeGen {
    Maze maze;
    Random rand;
    public Maze Gen(Random rng, int rows, int cols, Cell start) {
        rand = rng;
        maze = new Maze(rows, cols, start);
        Stack<Cell> stack = new Stack<Cell>();
        Cell current_cell;
        Cell next_cell;
        stack.Push(start);
        maze.Visit(start);

        while (stack.Count > 0) {
            current_cell = stack.Pop();
            if (HasUnvisitedNeighbors(current_cell)) {
                stack.Push(current_cell);
                Direction next_dir = PickUnvisitedNeighbor(current_cell);
                next_cell = GetNextCell(current_cell, next_dir);
                maze.Visit(next_cell);
                Console.Write("direction: " + next_dir.ToString("D") + "\n");
                Console.Write("next: row: " + next_cell.GetRow().ToString() + " col: " + next_cell.GetCol().ToString() + "\n");
                maze.BreakWall(current_cell, next_dir);
                stack.Push(next_cell);
            }
            Console.Write("current: row: " + current_cell.GetRow().ToString() + " col: " + current_cell.GetCol().ToString() + "\n\n");
        }
        return maze;
    }

    private bool HasUnvisitedNeighbors(Cell cell) {

        try {
            if (!maze.HasVisited(cell.GetRow() - 1, cell.GetCol())) {
                return true;
            }
        }
        catch (ArgumentOutOfRangeException) {}

        try {
            if (!maze.HasVisited(cell.GetRow() + 1, cell.GetCol())) {
                return true;
            }
        }
        catch (ArgumentOutOfRangeException) {}

        try {
            if (!maze.HasVisited(cell.GetRow(), cell.GetCol() - 1)) {
                return true;
            }
        }
        catch (ArgumentOutOfRangeException) {}

        try {
            if (!maze.HasVisited(cell.GetRow(), cell.GetCol() + 1)) {
                return true;
            }
        }
        catch (ArgumentOutOfRangeException) {}

        return false;
    }

    private Direction PickUnvisitedNeighbor(Cell cell) {
        if (!HasUnvisitedNeighbors(cell)) {
            throw new ArgumentException("Cell has no unvisited neighbors.");
        }
        Direction current_dir;
        Direction[] dirs = (Direction[]) Enum.GetValues(typeof(Direction));
        int row;
        int col;
        while (true) {
            current_dir = (Direction) dirs.GetValue(rand.Next(0, dirs.Length));
            switch (current_dir) {
                case Direction.Left:
                    row = cell.GetRow();
                    col = cell.GetCol() - 1;
                    break;
                case Direction.Right:
                    row = cell.GetRow();
                    col = cell.GetCol() + 1;
                    break;
                case Direction.Up:
                    row = cell.GetRow() - 1;
                    col = cell.GetCol();
                    break;
                case Direction.Down:
                    row = cell.GetRow() + 1;
                    col = cell.GetCol();
                    break;
                default:
                    throw new ArgumentException("direction is somehow invalid. ya dun fucked up. skill issue.");

            }
            try {
                if (!maze.HasVisited(row, col)) {
                    return current_dir;
                }
            }
            catch (ArgumentOutOfRangeException) {}
        }
    }

    private Cell GetNextCell(Cell current, Direction dir) {
        Cell next = new Cell(current.GetRow(), current.GetCol());
        switch (dir) {
            case Direction.Left:
                next.SetCol(current.GetCol() - 1);
                return next;
            case Direction.Right:
                next.SetCol(current.GetCol() + 1);
                return next;
            case Direction.Up:
                next.SetRow(current.GetRow() - 1);
                return next;
            case Direction.Down:
                next.SetRow(current.GetRow() + 1);
                return next;
        }
        throw new ArgumentException("direction is somehow invalid. ya dun fucked up. skill issue.");
    }
}