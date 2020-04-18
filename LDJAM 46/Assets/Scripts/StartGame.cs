using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
    public void StartNewGame () {
        GameManager.instance.StartNewGame ();
    }
    public void QuitGame () {
        Application.Quit ();
    }
}