using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEnemyAIController : MonoBehaviour {
    public static GridEnemyAIController instance;
    // Start is called before the first frame update
    private Coroutine enemyAI;
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (this);
        }
    }
    void Start () {

    }

    public void StartEnemyTurn () {
        Debug.Log ("Starting enemy AI");
        enemyAI = StartCoroutine (EnemyAICoroutine ());
    }

    IEnumerator EnemyAICoroutine () {
        yield return new WaitForSeconds (1f);
        EndEnemyTurn ();
    }

    public void EndEnemyTurn () {
        Debug.Log ("Ending enemy turn");
        GridUIManager.instance.StartTurn();
    }

    // Update is called once per frame
    void Update () {

    }

}