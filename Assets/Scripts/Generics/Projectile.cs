using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public abstract class Projectile : MonoBehaviour {

        [Tooltip("Lifetime of the projectile in milliseconds")]
        public float Lifetime = 15000f;

        protected bool flying;

        protected virtual void Awake() {
            flying = true;

            Invoke("Disappear", Lifetime / 1000);
        }

        protected virtual void OnTriggerEnter(Collider col) {
            if (col.tag == "Player") {
                return;
            }
            Disappear();
        }

        public void MakeDefault() {
            CancelInvoke("Disappear");
        }

        protected virtual void Disappear() {
            Destroy(gameObject);
        }
    }
}
