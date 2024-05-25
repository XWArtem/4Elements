using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private DataStorageGrid dataGrid;
    private GridBase gridBase;

    private ControllerGrid controllerGrid;

    private const float matrixStartX = -4.375f;
    private const float matrixStartY = 2.1f;

    private float deltaY;

    private const float CellWidth = 1.75f;
    public readonly float CellHeight = 1.75f;

    private int cellsAvailableOnStart;
    private int cellsInFog;
    public int LevelDepthTotal => cellsAvailableOnStart + cellsInFog;

    private int columns;

    public static float InputZoneLeftBorder => matrixStartX + CellWidth / 2;
    public static float InputZoneRightBorder => matrixStartX + 4.5f * CellWidth;

    private int TotalLength;
    private int FirstLength = 5;
    private int SecondLength = 8;

    private void Awake()
    {
        TotalLength = FirstLength + SecondLength;
    }

    public void Init(int levelIndex, List<int> lockedX, List<int> lockedY)
    {
        controllerGrid = GetComponent<ControllerGrid>();

        deltaY = transform.position.y;

        cellsAvailableOnStart = DataStorageGrid.GridDepthOnLevel[levelIndex].GridAvailableRows;
        cellsInFog = DataStorageGrid.GridDepthOnLevel[levelIndex].GridRowsInFog;

        gridBase = DataStorageGrid.GridDepthOnLevel[levelIndex];
        columns = gridBase.GridColumns;
        GameObject newTile;

        controllerGrid.InitGridMatrix(LevelDepthTotal + 1, columns);

        for (int i = 0; i < LevelDepthTotal; i++)
        {
            if (i < cellsAvailableOnStart)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0 || j == columns - 1)
                    {
                        newTile = Instantiate(tilePrefab, new Vector3((matrixStartX + CellWidth * j), matrixStartY - CellHeight * i + deltaY), Quaternion.identity, transform);
                        newTile.GetComponent<ControllerTile>().Init(tileType.locked, false, i, j);
                        controllerGrid.WriteTile(i, j, tileType.locked, false);
                    }
                    else
                    {
                        newTile = Instantiate(tilePrefab, new Vector3((matrixStartX + CellWidth * j), matrixStartY - CellHeight * i + deltaY), Quaternion.identity, transform);
                        bool spawned = false;
                        // random locked in the center of field
                        for (int rand = 0; rand < lockedX.Count; rand++)
                        {
                            if ((i == lockedX[rand] && j == lockedY[rand]))
                            {
                                newTile.GetComponent<ControllerTile>().Init(tileType.locked, false, i, j);
                                controllerGrid.WriteTile(i, j, tileType.locked, false);
                                spawned = true;
                            }
                        }
                        // random iron or diamond tiles
                        if (!spawned)
                        {
                            if (Random.Range(0, 10) == 0)
                            {
                                newTile.GetComponent<ControllerTile>().Init(tileType.ironAvailable, false, i, j);
                                controllerGrid.WriteTile(i, j, tileType.ironAvailable, false);
                                spawned = true;
                            }
                            else if (Random.Range(0, 14) == 0)
                            {
                                newTile.GetComponent<ControllerTile>().Init(tileType.diamondAvailable, false, i, j);
                                controllerGrid.WriteTile(i, j, tileType.diamondAvailable, false);
                                spawned = true;
                            }
                        }
                        if (!spawned)
                        {
                            newTile.GetComponent<ControllerTile>().Init(tileType.available, false, i, j);
                            controllerGrid.WriteTile(i, j, tileType.available, false);
                        }
                    }
                    newTile.name = $"Tile-row:{i}-col:{j}";
                    controllerGrid.AddTileController(newTile.GetComponent<ControllerTile>(), i, j);
                }
            }
            else
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0 || j == columns - 1)
                    {
                        newTile = Instantiate(tilePrefab, new Vector3((matrixStartX + CellWidth * j), matrixStartY - CellHeight * i + deltaY), Quaternion.identity, transform);
                        newTile.GetComponent<ControllerTile>().Init(tileType.locked, true, i, j);
                        controllerGrid.WriteTile(i, j, tileType.locked, true);
                    }
                    else
                    {
                        newTile = Instantiate(tilePrefab, new Vector3((matrixStartX + CellWidth * j), matrixStartY - CellHeight * i + deltaY), Quaternion.identity, transform);
                        if (Random.Range(0, 20) == 0)
                        {
                            newTile.GetComponent<ControllerTile>().Init(tileType.ironAvailable, true, i, j);
                            controllerGrid.WriteTile(i, j, tileType.ironAvailable, true);
                        }
                        else if (Random.Range(0, 25) == 0)
                        {
                            newTile.GetComponent<ControllerTile>().Init(tileType.diamondAvailable, true, i, j);
                            controllerGrid.WriteTile(i, j, tileType.diamondAvailable, true);
                        }
                        else
                        {
                            newTile.GetComponent<ControllerTile>().Init(tileType.available, true, i, j);
                            controllerGrid.WriteTile(i, j, tileType.available, true);
                        }
                    }
                    newTile.name = $"Tile-row:{i}-col:{j}";
                    controllerGrid.AddTileController(newTile.GetComponent<ControllerTile>(), i, j);
                }
            }
        }

        for (int j = 0; j < columns; j++)
        {
            newTile = Instantiate(tilePrefab, new Vector3((matrixStartX + CellWidth * j), matrixStartY - CellHeight * LevelDepthTotal + deltaY), Quaternion.identity, transform);
            newTile.GetComponent<ControllerTile>().Init(tileType.locked, true, LevelDepthTotal, j);
            newTile.name = $"Tile-row:-{LevelDepthTotal}-col:{j}";
            controllerGrid.WriteTile(LevelDepthTotal, j, tileType.locked, true);
            controllerGrid.AddTileController(newTile.GetComponent<ControllerTile>(), LevelDepthTotal, j);
        }

        ControllerGrid.Instance.UpdateMatrixBackend(true);
    }
}
