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
    int rows = 10;
    int cols = 10;
    int wall_len = 5;
    Cell start = new Cell(0, 0);
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
    }

    public void DrawWalls(Cell cell) {
        Vector3 cell_coords = GetCellCoords(cell);
        if (maze.CellHasWall(cell, Direction.Left)) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + left_offset;
            c_u_b_e.transform.localScale = vert_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "l";
        }
        if (maze.CellHasWall(cell, Direction.Right)) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + right_offset;
            c_u_b_e.transform.localScale = vert_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "r";
        }
        if (maze.CellHasWall(cell, Direction.Up)) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + up_offset;
            c_u_b_e.transform.localScale = horiz_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "u";
        }
        if (maze.CellHasWall(cell, Direction.Down)) {
            GameObject c_u_b_e = GetCube();
            c_u_b_e.transform.position = cell_coords + down_offset;
            c_u_b_e.transform.localScale = horiz_wall_size;
            c_u_b_e.name = "r" + cell.GetRow().ToString() + "c" + cell.GetCol().ToString() + "d";
        }
    }

    public Vector3 GetCellCoords(Cell cell) {
        Vector3 coords = new Vector3(cell.GetCol() * first_cube_size.x * (wall_len + 1) + first_cube_pos.x, first_cube_pos.y, cell.GetRow() * first_cube_size.z * (wall_len + 1) + first_cube_pos.z);
        coords += down_offset + right_offset;
        return coords;
    }

    public GameObject GetCube() {
        return Instantiate<GameObject>(GameObject.Find("c u b e"));
    }

    public void PrintCorners() {
        GameObject cube;
        Vector3 pos = new Vector3();
        pos.y = first_cube_pos.y;
        for (int i = 0; i < rows + 1; ++i) {
            for (int j = 0; j < rows + 1; ++j) {
                cube = GetCube();
                pos.x = j * first_cube_size.x * (wall_len + 1) + first_cube_pos.x;
                pos.z = i * first_cube_size.z * (wall_len + 1) + first_cube_pos.z;
                cube.transform.position = pos;
                cube.name = "corner-r" + i.ToString() + "c" + j.ToString();
            }
        }
    }
}
