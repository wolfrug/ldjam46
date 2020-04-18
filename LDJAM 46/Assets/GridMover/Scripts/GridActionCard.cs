using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridActionCard : MonoBehaviour {

    public GridObjectInteractDataBase data;
    public Button useButton;
    public TextMeshProUGUI actionNameText;
    public TextMeshProUGUI actionAPCostText;
    public TextMeshProUGUI actionDescText;
    public Image actionImage;
    public Animator animator;
    public bool actionUsed = false;

    // Start is called before the first frame update
    void Start () {
        //useButton.onClick.AddListener (() => ActivateAction (false));
    }
    void OnEnable () {
        animator.SetBool ("Active", !actionUsed);
    }

    public void UpdateActionCard (string name, string apCost, string desc, Sprite image) {
        actionNameText.text = name;
        actionAPCostText.text = "AP: " + apCost;
        actionDescText.text = desc;
        actionImage.sprite = image;
    }

    public void ActivateAction (bool activate) {
        animator.SetBool ("Active", activate);
        actionUsed = !activate;
    }

    // Update is called once per frame
    void Update () {

    }
}