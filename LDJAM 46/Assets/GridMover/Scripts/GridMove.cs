using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GridMove : MonoBehaviour { // from: http://wiki.unity3d.com/index.php/GridMove
    private float moveSpeed = 3f;
    [SerializeField]
    private float gridSize = 0.5f;
    public enum Orientation {
        Horizontal,
        Vertical
    }
    public Orientation gridOrientation = Orientation.Horizontal;
    public bool allowDiagonals = false;
    public bool correctDiagonalSpeed = true;
    public bool isometric = true;
    public Vector2 isometricAngle = new Vector2 (0.5f, 0.5f);

    public LayerMask blockingMask;
    public bool onlyAllowOnMask = false;
    [SerializeField]
    private Vector2 input;

    public Vector2 yAndXDegrees = new Vector2 (1f, 1f);
    [SerializeField]
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    public void Update () {
        if (!isMoving) {
            input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
            if (!allowDiagonals) {
                if (Mathf.Abs (input.x) > Mathf.Abs (input.y)) {
                    input.y = 0;
                } else {
                    input.x = 0;
                }
            }

            if (input != Vector2.zero) {
                if (isometric) {
                    if (input.y > 0) {
                        input.x = isometricAngle.x;
                        input.y = isometricAngle.y;
                    } else if (input.y < 0) {
                        input.x = -isometricAngle.x;
                        input.y = -isometricAngle.y;
                    } else if (input.x > 0) {
                        input.y = -isometricAngle.y;
                        input.x = isometricAngle.x;
                    } else if (input.x < 0) {
                        input.y = isometricAngle.y;
                        input.x = -isometricAngle.x;
                    }
                };
                StartCoroutine (move (transform));
            }
        }
    }

    public IEnumerator move (Transform transform) {
        isMoving = true;
        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal) {
            endPosition = new Vector3 (startPosition.x + System.Math.Sign (input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign (input.y) * gridSize);
        } else {
            endPosition = new Vector3 (startPosition.x + (input.x) * gridSize,
                startPosition.y + (input.y) * gridSize, startPosition.z);
        }

        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
            factor = 0.7071f;
        } else {
            factor = 1f;
        }
        if (CanMove (startPosition, endPosition)) {
            while (t < 1f) {
                t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                transform.position = Vector3.Lerp (startPosition, endPosition, t);
                yield return null;
            }
        };
        isMoving = false;
        yield return 0;
    }

    public bool CanMove (Vector2 startPosition, Vector2 endPosition) {

        Collider2D[] results = { };
        Vector2 dir = endPosition - startPosition;
        ContactFilter2D filter = new ContactFilter2D ();
        filter.useLayerMask = true;
        filter.layerMask = blockingMask;
        // endPosition = new Vector2 (endPosition.x, endPosition.y - 2f);
        startPosition = new Vector2 (startPosition.x, startPosition.y - 2f);
        Debug.DrawLine (startPosition, dir, Color.red, 5f);
        results = Physics2D.OverlapCircleAll (endPosition, gridSize, blockingMask);
        if (results.Length > 0) {
            foreach (Collider2D hit in results) {
                Debug.Log (hit.transform.position);
            }
            if (onlyAllowOnMask) {
                return true;
            } else {

                return false;
            };
        } else {
            if (onlyAllowOnMask) {
                return false;
            } else {
                return true;
            };
        }
    }
}