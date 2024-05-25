public enum tileType
{
    locked,
    available,
    road,
    occupied,
    ironAvailable,
    diamondAvailable
}

public class TileBase
{
    public int X, Y;
    public tileType TileType;
    public bool HiddenInFog;
    public bool SupportTowerTile = false;
    public bool Diggable = false;
    public bool OccupiedByTower = false;
}
