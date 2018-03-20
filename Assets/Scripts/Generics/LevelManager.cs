using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public abstract class LevelManager : MonoBehaviour {
        public AudioClip MainBGM;
        public Damageable Player;

        [Header("Death BGM Change")]

        [Range(0, 1f)]
        [Tooltip("Lowest pitch to drop to on death")]
        public float PitchCutoff = 0.1f;

        [Tooltip("How much to drop pitch each frame")]
        public float PitchStep = 0.005f;

        [Range(0, 1f)]
        [Tooltip("Lowest volume to drop to on death")]
        public float VolumeCutoff = 0.2f;

        [Tooltip("How much to drop volume each frame")]
        public float VolumeStep = 0.01f;

        [Tooltip("How long to keep playing after reaching both cutoffs, in milliseconds")]
        public float AudioCutoffTime = 2000f;

        protected AudioSource audioSource;

        protected virtual void Awake() {
            if (!Player) {
                Debug.LogErrorFormat("No player attached, check {0} in the editor", name);
            }

            if (GetComponent<AudioSource>()) {
                audioSource = GetComponent<AudioSource>();
            } else {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            if (MainBGM) {
                audioSource.spatialize = false;
                audioSource.loop = true;
                audioSource.clip = MainBGM;
                audioSource.Play();
            }
        }

        protected virtual void Update() {
            if (Player) {
                if (Player.HasDied()) {
                    if (audioSource.volume > VolumeCutoff) {
                        audioSource.volume -= VolumeStep;

                        if (audioSource.volume < VolumeCutoff) {
                            audioSource.volume = VolumeCutoff;
                        }
                    }

                    if (audioSource.pitch > PitchCutoff) {
                        audioSource.pitch -= PitchStep;

                        if (audioSource.pitch < PitchCutoff) {
                            audioSource.pitch = PitchCutoff;
                        }
                    }
                    
                    if (audioSource.volume <= VolumeCutoff && audioSource.pitch <= PitchCutoff) {
                        audioSource.volume = audioSource.pitch = 0;
                    }
                }
            }
        }
    }
}
