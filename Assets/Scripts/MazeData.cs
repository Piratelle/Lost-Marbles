using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/**
 * A class handling the size and shape of an individual maze design.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class MazeData
{
    private static readonly int MAZE_SCALE = 2; // # of unity squares that make up 1 side of 1 maze square
    private static readonly float BASE_HEIGHT = 0.05f;
    private static readonly float WALL_THICKNESS = 0.05f;
    private static readonly float WALL_HEIGHT = 1f;

    // Maze Dimensions
    // best if these are odd (central (0,0) tile), but not required
    // also best if these * MAZE_SCALE produce even number, otherwise positional calculations may be wonky
    private int mazeWidth;
    private int mazeDepth;

    // Maze Positions
    private Vector2 spawnPosition;
    private Vector2 goalPosition;

    // Maze Edges
    float left;
    float right;
    float back;
    float front;

    public readonly List<Wall> walls = new List<Wall>();

    /**
     * Struct to contain Wall data (endpoints).
     */
    public struct Wall
    {
        public Vector2 botLeft;
        public Vector2 topRight;

        /**
         * Constructor which takes in two sets of maze coordinates
         * 
         * @param x1    The X coordinate for the bottom/left-most endpoint of this Wall.
         * @param z1    The Z coordinate for the bottom/left-most endpoint of this Wall.
         * @param x2    The X coordinate for the top/right-most endpoint of this Wall.
         * @param z2    The Z coordinate for the top/right-most endpoint of this Wall.
         */
        public Wall(float x1, float z1, float x2, float z2)
        {
            botLeft = new Vector2(x1, z1);
            topRight = new Vector2(x2, z2);
        }
    }

    /**
     * Constructor for maze, accepting all flexible values.
     * 
     * @param mazeWidth     The total # of maze squares in the X dimension.
     * @param mazeDepth     The total # of maze squares in the Z dimension.
     */
    public MazeData(int mazeWidth, int mazeDepth)
    {
        // load dimensions (in maze units)
        this.mazeWidth = mazeWidth;
        this.mazeDepth = mazeDepth;

        // calculate edge positions
        left = -1 * mazeWidth / 2f;
        right = mazeWidth / 2f;
        back = mazeDepth / 2f;
        front = -1 * mazeDepth / 2f;
        //Debug.Log("Edges detected: L {" + left + "} R {" + right + "} F {" + front + "} B {" + back + "}");

        // define exterior walls
        walls.Add(new Wall(left, front, right, front)); // Front wall
        walls.Add(new Wall(left, back, right, back)); // Back wall
        walls.Add(new Wall(left, front, left, back)); // Left wall
        walls.Add(new Wall(right, front, right, back)); // Right wall

        GenerateDefaultWalls();
        //GenerateWilsonWalls();

        // set spawn and goal positions (in maze coordinates)
        spawnPosition = new Vector2(left + 0.5f, front + 0.5f);
        goalPosition = new Vector2(right - 0.5f, back - 1.5f);
    }

    /**
     * Generates pre-defined maze walls.
     */
    void GenerateDefaultWalls()
    {
        // now add interior walls
        walls.Add(new Wall(left, back - 2, left + 1, back - 2));
        walls.Add(new Wall(left + 1, front + 1, left + 1, back - 4));
        walls.Add(new Wall(left + 1, back - 4, left + 3, back - 4));
        walls.Add(new Wall(left + 2, back - 4, left + 2, back - 1));
        walls.Add(new Wall(left + 2, front + 1, left + 2, back - 5));
        walls.Add(new Wall(left + 2, back - 5, left + 3, back - 5));
        walls.Add(new Wall(left + 2, front + 2, left + 3, front + 2));
        walls.Add(new Wall(left + 3, back - 4, left + 3, back - 1));
        walls.Add(new Wall(left + 3, front + 5, left + 3, back - 5));
        walls.Add(new Wall(left + 3, front + 2, left + 3, front + 4));
        walls.Add(new Wall(left + 3, back - 1, left + 4, back - 1));
        walls.Add(new Wall(left + 4, back - 1, left + 4, back));
        walls.Add(new Wall(left + 4, back - 4, left + 4, back - 2));
        walls.Add(new Wall(left + 4, front + 1, left + 4, front + 5));
        walls.Add(new Wall(left + 4, front + 1, left + 5, front + 1));
        walls.Add(new Wall(left + 5, back - 2, left + 5, back - 1));
        walls.Add(new Wall(left + 5, front + 5, left + 5, back - 5));
        walls.Add(new Wall(left + 5, front + 1, left + 5, front + 3));
        walls.Add(new Wall(left + 5, back - 1, right - 5, back - 1));
        walls.Add(new Wall(left + 5, back - 2, right - 4, back - 2));
        walls.Add(new Wall(left + 5, front + 5, right - 5, front + 5));
        walls.Add(new Wall(left + 5, front + 2, right - 3, front + 2));
        walls.Add(new Wall(right - 5, back - 3, right - 5, back - 2));
        walls.Add(new Wall(right - 5, front + 3, right - 3, front + 3));
        walls.Add(new Wall(right - 4, back - 2, right - 4, back));
        walls.Add(new Wall(right - 4, front + 5, right - 4, back - 4));
        walls.Add(new Wall(right - 4, front, right - 4, front + 1));
        walls.Add(new Wall(right - 4, front + 1, right - 2, front + 1));
        walls.Add(new Wall(right - 3, back - 3, right - 3, back));
        walls.Add(new Wall(right - 3, front + 3, right - 3, front + 5));
        walls.Add(new Wall(right - 3, back - 5, right - 1, back - 5));
        walls.Add(new Wall(right - 2, back - 5, right - 2, back - 1));
        walls.Add(new Wall(right - 2, front + 3, right - 2, front + 5));
        walls.Add(new Wall(right - 2, back - 1, right - 1, back - 1));
        walls.Add(new Wall(right - 2, front + 3, right, front + 3));
        walls.Add(new Wall(right - 1, back - 2, right - 1, back - 1));
        walls.Add(new Wall(right - 1, front + 5, right - 1, back - 5));
        walls.Add(new Wall(right - 1, back - 2, right, back - 2));
        walls.Add(new Wall(right - 1, front + 2, right, front + 2));
    }

    /**
     * Generate maze walls using Wilson's algorithm.
     */
    void GenerateWilsonWalls()
    {
        // generate list of all vertices
        List<Vector2Int> toBeAdded = new List<Vector2Int>();
        List<Vector2Int> added = new List<Vector2Int>();
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeDepth; z++)
            {
                toBeAdded.Add(new Vector2Int(x, z));
            }
        }

        // generate list of all edges
        List<Tuple<Vector2Int, Vector2Int>> edges = new List<Tuple<Vector2Int, Vector2Int>>();
        foreach (Vector2Int startSquare in toBeAdded)
        {
            // test neighbors
            Vector2Int nextSquare = new Vector2Int(startSquare.x - 1, startSquare.y);
            if (IsValidMazeSquare(nextSquare))
            {
                if (!edges.Contains(new Tuple<Vector2Int, Vector2Int>(nextSquare, startSquare)))
                edges.Add(new Tuple<Vector2Int, Vector2Int>(startSquare, nextSquare));
            }

            nextSquare = new Vector2Int(startSquare.x, startSquare.y + 1);
            if (IsValidMazeSquare(nextSquare))
            {
                if (!edges.Contains(new Tuple<Vector2Int, Vector2Int>(nextSquare, startSquare)))
                edges.Add(new Tuple<Vector2Int, Vector2Int>(startSquare, nextSquare));
            }

            nextSquare = new Vector2Int(startSquare.x + 1, startSquare.y);
            if (IsValidMazeSquare(nextSquare))
            {
                if (!edges.Contains(new Tuple<Vector2Int, Vector2Int>(nextSquare, startSquare)))
                edges.Add(new Tuple<Vector2Int, Vector2Int>(startSquare, nextSquare));
            }

            nextSquare = new Vector2Int(startSquare.x, startSquare.y - 1);
            if (IsValidMazeSquare(nextSquare))
            {
                if (!edges.Contains(new Tuple<Vector2Int, Vector2Int>(nextSquare, startSquare)))
                edges.Add(new Tuple<Vector2Int, Vector2Int>(startSquare, nextSquare));
            }
        }

        // add random square to maze
        int i = UnityEngine.Random.Range(0, toBeAdded.Count);
        added.Add(toBeAdded[i]);
        toBeAdded.RemoveAt(i);

        // run Wilson's algorithm
        while (toBeAdded.Count > 0)
        {
            // pick a random square
            int j = UnityEngine.Random.Range(0, toBeAdded.Count);
            Vector2Int newStart = toBeAdded[j];

            // begin walk
            Dictionary<Vector2Int, int> walk = new Dictionary<Vector2Int, int>();
            Vector2Int prevSquare = newStart;
            Vector2Int nextSquare = newStart;
            while (!added.Contains(nextSquare))
            {
                int dir = -1;
                bool isValid = false;
                prevSquare = nextSquare;
                while (!isValid)
                {
                    // get random direction
                    dir = UnityEngine.Random.Range(0, 4);
                    switch(dir)
                    {
                        case 0: // left
                            nextSquare += new Vector2Int(-1, 0);
                            break;
                        case 1: // up
                            nextSquare += new Vector2Int(0, 1);
                            break;
                        case 2: // right
                            nextSquare += new Vector2Int(1, 0);
                            break;
                        case 3: // down
                            nextSquare += new Vector2Int(0, -1);
                            break;
                        default:
                            break;
                    }
                    isValid = IsValidMazeSquare(nextSquare);
                }

                // record walk
                if (walk.ContainsKey(prevSquare)) walk.Remove(prevSquare); // need to update direction
                walk.Add(prevSquare, dir);
            }

            // add walk to maze
            nextSquare = newStart;
            while(!added.Contains(nextSquare))
            {
                prevSquare = nextSquare;
                added.Add(prevSquare);
                int dir = walk[prevSquare];
                walk.Remove(prevSquare);
                switch (dir)
                {
                    case 0: // left
                        nextSquare += new Vector2Int(-1, 0);
                        break;
                    case 1: // up
                        nextSquare += new Vector2Int(0, 1);
                        break;
                    case 2: // right
                        nextSquare += new Vector2Int(1, 0);
                        break;
                    case 3: // down
                        nextSquare += new Vector2Int(0, -1);
                        break;
                    default:
                        break;
                }

                // remove the edge so no wall is generated
                Tuple<Vector2Int, Vector2Int> testEdge = new Tuple<Vector2Int, Vector2Int>(prevSquare, nextSquare);
                if (edges.Contains(testEdge)) edges.Remove(testEdge);
                testEdge = new Tuple<Vector2Int, Vector2Int>(nextSquare, prevSquare);
                if (edges.Contains(testEdge)) edges.Remove(testEdge);
            }
        }

        // now build walls
        foreach (Tuple<Vector2Int, Vector2Int> edge in edges)
        {
            Vector2Int sq1 = edge.Item1;
            Vector2Int sq2 = edge.Item2;

            if (sq1.x == sq2.x)
            {
                float y = (sq1.y + sq2.y) / 2f;
                walls.Add(new Wall(sq1.x - 0.5f, y, sq1.x + 0.5f, y));
            } else
            {
                float x = (sq1.x + sq2.x) / 2f;
                walls.Add(new Wall(x, sq1.y - 0.5f, x, sq1.y + 0.5f));
            }
        }
    }

    /**
     * Checks if given maze square is within the bounds of the maze.
     * @return      True if maze square is within the bounds of the maze, False otherwise.
     */
    private bool IsValidMazeSquare(Vector2Int mazeSquare)
    {
        return (mazeSquare.x >= left && mazeSquare.x <= right && mazeSquare.y >= front && mazeSquare.y <= back);
    }

    /**
     * Accessor for the maze's local X scale.
     * @return      Maze's local X scale.
     */
    public float Width()
    {
        return MAZE_SCALE * this.mazeWidth;
    }

    /**
     * Accessor for the maze's local Z scale.
     * @return      Maze's local Z scale.
     */
    public float Depth()
    {
        return MAZE_SCALE * this.mazeDepth;
    }

    /**
     * Accessor for the maze's local Y scale.
     * @return      Maze's local Y scale.
     */
    public float Height()
    {
        return MAZE_SCALE * BASE_HEIGHT;
    }

    /**
     * Accessor for spawn position as non-child local position.
     * @return      Maze's spawn position as a local transform position for a non-child object (marble).
     */
    public Vector3 SpawnPos()
    {
        return MazeToLocalPos(spawnPosition, 1f); // marble height is 1
    }

    /**
     * Accessor for goal position as non-child local position.
     * @return      Maze's goal position as a local transform position for a non-child object (marble).
     */
    public Vector3 GoalPos()
    {
        return MazeToLocalPos(goalPosition, 1f); // marble height is 1
    }

    /**
     * Converts a maze position (X, Z) into a local unity transform position (X, Y, Z) based on the non-child object's height.
     * 
     * @param mazePos       The position in maze coordinates.
     * @param objHeight     The height of the object being positioned.
     * @return              The local position for the object.
     */
    private Vector3 MazeToLocalPos(Vector2 mazePos, float objHeight)
    {
        return new Vector3(MAZE_SCALE * mazePos.x, objHeight / 2, MAZE_SCALE * mazePos.y);
    }

    /**
     * Converts a maze position (X, Z) into a local unity transform position (X, Y, Z) based on the child object's height.
     * 
     * @param mazePos           The position in maze coordinates.
     * @param childHeight       The height of the object being positioned.
     * @return                  The local position for the child object.
     */
    private Vector3 MazeToChildPos(Vector2 mazePos, float childHeight)
    {
        return new Vector3(mazePos.x / mazeWidth, childHeight / (2 * BASE_HEIGHT), mazePos.y / mazeDepth);
    }

    /**
     * Converts a child object's maze dimensions (width, depth, height) into a local unity transform scale (X, Y, Z).
     * 
     * @param width         The child object's X dimension in maze units.
     * @param depth         The child object's Z dimension in maze units.
     * @param height        The child object's Y dimension in maze units.
     * @return              The local scale for the child object.
     */
    private Vector3 MazeToChildScale(float width, float depth, float height)
    {
        return new Vector3(width / mazeWidth, height / BASE_HEIGHT, depth / mazeDepth);
    }

    /**
     * Apply Maze wall traits to GameObject wall.
     * 
     * @param gameObject        The GameObject to apply the maze wall traits to.
     * @param wall              The maze Wall whose traits will be applied to the GameObject.
     */
    public void BuildWall(GameObject wallGO, Wall wall)
    {
        // Learn wall's maze dimensions
        float width = wall.topRight.x - wall.botLeft.x;
        if (width == 0) width = WALL_THICKNESS;
        float depth = wall.topRight.y - wall.botLeft.y;
        if (depth == 0) depth = WALL_THICKNESS;

        // determine central point
        float x = (wall.topRight.x + wall.botLeft.x) / 2;
        float z = (wall.topRight.y + wall.botLeft.y) / 2;

        // Set local scale
        wallGO.transform.localScale = MazeToChildScale(width, depth, WALL_HEIGHT);

        // Set local position
        wallGO.transform.localPosition = MazeToChildPos(new Vector2(x, z), WALL_HEIGHT);
        
    }
}
