using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TileType
{
    INIT,
    EMPTY,
    HOLE,
    PURP,
    ROCK,
    TREE_1,
    TREE_2,
    LETTER
}

public enum PlatformType
{
    STARTING,
    EMPTY, //C
    SLALLON, //S
    MULTIPLE, //M
    PURP, //P
    MAP
}

public static class RandomTiles {

    private static TileType[] lastTileGroup;
    private static TileType lastTile;
    private static System.Random rng = new System.Random();
    static PlatformType lastPlatform = PlatformType.EMPTY;

    private readonly static float[] STARTING_WEIGHTS = new float[4] { 0.25f, 0.25f, 0.25f, 0.25f };//Order is S, M, P, C
    private readonly static float[] EMPTY_WEIGHTS = new float[4] { 0.20f, 0.20f, 0.20f, 0.40f };//Order is S, M, P, C
    private readonly static float[] SLALOM_WEIGHTS = new float[4] { 0.60f, 0.19f, 0.2f, 0.01f }; //Order is S, M, P, C
    private readonly static float[] MULTIPLE_WEIGHTS = new float[4] { 0.55f, 0.15f, 0.29f, 0.01f }; //Order is M, S, P, C
    private readonly static float[] PURP_WEIGHTS = new float[4] { 0.6f, 0.2f, 0.19f, 0.01f }; //Order is P, M, S, C

    public static PlatformType[] RandomTilesSelector(int lanesHeight, int seed = 3,  int tilesOfTypeBeforeChanging = 5, int initialEmptyTiles = 1)
    {
        //rng = new System.Random(seed);
     
        int totalPlatforms = lanesHeight + initialEmptyTiles;
        PlatformType[] selectedTiles = new PlatformType[totalPlatforms];

        for (int i = 0; i < initialEmptyTiles; i++)
        {
            selectedTiles[i] = PlatformType.EMPTY;
        }

        int remainingPlatformsOfType = tilesOfTypeBeforeChanging;
        int randomOffset = remainingPlatformsOfType + rng.Next(-2, 2);

        remainingPlatformsOfType = (randomOffset < 1) ? tilesOfTypeBeforeChanging : randomOffset;

        var numberOfElementsInPlatform = Enum.GetNames(typeof(PlatformType)).Length;
        var platforms = Enum.GetValues(typeof(PlatformType));
        
        var currentPlatform = (PlatformType)platforms.GetValue(rng.Next(0, numberOfElementsInPlatform));
        for (int i = initialEmptyTiles; i < totalPlatforms; i++)
        {
            if(currentPlatform == PlatformType.EMPTY)
            {
                remainingPlatformsOfType = 1;
            }

            if (remainingPlatformsOfType > 0)
            {
                selectedTiles[i] = currentPlatform;
                remainingPlatformsOfType--;
            }
            else
            {
                currentPlatform = (PlatformType)platforms.GetValue(rng.Next(0, numberOfElementsInPlatform));

                while(currentPlatform == PlatformType.EMPTY)
                {
                    currentPlatform = (PlatformType)platforms.GetValue(rng.Next(0, numberOfElementsInPlatform));
                }
                randomOffset = remainingPlatformsOfType + rng.Next(-2, 2);

                remainingPlatformsOfType = (randomOffset < 1) ? tilesOfTypeBeforeChanging : randomOffset;
            }
        }       

        return selectedTiles;
    }

    public static TileType[,] RandomTilesSelector(int laneWidth,  int lanesHeight, int seed = 3, int emptyTilesBeforeObstacle = 0, int initialEmptyTiles = 3)
    {
        
        int spacedTilesHeight = lanesHeight + emptyTilesBeforeObstacle * (lanesHeight - 1) + initialEmptyTiles;
        TileType[,] selectedTileTypes = new TileType[spacedTilesHeight, laneWidth];
        lastTileGroup = new TileType[laneWidth];

        //Initial filling of the empty lanes for starting the game
        for (int i = 0; i < initialEmptyTiles; i++)
        {
            for (int j = 0; j < laneWidth; j++)
            {
                selectedTileTypes[i, j] = TileType.EMPTY;
            }
        }

        for (int i = 0; i < laneWidth; i++)
        {
            lastTileGroup[i] = selectedTileTypes[initialEmptyTiles - 1, i];
        }

        //Start the counter in 0, so that it has an obstacle right away
        int remainingTilesUntilObstacle = 0;

        for (int i = initialEmptyTiles; i < spacedTilesHeight; i++)
        {
            int lastGroupEmptyTilesCount = EmptyTilesCountInLasTileGroup(laneWidth);
            List<int> lastGroupEmptyTilesPosition = EmptyTilesPositionsInLasTileGroup(laneWidth);

            #region Checks and errors
            if (lastGroupEmptyTilesCount != lastGroupEmptyTilesPosition.Count)
            {
                string messageError = 
                    string.Format(
                        "Found {0} empty lanes, but there are {1} positions",
                        lastGroupEmptyTilesCount,
                        lastGroupEmptyTilesPosition.Count);

                Debug.LogError(messageError);

                return null;
            }

            if(lastGroupEmptyTilesCount == 0)
            {
                Debug.LogError("No path available for player!!!");

                return null;
            }

            #endregion

            if (laneWidth > 1)
            {
                if (remainingTilesUntilObstacle == 0)
                {
                    //Create the posible positions for next empty lane
                    int randTilePosCenter = rng.Next(0, lastGroupEmptyTilesCount);  //0
                    int randTilePosRight = randTilePosCenter + 1;                       //1
                    int randTilePosLeft = randTilePosCenter - 1;                        //2

                    int posiblePosition = rng.Next(0, 3);

                    //Fill the next empty lane based on the selected posible position
                    if (posiblePosition == 0)
                    {
                        selectedTileTypes[i, randTilePosCenter] = TileType.EMPTY;
                    }
                    else if (posiblePosition == 1 && randTilePosRight < laneWidth)
                    {
                        selectedTileTypes[i, randTilePosRight] = TileType.EMPTY;
                    }
                    else if (posiblePosition == 2 && randTilePosLeft >= 0)
                    {
                        selectedTileTypes[i, randTilePosLeft] = TileType.EMPTY;
                    }
                    else
                    {
                        selectedTileTypes[i, randTilePosCenter] = TileType.EMPTY;
                    }

                    //Add different obstacles

                    for (int j = 0; j < laneWidth; j++)
                    {
                        if (selectedTileTypes[i, j] != TileType.EMPTY)
                            selectedTileTypes[i, j] = TileType.ROCK;
                    }

                    remainingTilesUntilObstacle = emptyTilesBeforeObstacle;
                }
                else
                {
                    for (int j = 0; j < laneWidth; j++)
                    {
                        selectedTileTypes[i, j] = TileType.EMPTY;
                    }
                    remainingTilesUntilObstacle--;
                }               
            }

            //Set the last tiles to the one just created
            for (int j = 0; j < lastTileGroup.Length; j++)
            {
                lastTileGroup[j] = selectedTileTypes[i, j];
            }
           
        }

        return selectedTileTypes;

    }

    public static PlatformType[] GrammarBasedPlatformSelector(int lanesHeight, int seed = 3, int initialEmptyTiles = 1)
    {
        
        int totalPlatforms = lanesHeight + initialEmptyTiles + 1;
        PlatformType[] selectedPlatforms = new PlatformType[totalPlatforms];

        for (int i = 0; i < initialEmptyTiles; i++)
        {
            selectedPlatforms[i] = PlatformType.STARTING;
        }
        
        float probability, acumProbability;

        for (int i = initialEmptyTiles; i < totalPlatforms - 1; i++)
        {
            probability = rng.Next(100);
            acumProbability = 0f;

            if(lastPlatform == PlatformType.STARTING)
            {
                for (int j = 0; j < EMPTY_WEIGHTS.Length; j++)
                {
                    acumProbability += Mathf.RoundToInt(STARTING_WEIGHTS[j] * 100f);

                    if (probability >= acumProbability)
                    {
                        continue;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            selectedPlatforms[i] = PlatformType.SLALLON;
                            lastPlatform = PlatformType.SLALLON;
                        }
                        else if (j == 1)
                        {
                            selectedPlatforms[i] = PlatformType.MULTIPLE;
                            lastPlatform = PlatformType.MULTIPLE;
                        }
                        else if (j == 2)
                        {
                            selectedPlatforms[i] = PlatformType.PURP;
                            lastPlatform = PlatformType.PURP;
                        }
                        else if (j == 3)
                        {
                            selectedPlatforms[i] = PlatformType.EMPTY;
                            lastPlatform = PlatformType.EMPTY;
                        }
                        break;
                    }
                }
            }
            else if(lastPlatform == PlatformType.EMPTY)
            {
                for (int j = 0; j < EMPTY_WEIGHTS.Length; j++)
                {
                    acumProbability += Mathf.RoundToInt(EMPTY_WEIGHTS[j] * 100f);

                    if(probability >= acumProbability)
                    {
                        continue;
                    }
                    else
                    {
                        if(j == 0)
                        {
                            selectedPlatforms[i] = PlatformType.SLALLON;
                            lastPlatform = PlatformType.SLALLON;
                        }
                        else if(j == 1)
                        {
                            selectedPlatforms[i] = PlatformType.MULTIPLE;
                            lastPlatform = PlatformType.MULTIPLE;
                        }
                        else if (j == 2)
                        {
                            selectedPlatforms[i] = PlatformType.PURP;
                            lastPlatform = PlatformType.PURP;
                        }
                        else if (j == 3)
                        {
                            selectedPlatforms[i] = PlatformType.EMPTY;
                            lastPlatform = PlatformType.EMPTY;
                        }
                        break;
                    }
                }
            }
            else if(lastPlatform == PlatformType.SLALLON)
            {
                for (int j = 0; j < SLALOM_WEIGHTS.Length; j++)
                {
                    acumProbability += Mathf.RoundToInt(SLALOM_WEIGHTS[j] * 100f);

                    if (probability >= acumProbability)
                    {
                        continue;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            selectedPlatforms[i] = PlatformType.SLALLON;
                            lastPlatform = PlatformType.SLALLON;
                        }
                        else if (j == 1)
                        {
                            selectedPlatforms[i] = PlatformType.MULTIPLE;
                            lastPlatform = PlatformType.MULTIPLE;
                        }
                        else if (j == 2)
                        {
                            selectedPlatforms[i] = PlatformType.PURP;
                            lastPlatform = PlatformType.PURP;
                        }
                        else if (j == 3)
                        {
                            selectedPlatforms[i] = PlatformType.EMPTY;
                            lastPlatform = PlatformType.EMPTY;
                        }
                        break;
                    }
                }
            }
            else if(lastPlatform == PlatformType.MULTIPLE)
            {
                for (int j = 0; j < MULTIPLE_WEIGHTS.Length; j++)
                {
                    acumProbability += Mathf.RoundToInt(MULTIPLE_WEIGHTS[j] * 100f);

                    if (probability >= acumProbability)
                    {
                        continue;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            selectedPlatforms[i] = PlatformType.MULTIPLE;
                            lastPlatform = PlatformType.MULTIPLE;
                        }
                        else if (j == 1)
                        {
                            selectedPlatforms[i] = PlatformType.SLALLON;
                            lastPlatform = PlatformType.SLALLON;
                        }
                        else if (j == 2)
                        {
                            selectedPlatforms[i] = PlatformType.PURP;
                            lastPlatform = PlatformType.PURP;
                        }
                        else if (j == 3)
                        {
                            selectedPlatforms[i] = PlatformType.EMPTY;
                            lastPlatform = PlatformType.EMPTY;
                        }
                        break;
                    }
                }
            }
            else if(lastPlatform == PlatformType.PURP)
            {
                for (int j = 0; j < PURP_WEIGHTS.Length; j++)
                {
                    acumProbability += Mathf.RoundToInt(PURP_WEIGHTS[j] * 100f);

                    if (probability >= acumProbability)
                    {
                        continue;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            selectedPlatforms[i] = PlatformType.PURP;
                            lastPlatform = PlatformType.PURP;
                        }
                        else if (j == 1)
                        {
                            selectedPlatforms[i] = PlatformType.MULTIPLE;
                            lastPlatform = PlatformType.MULTIPLE;
                        }
                        else if (j == 2)
                        {
                            selectedPlatforms[i] = PlatformType.SLALLON;
                            lastPlatform = PlatformType.SLALLON;
                        }
                        else if (j == 3)
                        {
                            selectedPlatforms[i] = PlatformType.EMPTY;
                            lastPlatform = PlatformType.EMPTY;
                        }
                        break;
                    }
                }
            }
        }

        selectedPlatforms[totalPlatforms - 1] = PlatformType.MAP;

        return selectedPlatforms;
    }

    private static int EmptyTilesCountInLasTileGroup(int laneWidth)
    {
        int count = 0;
        for (int i = 0; i < laneWidth; i++)
        {
            if(lastTileGroup[i] == TileType.EMPTY)
            {
                count++;
            }
        }
        return count;
    }

    private static List<int> EmptyTilesPositionsInLasTileGroup(int laneWidth)
    {
        List<int> count = new List<int>();
        for (int i = 0; i < laneWidth; i++)
        {
            if (lastTileGroup[i] == TileType.EMPTY)
            {
                count.Add(i);
            }
        }
        return count;
    }


}
