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
    public GameObject canvas;
    public List<GridObjectInteractDataBase> playerSkills = new List<GridObjectInteractDataBase> { };
    public Trait[] allTraits;
    public string[] scenes;
    public int currentScene;
    public int currentTownScene = -1;

    public AudioClip townMusic;
    public AudioClip battleMusic;
    public AudioClip specialMusic;
    private AudioSource audioSource;
    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            audioSource = GetComponent<AudioSource> ();
        } else {
            Destroy (gameObject);
        }
    }
    // Start is called before the first frame update
    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        Debug.Log ("Scene loaded: " + scene.name);
        if ((scene.name == "MainMenu") || (scene.name == "EndScene")) {
            //canvas.SetActive (false);
        } else {
            canvas.SetActive (true);
        }
        // Narrative gameplay
        Invoke ("InitThings", 0.1f);
    }

    void InitThings () {
        if (UIManager.instance != null) {
            currentTownScene++;
            UIManager.instance.Init ();
            InkWriter.main.GoToKnot ("town_" + currentTownScene);
            StartCoroutine (FadeInOutMusic (townMusic));
        }
        // Combat gameplay
        if (GridObjectManager.instance != null) {
            GridObjectManager.instance.objectDieEvent.AddListener (CombatActorDie);
            StartCoroutine (FadeInOutMusic (battleMusic));
        }
        // Autosave!
        SaveGame ();
    }

    void CombatActorDie (GridObject character) {
        Debug.Log ("GameManager track dead: " + character);
        Debug.Log ("Friendly alive: " + GridObjectManager.instance.LivingEntities (GridObjectType.PLAYABLE));
        Debug.Log ("Enemy alive: " + GridObjectManager.instance.LivingEntities (GridObjectType.ENEMY));

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

    IEnumerator FadeInOutMusic (AudioClip newClip) {
        while (audioSource.volume > 0f) {
            audioSource.volume -= Time.deltaTime * 2f;
            yield return null;
        }
        audioSource.clip = newClip;
        audioSource.Play ();
        while (audioSource.volume < 1f) {
            audioSource.volume += Time.deltaTime * 2f;
            yield return null;
        }
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
        Reset ();
        LoadNextScene ();
    }

    public void LoadScene (string name) {
        SceneManager.LoadScene (name);
    }
    public void LoadScene (int scene) {
        LoadScene (scenes[scene]);
    }
    public void LoadNextScene () {
        currentScene++;
        LoadScene (scenes[currentScene]);
    }

    public void SaveGame () {
        if (InkWriter.main != null) {
            InkWriter.main.SaveStory ();
        };
        ES3.Save<GameManager> ("LD46_BloodOnTheSnow", this);
        //ES3.Save<int> ("LD46_BloodOnTheSnow_CurrentLevel", currentScene);
        //ES3.Save<int> ("LD46_BloodOnTheSnow_CurrentTownLevel", currentTownScene);
        //ES3.Save<Trait[]> ("LD46_BloodOnTheSnow_Skills", allTraits);
    }

    public void LoadGame () {
        if (InkWriter.main != null) {
            InkWriter.main.LoadStory ();
        };
        ES3.LoadInto<GameManager> ("LD46_BloodOnTheSnow", this);
        //currentScene = ES3.LoadInto<T> ("LD46_BloodOnTheSnow_CurrentLevel", this.currentScene);
        //currentTownScene = ES3.LoadInto<T> ("LD46_BloodOnTheSnow_CurrentTownLevel", this.currentTownScene);
        //allTraits = ES3.LoadInto<Trait[]> ("LD46_BloodOnTheSnow_Skills", this.allTraits);
        LoadScene (currentScene);
    }

    [EasyButtons.Button]
    public void Reset () {
        currentScene = 0;
        currentTownScene = -1;
        ES3.DeleteKey ("LD46_BloodOnTheSnow_CurrentLevel");
        ES3.DeleteKey ("LD46_BloodOnTheSnow_CurrentTownLevel");
        ES3.DeleteKey ("LD46_BloodOnTheSnow_Skills");
        ES3.DeleteKey ("ldjam46" + "savedInkStory");
    }

    public void RestartScene () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    public void QuitGame () {
        Application.Quit ();
    }

    // Update is called once per frame
    void Update () {

    }
}