using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemysZ; // z����
    [SerializeField] GameObject[] enemysSin; // sin�g
    [SerializeField] GameObject[] enemysCin; // cin
    [SerializeField] GameObject[] enemysShake; // ����񂮂��
    [SerializeField] GameObject[] enemysRain; // �J
    [SerializeField] GameObject[] enemysChaos; // ���΂�
    private Dictionary<int, GameObject[]> enemysArray = new Dictionary<int, GameObject[]>();

    private float intervalSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // �G�̎�ނ��ЂƂ܂Ƃ߂�
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
            // ���I���Đ���
            int rand = Random.Range(0, enemysArray.Count);
            StartCoroutine(SpawnEnemies(rand));
        }
        else intervalSpawn -= Time.deltaTime;
    }

    /// <summary>
    /// �G�̐���
    /// </summary>
    /// <param name="go">��������G</param>
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
