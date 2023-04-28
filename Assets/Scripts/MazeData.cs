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
    private static readonly float MAZE_SCALE = 2f; // # of unity grid squares corresponding to 1 maze square, in 1 dimension
    private static readonly float BASE_HEIGHT = 0.05f;
    private static readonly float WALL_THICKNESS = 0.1f;
    private static readonly float WALL_HEIGHT = 1.0f;

    // Maze Dimensions
    // best if these are odd (central (0,0) tile), but not required
    private int mazeWidth;
    private int mazeDepth;

    // Maze Positions
    private Vector2 spawnPosition;
    private Vector2 goalPosition;

    public readonly List<Wall> walls = new List<Wall>();

    public struct Wall
    {
        public Vector2 centerPos;
        public int width;
        public int depth;

        public Wall(int xPos, int yPos, int w, int d)
        {
            centerPos = new Vector2(xPos, yPos);
            width = w;
            depth = d;
        }
    }

    /**
     * Constructor for maze, accepting all flexible values.
     */
    public MazeData(int mazeWidth, int mazeDepth)
    {
        // load dimensions (in maze units)
        this.mazeWidth = mazeWidth;
        this.mazeDepth = mazeDepth;

        // define walls
        walls.Add(new Wall(0, MaxZ(), mazeWidth, 0)); // Front wall
        walls.Add(new Wall(0, MinZ(), mazeWidth, 0)); // Back wall
        walls.Add(new Wall(MaxX(), 0, 0, mazeDepth)); // Right wall
        walls.Add(new Wall(MinX(), 0, 0, mazeDepth)); // Left wall

        // set spawn and goal positions (in maze coordinates)
        spawnPosition = new Vector2(-5, -5);
        goalPosition = new Vector2(5, 4);
    }

    /**
     * Accessor for the maze's local X scale.
     * 
     * @return      Maze's local X scale.
     */
    public float Width()
    {
        return MAZE_SCALE * this.mazeWidth;
    }

    /**
     * Accessor for the maze's local Z scale.
     * 
     * @return      Maze's local Z scale.
     */
    public float Depth()
    {
        return MAZE_SCALE * this.mazeDepth;
    }

    /**
     * Accessor for the maze's local Y scale.
     * 
     * @return      Maze's local Y scale.
     */
    public float Height()
    {
        return MAZE_SCALE * BASE_HEIGHT;
    }

    /**
     * Accessor for spawn position as non-child local position.
     * 
     * @return      Maze's local spawn position for a non-child object (marble).
     */
    public Vector3 SpawnPos()
    {
        return GetLocalPos(spawnPosition, 1); // marble height is 1
    }

    /**
     * Accessor for goal position as non-child local position.
     * 
     * @return      Maze's local goal position for a non-child object (marble).
     */
    public Vector3 GoalPos()
    {
        return GetLocalPos(goalPosition, 1); // marble height is 1
    }

    /**
     * Apply Maze wall traits to GameObject wall.
     * 
     * @param gameObject        The GameObject to apply the maze wall traits to.
     * @param wall              The Maze Wall whose traits will be applied to the GameObject.
     */
    public void BuildWall(GameObject gameObject, Wall wall)
    {
        Debug.Log("Building wall @ (" + wall.centerPos.x + "," + wall.centerPos.y + ")");
        Debug.Log("Width: " + wall.width + "/ Depth: " + wall.depth);

        // Set local scale
        float width = (wall.width == 0 ? WALL_THICKNESS : wall.width);
        float depth = (wall.depth == 0 ? WALL_THICKNESS : wall.depth);
        gameObject.transform.localScale = GetScaleForChild(width, WALL_HEIGHT, depth);

        // Set local position
        gameObject.transform.localPosition = GetPosForChild(wall.centerPos, WALL_HEIGHT);
    }

    /**
     * Accessor for right-most maze coordinate.
     * 
     * @return      Maze's right-most maze coordinate.
     */
    private int MaxX()
    {
        return (mazeWidth - 1) / 2;
    }

    /**
     * Accessor for left-most maze coordinate.
     * 
     * @return      Maze's left-most maze coordinate.
     */
    private int MinX()
    {
        return (-1 * mazeWidth) / 2;
    }

    /**
     * Accessor for nearest maze coordinate.
     * 
     * @return      Maze's nearest maze coordinate.
     */
    private int MaxZ()
    {
        return (mazeDepth - 1) / 2;
    }

    /**
     * Accessor for farthest maze coordinate.
     * 
     * @return      Maze's farthest maze coordinate.
     */
    private int MinZ()
    {
        return (-1 * mazeDepth) / 2;
    }

    /**
     * Converts a maze position (X, Z) into a local unity transform position (X, Y, Z) based on the non-child object's height.
     * 
     * @param mazePos       The position in maze coordinates.
     * @param objHeight     The height of the object being positioned.
     * @return              The local position for the object.
     */
    private Vector3 GetLocalPos(Vector2 mazePos, float objHeight)
    {
        return new Vector3(MAZE_SCALE * mazePos.x, objHeight / 2, MAZE_SCALE * mazePos.y);
    }

    /**
     * Converts a maze position (X, Z) into a local unity transform position (X, Y, Z) based on the child object's height.
     * 
     * @param mazePos       The position in maze coordinates.
     * @param objHeight     The height of the object being positioned.
     * @return              The local position for the object.
     */
    private Vector3 GetPosForChild(Vector2 mazePos, float objHeight)
    {
        return new Vector3(GetXForChild(mazePos.x), GetYForChild(objHeight / 2), GetZForChild(mazePos.y));
    }

    /**
     * Converts a maze scale (width, height, depth) into a local unity transform scale (X, Y, Z) based on the child object's height.
     */
    private Vector3 GetScaleForChild(float widthInMaze, float heightInMaze, float depthInMaze)
    {
        return new Vector3(GetXForChild(widthInMaze), GetYForChild(heightInMaze), GetZForChild(depthInMaze));
    }

    /**
     * Converts an X value in maze units to a local unity transform X dimension value.
     * 
     * @param xInMaze       The X value in maze units to convert.
     * @return              A float containing the correct X value to supply to a local transform.
     */
    private float GetXForChild(float xInMaze)
    {
        return GetDimForChild(xInMaze, Width());
    }

    /**
     * Converts a Z value in maze units to a local unity transform Z dimension value.
     * 
     * @param zInMaze       The Z value in maze units to convert.
     * @return              A float containing the correct Z value to supply to a local transform.
     */
    private float GetZForChild(float zInMaze)
    {
        return GetDimForChild(zInMaze, Depth());
    }

    /**
     * Converts a Y value in maze units to a local unity transform Y dimension value.
     * 
     * @param yInMaze       The Y value in maze units to convert.
     * @return              A float containing the correct Y value to supply to a local transform.
     */
    private float GetYForChild(float yInMaze)
    {
        return GetDimForChild(yInMaze, Height());
    }

    /**
     * Converts a value in maze units to a local unity transform value for a child object of this Level.
     * 
     * @param diminMaze     The value in maze units to convert.
     * @param mazeDim       The maze value of the relevant maze scale dimension.
     * @return              A float containing the correct value to supply to a local transform.
     */
    private float GetDimForChild(float dimInMaze, float mazeDim)
    {
        if (mazeDim == 0)
        {
            return 0f;
        }
        else
        {
            return MAZE_SCALE * dimInMaze / mazeDim;
        }
    }
}
