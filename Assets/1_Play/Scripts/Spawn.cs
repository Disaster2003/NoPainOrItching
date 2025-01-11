using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemysZ;
    [SerializeField] GameObject[] enemysSin;
    [SerializeField] GameObject[] enemysCin;
    [SerializeField] GameObject[] enemysShake;
    [SerializeField] GameObject[] enemysRain;
    [SerializeField] GameObject[] enemysChaos;
    private Dictionary<int, GameObject[]> enemysArray = new Dictionary<int, GameObject[]>();

    [SerializeField, Header("ìGÇÃê∂ê¨ä‘äu(ïb)")]
    private float INTERVAL_SPAWN = 5.0f;
    private float intervalSpawn;
    private bool isFinished; // true = ê∂ê¨èIóπ, false = ñ¢ê∂ê¨

    // Start is called before the first frame update
    void Start()
    {
        // ìGÇÃéÌóﬁÇÇ–Ç∆Ç‹Ç∆ÇﬂÇ…
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

            // íäëIÇµÇƒê∂ê¨
            int rand = Random.Range(0, enemysArray.Count);
            StartCoroutine(SpawnEnemies(rand));
        }
        else if (!isFinished) intervalSpawn -= Time.deltaTime;
    }

    /// <summary>
    /// ìGÇÃê∂ê¨
    /// </summary>
    /// <param name="rand">íäëIÇ≈ëIÇŒÇÍÇΩêî</param>
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
