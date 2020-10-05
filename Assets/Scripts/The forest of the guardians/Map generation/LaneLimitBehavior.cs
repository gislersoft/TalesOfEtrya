using UnityEngine;
public static class LaneLimitBehavior {

    public static int[] laneLimits;
    public static int spacing = 1;
    public static int currentLane = 0;

    public static void LaneLimits(int numberOfLanes, int laneSpacing, int startingLane)
    {
        laneLimits = new int[numberOfLanes];
        spacing = laneSpacing;

        for (int i = 0; i < numberOfLanes; i++)
        {
            laneLimits[i] = laneSpacing * i;
        }

        currentLane = startingLane;
    }

    public static void NextLane()
    {
        currentLane++;
        if(currentLane >= laneLimits.Length)
        {
            currentLane = laneLimits.Length - 1;
        }
    }

    public static void PreviousLane()
    {
        currentLane-= 1;
        
        if (currentLane < 0)
        {
            currentLane = 0;
        }
    }

    public static float CurrentLaneLimit()
    {
        return laneLimits[currentLane];
    }

    public static float NextLaneLimit()
    {
        int nextLanel = currentLane + 1;
        if (nextLanel >= laneLimits.Length)
        {
            nextLanel = laneLimits.Length - 1;
        }
        return laneLimits[nextLanel];
    }

    public static float PreviousLaneLimit()
    {
        int previousLane = currentLane + 1;
        if (previousLane < 0)
        {
            previousLane = 0;
        }
        return laneLimits[previousLane];
    }
}
