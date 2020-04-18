using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "Interaction - Move", order = 1)]
public class Interaction_Move : GridObjectInteractDataBase {

    public int moveRange = 4;
    public GridActionFacing facing = GridActionFacing.NONE;
    public string animationTrigger = "";

    public override void DoAction (GridObject self) {
        // Move!
        self.pathfinder.maxDistance = moveRange;
        self.pathfinder.StartVisualizing ();
    }
}