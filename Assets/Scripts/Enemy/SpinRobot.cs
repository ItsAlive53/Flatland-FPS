using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRobot : Generics.Enemy {

    private bool canHit = true;

    private Vector3 targetPosition;

    protected override void Update() {
        base.Update();

        if (!IsInvoking("EndCooldown")) {
            canHit = true;
        }

        Move();
    }

    protected void Hit(Generics.Damageable entity) {
        if (!canHit || IsInvoking("EndCooldown")) return;

        entity.Damage(HitDamage);
        canHit = false;
        Invoke("EndCooldown", HitCooldownSeconds);
    }

    protected void EndCooldown() {
        canHit = true;
    }

    protected void Move() {
        var target = FindObjectOfType<Generics.LevelManager>().Player;

        if (!target) return;

        if (target.HasDied()) {
            if (transform.position.Equals(targetPosition)) {
                targetPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)) + transform.position;
            }

            transform.Translate(targetPosition.normalized * MovementSpeed * Time.deltaTime, Space.World);
            return;
        }

        targetPosition = target.transform.position - transform.position;

        targetPosition.y = transform.position.y;

        transform.Translate(targetPosition.normalized * MovementSpeed * Time.deltaTime, Space.World);
    }

    protected void OnCollisionStay(Collision c) {
        if (c.collider.GetComponent<Generics.Damageable>() && c.collider.tag.Equals("Player")) {
            Hit(c.collider.GetComponent<Generics.Damageable>());
        }
    }
}
