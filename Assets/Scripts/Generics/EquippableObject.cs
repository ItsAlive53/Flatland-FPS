using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public abstract class EquippableObject : MonoBehaviour {

        [HideInInspector]
        public Transform GrabbingPlayer;

        public Color HighlightColor = new Color(60f, 60f, 60f);
        public bool UpscaleOnEquip;
        [Range(1f,10f)]
        public float UpscaleOnEquipMultiplier = 1f;

        [HideInInspector]
        public Vector3 offset = Vector3.zero;

        protected Vector3 initialScale;

        protected virtual void Awake() {
            initialScale = transform.localScale;
        }

        protected virtual void FixedUpdate() {
            if (GrabbingPlayer) {
                if (UpscaleOnEquip) {
                    transform.localScale = initialScale * UpscaleOnEquipMultiplier;
                }
                transform.position = GrabbingPlayer.Find("Head").position;
                transform.rotation = Quaternion.Euler(new Vector3(GrabbingPlayer.Find("Head").eulerAngles.x - 90, GrabbingPlayer.eulerAngles.y, GrabbingPlayer.eulerAngles.z - 5f));
                transform.Translate(new Vector3(0.75f, -0.5f, 0) + offset);

                if (GetComponent<Collider>()) {
                    GetComponent<Collider>().enabled = false;
                }

                foreach (var c in GetComponentsInChildren<Collider>()) {
                    c.enabled = false;
                }

                if (GetComponent<Rigidbody>()) {
                    GetComponent<Rigidbody>().isKinematic = true;
                }

                if (GetComponent<Animator>()) {
                    GetComponent<Animator>().enabled = false;
                }
            } else {
                transform.localScale = initialScale;

                if (GetComponent<Collider>()) {
                    GetComponent<Collider>().enabled = true;
                }

                foreach (var c in GetComponentsInChildren<Collider>()) {
                    c.enabled = true;
                }

                if (GetComponent<Rigidbody>()) {
                    GetComponent<Rigidbody>().isKinematic = false;
                }

                if (GetComponent<Animator>()) {
                    GetComponent<Animator>().enabled = true;
                }
            }
        }

        public void Highlight() {
            if (GetComponent<Renderer>()) {
                GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                GetComponent<Renderer>().material.SetColor("_EmissionColor", HighlightColor);
            }

            foreach (var obj in GetComponentsInChildren<Renderer>()) {
                obj.material.EnableKeyword("_EMISSION");
                obj.material.SetColor("_EmissionColor", HighlightColor);
            }

            if (IsInvoking("UnHighlight")) {
                CancelInvoke("UnHighlight");
            }
            Invoke("UnHighlight", 0.1f);
        }

        public void UnHighlight() {
            if (GetComponent<Renderer>()) {
                GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            }

            foreach (var obj in GetComponentsInChildren<Renderer>()) {
                obj.material.DisableKeyword("_EMISSION");
            }
        }

        public void UnEquip() {
            if (GetComponent<Rigidbody>()) {
                GetComponent<Rigidbody>().isKinematic = false;
            }

            GrabbingPlayer = null;

            if (GetComponent<Animator>()) {
                GetComponent<Animator>().enabled = true;
            }
        }
    }
}
