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
                left_wall[i, j] = true;
                right_wall[i, j] = true;
                up_wall[i, j] = true;
                down_wall[i, j] = true;

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
        CheckBounds(cell);
        visited_cells[cell.GetRow(), cell.GetCol()] = true;
    }

    public bool HasVisited(int row, int col) {
        Cell cell = new Cell(row, col);
        CheckBounds(cell);
        return visited_cells[row, col];
    }

    public void BreakWall(Cell cell, Direction direction) {
        CheckBounds(cell);
        switch (direction) {
            case Direction.Left:
                if (cell.GetCol() == 0) {
                    throw new ArgumentOutOfRangeException("Invalid cell and direction, wall cannot be broken.");
                }

                if (!left_wall[cell.GetRow(), cell.GetCol()]) {
                    throw new InvalidOperationException("Wall has already been broken.");
                }

                left_wall[cell.GetRow(), cell.GetCol()] = false;
                right_wall[cell.GetRow(), cell.GetCol() - 1] = false;
                break;
            case Direction.Right:
                if (cell.GetCol() == cols - 1) {
                    throw new ArgumentOutOfRangeException("Invalid cell and direction, wall cannot be broken.");
                }

                if (!right_wall[cell.GetRow(), cell.GetCol()]) {
                    throw new InvalidOperationException("Wall has already been broken.");
                }

                left_wall[cell.GetRow(), cell.GetCol() + 1] = false;
                right_wall[cell.GetRow(), cell.GetCol()] = false;
                break;
            case Direction.Up:
                if (cell.GetRow() == 0) {
                    throw new ArgumentOutOfRangeException("Invalid cell and direction, wall cannot be broken.");
                }

                if (!up_wall[cell.GetRow(), cell.GetCol()]) {
                    throw new InvalidOperationException("Wall has already been broken.");
                }

                up_wall[cell.GetRow(), cell.GetCol()] = false;
                down_wall[cell.GetRow() - 1, cell.GetCol()] = false;
                break;
            case Direction.Down:
                if (cell.GetRow() == rows - 1) {
                    throw new ArgumentOutOfRangeException("Invalid cell and direction, wall cannot be broken.");
                }

                if (!down_wall[cell.GetRow(), cell.GetCol()]) {
                    throw new InvalidOperationException("Wall has already been broken.");
                }

                up_wall[cell.GetRow() + 1, cell.GetCol()] = false;
                down_wall[cell.GetRow(), cell.GetCol()] = false;
                break;
        }
    }

    public bool CellHasWall(Cell cell, Direction direction) {
        switch (direction) {
            case Direction.Left:
                return left_wall[cell.GetRow(), cell.GetCol()];
            case Direction.Right:
                return right_wall[cell.GetRow(), cell.GetCol()];
            case Direction.Up:
                return up_wall[cell.GetRow(), cell.GetCol()];
            case Direction.Down:
                return down_wall[cell.GetRow(), cell.GetCol()];
        }

        throw new ArgumentException("direction is invalid. ya dun fucked up. skill issue.");
    }


    public void CheckBounds(Cell cell) {
        if (cell.GetRow() >= rows || cell.GetRow() < 0 || cell.GetCol() >= cols || cell.GetCol() < 0) {
            throw new ArgumentOutOfRangeException("Cell is out of bounds.");
        }
    }
}