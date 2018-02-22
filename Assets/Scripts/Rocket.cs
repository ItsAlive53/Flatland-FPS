using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Generics.Projectile {

    public float ExplosionForce = 20f;
    public float ExplosionRadius = 10f;

    protected override void Disappear() {
        foreach (var c in Physics.OverlapSphere(transform.position, ExplosionRadius)) {
            if (c.GetComponent<Rigidbody>()) {
                c.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 0, ForceMode.Impulse);
            }
        }

        base.Disappear();
    }
}
