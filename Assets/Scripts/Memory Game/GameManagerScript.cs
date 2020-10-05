using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	/*public Sprite[] cardFronts;
	public Sprite cardBack;
    public TextMeshProUGUI matchText;
    public GameObject cardListPanel;
    public GameObject cardPrefab;

    List<GameObject> cardList;
    int cardListSize;
    
    
	private bool _init = false;
	private int _matches;*/

    #region Singleton
    public Sprite[] cardFronts;
    public Sprite cardBack;
    public TextMeshProUGUI matchText;
    public GameObject cardListPanel;
    public GameObject cardPrefab;

    List<GameObject> cardList;
    int cardListSize;
    
    private bool _init = false;
    int _matches = 0;

    public static GameManagerScript instance;
    private static GameManagerScript gameManagerScript;

    private void Awake() {
        instance = this;
    }

    #endregion
    // Use this for initialization

    public static GameManagerScript Instance() {
        if (!gameManagerScript) {
            gameManagerScript = FindObjectOfType( typeof( GameManagerScript ) ) as GameManagerScript;
            if (!gameManagerScript)
                Debug.LogError( "There needs to be one active GetPlayerPosition script on a GameObject in your scene." );
        }
        return gameManagerScript;
    }

    private void Start() {
        _matches = cardFronts.Length;
        cardList = new List<GameObject>();
        cardListSize = cardFronts.Length;
        Debug.Log( cardListSize );
        AddCards();
    }

    void Update () {
		if (Input.GetMouseButtonUp (0)) {
			CheckCards ();
		}
	}

    public void AddCards() {
        gameManagerScript = GameManagerScript.Instance();
        Debug.Log( cardListSize );
        for (int i = 0; i < cardListSize*2; i++) {
            GameObject cardClone = (GameObject)Instantiate( cardPrefab );
            cardClone.transform.parent = cardListPanel.transform;
            gameManagerScript.AddToCardList( cardClone );
            Debug.Log( cardPrefab );
        }
    }

    public void InitializeCards(){
        Debug.Log( cardList.Count ); 

        for (int i = 0; i < cardListSize; i++) {
            Debug.Log( cardList[ i ] );
        }

        for (int k = 0; k < cardListSize; k++) { 
			bool test = false;
			int selectedId = 0;
            while(!test){
				selectedId = Random.Range (1, cardListSize * 2);
                Debug.Log( selectedId );
                Debug.Log( cardListSize );
                //Debug.Log( cardList[ 0 ] );
                test = !(cardList [selectedId].GetComponent<CardScript> ().initialized);
			}
			cardList [selectedId].GetComponent<CardScript> ().cardValue = k;
			cardList [selectedId].GetComponent<CardScript> ().initialized = true;
		}

		foreach (GameObject c in cardList) {
			c.GetComponent<CardScript> ().setupGame ();
		}

        _init = true;

	}

    public void AddToCardList(GameObject gameObject) {
        cardList.Add( gameObject );
    }

	public Sprite getCardBack(){
		return cardBack;
	}

	public Sprite getCardFront(int value){
		return cardFronts [value];
	}

	void CheckCards(){
		List<int> c = new List<int>{};
		for (int i = 0; i < cardList.Count; i++) {
			if (cardList [i].GetComponent<CardScript> ().state == 1) {
				c.Add (i);
			}
			if (c.Count == 2) {
				CardComparison (c);
			}
		}
	}

    public void SetCardList(List<GameObject> list) {
        cardList = list;
    }


    void CardComparison(List<int> c){
		CardScript.doNotFlip = true;
		int x = 0;

		if(cardList[c[0]].GetComponent<CardScript>().cardValue == cardList[c[1]].GetComponent<CardScript>().cardValue){
			x = 2;
			_matches = _matches - 1;
			matchText.text = "Matches left: " + _matches;

			if(_matches == 0){
				matchText.text = "You win";
			}
		}
		for (int i = 0; i < c.Count; i++) {
			cardList [c [i]].GetComponent<CardScript> ().state = x;
			cardList [c [i]].GetComponent<CardScript> ().userCheck();
		}
		c.Remove (0);
		c.Remove (1);
	}

    public int getCardListSize() {
        return cardListSize;
    }
}
