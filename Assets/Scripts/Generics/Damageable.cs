using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public abstract class Damageable : MonoBehaviour {
        
        public float MaxHealth = 20f;
        public float StartingHealth = 20f;

        [Tooltip("Object takes critical damage")]
        public bool IsCritical = false;

        public AudioClip HitSound;

        protected float health;
        AudioSource audioSource;

        protected virtual void Awake() {
            health = StartingHealth;

            if (GetComponent<AudioSource>()) {
                audioSource = GetComponent<AudioSource>();
            } else {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        protected virtual void Update() {
            if (HasDied()) {
                try {
                    Die();
                } catch (NullReferenceException) {
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public float GetHealth() {
            return health;
        }

        public float GetHealthPercentage() {
            // Avoid dividing by zero o.o
            if (MaxHealth == 0) {
                return 0;
            }
            return health / MaxHealth;
        }

        public float Heal(float amount) {
            if (HasDied()) {
                return health;
            }
            health += amount;
            if (health > MaxHealth) {
                health = MaxHealth;
            }
            return health;
        }

        public float Damage(float amount) {
            if (HasDied()) {
                return health;
            }

            health -= amount;

            if (health < 0) {
                health = 0;
            }

            if (HitSound) {
                audioSource.clip = HitSound;
                audioSource.loop = false;
                audioSource.spatialize = true;
                audioSource.spatialBlend = 1f;
                audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                audioSource.volume = 0.8f;
                audioSource.Play();
            }

            return health;
        }

        public bool HasDied() {
            return health <= 0;
        }

        protected abstract void Die();
    }
}