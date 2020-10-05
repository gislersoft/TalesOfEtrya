using System;
using UnityEngine;

public class SpritesAndText : MonoBehaviour {
    public Sprite emptySprite;

    public Sprite clueSprite;
    public Sprite placeSprite;
    public Sprite statusSprite;
    public String clueText;
    private String objectiveText = "";
    public string placeDefinition;
    public string objectDefinition;
    public bool firstClue;
    public bool lastClue;
    public bool breakAfter;
    public bool cleanAfter;
    //public Text textComponent;

    public Sprite GetClueSprite()
    {
        if (clueSprite != null)
        {
            return clueSprite;
        }
        else return emptySprite;
    }

    public void SetClueSprite(Sprite sprite)
    {
        clueSprite = sprite;
    }

    public Sprite GetPlaceSprite()
    {
        if (clueSprite != null)
        {
            return placeSprite;
        }
        else return emptySprite;
    }

    public void SetPlaceSprite(Sprite sprite)
    {
        placeSprite = sprite;
    }

    public Sprite GetStatusSprite()
    {
        if (clueSprite != null)
        {
            return statusSprite;
        }
        else return emptySprite;
    }

    public void SetStatusSprite(Sprite sprite)
    {
        statusSprite = sprite;
    }

    public String GetClueText()
    {
        if (clueSprite != null)
        {
            return clueText;
        }
        else return "";
    }

    public void SetClueText(String text)
    {
        clueText = text;
    }

    public String GetObjectiveText()
    {
        if (clueSprite != null)
        {
            return objectiveText;
        }
        else return "";
    }

    public void SetObjectiveText(String text)
    {
        objectiveText = text;
    }
}
