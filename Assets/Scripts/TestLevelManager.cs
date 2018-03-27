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
}
