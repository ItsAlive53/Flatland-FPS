using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Generics.EquippableObject {

    private int ClipAmmo;
    public int ClipSize = 1;
    private int AmmoLeft;
    public int AmmoStorageSize = 10;
    public GameObject Projectile;

    protected override void Awake() {
        base.Awake();

        ClipAmmo = ClipSize;
        AmmoLeft = AmmoStorageSize;

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

    public virtual int GetClipAmmo() {
        return ClipAmmo;
    }

    public virtual int GetAmmoLeft() {
        return AmmoLeft;
    }

    public virtual void SetClipInfinite(bool isInfinite) {
        if (isInfinite) ClipAmmo = -1;
        else ClipAmmo = 0;
    }

    public virtual void Reload() {
        // TODO
        ClipAmmo = ClipSize;
        Debug.Log("reloaded");
    }

    public virtual void NoAmmo() {
        // TODO
        Debug.Log("Ammo out");
    }

    public virtual void Fire() {
        if (!GrabbingPlayer) {
            Debug.LogError("No player found");
            return;
        }

        if (ClipAmmo == 0) {
            if (AmmoLeft > 0) {
                Reload();
            } else {
                NoAmmo();
            }
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

        if (ClipAmmo > 0) ClipAmmo--;

        if (ClipAmmo == 0) {
            if (AmmoLeft > 0) {
                Reload();
            }
        }
    }
}
