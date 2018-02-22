using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Generics.Projectile {

    public float ExplosionForce = 20f;
    public float ExplosionForceRadius = 10f;
    public float EntityDamage = 15f;

    protected override void Disappear() {
        foreach (var c in Physics.OverlapSphere(transform.position, ExplosionForceRadius)) {
            if (c.GetComponent<Rigidbody>()) {
                c.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionForceRadius, 1f, ForceMode.Impulse);
            }

            if (c.GetComponent<Generics.Damageable>()) {
                c.GetComponent<Generics.Damageable>().Damage(EntityDamage * (1 - (Vector3.Distance(c.ClosestPoint(transform.position), transform.position) / ExplosionForceRadius)));
            }
        }

        base.Disappear();
    }
}
