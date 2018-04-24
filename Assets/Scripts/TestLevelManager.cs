using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelManager : Generics.LevelManager {
    [Header("Enemy auto-spawn")]

    public GameObject Enemy;

    [Range(0, 1000)]
    public int EnemyAmount = 1;

    [Range(0, 1000f)]
    public float SpawnRadius = 50f;

    [Range(0, 50f)]
    public float SafeZone = 3f;

    protected override void Awake() {
        base.Awake();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!Enemy) {
            Debug.LogWarningFormat("No enemy prefab in {0}", name);
        } else {
            for (var i = 0; i < EnemyAmount; i++) {
                float x = 0;
                float z = 0;

                while (/*x > -SafeZone || x < SafeZone || z > -SafeZone || z < SafeZone*/ x == 0 || z == 0) {
                    x = Random.Range(-SpawnRadius, SpawnRadius);
                    z = Random.Range(-SpawnRadius, SpawnRadius);
                }

                CreateEnemy(new Vector3(x, 5f, z));
            }
        }
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

    void CreateEnemy(Vector3 position) {
        var e = Instantiate(Enemy);
        e.transform.position = position;
        e.SetActive(true);

        if (!e.GetComponent<Rigidbody>()) {
            e.AddComponent<Rigidbody>();
        }

        e.GetComponent<Rigidbody>().useGravity = true;

        if (!e.GetComponent<Generics.Enemy>()) {
            e.AddComponent<Generics.Enemy>();
        }

        e.GetComponent<Generics.Enemy>().SetLevelManager(this);
    }

    protected override void ExitLevel() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Load first scene, assume first scene is the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
