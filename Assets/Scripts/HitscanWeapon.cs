using System;
using System.Collections;
using UnityEngine;

class HitscanWeapon : Generics.EquippableObject {
    [Tooltip("Delay between each fire in a burst, in milliseconds")]
    public float FireRate = 100f;

    public float Damage = 10f;
    public float CriticalMultiplier = 1.5f;

    public float Knockback = 5f;

    bool canFire = true;

    public virtual void Fire() {
        if (!canFire) return;

        if (!GrabbingPlayer) return;

        canFire = false;
        StartCoroutine(FireDelay());

        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit rh;
        if (Physics.Raycast(ray, out rh)) {
            if (rh.collider.GetComponent<Generics.Damageable>()) {
                var dmg = rh.collider.GetComponent<Generics.Damageable>();

                if (dmg.IsCritical) {
                    dmg.Damage(Damage * CriticalMultiplier);
                } else {
                    dmg.Damage(Damage);
                }
            }

            if (rh.collider.GetComponent<Rigidbody>()) {
                var rb = rh.collider.GetComponent<Rigidbody>();

                rb.AddForce(GrabbingPlayer.forward * Knockback, ForceMode.Impulse);
            }
        }
    }

    IEnumerator FireDelay() {
        yield return new WaitForSeconds(FireRate / 1000);
        canFire = true;
        yield break;
    }
}
