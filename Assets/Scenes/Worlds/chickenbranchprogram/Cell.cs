public class Cell {
    private int row;
    private int col;

    public Cell(int cell_row, int cell_col) {
        row = cell_row;
        col = cell_col;
    }

    public int GetRow() {
        return row;
    }

    public int GetCol() {
        return col;
    }

    public void SetRow(int cell_row) {
        row = cell_row;
    }

    public void SetCol(int cell_col) {
        col = cell_col;
    }
}