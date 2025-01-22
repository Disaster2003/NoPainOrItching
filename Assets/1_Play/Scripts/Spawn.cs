using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject enemysSin_y;
    [SerializeField] private GameObject enemysSin_z;
    [SerializeField] private GameObject enemysCircle;
    [SerializeField] private GameObject enemysShake;
    [SerializeField] private GameObject enemysRain;
    [SerializeField] private GameObject enemysChaos;
    private List<GameObject> enemysArray = new List<GameObject>();
    [SerializeField] private TextAsset enemy_status;
    private Dictionary<int, List<Vector3>> positionsStart = new Dictionary<int, List<Vector3>>();

    [SerializeField, Header("敵の生成間隔(秒)")]
    private float INTERVAL_SPAWN = 5.0f;
    private float intervalSpawn;
    private bool isFinished; // true = 生成終了, false = 未生成

    [SerializeField] GameObject bulletIncrease;

    // Start is called before the first frame update
    void Start()
    {
        // 敵の種類をひとまとめに
        enemysArray.Add(enemysSin_y);
        enemysArray.Add(enemysSin_z);
        enemysArray.Add(enemysCircle);
        enemysArray.Add(enemysShake);
        enemysArray.Add(enemysRain);
        enemysArray.Add(enemysChaos);

        // csvの読み込み
        StringReader reader = new StringReader(enemy_status.text); // TextAssetをStringReaderに変換
        List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            csvData.Add(line.Split(',')); // csvDataリストに追加する
        }

        // 動的確保
        for(int i = 0; i < enemysArray.Count; i++)
        {
            positionsStart[i] = new List<Vector3>();
        }

        // データの代入
        for (int i = 1; i < csvData.Count; i++)
        {
            int index = int.Parse(csvData[i][0]);
            float position_x = float.Parse(csvData[i][1]);
            float position_y = float.Parse(csvData[i][2]);
            float position_z = float.Parse(csvData[i][3]);
            Vector3 positionTmp = new Vector3(position_x, position_y, position_z);
            positionsStart[index].Add(positionTmp);
        }

        intervalSpawn = INTERVAL_SPAWN;
        isFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        // nullチェック
        if(bulletIncrease == null)
        {
            Debug.LogError("敵の弾が未設定です");
            return;
        }

        // 次へ
        if (Timer.GetTimer > 90.0f)
        {
            Instantiate(bulletIncrease, new Vector3(-1, 1, -7.5f), Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (!isFinished)
        {
            if (intervalSpawn <= 0)
            {
                isFinished = true;

                // 抽選して生成
                int rand = Random.Range(0, enemysArray.Count);
                SpawnEnemies(rand);
            }
            else intervalSpawn -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    /// <param name="rand">抽選で選ばれた数</param>
    private void SpawnEnemies(int rand)
    {
        for (int i = 0; i < positionsStart[rand].Count; i++)
        {
            enemysArray[rand].GetComponent<EnemyComponent>().POSITION_START = positionsStart[rand][i];
            Instantiate(enemysArray[rand]);
        }

        intervalSpawn = INTERVAL_SPAWN;
        isFinished = false;
    }
}
