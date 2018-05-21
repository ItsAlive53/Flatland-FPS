using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public class Enemy : Damageable {
        private LevelManager attachedLevelMgr;

        [Header("Hit")]
        public float HitDamage = 5f;
        public float HitCooldownSeconds = 1f;

        [Header("Movement")]
        public float MovementSpeed = 1f;

        protected override void Awake() {
            base.Awake();
        }

        public void SetLevelManager(LevelManager levelManager) {
            attachedLevelMgr = levelManager;
        }

        protected override void Die() {
            if (attachedLevelMgr) {
                attachedLevelMgr.IncrementScore();
            }

            Destroy(gameObject);
        }

        public void SetSpeed(float speed) {
            MovementSpeed = speed;
        }

        public void SetHealth(float health) {
            StartingHealth = MaxHealth = this.health = health;
        }

        public void SetDamage(float damage) {
            HitDamage = damage;
        }
    }
}
