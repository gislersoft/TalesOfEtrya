using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedClueController : MonoBehaviour {

    public Image ImageObjective;
    public GameObject Clue;
    public Image ImagePlace;
    public Animator AnimatorProperty;


    Sprite clueSprite;
    Sprite placeSprite;
    Sprite statusSprite;
    String clueText;
    String objectiveText;
    bool firstClue;
    bool lastClue;

    private static AnimatedClueController animatedClueController;
    #region Singleton
    public static AnimatedClueController instance;
    private void Awake() {
        instance = this;
    }
    #endregion



    public static AnimatedClueController Instance() {
        if (!animatedClueController) {
            animatedClueController = FindObjectOfType( typeof( AnimatedClueController ) ) as AnimatedClueController;
            if (!animatedClueController)
                Debug.LogError( "There needs to be one active ObjectiveManager script on a GameObject in your scene." );
        }

        return animatedClueController;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetClueSprite(Sprite sprite) {
        clueSprite = sprite;
        UpdateProperties();
    }

    public void SetPlaceSprite(Sprite sprite) {
        placeSprite = sprite;
        UpdateProperties();
    }

    public void SetStatusSprite(Sprite sprite) {
        statusSprite = sprite;
        UpdateProperties();
    }

    public void SetClueText(String text) {
        clueText = text;
        UpdateProperties();
    }

    public void SetObjectiveText(String text) {
        objectiveText = text;
        UpdateProperties();
    }


    public void SetPropieties(Sprite clueSprite, Sprite placeSprite, String clueText) {
        SetClueSprite( clueSprite );
        SetPlaceSprite( placeSprite );
        SetClueText( clueText );
    }

    public void UpdateProperties() {
        ImageObjective.sprite = clueSprite;
        ImagePlace.sprite = placeSprite;
        Clue.GetComponent<TextMeshProUGUI>().text = clueText;

        AnimatorProperty.SetTrigger( "ActiveAnimation" );
        //animatedCongratulationController.Animate();
        //AnimatedCongratulationController.instance.Animate();
    }

    public void SetAndUpdatePropieties(Sprite clueSprite, Sprite placeSprite, String clueText) {
        this.SetClueSprite( clueSprite );
        this.SetPlaceSprite( placeSprite );
        this.SetClueText( clueText );
        //UpdateProperties();

        ImageObjective.sprite = clueSprite;
        ImagePlace.sprite = placeSprite;
        Clue.GetComponent<TextMeshProUGUI>().text = clueText;

        AnimatorProperty.SetTrigger( "ActiveAnimation" );
        //animatedCongratulationController.Animate();
        //AnimatedCongratulationController.instance.Animate();
    }


}
