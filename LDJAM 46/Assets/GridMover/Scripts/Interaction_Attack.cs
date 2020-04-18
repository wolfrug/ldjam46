using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "Interaction - Attack", order = 1)]
public class Interaction_Attack : GridObjectInteractDataBase {

    public int attackRange = 1;
    public int damage = 1;
    public GridActionFacing facing = GridActionFacing.FORWARD;
    public string animationTrigger = "Attack";

    public override void DoAction (GridObject self) {
        // Attack everything 1 square from self
        List<GridObject> target = new List<GridObject> { };
        target = GetGridObject (self, attackRange, ConvertRelativeFacingToAbsolute (facing, self.facing));
        if (target.Count > 0) {
            self.StartCoroutine (AttackAnimation (self, target[0]));
        } else {
            self.animator.SetTrigger (animationTrigger);
        }
    }
    IEnumerator AttackAnimation (GridObject self, GridObject target) {
        self.animator.SetTrigger (animationTrigger);
        yield return new WaitForSeconds (self.animator.GetCurrentAnimatorStateInfo (0).length + 0.1f);
        target.Damage (damage);

    }

}