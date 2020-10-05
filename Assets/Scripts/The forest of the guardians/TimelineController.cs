using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour {

	public List<PlayableDirector> directors;
	public List<TimelineAsset> timelines;
	public void Play(){
		for(int i = 0; i < directors.Count; i++){
            directors[i].enabled = true;
			directors[i].Play();
		}
	}	

	public void PlayFromTimelines(int index)
    {
        TimelineAsset selectedAsset;

        if (timelines.Count <= index) 
        {
            selectedAsset = timelines [timelines.Count - 1];
        } 
        else 
        {
            selectedAsset = timelines [index];
        }

        directors [0].Play (selectedAsset);
    }
}
