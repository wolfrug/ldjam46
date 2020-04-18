using System.Collections;
using UnityEngine;

public enum GridObjectType {
    NONE = 1000,
    PLAYABLE = 2000,
    BLOCKER = 3000,
    ENEMY = 4000,
}

[CreateAssetMenu (fileName = "Data", menuName = "GridObjectData", order = 1)]
public class GridObjectData : ScriptableObject {
    public string name_;
    public GridObjectType type_ = GridObjectType.NONE;
    public GridObjectInteractDataBase[] defaultActions;
    public int actionpoints = 5;
    public int health = 2;
    public GameObject prefab_;
    public GameObject iconPrefab_;
    public Sprite portraitImage;

}