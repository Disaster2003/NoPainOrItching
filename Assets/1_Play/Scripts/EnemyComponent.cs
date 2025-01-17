using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    private Vector3 SCALE_BASE = new Vector3(0.3f, 0.3f, 0.3f);

    private enum STATE_ENEMY
    {
        SIN_Y,  // sin波をy座標に適応
        SIN_Z,  // sin波をz座標に適応
        CIRCLE, // 円状
        SHAKE,  // 揺れ?
        RAIN,   // 雨
        CHAOS,  // カオス
    }
    [SerializeField] private STATE_ENEMY state_enemy;

    public Vector3 POSITION_START;

    private float time,
        radian,
        radius,
        timerDistance;
    private int plusAndMinus;

    // Start is called before the first frame update
    void Start()
    {
        // 大きさの初期化
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // トリガー化
        GetComponent<BoxCollider>().isTrigger = true;

        // コンポーネントの取得
        Rigidbody rb = GetComponent<Rigidbody>();

        // 重力落下、回転禁止
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // 位置の初期化
        transform.position = POSITION_START;

        radian = 0;
        radius = 0;

        // 正負の判断
        switch(state_enemy)
        {
            case STATE_ENEMY.SIN_Y:
                plusAndMinus = (POSITION_START.y < 1) ? 1 : -1;
                break;
            case STATE_ENEMY.SIN_Z:
            case STATE_ENEMY.SHAKE:
                timerDistance = POSITION_START.y;
                plusAndMinus = (POSITION_START.y % 1.0f == 0) ? 1 : -1;
                break;
            case STATE_ENEMY.CIRCLE:
            case STATE_ENEMY.RAIN:
            case STATE_ENEMY.CHAOS:
                timerDistance = POSITION_START.x;

                if(POSITION_START.x < 0) plusAndMinus = 1;
                else plusAndMinus = -1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.sqrMagnitude < SCALE_BASE.sqrMagnitude)
        {
            // 拡大
            transform.localScale = Vector3.MoveTowards(transform.localScale, SCALE_BASE, Time.deltaTime);
            return;
        }

        float sin = 0.5f * Mathf.Sin(2 * Time.time);

        switch (state_enemy)
        {
            case STATE_ENEMY.SIN_Y:
                if (transform.position.x <= -3.0f) Destroy(gameObject);
                
                // 移動
                transform.Translate(-Time.deltaTime, 0, 0);
                
                // sin波をy座標に適用
                transform.position =
                    new Vector3
                    (
                        transform.position.x,
                        POSITION_START.y + plusAndMinus * sin,
                        POSITION_START.z
                    );
                break;
            case STATE_ENEMY.SIN_Z:
                if (transform.position.x <= -3.0f) Destroy(gameObject);

                // 移動
                transform.Translate(-Time.deltaTime, 0, 0);

                // sin波をz座標に適応
                transform.position =
                    new Vector3
                    (
                        transform.position.x,
                        POSITION_START.y,
                        POSITION_START.z + plusAndMinus * sin
                    );
                break;
            case STATE_ENEMY.CIRCLE:
                if (POSITION_START.x < 0)
                {
                    if (transform.position.x >= 3.0f)
                    {
                        Destroy(gameObject);
                    }
                }
                else if (transform.position.x <= -3.0f) Destroy(gameObject);

                // 円運動
                transform.position =
                    new Vector3
                    (
                        POSITION_START.x + plusAndMinus * Mathf.Cos(radian) * radius,
                        POSITION_START.y + Mathf.Sin(radian) * radius,
                        POSITION_START.z
                    );
                radian += 2 * Time.deltaTime;
                radius += 0.3f * Time.deltaTime;
                break;
            case STATE_ENEMY.SHAKE:
                if (timerDistance < 2)
                {
                    timerDistance += Time.deltaTime;
                    return;
                }
                if (Mathf.Abs(transform.position.x) >= 3.0f) Destroy(gameObject);

                // ゆらゆら
                time += Time.deltaTime;
                transform.position = new Vector3(plusAndMinus * time * sin, POSITION_START.y, POSITION_START.z);
                break;
            case STATE_ENEMY.RAIN:
                if (timerDistance < 2)
                {
                    timerDistance += Time.deltaTime;
                    return;
                }
                if (transform.position.y <= 0) Destroy(gameObject);

                // 落下
                transform.Translate(0, -Time.deltaTime, 0);
                break;
            case STATE_ENEMY.CHAOS:
                if (Mathf.Abs(timerDistance) < 2)
                {
                    if (timerDistance >= 0) timerDistance += Time.deltaTime;
                    else timerDistance += -Time.deltaTime;
                    return;
                }
                if (transform.position.y <= 0) Destroy(gameObject);

                // 交差するように落下
                transform.Translate(-POSITION_START.x * Time.deltaTime, 0.75f * -Time.deltaTime, 0);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Bullet")) Destroy(gameObject);
    }
}
