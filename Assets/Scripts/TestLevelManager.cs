using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelManager : Generics.LevelManager {

    [Header("Wave Spawns")]
    public WaveList Waves;

    [Tooltip("Time between spawns")]
    public float SpawnTimer = 5f;

    [Tooltip("Whether or not the wave list should loop on reaching the end")]
    public bool InfiniteSpawn = false;

    protected int waveIndex = 0;

    protected override void Awake() {
        base.Awake();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        NextWave();
    }

    protected override void Defeat() {
        base.Defeat();
    }

    protected override void Update() {
        base.Update();
    }

    private void OnApplicationFocus(bool focus) {
        if (focus) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    protected void NextWave() {
        if (waveIndex >= Waves.List.Count) {
            if (!InfiniteSpawn) {
                return;
            }

            waveIndex = 0;
        }

        if (Waves.List.Count <= 0)
            return;

        var wave = Waves.List[waveIndex];

        foreach (var spawn in wave.Enemies.List) {
            for (var i = 0; i < spawn.Amount; i++) {
                float x = 0;
                float z = 0;

                while (x == 0 || z == 0) {
                    x = Random.Range(spawn.MinDistance, spawn.MaxDistance);
                    z = Random.Range(spawn.MinDistance, spawn.MaxDistance);

                    x *= Random.value > 0.5f ? 1 : -1;
                    z *= Random.value > 0.5f ? 1 : -1;
                }

                x += Player.transform.position.x;
                z += Player.transform.position.z;

                CreateEnemy(new Vector3(x, 5f, z), spawn);
            }
        }

        waveIndex++;

        Invoke("NextWave", SpawnTimer);
    }

    void CreateEnemy(Vector3 position, EnemySpawn enemy) {
        var e = Instantiate(enemy.Enemy);
        e.transform.position = position;
        e.SetActive(true);

        if (!e.GetComponent<Rigidbody>()) {
            e.AddComponent<Rigidbody>();
        }

        e.GetComponent<Rigidbody>().useGravity = true;

        if (!e.GetComponent<Generics.Enemy>()) {
            e.AddComponent<Generics.Enemy>();
        }

        e.GetComponent<Generics.Enemy>().SetDamage(enemy.Damage);
        e.GetComponent<Generics.Enemy>().SetHealth(enemy.Health);
        e.GetComponent<Generics.Enemy>().SetSpeed(enemy.Speed);
        e.GetComponent<Generics.Enemy>().SetLevelManager(this);
    }

    protected override void ExitLevel() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Load first scene, assume first scene is the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
