using System.Collections.Generic;

public struct Coordinates
{
    public List<int> X;
    public List<int> Y;

    public Coordinates(List<int> x, List<int> y)
    {
        this.X = x;
        this.Y = y;
    }
}

public class PathJoint
{
    public float X;
    public float Y;
}

public abstract class DataStorageGrid
{
    public static Dictionary<int, GridBase> GridDepthOnLevel = new Dictionary<int, GridBase>()
    {
        {0, new GridBase(4, 4)}, // test level
        {1, new GridBase(4, 5)},
        {2, new GridBase(4, 5)},
        {3, new GridBase(4, 5)},
        {4, new GridBase(4, 5)},
        {5, new GridBase(3, 6)},
        {6, new GridBase(3, 6)},
        {7, new GridBase(3, 6)},
        {8, new GridBase(3, 7)},
        {9, new GridBase(3, 7)},
        {10, new GridBase(4, 8)},
        {11, new GridBase(4, 8)},
        {12, new GridBase(4, 8)},
        {13, new GridBase(5, 9)},
        {14, new GridBase(5, 9)},
        {15, new GridBase(5, 9)},
        {16, new GridBase(5, 10)},
        {17, new GridBase(5, 10)},
        {18, new GridBase(5, 10)},
        {19, new GridBase(4, 11)},
        {20, new GridBase(4, 11)},
        {21, new GridBase(4, 11)},
        {22, new GridBase(4, 11)},
        {23, new GridBase(4, 12)},
        {24, new GridBase(4, 12)},
        {25, new GridBase(4, 12)},
        {26, new GridBase(4, 14)},
        {27, new GridBase(5, 14)},
        {28, new GridBase(5, 14)},
        {29, new GridBase(5, 15)},
        {30, new GridBase(5, 16)}
    };

    public static List<Coordinates> RandomLockedTiles2 = new List<Coordinates>()
    {
        new Coordinates(new List<int>(){},new List<int>(){}),
        new Coordinates(new List<int>(){3, 3},new List<int>(){2, 3 }),
        new Coordinates(new List<int>(){5, 5},new List<int>(){2, 3 }),
        new Coordinates(new List<int>(){5, 5},new List<int>(){3, 4 }),
    };
}