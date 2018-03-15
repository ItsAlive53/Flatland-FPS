using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Generics.Projectile {

    public float ExplosionForce = 20f;
    public float ExplosionForceRadius = 10f;
    public float EntityDamage = 15f;

    public float TravelSoundRange = 30f;
    public float ExplosionSoundRange = 15f;

    public AudioClip TravelSound;
    public AudioClip ExplosionSound;

    AudioSource audioSource;

    protected override void Awake() {
        base.Awake();

        if (!GetComponent<AudioSource>()) {
            audioSource = gameObject.AddComponent<AudioSource>();
        } else {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update() {
        if (flying) {
            if (TravelSound) {
                if (!audioSource.isPlaying) {
                    audioSource.clip = TravelSound;
                    audioSource.loop = true;
                    audioSource.spatialize = true;
                    audioSource.spatialBlend = 1f;
                    audioSource.pitch = Random.Range(0.9f, 1.1f);
                    audioSource.Play();
                }
            }
        }
    }

    float GetVolume(float range, float minVolume) {
        var dist = Vector3.Distance(Camera.main.transform.position, transform.position);

        var vol = dist / range;

        if (vol <= 0) {
            vol = 0;
        }

        if (vol >= 1f) {
            return minVolume;
        }

        return 1f - vol;
    }

    float GetVolume(float range) {
        return GetVolume(range, 0);
    }

    protected override void Disappear() {
        flying = false;
        audioSource.Stop();

        if (ExplosionSound) {
            audioSource.clip = ExplosionSound;
            if (!audioSource.isPlaying) {
                // audioSource.volume = GetVolume(ExplosionSoundRange, 0.01f);
                audioSource.loop = false;
                audioSource.spatialize = true;
                audioSource.spatialBlend = 1f;
                audioSource.pitch = Random.Range(0.5f, 1f);
                audioSource.Play();
            }
        }

        foreach (var c in Physics.OverlapSphere(transform.position, ExplosionForceRadius)) {
            if (c.GetComponent<Rigidbody>()) {
                c.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionForceRadius, 1f, ForceMode.Impulse);
            }

            if (c.GetComponent<Generics.Damageable>()) {
                c.GetComponent<Generics.Damageable>().Damage(EntityDamage * (1 - (Vector3.Distance(c.ClosestPoint(transform.position), transform.position) / ExplosionForceRadius)));
            }
        }

        if (GetComponent<Collider>()) {
            GetComponent<Collider>().enabled = false;
        }

        foreach (var c in GetComponentsInChildren<Collider>()) {
            c.enabled = false;
        }

        if (ExplosionSound) {
            if (GetComponent<MeshRenderer>()) {
                GetComponent<MeshRenderer>().enabled = false;
            }

            foreach (var mesh in GetComponentsInChildren<MeshRenderer>()) {
                mesh.enabled = false;
            }

            if (GetComponent<Rigidbody>()) {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            StartCoroutine(Delay());
        } else {
            base.Disappear();
        }
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(ExplosionSound.length);

        base.Disappear();

        yield break;
    }
}
