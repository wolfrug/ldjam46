using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericWorldSpaceToCanvasIcon : MonoBehaviour {
    public GameObject target;
    public RectTransform canvasObject;

    public bool scaleByDistanceFromCamera = true;
    [SerializeField]
    private float distanceForScaleOne = 200f;

    [SerializeField]
    private Camera targetCamera;
    private Vector3 pos;

    void Awake () {
        if (target == null) {
            target = gameObject;
        }
        if (targetCamera == null) {
            targetCamera = Camera.main;
        };
    }

    // Update is called once per frame
    void Update () {
        if (canvasObject != null) {
            pos = RectTransformUtility.WorldToScreenPoint (targetCamera, target.transform.position);
            canvasObject.position = pos;
            if (scaleByDistanceFromCamera) {
                float distance = Vector3.Distance (targetCamera.transform.position, target.transform.position);
                float newscale = Mathf.Clamp (distanceForScaleOne / distance, 0.2f, 1f);
                // Debug.Log ("Distance between camera and " + gameObject.name + " :" + distance.ToString ());
                canvasObject.localScale = new Vector3 (newscale, newscale, newscale);
            }
        };
    }
}