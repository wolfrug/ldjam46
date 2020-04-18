using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
    public GameObject startGame;
    public GameObject continueGame;

    void Start () {
        if (ES3.KeyExists ("LD46_BloodOnTheSnow_CurrentLevel")) {
            continueGame.SetActive (true);
            startGame.SetActive (true);
            startGame.GetComponentInChildren<TMPro.TextMeshProUGUI> ().text = "Start New Game";
        } else {
            continueGame.SetActive (false);
        }
    }
    public void StartNewGame () {
        GameManager.instance.StartNewGame ();
    }
    public void LoadGame () {
        GameManager.instance.LoadGame ();
    }
    public void QuitGame () {
        Application.Quit ();
    }
}