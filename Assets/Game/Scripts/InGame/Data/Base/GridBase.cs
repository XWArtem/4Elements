public class GridBase
{
    public int GridAvailableRows;
    public int GridRowsInFog;
    public int GridColumns;

    public GridBase(int availableRows, int gridRowsInFog, int cols = 6)
    {
        GridAvailableRows = availableRows;
        GridRowsInFog = gridRowsInFog;
        GridColumns = cols;
    }
}