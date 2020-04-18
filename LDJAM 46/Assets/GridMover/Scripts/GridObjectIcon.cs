using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridObjectIcon : MonoBehaviour {
    public TextMeshProUGUI nameText;
    public Image healthBar;

    public float healthBarFill = 1f;

    // Start is called before the first frame update
    void Start () {

    }

    public void SetName (string name) {
        nameText.text = name;
    }

    // Update is called once per frame
    void Update () {

        if (healthBar.fillAmount != healthBarFill) {
            if (healthBarFill > healthBar.fillAmount) {
                healthBar.fillAmount += Time.deltaTime;
            } else {
                healthBar.fillAmount -= Time.deltaTime;
            }
            if (healthBar.fillAmount - healthBarFill == float.Epsilon) {
                healthBar.fillAmount = healthBarFill;
            }
        }
    }
}