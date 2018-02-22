using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public class Projectile : MonoBehaviour {

        [Tooltip("Lifetime of the projectile in milliseconds")]
        public float Lifetime = 15000f;

        private void Awake() {
            Invoke("Disappear", Lifetime / 1000);
        }

        private void OnTriggerEnter(Collider collision) {
            if (collision.tag == "Player") {
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
