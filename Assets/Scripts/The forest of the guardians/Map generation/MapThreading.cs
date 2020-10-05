using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class MapThreading : MonoBehaviour {

    private DrawTiles drawTiles;
    public Queue<MapThreadInfo> threadQueue;
    private void Start()
    {
        drawTiles = GetComponent<DrawTiles>();
        threadQueue = new Queue<MapThreadInfo>();
    }

    public void RequestPlatforms(Action<PlatformType[]> callback)
    {
        ThreadStart threadStart = delegate
        {
            PlatformThread(callback);
        };
 
        new Thread(threadStart).Start();
    }

    public void PlatformThread(Action<PlatformType[]> callback)
    {
        PlatformType[] selectedPlatforms = drawTiles.GetPlatformsToDraw();
        lock(threadQueue)
        {
            threadQueue.Enqueue(new MapThreadInfo(selectedPlatforms, callback));
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		if(threadQueue.Count > 0)
        {
            for (int i = 0; i < threadQueue.Count; i++)
            {
                MapThreadInfo mapThreadInfo = threadQueue.Dequeue();
                mapThreadInfo.callback(mapThreadInfo.platforms);
            }
        }
	}

    public struct MapThreadInfo
    {
        public readonly PlatformType[] platforms;
        public readonly Action<PlatformType[]> callback;

        public MapThreadInfo(PlatformType[] platforms, Action<PlatformType[]> callback)
        {
            this.platforms = platforms;
            this.callback = callback;
        }
    }
}
