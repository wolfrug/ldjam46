using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class LoadingFinished : UnityEvent { }
public class GenericLoadingBar : MonoBehaviour {
    public TextMeshProUGUI loadingBarText;
    public Image loadingBar;
    public bool deactivateSelfOnFinish = true;
    public float currentState = 0f;
    private int max = 1;
    private int current = 0;
    public string format = "{0} / {1}";

    public bool active = false;

    public LoadingFinished onLoadFinished;
    // Start is called before the first frame update
    void Start () {

    }

    public void SetupBar (int start, int endGoal) {
        max = endGoal;
        current = start;
        loadingBarText.text = string.Format (format, start, endGoal);
        active = true;
    }
    public void PauseBar () {
        active = false;
    }

    public void SetState (int newValue) {
        if (newValue > 0) {
            current = newValue;
            currentState = (float) current / (float) max;
            if (current >= max) {
                onLoadFinished.Invoke ();
                active = false;
                if (deactivateSelfOnFinish) {
                    gameObject.SetActive (false);
                }
            }
        };
    }

    // Update is called once per frame
    void Update () {
        if (active) {
            loadingBar.fillAmount = currentState;
            loadingBarText.text = string.Format (format, current, max);
        }
    }
}