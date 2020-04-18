using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridUIManager : MonoBehaviour {
    public static GridUIManager instance;
    public Canvas canvas;

    public GridObject selectedCharacter;
    public Image selectedCharacterPortrait;
    public TextMeshProUGUI selectedCharacterName;
    public TextMeshProUGUI selectedCharacterHealth;
    public TextMeshProUGUI selectedCharacterAP;
    public Button endTurnButton;
    public TextMeshProUGUI turnNumberText;
    public TextMeshProUGUI scenarioName;
    public bool playerTurn = true;
    public int turnNumber = 0;

    public Dictionary<GridObject, GridObjectIcon> iconDictionary = new Dictionary<GridObject, GridObjectIcon> { };
    public Dictionary<GridObject, List<GridActionCard>> actionDictionary = new Dictionary<GridObject, List<GridActionCard>> { };

    public List<GameObject> spawnedActions = new List<GameObject> { };
    public Transform actionListParent;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }
    }

    void Start () {
        endTurnButton.onClick.AddListener (() => EndTurn ());
        if (playerTurn) { // Slightly late start to let things settle
            Invoke ("StartTurn", 0.1f);
        }
    }

    public void EndTurn () {
        if (playerTurn) {
            endTurnButton.interactable = false;
            playerTurn = false;
            ClearActions ();
            // HOLY SHIT BATMAN DO ALL THE AI NOW????????
            GridEnemyAIController.instance.StartEnemyTurn ();
        }
    }

    public void StartTurn () {
        turnNumber++;
        turnNumberText.text = turnNumber.ToString ();
        playerTurn = true;
        endTurnButton.interactable = true;
        // refresh actions
        GridObjectManager.instance.RefreshAllObjects ();
        RefreshActions ();
        // Select first playable object
        GridObjectManager.instance.allPlayableObjects[0].SelectObject (true);
    }

    public void AddGridObjectIcon (GridObject obj, GridObjectIcon icon) {
        if (!iconDictionary.ContainsKey (obj)) {
            iconDictionary.Add (obj, icon);
            icon.selectButton.onClick.AddListener (() => obj.SelectObject (true));
        }
    }
    void ClearActions () {
        foreach (GameObject obj in spawnedActions) {
            obj.SetActive (false);
        }
    }
    void RefreshActions () { // naively assume all cards are refreshed...

        foreach (KeyValuePair<GridObject, List<GridActionCard>> entry in actionDictionary) {
            // do something with entry.Value or entry.Key
            foreach (GridActionCard card in entry.Value) {
                card.ActivateAction (true);
            }
        }
    }

    public void SetupActions (GridObject character) {
        ClearActions ();
        if (!actionDictionary.ContainsKey (character)) { // create a list of all actions and associate them in the dictionary
            List<GridActionCard> actions = new List<GridActionCard> { };
            foreach (GridObjectInteractDataBase data in character.action.allActionsComplete) {
                GameObject newAction = Instantiate (data.cardPrefab, actionListParent);
                spawnedActions.Add (newAction);
                GridActionCard card = newAction.GetComponent<GridActionCard> ();
                card.UpdateActionCard (data.actionName, data.actionCost.ToString (), data.actionDescription, data.actionIcon);
                card.useButton.onClick.AddListener (() => card.ActivateAction (false));
                actions.Add (card);
                newAction.SetActive (false);
                // Set up the listener to the action also!
                card.useButton.onClick.AddListener (() => character.DoAction (data));
                // give it the data so it knows what it is
                card.data = data;
            }
            actionDictionary.Add (character, actions);
        }
        // Let's only show the active ones tho!
        foreach (GridObjectInteractDataBase action in character.action.activeActions) {
            foreach (GridActionCard card in actionDictionary[character]) {
                if (card.data == action) {
                    card.gameObject.SetActive (true);
                }
            }
        }

    }

    public void SelectCharacter (GridObject character) {
        selectedCharacterPortrait.sprite = character.data.portraitImage;
        selectedCharacterName.text = character.data.name_;
        selectedCharacterHealth.text = character.health.ToString () + "/" + character.data.health.ToString ();
        selectedCharacterAP.text = character.actionpoints.ToString () + "/" + character.data.actionpoints.ToString ();
        if (!character.dead && character.data.type_ == GridObjectType.PLAYABLE) {
            SetupActions (character);
        } else {
            ClearActions ();
        }
        selectedCharacter = character;
    }
    public void UpdateCharacter (GridObject character) {
        if (selectedCharacter == character) {
            selectedCharacterPortrait.sprite = character.data.portraitImage;
            selectedCharacterName.text = character.data.name_;
            selectedCharacterHealth.text = character.health.ToString () + "/" + character.data.health.ToString ();
            selectedCharacterAP.text = character.actionpoints.ToString () + "/" + character.data.actionpoints.ToString ();
        }
    }
}