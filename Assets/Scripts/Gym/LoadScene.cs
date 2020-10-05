using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    public static LoadScene level;
    public int topic;
    public GameObject winneAndDefeat;

    // Use this for initializationW
    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }

    private void Awake() {
        if (level == null) {
            level = this;
            DontDestroyOnLoad( gameObject );
        } else if (level != this) {
            Destroy( gameObject );
        }
    }

    public void NewGame(int selecte) {
        if (selecte == 1) {
            this.SetTopic( 1);
            SceneManager.LoadScene( "Main Gym2", LoadSceneMode.Single );
        }
        if (selecte == 2) {
            this.SetTopic( 2 );
            SceneManager.LoadScene( "Main Gym2", LoadSceneMode.Single );
        }
        if (selecte == 3) {
            this.SetTopic( 3 );
            SceneManager.LoadScene( "Main Gym2", LoadSceneMode.Single );
        } 
    }

    public void SelectedTopic() {
        SceneManager.LoadScene( "Topics Gym", LoadSceneMode.Single );
    }

    public void Exit() {
        Application.Quit();
    }

    public int GetTopic() {
        return topic;
    }
    public void SetTopic(int topic) {
        this.topic = topic;
    }
    public void AceptWAndD() {
        winneAndDefeat.SetActive( true );
    }
    public void DisAceptWAndD() {
        winneAndDefeat.SetActive( false );
    }
}
