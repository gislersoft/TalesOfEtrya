using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriGameController : MonoBehaviour {



    public List<Button> _ButtonsList;

    public List<Sprite> _ImageList;

    public List<Sprite> _ImageTextList;

    private List<bool> _FlipState;

    private List<bool> _MatchState;

    private bool firstCard = false;

    private int selectedCard = -1;

    private int matchingCard = -1;

    private int numberOfCards = 12;

    private List<Sprite> arrDes1 = new List<Sprite>();

    private List<Sprite> arrDes2 = new List<Sprite>();

    Image imageComponent;


    // Use this for initialization
    void Start() {
        _FlipState = new List<bool>();
        for (int i = 0; i < _ButtonsList.Count; i++)
        {
            _FlipState.Add(false);
        }
        RandomizeList(_ImageList, _ImageTextList);
        PutSpritesInButtons();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FlipCard(int index)
    {
        _FlipState[index] = !_FlipState[index];
        firstCard = !firstCard;
        if (firstCard)
        {
            selectedCard = index;
        }
        else
        {
            matchingCard = index;
        }
        
    }

    void FlipAllCards()
    {
        for (int i = 0; i < _ButtonsList.Count; i++)
        {
            _FlipState[i] = false;
        }
    }

    bool MatchSucceful()
    {

        return false;

    }

    void RandomizeList(List<Sprite> list1, List<Sprite> list2)
    {
        List<Sprite> arr1 = list1;
        List<Sprite> arr2 = list2;
        arrDes1 = new List<Sprite>();
        arrDes2 = new List<Sprite>();

        UnityEngine.Random randNum = new UnityEngine.Random();
        while (arr1.Count > 0)
        {
            int val = UnityEngine.Random.Range(0, arr1.Count - 1);
            arrDes1.Add(arr1[val]);
            arrDes2.Add(arr2[val]);
            arr1.RemoveAt(val);
            arr2.RemoveAt(val);
        }

    }

    void PutSpritesInButtons()
    {
        int counterButton = 0;
        for (int i = 0; i < numberOfCards; i++)
        {
            imageComponent = _ButtonsList[counterButton].GetComponent<Image>();
            imageComponent.sprite = arrDes1[i];
            counterButton++;
        }
        for (int i = 0; i < numberOfCards; i++)
        {
            imageComponent = _ButtonsList[counterButton].GetComponent<Image>();
            imageComponent.sprite = arrDes2[i];
            counterButton++;
        }
    }

    public void GetButtonIndex(Button buttonInput)
    {
        for(int i = 0; i < _ButtonsList.Count; i++)
        {
            if(_ButtonsList[i] == buttonInput)
            {
                print("index" + i);
            }
        }
    }

}
