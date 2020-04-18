using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public Canvas canvas;

    public GameObject[] allScenes;
    public InkCharacterObject[] characters;
    public Transform portraitParent;
    public Transform TraitAddingParent;
    public List<GridActionCard> traitButtons = new List<GridActionCard> { };
    public bool pickingTraits = false;
    public GenericLoadingBar loadingBar;
    [SerializeField]
    private UIResourceObject[] allResourceObjects;
    private Dictionary<string, ProgressBar> progressBars = new Dictionary<string, ProgressBar> { };
    private Dictionary<string, InkCharacterObject> charactersDict = new Dictionary<string, InkCharacterObject> { };
    private Dictionary<InkCharacterObject, GameObject> spawnedPortraits = new Dictionary<InkCharacterObject, GameObject> { };
    public GenericMouseOver[] allSelectables;
    public Material mouseOverSelectedMat;
    public Material mouseOverDeselectedMat;
    public bool canSelectNext = true;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (this);
        }
    }

    // Start is called before the first frame update
    void Start () {
        foreach (InkCharacterObject ico in characters) {
            charactersDict.Add (ico.characterName, ico);
        }
    }

    public void EventListenerCanSelect (string tag, int valuechange) {
        Debug.Log ("Var changed: " + tag + valuechange);
        canSelectNext = valuechange > 0;
        UIManager.instance.SelectableDeselectAll ();
    }

    public void EventListenerTraitChanged (string tag, string newvalue) {
        Debug.Log ("Var changed: " + tag + newvalue);
        GameManager.instance.AddTraitChoice (newvalue);
    }

    public void Init () {
        foreach (UIResourceObject obj in allResourceObjects) {
            obj.Init ();
        }
        allResourceObjects = FindObjectsOfType<UIResourceObject> ();
        // Start the correct scene!
        GameObject currentScene = allScenes[GameManager.instance.currentTownScene];
        foreach (GameObject obj in allScenes) { obj.SetActive (false); };
        currentScene.SetActive (true);
        SelectableSetup ();
        InkWriter.main.StartStory ();
        InkWriter.main.story.ObserveVariable ("canSelectNext", (string varName, object newValue) => {
            UIManager.instance.EventListenerCanSelect (varName, (int) newValue);
        });
    }

    public void ContinueToNextScene (string tag) {
        Debug.Log ("Tag received: " + tag);
        if (tag == "continueStory") {
            LoadNextScene ();
        }
    }

    public void LoadNextScene () {
        GameManager.instance.LoadNextScene ();
    }

    public void LoadScene (string scene) {
        GameManager.instance.LoadScene (scene);
    }

    public void AddTraitChoice (GridObjectInteractDataBase data) {

        if ((int) InkWriter.main.story.variablesState[(data.id)] == 0) {
            GameObject newAction = Instantiate (data.cardPrefab, TraitAddingParent);
            GridActionCard card = newAction.GetComponent<GridActionCard> ();
            traitButtons.Add (card);
            card.UpdateActionCard (data.actionName, data.actionCost.ToString (), data.actionDescription, data.actionIcon);
            // actions.Add (card);
            newAction.SetActive (true);
            // Set up the listener to the action also!
            //card.useButton.onClick.RemoveAllListeners ();
            card.useButton.onClick.AddListener (() => PickTrait (data));
            // give it the data so it knows what it is
            card.data = data;
        };
    }

    void PickTrait (GridObjectInteractDataBase data) { // You picked this trait!
        GameManager.instance.AddTrait (data);
        foreach (GridActionCard trait in traitButtons) {
            if (trait.data != data) {
                trait.ActivateAction (false);
                InkWriter.main.story.variablesState[(trait.data.id)] = -1;
                Destroy (trait.gameObject, 0.5f);
            } else {
                Destroy (trait.gameObject, 1f);
            }
        }
        traitButtons.Clear ();
        string knot = (string) InkWriter.main.story.variablesState["traitDoneKnot"];
        if (knot != "") {
            InkWriter.main.GoToKnot (knot);
            InkWriter.main.story.variablesState["traitDoneKnot"] = "";
        } else {
            Debug.LogWarning ("Someone forgot to set the 'traitDoneKNot' variable, oops.");
            InkWriter.main.story.variablesState[("canSelectNext")] = 1;
        }

    }

    void SelectableSetup () {
        allSelectables = FindObjectsOfType<GenericMouseOver> ();
        foreach (GenericMouseOver selectable in allSelectables) {
            selectable.mouseOverEvent.AddListener (SelectableSetMaterialSelect);
            selectable.mouseExitEvent.AddListener (SelectableSetMaterialDeSelect);
            selectable.mouseLeftClickEvent.AddListener (SelectableClick);
        }
    }
    void SelectableSetMaterialSelect (GenericMouseOver obj) {
        //Debug.Log ("Mouse over: " + obj.id, obj.gameObject);
        if (canSelectNext) {
            Renderer rend = obj.gameObject.GetComponent<MeshRenderer> ();
            rend.material = mouseOverSelectedMat;
        };
    }
    void SelectableSetMaterialDeSelect (GenericMouseOver obj) {
        if (canSelectNext) {
            Renderer rend = obj.gameObject.GetComponent<MeshRenderer> ();
            rend.material = mouseOverDeselectedMat;
        };
    }
    void SelectableDeselectAll () {
        foreach (GenericMouseOver obj in allSelectables) {
            SelectableSetMaterialDeSelect (obj);
        }
    }
    void SelectableClick (GenericMouseOver obj) {
        Debug.Log ("Mouse clicked: " + obj.id, obj.gameObject);
        if (canSelectNext) {
            //canSelectNext = false;
            InkWriter.main.story.variablesState[("canSelectNext")] = 0;
            InkWriter.main.GoToKnot ("start_" + obj.id);
        }
    }

    public void AddProgressBar (ProgressBar bar) {
        if (!progressBars.ContainsValue (bar)) {
            progressBars.Add (bar.barName, bar);
            bar.SetValueToInkValue ();
        }
    }

    public void UpdateProgressBar (string tag, int value) {
        ProgressBar outBar;
        progressBars.TryGetValue (tag, out outBar);
        if (outBar != null) {
            outBar.SetValue (value);
        }
    }

    public void ChangePortrait (object[] inputVariables) {
        // assume there is only one object in the list
        Debug.Log (inputVariables[0]);
        string character = inputVariables[0].ToString ();
        InkCharacterObject outVar;
        charactersDict.TryGetValue (character, out outVar);
        Debug.Log (outVar?.characterName);
        // Is the portrait already spawned?
        GameObject tryGetObject = null;
        spawnedPortraits.TryGetValue (outVar, out tryGetObject);
        // If spawned, make it visible and make all others invisible
        if (tryGetObject != null) {
            DeactivatePortraits ();
            tryGetObject.SetActive (true);

        } else { // otherwise, spawn it - and make everything else invisible.
            DeactivatePortraits ();
            GameObject newPortrait = Instantiate (outVar.portraitPrefab, portraitParent);
            spawnedPortraits.Add (outVar, newPortrait);
        }

    }

    void DeactivatePortraits () {
        foreach (KeyValuePair<InkCharacterObject, GameObject> entry in spawnedPortraits) {
            entry.Value.SetActive (false);
        }
    }

    /*    public void PauseGame (bool pause) {
            GameManager.instance.PauseGame (pause);
        }

        public void SaveGame () {
            GameManager.instance.SaveGame ();
        }
        public void LoadGame () {
            GameManager.instance.LoadGame ();
        }

        public void Restart () {
            GameManager.instance.Restart ();
        }

        [EasyButtons.Button]
        public void ResetGame () {
            GameManager.instance.ResetGame ();
        }

        public void QuitGame () {
            GameManager.instance.QuitGame ();
        }

        public void WinGame () {
            GameManager.instance.WinGame ();
        }
    */
    // Update is called once per frame
    void Update () {

    }
}