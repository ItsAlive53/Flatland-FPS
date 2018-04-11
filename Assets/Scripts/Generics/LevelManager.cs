using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Generics {
    public abstract class LevelManager : MonoBehaviour {
        public AudioClip MainBGM;
        public FPS_Player Player;

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

        [Header("Textures")]
        public Texture DotCrosshairTexture;
        public Texture CrossCrosshairTexture;

        protected AudioSource audioSource;
        protected Builders.HUD hud;
        protected Builders.Bar healthBar;
        protected Builders.CustomText healthText;
        protected Builders.CustomText scoreText;
        protected Builders.Crosshair crosshair;
        protected Builders.CustomText ammoText;

        protected float score;

        protected virtual void Awake() {
            if (!Player) {
                Debug.LogErrorFormat("No player attached, check {0} in the editor", name);
            }

            score = 0;

            hud = new Builders.HUD();
            healthBar = hud.CreateBar(Builders.HUD.ScreenPoint.TopLeft, new Vector2(0.4f, 0.05f), Builders.HUD.ValueType.Percentage, 15f, new Vector2(35f, -35f), Color.grey, Color.red);
            healthText = hud.CreateText(Builders.HUD.ScreenPoint.TopLeft, 20, Font.CreateDynamicFontFromOSFont("OCR A Std", 15), new Vector2(40f, -45f), Color.black);
            if (Player) {
                healthText.SetTextString(Mathf.RoundToInt(Player.GetHealth()).ToString() + " / " + Mathf.RoundToInt(Player.MaxHealth) + " (" + (Player.GetHealthPercentage() * 100).ToString("N0") + " %)");
            }
            scoreText = hud.CreateText(Builders.HUD.ScreenPoint.TopMiddle, 30, Font.CreateDynamicFontFromOSFont("OCR A Std", 20), new Vector2(0, -30f), Color.white);
            scoreText.SetTextString(Mathf.RoundToInt(score).ToString());
            crosshair = hud.CreateCrosshair<Builders.Crosshairs.DottedCross>();
            var tempCH = (Builders.Crosshairs.DottedCross)crosshair;
            tempCH.SetTexture(DotCrosshairTexture, CrossCrosshairTexture);
            ammoText = hud.CreateText(Builders.HUD.ScreenPoint.BottomRight, 30, Font.CreateDynamicFontFromOSFont("OCR A Std", 20), new Vector2(-10f, 10f), Color.white);
            ammoText.SetTextString("");

            if (GetComponent<AudioSource>()) {
                audioSource = GetComponent<AudioSource>();
            } else {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            if (MainBGM) {
                audioSource.spatialize = false;
                audioSource.loop = true;
                audioSource.clip = MainBGM;
                audioSource.volume = 0.6f;
                audioSource.Play();
            }

            if (!GetComponent<EasterEggsManager>()) {
                gameObject.AddComponent<EasterEggsManager>();
            }

            GetComponent<EasterEggsManager>().AddSequence(() => {
                if (Player) {
                    // Heal for enough to leave existing damage
                    var newHealth = 9999f - Player.GetHealth()*2 - Player.MaxHealth;
                    Debug.Log(newHealth);
                    Player.MaxHealth = 9999f;
                    Player.Heal(newHealth);
                }
            }, KeyCode.G, KeyCode.O, KeyCode.D, KeyCode.M, KeyCode.O, KeyCode.D, KeyCode.E);

            GetComponent<EasterEggsManager>().AddSequence(() => {
                if (Player.EquippedObject) {
                    if (Player.EquippedObject.GetComponent<ProjectileWeapon>()) {
                        Player.EquippedObject.GetComponent<ProjectileWeapon>().SetClipInfinite(true);
                    }
                }
            }, KeyCode.I, KeyCode.D, KeyCode.D, KeyCode.Q, KeyCode.D);
        }

        protected virtual void Update() {
            if (Input.GetKey(KeyCode.Escape)) {
                ExitLevel();
            }

            scoreText.SetTextString(Mathf.RoundToInt(score).ToString("N0"));
            healthText.SetTextString(Mathf.RoundToInt(Player.GetHealth()).ToString() + " / " + Mathf.RoundToInt(Player.MaxHealth) + " (" + (Player.GetHealthPercentage() * 100).ToString("N0") + " %)");

            if (Player) {
                healthBar.SetWidthPercentage(Player.GetHealthPercentage());

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

                if (Player.EquippedObject) {
                    if (Player.EquippedObject.GetComponent<ProjectileWeapon>()) {
                        if (Player.EquippedObject.GetComponent<ProjectileWeapon>().GetClipAmmo() < 0) {
                            ammoText.SetTextString("∞ / " + Player.EquippedObject.GetComponent<ProjectileWeapon>().GetAmmoLeft());
                        } else {
                            ammoText.SetTextString(Player.EquippedObject.GetComponent<ProjectileWeapon>().GetClipAmmo().ToString() + " / " + Player.EquippedObject.GetComponent<ProjectileWeapon>().GetAmmoLeft().ToString());
                        }
                    }
                }
            }
        }

        public virtual void IncrementScore() {
            score++;
        }

        protected abstract void ExitLevel();
    }
}
