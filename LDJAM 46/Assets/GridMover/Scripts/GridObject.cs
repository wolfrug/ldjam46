using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GridObjectDie : UnityEvent<GridObject> { }

[System.Serializable]
public class GridObjectHurt : UnityEvent<GridObject, int> { }

public enum GridObjectFacing {
    NONE = 0000,
    UP_RIGHT = 1000,
    UP_LEFT = 2000,
    DOWN_RIGHT = 3000,
    DOWN_LEFT = 4000,
}

public class GridObject : MonoBehaviour {
    public GridObjectData data;
    public GridPathfinder pathfinder;
    public GridAction action;
    public GameObject avatar;
    public Animator animator;
    // ...I am lazy
    private GameObject icon;
    private GenericWorldSpaceToCanvasIcon iconScript;
    private GridObjectIcon iconUIScript;
    // This is a static bool, shared across everything, since only one object can be selected at a time!
    public static GridObject selected;
    public int health;
    public int actionpoints;
    public bool dead;
    public Vector2Int facing;

    public GridObjectDie deathEvent;
    public GridObjectHurt hurtEvent;

    // Start is called before the first frame update
    void Start () {
        if (data == null) {
            Debug.LogWarning ("Object does not have data object attached!", gameObject);
        }
        if (pathfinder == null) {
            pathfinder = GetComponentInChildren<GridPathfinder> ();
        }
        // set up pathfinder
        //pathfinder.maxDistance = data.actionpoints;
        // set up animator
        if (animator != null) {
            pathfinder.moveStartedEvent.AddListener (AnimateMovement);
            pathfinder.moveFinishedEvent.AddListener (StopMoving);
            pathfinder.waypointReachedEvent.AddListener (AnimateMovement);
        }
        // Spawn character icon
        if (icon == null && data.iconPrefab_ != null) {
            iconScript = GetComponent<GenericWorldSpaceToCanvasIcon> ();
            if (iconScript != null) {
                icon = Instantiate (data.iconPrefab_, GridUIManager.instance.canvas.transform);
                iconScript.target = avatar;
                iconScript.canvasObject = icon.GetComponent<RectTransform> ();
            };
        }
        // setup icon
        iconUIScript = icon.GetComponent<GridObjectIcon> ();
        iconUIScript.SetName (data.name_);
        GridUIManager.instance.AddGridObjectIcon (this, iconUIScript);

        // setup controls
        if (data.type_ == GridObjectType.PLAYABLE) {
            GridCameraController.instance.mouseLeftClickEvent.AddListener (ClickedToMove);
            GridCameraController.instance.mouseRightClickEvent.AddListener (ClickToChangeFacing);
        }
        // setup action
        if (action == null) {
            action = GetComponent<GridAction> ();
        }
        action.SetActions (data.defaultActions);

        // setup other things
        health = data.health;
        actionpoints = data.actionpoints;

    }

    [EasyButtons.Button]
    void SelectDebug () {
        SelectObject (true);
    }

    [EasyButtons.Button]
    void AttackDebug () {
        if (!dead) {
            action.DoAction ("attack_simple");
        };
    }
    public void DoAction (string id) {
        if (selected == this && !dead) {
            GridObjectInteractDataBase theaction = action.GetAction (id);
            if (theaction != null) {
                if (theaction.actionCost <= actionpoints) {
                    actionpoints -= theaction.actionCost;
                    action.DoAction (id);
                    GridUIManager.instance.UpdateCharacter (this);
                };
            };
        }
    }
    public void DoAction (GridObjectInteractDataBase data) {
        DoAction (data.id);
    }

    public void SelectObject (bool select) {
        if (select) {
            selected = this;
            GridCameraController.instance.SetCamTarget (avatar.transform);
            GridUIManager.instance.SelectCharacter (this);
        }
    }

    void ClickedToMove (TileInfo info) {
        if (!dead && selected == this && data.type_ == GridObjectType.PLAYABLE && GridUIManager.instance.playerTurn) {
            pathfinder.ClickedToMove (info);
        };
    }
    void ClickToChangeFacing (TileInfo info) { // change facing in the direction of a right-click when selected
        if (!dead && selected == this && data.type_ == GridObjectType.PLAYABLE && actionpoints > 0) {
            Vector2Int oldfacing = facing;
            Vector3Int mousePos = GridManager.instance.maingrid.WorldToCell (GridCameraController.instance.mainCam.ScreenToWorldPoint (Input.mousePosition));
            Vector3Int avatarPos = pathfinder.selfLocation;
            //Debug.Log ("Mouse X" + mousePos.x + "Avatar X: " + avatarPos.x + "Mouse Y: " + mousePos.y + "Avatar Y: " + avatarPos.y);
            if (mousePos.x == avatarPos.x && mousePos.y > avatarPos.y) {
                ChangeFacing (GridObjectFacing.UP_LEFT);
            } else if (mousePos.x == avatarPos.x && mousePos.y < avatarPos.y) {
                ChangeFacing (GridObjectFacing.DOWN_RIGHT);
            } else if (mousePos.x > avatarPos.x && mousePos.y == avatarPos.y) {
                ChangeFacing (GridObjectFacing.UP_RIGHT);
            } else if (mousePos.x < avatarPos.x && mousePos.y == avatarPos.y) {
                ChangeFacing (GridObjectFacing.DOWN_LEFT);
            }
            if (facing != oldfacing) {
                actionpoints--;
                GridUIManager.instance.UpdateCharacter (this);
            }
        }
    }

    void AnimateMovement (GridPathfinder pf, Vector3Int startPoint, Vector3Int endPoint) {
        if (animator != null) {
            Vector2Int movementDirection = GetMovementDirection (startPoint, endPoint);
            animator.SetInteger ("FaceX", (int) movementDirection.x);
            animator.SetInteger ("FaceY", (int) movementDirection.y);
            animator.SetBool ("Moving", true);
            facing.x = movementDirection.x;
            facing.y = movementDirection.y;
        }
    }
    public void ChangeFacing (GridObjectFacing facing) {
        Vector2Int facingVector = GridObjectManager.instance.ConvertFacingEnumToNumbers (facing);
        ChangeFacing (facingVector.x, facingVector.y);
    }
    public void ChangeFacing (int x, int y) {
        animator.SetFloat ("IdleBlend", GetDirectionFloat (x, y));
        facing.x = x;
        facing.y = y;
    }
    void StopMoving (GridPathfinder pf, Vector3Int startPoint, Vector3Int endPoint) {
        if (animator != null) {
            animator.SetBool ("Moving", false);
            // set facing in blendtree
            //0 = up right, 1 = down left, 2 = down right, 3 = up left
            //1,0 = up right, down left = -1, 0, down right = 0, -1, up left = 0, 1
            ChangeFacing (animator.GetInteger ("FaceX"), animator.GetInteger ("FaceY"));
        }
    }

    float GetDirectionFloat (int x, int y) {
        if (x == 1 && y == 0) {
            return 0f;
        } else if (x == -1 && y == 0) {
            return 1f;
        } else if (x == 0 && y == 1) {
            return 2f;
        } else if (x == 0 && y == -1) {
            return 3f;
        } else {
            return 0f;
        }
    }

    void AnimateDamage (float damage) {
        if (animator != null) {
            animator.SetTrigger ("Hurt");
            iconUIScript.healthBarFill = damage;
        }
    }
    void AnimateDeath (bool death = true) {
        if (animator != null) {
            animator.SetBool ("Dead", death);
            iconUIScript.healthBarFill = health;
            deathEvent.Invoke (this);
        }
    }

    public void Damage (int damage) {
        if (!dead) {
            health -= damage;
            hurtEvent.Invoke (this, damage);
            if (health > 0) {
                AnimateDamage ((float) health / (float) data.health);
            } else {
                AnimateDeath ();
                dead = true;
            }
        };
    }

    public Vector2Int GetMovementDirection (Vector3Int start, Vector3Int end) { // For the animator
        Vector2Int movementDirection = new Vector2Int (0, 0);
        if (end.x > start.x) {
            movementDirection.x = 1;
        } else if (end.x < start.x) {
            movementDirection.x = -1;
        } else {
            movementDirection.x = 0;
        }
        if (end.y > start.y) {
            movementDirection.y = 1;
        } else if (end.y < start.y) {
            movementDirection.y = -1;
        } else {
            movementDirection.y = 0;
        }
        return movementDirection;
    }

    // Update is called once per frame
    void Update () {

    }
}