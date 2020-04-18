using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Trait {
    public string id;
    public GridObjectInteractDataBase data;
    public bool acquired;
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public List<GridObjectInteractDataBase> playerSkills = new List<GridObjectInteractDataBase> { };
    public Trait[] allTraits;
    public string[] scenes;
    public int currentScene;
    public int currentTownScene = -1;
    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else {
            Destroy (this);
        }
    }
    // Start is called before the first frame update
    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        Debug.Log ("Scene loaded: " + scene.name);
        // Narrative gameplay
        Invoke ("InitThings", 0.1f);
    }

    void InitThings () {
        if (UIManager.instance != null) {
            currentTownScene++;
            UIManager.instance.Init ();
        }
        // Combat gameplay
        if (GridObjectManager.instance != null) {
            GridObjectManager.instance.objectDieEvent.AddListener (CombatActorDie);
        }
    }

    void CombatActorDie (GridObject character) {
        if (GridObjectManager.instance.LivingEntities (GridObjectType.ENEMY) == 0) {
            CombatAllEnemiesDead ();
        } else if (GridObjectManager.instance.LivingEntities (GridObjectType.PLAYABLE) == 0) {
            CombatAllPlayersDead ();
        }
    }
    void CombatAllPlayersDead () {
        Debug.Log ("Oh no we lose");
        RestartScene ();
    }
    void CombatAllEnemiesDead () {
        Debug.Log ("We win yay!");
        LoadNextScene ();
    }

    public void AddTraitChoice (string id) {
        foreach (Trait trait in allTraits) {
            if (trait.id == id && !trait.acquired) {
                UIManager.instance.AddTraitChoice (trait.data);
            }
        }
    }
    public void AddTrait (GridObjectInteractDataBase data) {
        foreach (Trait trait in allTraits) {
            if (trait.data == data) {
                trait.acquired = true;
                InkWriter.main.story.variablesState[data.id] = 1;
            }
        }
    }

    public void StartNewGame () {
        currentScene = 0;
        currentTownScene = -1;
        LoadNextScene ();
    }

    public void LoadScene (string name) {
        SceneManager.LoadScene (name);
    }
    public void LoadScene (Scene scene) {
        LoadScene (scene.name);
    }
    public void LoadNextScene () {
        currentScene++;
        LoadScene (scenes[currentScene]);
    }

    public void SaveGame () {
        InkWriter.main.SaveStory ();
    }

    public void RestartScene () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    // Update is called once per frame
    void Update () {

    }
}