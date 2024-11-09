using System;

public class Maze {

    private int rows;
    private int cols;
    private bool[,] visited_cells;
    private bool[,] left_wall;
    private bool[,] right_wall;
    private bool[,] up_wall;
    private bool[,] down_wall;
    private Cell start;

    public Maze(int num_rows, int num_cols, Cell maze_start)  {
        rows = num_rows;
        cols = num_cols;

        visited_cells = new bool[rows, cols];
        left_wall = new bool[rows, cols];
        right_wall = new bool[rows, cols];
        up_wall = new bool[rows, cols];
        down_wall = new bool[rows, cols];
        start = maze_start;

        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < rows; ++j) {
                visited_cells[i, j] = false;
                left_wall[i, j] = false;
                right_wall[i, j] = false;
                up_wall[i, j] = false;
                down_wall[i, j] = false;

            }
        }
    }

    public int GetRowSize() {
        return rows;
    }

    public int GetColSize() {
        return cols;
    }

    public Cell GetStart() {
        return start;
    }

    public void Visit(Cell cell) {
        visited_cells[cell.GetRow(), cell.GetCol()] = true;
    }

    public bool HasVisited(Cell cell) {
        return visited_cells[cell.GetRow(), cell.GetCol()];
    }

    public void BreakWall(Cell cell, Direction direction) {

    }


    private void CheckBounds(Cell cell) {
        if (cell.GetRow() >= rows || cell.GetRow() < 0 || cell.GetCol() >= cols || cell.GetCol() < 0) {
            throw new ArgumentException();
        }
    }
}