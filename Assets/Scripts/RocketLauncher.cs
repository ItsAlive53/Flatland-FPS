using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : ProjectileWeapon {

    bool recoiling = false;
    bool recoilReturning = false;

    public float RecoilAmount = 1f;
    public float RecoilStep = 0.05f;

    public AudioClip FireSound;

    AudioSource audioSource;

    public override void Fire() {
        if (recoiling) return;

        base.Fire();

        if (GetClipAmmo() == 0) {
            return;
        }

        if (FireSound) {
            audioSource.clip = FireSound;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }

        foreach (var rb in GetComponentsInChildren<Rigidbody>()) {
            rb.AddForce(rb.transform.forward * RecoilAmount * 15f, ForceMode.Impulse);
        }

        recoiling = true;
    }

    protected override void Awake() {
        base.Awake();

        if (GetComponent<AudioSource>()) {
            audioSource = GetComponent<AudioSource>();
        } else {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected override void FixedUpdate() {
        if (recoiling) {
            if (!recoilReturning && offset.y < RecoilAmount) {
                offset.y += RecoilStep;
            } else if (!recoilReturning) {
                offset.y = RecoilAmount;
                recoilReturning = true;
            }

            if (recoilReturning && offset.y > 0) {
                offset.y -= RecoilStep;
            } else if (recoilReturning) {
                offset.y = 0;
                recoilReturning = false;
                recoiling = false;
            }
        }

        base.FixedUpdate();
    }
}
