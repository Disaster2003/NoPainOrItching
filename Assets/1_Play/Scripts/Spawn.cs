using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemysZ; // zé≤Ç…
    [SerializeField] GameObject[] enemysSin; // sinîg
    [SerializeField] GameObject[] enemysCin; // cin
    [SerializeField] GameObject[] enemysShake; // ÇÆÇÌÇÒÇÆÇÌÇÒ
    [SerializeField] GameObject[] enemysRain; // âJ
    [SerializeField] GameObject[] enemysChaos; // óºéŒÇﬂ
    private Dictionary<int, GameObject[]> enemysArray = new Dictionary<int, GameObject[]>();

    private float intervalSpawn;

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

        intervalSpawn = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(intervalSpawn <= 0)
        {
            // íäëIÇµÇƒê∂ê¨
            int rand = Random.Range(0, enemysArray.Count);
            StartCoroutine(SpawnEnemies(rand));
        }
        else intervalSpawn -= Time.deltaTime;
    }

    /// <summary>
    /// ìGÇÃê∂ê¨
    /// </summary>
    /// <param name="go">ê∂ê¨Ç∑ÇÈìG</param>
    private IEnumerator SpawnEnemies(int rand)
    {
        for (int i = 0; i < enemysArray[rand].Length; i++)
        {
            Instantiate(enemysArray[rand][i]);
            yield return new WaitForSeconds(1);
        }

        intervalSpawn = 5;
    }
}
