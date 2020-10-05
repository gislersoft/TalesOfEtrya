using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewWordProperties : MonoBehaviour {
    public Image ImageStatus;
    public Image ImageObjective;
    public GameObject Clue;
    public Image ImagePlace;
    public GameObject oclusionPanel;


    Sprite clueSprite;
    Sprite placeSprite;
    Sprite statusSprite;
    String clueText;
    String objectiveText;
    bool firstClue;
    bool lastClue;

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


    public void SetPropieties(Sprite clueSprite, Sprite placeSprite, Sprite statusSprite, String clueText, String objectiveText) {
        SetClueSprite( clueSprite );
        SetPlaceSprite( placeSprite );
        SetStatusSprite( statusSprite );
        SetClueText( clueText );
        SetObjectiveText( objectiveText );
    }

    public void UpdateProperties() {
        ImageObjective.sprite = clueSprite;
        ImagePlace.sprite = placeSprite;
        ImageStatus.sprite = statusSprite;
        Clue.GetComponent<TextMeshProUGUI>().text = clueText;
        //Clue.GetComponent<TextMeshPro>().text = objectiveText;

    }

    public void HidePanel() {
        //oclusionPanel.SetActive(true);
    }

    public void ShowPanel() {
        //oclusionPanel.SetActive(false);
    }

    public void SetAndUpdatePropieties(Sprite clueSprite, Sprite placeSprite, Sprite statusSprite, String clueText, String objectiveText) {
        this.SetClueSprite( clueSprite );
        this.SetPlaceSprite( placeSprite );
        this.SetStatusSprite( statusSprite );
        this.SetClueText( clueText );
        this.SetObjectiveText( objectiveText );
        //UpdateProperties();

        ImageObjective.sprite = clueSprite;
        ImagePlace.sprite = placeSprite;
        ImageStatus.sprite = statusSprite;
        Clue.GetComponent<TextMeshProUGUI>().text = clueText;
    }
}
