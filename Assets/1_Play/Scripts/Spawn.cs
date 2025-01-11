using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemysZ; // z軸に
    [SerializeField] GameObject[] enemysSin; // sin波
    [SerializeField] GameObject[] enemysCin; // cin
    [SerializeField] GameObject[] enemysShake; // ぐわんぐわん
    [SerializeField] GameObject[] enemysRain; // 雨
    [SerializeField] GameObject[] enemysChaos; // 両斜め
    private Dictionary<int, GameObject[]> enemysArray = new Dictionary<int, GameObject[]>();

    [SerializeField, Header("敵の生成間隔(秒)")]
    private float INTERVAL_SPAWN = 5.0f;
    private float intervalSpawn;
    private bool isFinished; // true = 生成終了, false = 未生成

    // Start is called before the first frame update
    void Start()
    {
        // 敵の種類をひとまとめに
        enemysArray[0] = enemysZ;
        enemysArray[1] = enemysSin;
        enemysArray[2] = enemysCin;
        enemysArray[3] = enemysShake;
        enemysArray[4] = enemysRain;
        enemysArray[5] = enemysChaos;

        intervalSpawn = INTERVAL_SPAWN;
        isFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (intervalSpawn <= 0)
        {
            intervalSpawn = INTERVAL_SPAWN;
            isFinished = true;

            // 抽選して生成
            int rand = Random.Range(0, enemysArray.Count);
            StartCoroutine(SpawnEnemies(rand));
        }
        else if (!isFinished) intervalSpawn -= Time.deltaTime;
    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    /// <param name="go">生成する敵</param>
    private IEnumerator SpawnEnemies(int rand)
    {
        for (int i = 0; i < enemysArray[rand].Length; i++)
        {
            Instantiate(enemysArray[rand][i]);
            yield return new WaitForSeconds(1);
        }

        isFinished = false;
    }
}
