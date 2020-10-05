using System.Collections.Generic;
using UnityEngine;

public class InGameUIWords : MonoBehaviour {

    List<GameObject> newWords;

    public GameObject wordsPanel;
    public GameObject newWord;

    private static InGameUIWords inGameUIWords;
    private SpritesAndText spritesAndText;

    #region Singleton
    public static InGameUIWords instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public static InGameUIWords Instance() {
        if (!inGameUIWords) {
            inGameUIWords = FindObjectOfType( typeof( InGameUIWords ) ) as InGameUIWords;
            if (!inGameUIWords)
                Debug.LogError( "There needs to be one active ObjectiveManager script on a GameObject in your scene." );
        }
        return inGameUIWords;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddWord(SpritesAndText info) {
        spritesAndText = info;
        GameObject a = Instantiate( newWord );
        ActualObjectiveProperties actualObjectiveProperties = a.GetComponent<ActualObjectiveProperties>();

        actualObjectiveProperties.SetPropieties( spritesAndText.GetClueSprite(), spritesAndText.GetPlaceSprite(), spritesAndText.GetStatusSprite(), spritesAndText.GetClueText(), spritesAndText.GetObjectiveText() );
        actualObjectiveProperties.UpdateProperties();
        actualObjectiveProperties.HidePanel();

        a.transform.SetParent( wordsPanel.transform, false );
    }

}
