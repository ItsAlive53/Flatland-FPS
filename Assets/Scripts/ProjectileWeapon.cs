﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Generics.EquippableObject {

    public GameObject Projectile;

    protected virtual void Awake() {
        if (!Projectile) {
            Debug.LogWarningFormat("No projectile object set in {0}, using default.", name);

            Projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Projectile.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            Projectile.GetComponent<SphereCollider>().isTrigger = true;
            var pr = Projectile.AddComponent<Generics.Projectile>();
            pr.MakeDefault();

            Projectile.name = "DefaultProjectile";

            Projectile.SetActive(false);
        }
    }

    public virtual void Fire() {
        if (!GrabbingPlayer) {
            Debug.LogError("No player found");
            return;
        }

        var newProjectile = Instantiate(Projectile);

        newProjectile.SetActive(true);

        newProjectile.name = "Projectile";

        var rb = newProjectile.GetComponent<Rigidbody>();
        if (!rb) {
            rb = newProjectile.AddComponent<Rigidbody>();
        }

        rb.isKinematic = false;
        rb.useGravity = false;
        
        newProjectile.transform.SetPositionAndRotation(Camera.main.transform.position, Camera.main.transform.rotation);
        newProjectile.transform.Translate(new Vector3(0, 0, 1f));
        rb.AddForce(rb.transform.forward * 15f, ForceMode.VelocityChange);
    }
}
