using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "Interaction - Attack", order = 1)]
public class Interaction_Attack : GridObjectInteractDataBase {

    public override void DoAction (GridObject self) {
        // Attack everything 1 square from self
        List<GridObject> target = new List<GridObject> { };
        target = GetGridObject (self, 1, self.facing);
        if (target.Count > 0) {
            self.StartCoroutine (AttackAnimation (self, target[0]));
        } else {
            self.animator.SetTrigger ("Attack");
        }
    }
    IEnumerator AttackAnimation (GridObject self, GridObject target) {
        self.animator.SetTrigger ("Attack");
        yield return new WaitForSeconds (self.animator.GetCurrentAnimatorStateInfo (0).length + 1f);
        target.Damage (2);

    }

}