using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrawMaze : MonoBehaviour
{
    Vector3 first_cube_pos;
    Vector3 first_cube_size;
    Vector3 up_offset;
    Vector3 down_offset;
    Vector3 left_offset;
    Vector3 right_offset;
    Vector3 vert_wall_size;
    Vector3 horiz_wall_size;
    Maze maze;
    int rows = 21;
    int cols = 21;
    int wall_len = 5;
    Cell start = new Cell(6, 0);
    // Start is called before the first frame update
    void Start()
    {
        GameObject first_cube = GetCube();
        first_cube_pos = first_cube.transform.position;
        first_cube_size = first_cube.transform.localScale;
        System.Random rng = new System.Random();
        MazeGen mgen = new MazeGen();
        maze = mgen.Gen(rng, rows, cols, start);
        down_offset = new Vector3(0, 0, 0.5f * wall_len * first_cube_size.z + 0.5f * first_cube_size.z);
        up_offset = -1 * down_offset;
        right_offset = new Vector3(0.5f * wall_len * first_cube_size.x + 0.5f * first_cube_size.x, 0, 0);
        left_offset = -1 * right_offset;
        vert_wall_size = new Vector3(first_cube_size.x, first_cube_size.y, first_cube_size.z * wall_len);
        horiz_wall_size = new Vector3(first_cube_size.x * wall_len, first_cube_size.y, first_cube_size.z);
        Cell cell = new Cell(0, 0);
        for (int i = 0; i < rows; ++i) {
            cell.SetRow(i);
            for (int j = 0; j < cols / 2; ++j) {
                cell.SetCol(j * 2 + i % 2);
                DrawWalls(cell);
            }
            if (i % 2 == 0 && cols % 2 == 1) {
                cell.SetCol(cols - 1);
                DrawWalls(cell);
            }
        }
        PrintCorners();
        PrintOuterWalls();
        DispSouls(rng, 20);
    }

    private void DrawWalls(Cell cell) {
        Vector3 cell_coords = GetCellCoords(cell);
        if (maze.CellHasWall(cell, Direction.Left) && cell.GetCol() != 0) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + left_offset;
            c_u_b_e.transform.localScale = vert_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "l";
        }
        if (maze.CellHasWall(cell, Direction.Right) && cell.GetCol() != (cols - 1)) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + right_offset;
            c_u_b_e.transform.localScale = vert_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "r";
        }
        if (maze.CellHasWall(cell, Direction.Up) && cell.GetRow() != 0) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + up_offset;
            c_u_b_e.transform.localScale = horiz_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "u";
        }
        if (maze.CellHasWall(cell, Direction.Down) && cell.GetRow() != rows - 1) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + down_offset;
            c_u_b_e.transform.localScale = horiz_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "d";
        }
    }

    private Vector3 GetCellCoords(Cell cell) {
        Vector3 coords = new Vector3(cell.GetCol() * first_cube_size.x * (wall_len + 1) + first_cube_pos.x, first_cube_pos.y, cell.GetRow() * first_cube_size.z * (wall_len + 1) + first_cube_pos.z);
        coords += down_offset + right_offset;
        return coords;
    }

    private GameObject GetCube() {
        return Instantiate<GameObject>(GameObject.Find("c u b e"));
    }

    private void PrintCorners() {
        GameObject cube;
        Vector3 pos = new Vector3();
        pos.y = first_cube_pos.y;
        for (int i = 1; i < rows; ++i) {
            for (int j = 1; j < rows; ++j) {
                cube = GetCube();
                pos.x = j * first_cube_size.x * (wall_len + 1) + first_cube_pos.x;
                pos.z = i * first_cube_size.z * (wall_len + 1) + first_cube_pos.z;
                cube.transform.position = pos;
                cube.name = "corner-r" + i.ToString() + "c" + j.ToString();
            }
        }
    }

    private void PrintOuterWalls() {
        GameObject up_wall = GetCube();
        GameObject down_wall = GetCube();
        GameObject left_wall_1 = GetCube();
        GameObject left_wall_2 = GetCube();
        GameObject right_wall = GetCube();

        float horiz_len = cols * wall_len * first_cube_size.z + (cols - 1) * first_cube_size.z;
        float vert_len = rows * wall_len * first_cube_size.x + (rows - 1) * first_cube_size.x;
        float lwall_len = (vert_len - wall_len * first_cube_size.z) / 2;

        Vector3 vert_size = new Vector3(first_cube_size.x, first_cube_size.y, horiz_len);
        Vector3 horiz_size = new Vector3(vert_len, first_cube_size.y, first_cube_size.z);
        Vector3 lwall_size = new Vector3(first_cube_size.x, first_cube_size.y, lwall_len);

        up_wall.transform.localScale = horiz_size;
        down_wall.transform.localScale = horiz_size;
        left_wall_1.transform.localScale = lwall_size;
        left_wall_2.transform.localScale = lwall_size;
        right_wall.transform.localScale = vert_size;

        Vector3 up_pos = new Vector3(first_cube_pos.x + (horiz_len + first_cube_size.x) / 2, first_cube_pos.y, first_cube_pos.z);
        Vector3 down_pos = new Vector3(first_cube_pos.x + (horiz_len + first_cube_size.x) / 2, first_cube_pos.y, first_cube_pos.z + vert_len + first_cube_size.z);
        Vector3 left1_pos = new Vector3(first_cube_pos.x, first_cube_pos.y, first_cube_pos.z + vert_len + first_cube_size.z - (0.5f * first_cube_size.z + lwall_len / 2));
        Vector3 left2_pos = new Vector3(first_cube_pos.x, first_cube_pos.y, first_cube_pos.z + 0.5f * first_cube_size.z + lwall_len / 2);
        Vector3 right_pos = new Vector3(first_cube_pos.x + horiz_len + first_cube_size.x, first_cube_pos.y, first_cube_pos.z + (vert_len + first_cube_size.z) / 2);

        up_wall.transform.position = up_pos;
        down_wall.transform.position = down_pos;
        left_wall_1.transform.position = left1_pos;
        left_wall_2.transform.position = left2_pos;
        right_wall.transform.position = right_pos;

        up_wall.name = "up-wall";
        down_wall.name = "down-wall";
        left_wall_1.name = "left-wall-1";
        left_wall_2.name = "left-wall-2";
        right_wall.name = "right-wall";

        GameObject up_corner = GetCube();
        GameObject up_right_corner = GetCube();
        GameObject right_corner = GetCube();

        Vector3 uc_pos = new Vector3(first_cube_pos.x, first_cube_pos.y, first_cube_pos.z + vert_len + first_cube_size.z);
        Vector3 urc_pos = new Vector3(first_cube_pos.x + horiz_len + first_cube_size.x, first_cube_pos.y, first_cube_pos.z + vert_len + first_cube_size.z);
        Vector3 rc_pos = new Vector3(first_cube_pos.x + horiz_len + first_cube_size.x, first_cube_pos.y, first_cube_pos.z);

        up_corner.transform.position = uc_pos;
        up_right_corner.transform.position = urc_pos;
        right_corner.transform.position = rc_pos;
    }

    public List<Vector3> GetRandomPosList(System.Random rng, int num_positions) {
        List<Vector3> list = new List<Vector3>();
        bool[,] objs_placed = new bool[rows, cols];
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {
                objs_placed[i, j] = false;
            }
        }
        int num_placed = 0;
        int rw = 0;
        int cl = 0;
        while (num_placed < num_positions) {
            rw = rng.Next(rows);
            cl = rng.Next(cols);
            if (!objs_placed[rw, cl]) {
                Cell cell = new Cell(rw, cl);
                list.Add(GetCellCoords(cell));
                objs_placed[rw, cl] = true;
                ++num_placed;
            }
        }
        return list;
    }

    private void DispSouls(System.Random rng, int num_souls) {
        List<Vector3> souls_list = GetRandomPosList(rng, num_souls);
        List<Vector3>.Enumerator lenum = souls_list.GetEnumerator();
        for (int i = 0; i < num_souls - 1; ++i) {
            lenum.MoveNext();
            PrintSoul(lenum.Current);
        }
        lenum.MoveNext();
        PrintTelep(lenum.Current);

    }

    private void PrintSoul(Vector3 coords) {
        GameObject sp = Instantiate<GameObject>(GameObject.Find("Soul"));
        coords.y += 1;
        sp.gameObject.transform.position = coords;
    }

    private void PrintTelep(Vector3 coords)
    {
        GameObject sp = Instantiate<GameObject>(GameObject.Find("Teleporter"));
        coords.y += 1;
        sp.transform.position = coords;
    }
}
