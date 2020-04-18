using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour {
    public static CameraController instance;

    public CinemachineVirtualCamera zoomedInCamera;
    public CinemachineVirtualCamera travelCamera;
    public CinemachineVirtualCamera interactCamera;
    public CinemachineVirtualCamera timerCamera;
    public CinemachineVirtualCamera temporaryInteractCamera;
    public CinemachineVirtualCamera currentCamera;
    private PostProcessVolume postProcessVolume;
    private float lerpStartValue = 0f;
    private float lerpValue = 0f;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }
    }
    // Start is called before the first frame update
    void Start () {
        postProcessVolume = GetComponent<PostProcessVolume> ();
        lerpStartValue = DistanceToPlayer ();
    }

    public void TravelCameraUse () {
        currentCamera = travelCamera;
        travelCamera.Priority = 10;
        zoomedInCamera.Priority = 9;
        interactCamera.Priority = 8;
        timerCamera.Priority = 7;
        if (temporaryInteractCamera != null) {
            temporaryInteractCamera.Priority = 1;
            temporaryInteractCamera = null;
        }
    }
    public void ZoomedInCameraUse () {
        currentCamera = zoomedInCamera;
        travelCamera.Priority = 9;
        zoomedInCamera.Priority = 10;
        interactCamera.Priority = 8;
        timerCamera.Priority = 7;
        if (temporaryInteractCamera != null) {
            temporaryInteractCamera.Priority = 1;
            temporaryInteractCamera = null;
        }
    }

    public void InteractCameraUse () {
        if (temporaryInteractCamera != null) {
            currentCamera = temporaryInteractCamera;
            temporaryInteractCamera.Priority = 11;
        } else {
            currentCamera = interactCamera;
        };
        travelCamera.Priority = 8;
        zoomedInCamera.Priority = 9;
        interactCamera.Priority = 10;
        timerCamera.Priority = 7;
    }
    public void TimerCameraUse () {
        currentCamera = timerCamera;
        travelCamera.Priority = 7;
        zoomedInCamera.Priority = 8;
        interactCamera.Priority = 9;
        timerCamera.Priority = 10;
        if (temporaryInteractCamera != null) {
            temporaryInteractCamera.Priority = 1;
            temporaryInteractCamera = null;
        }
    }

    public void SetTemporaryInteractCamera (CinemachineVirtualCamera newCamera) {
        temporaryInteractCamera = newCamera;
    }

    [EasyButtons.Button]
    private float DistanceToPlayer () {
        //Debug.Log (Vector3.Distance (transform.position, GameManager.instance.playerControllerVar.transform.position));
        //return Vector3.Distance (transform.position, GameManager.instance.playerController.transform.position);
        return DistanceToCameraTarget ();
    }
    private float DistanceToCameraTarget () {
        if (currentCamera.m_LookAt != null) {
            return Vector3.Distance (transform.position, currentCamera.m_LookAt.position);
        } else if (currentCamera.m_Follow != null) {
            return Vector3.Distance (transform.position, currentCamera.m_Follow.position);
        } else {
            return DistanceToPlayer ();
        }
    }

    [EasyButtons.Button]
    void SetDepthOfFieldFocus (float focus) {
        DepthOfField dof;
        postProcessVolume.profile.TryGetSettings<DepthOfField> (out dof);
        dof.focusDistance.value = focus;
    }
    // Update is called once per frame
    void Update () {
        /* if (lerpStartValue != DistanceToCameraTarget ()) {
             SetDepthOfFieldFocus (Mathf.Lerp (lerpStartValue, DistanceToCameraTarget (), lerpValue));
             lerpValue += 0.5f * Time.deltaTime;
         } else {
             lerpValue = 0f;
         }
         */
    }
}