using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logic : MonoBehaviour {


    public Image wordsUI;
    public Text optionOne;
    public Text optionThree;
    public Text optionFour;
    public Text optionFive;
    public Button buttonOne;
    public Button buttonTwo;
    public Button buttonThree;
    public Button buttonFour;
    private int flag = 0;
    private int legnth;
    private string correctAnswer;
    private int correctsAnswers = 0;
    public GameObject complete;
    public GameObject winneAndDefeat;
    public GameObject canvas;
    public Image imageWinAndDefeat;
    public Image life;
    public GameObject start1;
    public GameObject start2;
    public GameObject start3;
    public Text texWF;
    public Text score;
    public AudioSource winSource;
    public AudioClip clip; 
    public UIProgressBar bar;



    void Start() {

        winSource.clip = clip;
        if (LoadScene.level.topic == 1) {
            this.CorrectAnswerPlaces();
        }
        if (LoadScene.level.topic == 2) {
            this.CorrectAnswerIngredients();
        }
        if (LoadScene.level.topic == 3) {
            
            this.CorrectAnswerObjects();
        }
        wordsUI = GameObject.Find( "ImageChange").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update () {

    }


    public void Topic() {
        if (LoadScene.level.topic == 1) {
            if (flag == 6) {
                winneAndDefeat.SetActive( false );
                complete.SetActive( true );
                this.CalculateStarts();
                score.text = (correctsAnswers*100).ToString() ;
            } else {
                this.CorrectAnswerPlaces();

            }
        }
        if (LoadScene.level.topic == 2) {
            if (flag == 6) {
                winneAndDefeat.SetActive( false );
                complete.SetActive( true );

                this.CalculateStarts();
                score.text = (correctsAnswers * 100).ToString();
            } else {
                this.CorrectAnswerIngredients();

            }
        }
        if (LoadScene.level.topic == 3) {
            if (flag == 7) {
                winneAndDefeat.SetActive( false );
                complete.SetActive( true );
                this.CalculateStarts();
                score.text = (correctsAnswers * 100).ToString() ;

            } else {
                this.CorrectAnswerObjects();

            }
        }
    }


    public void CorrectAnswerObjects() {

        int rdnFail = Random.Range( 0, 7 );
        int twoOptionR = Random.Range( 0, 7 );
        int threeOptionR = Random.Range( 0, 7 );
        int fourOptionR = Random.Range( 0, 7 );
        string[] objects = new string[ 7 ] {"Dollar", "Engagement-ring", "Fashion", "Pendant","Reading-glasses","Shopping-bag","Watch"};
        legnth = objects.Length;
        Debug.Log( legnth );
        if (flag != twoOptionR && flag != threeOptionR && flag != rdnFail && twoOptionR != threeOptionR && twoOptionR != rdnFail && threeOptionR != rdnFail) {
            this.GenerateAnswers( objects[ flag ], objects[ rdnFail ], objects[ twoOptionR ], objects[ threeOptionR ] );
            wordsUI.sprite = Resources.Load<Sprite>( "SpritesO/" + objects[ flag ] );
            if (flag == 6) {
                complete.SetActive( true );
            }
            flag++;
        }else {
            this.CorrectAnswerObjects();
        }

    }

    public void CorrectAnswerPlaces() {

        int rdnFail = Random.Range( 0, 6 );
        int twoOptionR = Random.Range( 0, 6 );
        int threeOptionR = Random.Range( 0, 6 );
        string[] places = new string[ 6 ] { "Butcher-shop", "Drink", "Fire-station", "Hospital", "Pizzeria", "Stand"};
        legnth = places.Length;
        Debug.Log( legnth );
        if (flag != twoOptionR && flag != threeOptionR &&  flag != rdnFail && twoOptionR != threeOptionR &&  twoOptionR != rdnFail  && threeOptionR != rdnFail ) {
            this.GenerateAnswers( places[ flag ], places[ rdnFail ], places[ twoOptionR ], places[ threeOptionR ]  );
            wordsUI.sprite = Resources.Load<Sprite>( "SpritesP/" + places[ flag ] );
            if (flag == 6) {
                complete.SetActive( true );
            }
            flag++;
        } else {
            this.CorrectAnswerPlaces();
        }

    }

    public void CorrectAnswerIngredients() {

        int rdnFail = Random.Range( 0, 5);
        int twoOptionR = Random.Range( 0, 5 );
        int threeOptionR = Random.Range( 0, 5 );
        int fourOptionR = Random.Range( 0, 5 );
        string[] ingredients = new string[ 6 ] { "Cheese", "Fish", "Mushroom", "Olive", "Salami", "Tomato"};
        legnth = ingredients.Length;
        Debug.Log( legnth );
        if (flag != twoOptionR && flag != threeOptionR && flag != rdnFail && twoOptionR != threeOptionR && twoOptionR != rdnFail && threeOptionR != rdnFail) {
            this.GenerateAnswers( ingredients[ flag ], ingredients[ rdnFail ], ingredients[ twoOptionR ], ingredients[ threeOptionR ] );
            wordsUI.sprite = Resources.Load<Sprite>( "SpritesI/" + ingredients[ flag ] );
            if (flag == 6) {
                complete.SetActive( true );
            }
            flag++;
        } else {
            this.CorrectAnswerIngredients();
        }

    }

    public void GenerateAnswers(string answer, string failOne, string failTwo, string failThree) {

        this.SetCorrectAnswer( answer );

        int rdn = Random.Range( 0, 4 );
        int twoOptionR = Random.Range( 0, 4 );
        int threeOptionR = Random.Range( 0, 4 );
        int fourOptionR = Random.Range( 0, 4 );
        Text[] optionsAnswers = new Text[4] { optionOne, optionFour, optionThree, optionFive };
        Text correctAnswer = optionsAnswers[ rdn ];
        Text twoOption = optionsAnswers[ twoOptionR ];
        Text threeOption = optionsAnswers[ threeOptionR ];
        Text fourOption = optionsAnswers[ fourOptionR ];

        if (rdn != twoOptionR && rdn != threeOptionR && rdn != fourOptionR && twoOptionR != threeOptionR && twoOptionR != fourOptionR   && threeOptionR != fourOptionR ) {
            correctAnswer.text = answer;
            correctAnswer.fontSize = 33;
            threeOption.text = failThree;
            fourOption.text = failOne;
            twoOption.text = failTwo;
            twoOption.fontSize = 33;
            threeOption.fontSize = 33;
            fourOption.fontSize = 33;

        } else {
            this.GenerateAnswers(answer, failOne, failTwo, failThree );
        }

    }

    public void CalculateStarts() {

        if(correctsAnswers == legnth) {
            start1.SetActive( true );
            start2.SetActive( true );
            start3.SetActive( true );
        } else {
            if(correctsAnswers == 0) {
                start1.SetActive( false );
                start2.SetActive( false );
                start3.SetActive( false );
            }else {
                if (correctsAnswers < legnth / 2) {
                    start1.SetActive( true );
                    start2.SetActive( false );
                    start3.SetActive( false );
                } else {
                    if (correctsAnswers >= legnth / 2) {
                        start1.SetActive( true );
                        start2.SetActive( true );
                        start3.SetActive( false );
                    }
                }
            }
        }

    }

    public void WinnerS(int indexButton) {
        if(indexButton == 1) {
            if(this.correctAnswer == optionOne.text) {
                winneAndDefeat.SetActive( true );
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "1f60a" );
                texWF.text = "CONGRATULATIONS";
                correctsAnswers++;
                this.Topic();
            } else {
                winneAndDefeat.SetActive( true );
                texWF.text = "DEFEAT";
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "defeat" );
                texWF.fontSize = 110;
                this.Topic();
            }
        }
        if (indexButton == 3) {
            if (this.correctAnswer == optionThree.text) {
                winneAndDefeat.SetActive( true );
                texWF.text = "CONGRATULATIONS";
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "1f60a" );
                correctsAnswers++;
                this.Topic();
            } else {
                winneAndDefeat.SetActive( true );
                texWF.text = "DEFEAT";
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "defeat" );
                texWF.fontSize = 110;
                this.Topic();
            }
        }
        if (indexButton == 4) {
            if (this.correctAnswer == optionFour.text) {
                winneAndDefeat.SetActive( true );
                texWF.text = "CONGRATULATIONS";
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "1f60a" );
                correctsAnswers++;
                this.Play();
                this.Topic();
                life.fillAmount = 0;
            } else {
                winneAndDefeat.SetActive( true );
                texWF.text = "DEFEAT";
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "defeat" );
                texWF.fontSize = 110;
                bar.fillAmount = 0;
                this.Topic();
            }
        }
        if (indexButton == 5) {
            if (this.correctAnswer == optionFive.text) {

                winneAndDefeat.SetActive( true );
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "1f60a" );
                texWF.text = "CONGRATULATIONS";
                correctsAnswers++;
                this.Play();
                this.Topic();
            } else {
                winneAndDefeat.SetActive( true );
                texWF.text = "DEFEAT";
                imageWinAndDefeat.sprite = Resources.Load<Sprite>( "defeat" );
                texWF.fontSize = 110;
                this.Topic();
            }
        }
    }

    public void CountPng() {

        string[] dirs = Directory.GetFiles(@"D:\Users\Juan Jose Salazar\Documents\Strong Shot\Assets\Images\Gym", "*.PNG" );
        ArrayList answer = new ArrayList();
        int cantidad = dirs.Length;
        Debug.Log(cantidad);
        for (int i = 0; i < dirs.Length; i++) {
            Debug.Log( dirs[i] );
            Debug.Log( dirs[ i ].Substring( 67 ) );
            

        }
    }

    public string GetCorrectAnswer() {
        return correctAnswer;
    }
    public void SetCorrectAnswer(string correctAnswer) {
        this.correctAnswer = correctAnswer;
    }

    public void ActivateComponent() {
        complete.SetActive( true );
    }

    public void AceptWAndD() {
        winneAndDefeat.SetActive( false );
    }
    public void UpdateLife(int currentHealth, int maxHealth) {
        life.fillAmount = ((float)currentHealth / (float)maxHealth);
        Debug.Log(life.fillAmount);
    }

    public void DisableButtons() {
        buttonOne.enabled = false;
        buttonTwo.enabled = false;
        buttonThree.enabled = false;
        buttonFour.enabled = false;
    }
    public void EnableButtons() {
        buttonOne.enabled = true;
        buttonTwo.enabled = true;
        buttonThree.enabled = true;
        buttonFour.enabled = true;
    }

    public void Play() {
        winSource.Play();
    }
}


