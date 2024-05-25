using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrid : MonoBehaviour
{
    public static List<List<TileBase>> tiles;
    public static List<List<ControllerTile>> tileControllers;

    public static ControllerGrid Instance;

    public static Action OnTowersFieldChanged;

    private ControllerBuilderUI controllerBuilderUI;

    private int lastDiggedTileX, lastDiggedTileY;

    private List<EnemyWave> enemyWaves;
    private int enemyWaveIndex = 0;

    private List<PathJoint> path = new List<PathJoint>();

    public void Init(ControllerBuilderUI controllerBuilderUI)
    {
        if (Instance == null)
        {
            Instance = this;

            tiles = new List<List<TileBase>>();
            tileControllers = new List<List<ControllerTile>>();
        }
        else
        {
            Destroy(gameObject);
        }
        this.controllerBuilderUI = controllerBuilderUI;

        int lvlIndexCurrent = DataStorageSingletone.Instance.CurrentLevelIndex;
        enemyWaves = DataStorageSingletone.Instance.DataStorageEnemies.EnemyWavesOnLevel[lvlIndexCurrent];
    }

    private void OnDestroy()
    {
        tiles.Clear();
        tileControllers.Clear();
    }

    public void InitGridMatrix(int rows, int columns = 6)
    {
        for (int i = 0; i < rows; i++)
        {
            tiles.Add(new List<TileBase>());
            tileControllers.Add(new List<ControllerTile>(columns));
            for (int j = 0; j < columns; j++)
            {
                tiles[i].Add(new TileBase());
            }
        }
    }

    public void WriteTile(int X, int Y, tileType type, bool hiddenInFog)
    {
        tiles[X][Y].X = X;
        tiles[X][Y].Y = Y;
        tiles[X][Y].TileType = type;
        tiles[X][Y].HiddenInFog = hiddenInFog;
    }

    public void AddTileController(ControllerTile controllerTile, int X, int Y)
    {
        tileControllers[X].Add(controllerTile);
    }

    public void UpdateMatrixBackend(bool initUpdate = false)
    {
        if (initUpdate)
        {
            for (int i = 0; i < tileControllers.Count; i++)
            {
                for (int j = 0; j < tileControllers[i].Count; j++)
                {
                    tileControllers[i][j].BaseTile.Diggable = (i == 0 && tileControllers[i][j].BaseTile.TileType != tileType.locked);
                    tileControllers[i][j].SetCallToActionAnimation((i == 0 && tileControllers[i][j].BaseTile.TileType != tileType.locked));
                    //tileControllers[i][j].TileBase.Diggable = true;
                }
            }
        }
        else
        {
            // close all
            for (int i = 0; i < tileControllers.Count; i++)
            {
                for (int j = 0; j < tileControllers[i].Count; j++)
                {
                    tileControllers[i][j].BaseTile.Diggable = false;
                    tileControllers[i][j].SetCallToActionAnimation(false);
                }
            }
            // open only needed
            var nextTile = tileControllers[lastDiggedTileX + 1][lastDiggedTileY];
            if (lastDiggedTileX + 1 <= tileControllers.Count && CheckTileType(nextTile))
            {
                nextTile.BaseTile.Diggable = true;
                nextTile.SetCallToActionAnimation(true);
            }

            if (lastDiggedTileX < 1) return;

            nextTile = tileControllers[lastDiggedTileX][lastDiggedTileY + 1];
            var diagonaleRight = tileControllers[lastDiggedTileX - 1][lastDiggedTileY + 1];
            if (lastDiggedTileY + 1 < 5 && CheckTileType(nextTile) && CheckDiagonaleTileType(diagonaleRight)) // 6 cols
            {
                nextTile.BaseTile.Diggable = true;
                nextTile.SetCallToActionAnimation(true);
            }
            nextTile = tileControllers[lastDiggedTileX][lastDiggedTileY - 1];
            var diagonaleLeft = tileControllers[lastDiggedTileX - 1][lastDiggedTileY - 1];
            if (lastDiggedTileY - 1 >= 1 && CheckTileType(nextTile) && CheckDiagonaleTileType(diagonaleLeft)) // 6 cols
            {
                nextTile.BaseTile.Diggable = true;
                nextTile.SetCallToActionAnimation(true);
            }
        }
    }

    public bool CheckSupportTowerConnection(int X, int Y)
    {
        if (X < 0 || Y < 1) return false;
        if (X > tileControllers.Count - 1 || Y > 5) return false;
        if (tileControllers[X][Y].BaseTile.TileType == tileType.road || tileControllers[X][Y].BaseTile.TileType == tileType.locked) return false;
        if (tileControllers[X][Y].IsSupportTower) return false;

        return tileControllers[X][Y].BaseTile.OccupiedByTower;
    }

    public void SetSupportInjection(int towerX, int towerY, SupportTowerType injectionType, bool inject)
    {
        tileControllers[towerX][towerY].linkedTowerController?.SetSupportInjection(injectionType, inject);
    }

    public void DigTile(int X, int Y)
    {
        tileControllers[X][Y].BaseTile.Diggable = false;
        lastDiggedTileX = X;
        lastDiggedTileY = Y;

        // update path
        if (X == 0)
        {
            var joint = new PathJoint();
            joint.X = tileControllers[X][Y].transform.position.x;
            joint.Y = tileControllers[X][Y].transform.position.y + 1.75f/2;
            path.Add(joint);
        }
        else
        {
            var joint = new PathJoint();
            joint.X = tileControllers[X][Y].transform.position.x;
            joint.Y = tileControllers[X][Y].transform.position.y;
            path.Add(joint);
        }


        if (tileControllers[X][Y].BaseTile.HiddenInFog)  // clear 1st row fog
        {
            foreach(var tile in tileControllers[X])
            {
                tile.ClearFog();
            }
            // check for availability to go for 2nd row fog down
            int wavesLeft = enemyWaves.Count - enemyWaveIndex;
            int rowsLeft = tileControllers.Count - X - 1;
            if (wavesLeft < rowsLeft)
            {
                if (X + 1 <= tileControllers.Count)  //  clear 2nd row fog
                {
                    foreach (var tile in tileControllers[X + 1])
                    {
                        tile.ClearFog();
                    }
                }
            }

            // spawn enemies
            GateSpawner.Instance.SpawnGateSingleEnemyType(
                tileControllers[X][Y].transform, 
                enemyWaves[enemyWaveIndex].Count, 
                enemyWaves[enemyWaveIndex].Type, 
                new List<PathJoint>(path));

            enemyWaveIndex++;

            if (enemyWaveIndex == enemyWaves.Count)
            {
                enemyWaveIndex = 0;
                //Debug.Log("INDEX WAS OUT OF RANGE for enemyWaves in ControllerGrid! Returned to 0 to avoid errors, but check it out!");
                Debug.Log("LastWave");
            }
            //GateSpawner.Instance.SpawnGateMultipleEnemies(tileControllers[X][Y].transform, new List<int>(1) { 2 }, new List<EnemyType>(1) { EnemyType.fireGolem });
        }
    }

    private bool CheckTileType(ControllerTile controllerTile)
    {
        return controllerTile.BaseTile.TileType == tileType.available ||
            controllerTile.BaseTile.TileType == tileType.ironAvailable ||
            controllerTile.BaseTile.TileType == tileType.diamondAvailable;
    }

    private bool CheckDiagonaleTileType(ControllerTile controllerTile)
    {
        return controllerTile.BaseTile.TileType == tileType.available ||
            controllerTile.BaseTile.TileType == tileType.locked ||
            controllerTile.BaseTile.TileType == tileType.ironAvailable ||
            controllerTile.BaseTile.TileType == tileType.diamondAvailable;
    }

    public void TryOpenBuilder(ControllerTile controllerTile, int row)
    {
        if (controllerTile.BaseTile.X < lastDiggedTileX)
        {
            controllerBuilderUI.InvokeBuilder(controllerTile);
        }
    }

    public void TryOpenSmallBuilder(ControllerTile controllerTile, int totalValue, int nextGradeCost, ResourceType resType)
    {
        if (controllerTile.BaseTile.OccupiedByTower)
        {
            controllerBuilderUI.InvokeSmallBuilder(controllerTile, totalValue, nextGradeCost, resType);
        }
    }
}
