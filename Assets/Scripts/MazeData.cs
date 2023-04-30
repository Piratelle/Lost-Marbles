using System.Collections;
using System.Collections.Generic;
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
        float left = -1 * mazeWidth / 2f;
        float right = mazeWidth / 2f;
        float back = mazeDepth / 2f;
        float front = -1 * mazeDepth / 2f;
        //Debug.Log("Edges detected: L {" + left + "} R {" + right + "} F {" + front + "} B {" + back + "}");

        // define walls
        walls.Add(new Wall(left, front, right, front)); // Front wall
        walls.Add(new Wall(left, back, right, back)); // Back wall
        walls.Add(new Wall(left, front, left, back)); // Left wall
        walls.Add(new Wall(right, front, right, back)); // Right wall

        // set spawn and goal positions (in maze coordinates)
        spawnPosition = new Vector2(left + 0.5f, front + 0.5f);
        goalPosition = new Vector2(right - 0.5f, back - 1.5f);
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
