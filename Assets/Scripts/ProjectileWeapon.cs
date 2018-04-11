using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Generics.EquippableObject {

    private bool isReloading;
    private int ClipAmmo;
    public int ClipSize = 1;
    private int AmmoLeft;
    public int AmmoStorageSize = 10;
    public GameObject Projectile;
    public Animation ReloadAnimation;

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
        if (isInfinite) {
            ClipAmmo = -1;
        }
        else if (ClipAmmo < 0) {
            ClipAmmo = 0;
        }
    }

    public virtual void BeginReload() {
        isReloading = true;
        if (ReloadAnimation != null && !ReloadAnimation.isPlaying) {
            ReloadAnimation.Play();
            Invoke("EndReload", ReloadAnimation.clip.length);
        } else {
            EndReload();
        }
    }

    public virtual void EndReload() {
        if (AmmoLeft < ClipSize) {
            ClipAmmo = AmmoLeft;
            AmmoLeft = 0;
        } else {
            ClipAmmo = ClipSize;
            AmmoLeft -= ClipSize;
        }
        isReloading = false;
    }

    public virtual void NoAmmo() {
        // TODO
        Debug.Log("Ammo out");
    }

    public virtual void Fire() {
        if (isReloading) return;

        if (!GrabbingPlayer) {
            Debug.LogError("No player found");
            return;
        }

        if (ClipAmmo == 0) {
            if (AmmoLeft > 0) {
                BeginReload();
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
                BeginReload();
            }
        }
    }
}
