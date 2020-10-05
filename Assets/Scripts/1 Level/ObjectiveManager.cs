using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour {

    //public List<GameObject> objectiveList;
    public Sprite checkedObjective;
    public Sprite newObjective;
    public Sprite emptySprite;

    public GameObject notificationButtonBagPack;
    public GameObject notificationButtonMenu;

    public GameObject notificationTextMenu;
    private TextMeshProUGUI notificationTextMenuTextMesh;
    private Animator notificationTextMenuAnimator;
    //public TextMeshProUGUI notificationTextBagPack;

    public GameObject objectivesQurrentPanel;
    public GameObject objectivesNewPanel;
    public GameObject objectivesCheckedPanel;

    public GameObject objective;

    List<GameObject> collectablesList;

    private static ObjectiveManager inGameObjectiveManager;
    private static bool seen = false;

    SpritesAndText spritesAndText;


    AnimatedClueController actualClueController;

    int length = 0;
    #region Singleton
    public static ObjectiveManager instance;
    private SpritesAndText SpritesAndText;

    private void Awake()
    {
        instance = this;
        notificationTextMenuTextMesh = notificationTextMenu.GetComponent<TextMeshProUGUI>();
        notificationTextMenuAnimator = notificationTextMenu.GetComponent<Animator>();
        actualClueController = AnimatedClueController.Instance();
    }
    #endregion

    private void Update()
    {
        if (objectivesQurrentPanel.activeInHierarchy)
        {
            DeactivateNotifications();
        }
    }

    public static ObjectiveManager Instance()
    {
        if (!inGameObjectiveManager)
        {
            inGameObjectiveManager = FindObjectOfType(typeof(ObjectiveManager)) as ObjectiveManager;
            if (!inGameObjectiveManager)
                Debug.LogError("There needs to be one active ObjectiveManager script on a GameObject in your scene.");
        }

        return inGameObjectiveManager;
    }

    public void AddObjectives()
    {
        for (int i = 0; i < length; i++)
        {
            AddObjective(i);
        }

        ActiveFirtsObjective();

        ActivateNotifications();

        UpdateNotifications();
    }

    void AddObjective(int i)
    {
        //Debug.Log("adding objetive " + i);
        spritesAndText = collectablesList[i].GetComponent<SpritesAndText>();
        GameObject a = Instantiate(objective);
        ActualObjectiveProperties actualObjectiveProperties = a.GetComponent<ActualObjectiveProperties>();

        actualObjectiveProperties.SetPropieties(spritesAndText.GetClueSprite(), spritesAndText.GetPlaceSprite(), spritesAndText.GetStatusSprite(), spritesAndText.GetClueText(), spritesAndText.GetObjectiveText());
        actualClueController.SetPropieties( spritesAndText.GetClueSprite(), spritesAndText.GetPlaceSprite(), spritesAndText.GetClueText());

        actualObjectiveProperties.UpdateProperties();
        actualClueController.UpdateProperties();
        actualObjectiveProperties.HidePanel();
        
        a.transform.SetParent( objectivesNewPanel.transform, false );

    }

    public void ClearObjectives()
    {
        foreach (Transform child in objectivesQurrentPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in objectivesNewPanel.transform) {
            GameObject.Destroy( child.gameObject );
        }

        foreach (Transform child in objectivesCheckedPanel.transform) {
            GameObject.Destroy( child.gameObject );
        }

    }

    public void ActiveFirtsObjective (){

        ActualObjectiveProperties actualObjectiveProperties;

        foreach (Transform child in objectivesNewPanel.transform) {

            actualObjectiveProperties = child.GetComponent<ActualObjectiveProperties>();
            actualObjectiveProperties.ShowPanel();

            child.transform.SetParent( objectivesQurrentPanel.transform, false );

            actualClueController.SetPropieties( actualObjectiveProperties.GetClueSprite(), actualObjectiveProperties.GetPlaceSprite(), actualObjectiveProperties.GetClueText() );
            actualClueController.UpdateProperties();

            break;
        }
    }

    public void CheckFirstObjective()
    {

        ActualObjectiveProperties actualObjectiveProperties;


        foreach (Transform child in objectivesQurrentPanel.transform)
        {
            
            child.GetComponent<ActualObjectiveProperties>().SetStatusSprite(checkedObjective);
            child.SetAsLastSibling();
            child.transform.SetParent( objectivesCheckedPanel.transform, false );
            break;
        }

        foreach (Transform child in objectivesQurrentPanel.transform)
        {
            
            actualObjectiveProperties = child.GetComponent<ActualObjectiveProperties>();
            actualObjectiveProperties.ShowPanel();
            child.transform.SetParent( objectivesCheckedPanel.transform, false );
            break;
        }
        AnimatedCongratulationController.instance.Animate();
        ActiveFirtsObjective();

    }

    public void SetCollectables(List<GameObject> collectablesInput)
    {
        //Debug.Log("Seting objetives");
        collectablesList = collectablesInput;
        length = collectablesInput.Count;
        //Debug.Log(length);
    }

    void UpdateNotifications()
    {
        if (seen)
        {
            DeactivateNotifications();
        }
        else
        {
            ActivateNotifications();
        }
    }

    void DeactivateNotifications()
    {
        int counter = 0;
        foreach (Transform child in objectivesNewPanel.transform) {
            counter++;
            // do whatever you want with child transform object here
        }
        notificationTextMenuTextMesh.SetText( counter.ToString() );
        notificationTextMenuAnimator.SetBool( "Pulsing", false );
    }

    void ActivateNotifications()
    {
        int counter = 1;
        foreach (Transform child in objectivesNewPanel.transform) {
            counter++;
            // do whatever you want with child transform object here
        }
        notificationTextMenuTextMesh.SetText(counter.ToString());
        notificationTextMenuAnimator.SetBool("Pulsing", true);
        seen = false;
    }

    public void CheckObjective()
    {
        CheckFirstObjective();
        ActivateNotifications();
    }

}
