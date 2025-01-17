using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponent : MonoBehaviour
{
    private Vector3 inputMove;

    private ActionControll AC;

    [SerializeField] private GameObject bullet;
    private bool isShot; // true = 発射中, false = 未発射

    [SerializeField, Header("弾の生成を行うx座標")]
    private float position_xSpawnBullet = 0.3f;
    [SerializeField, Header("弾の発射間隔(秒)")]
    private float INTERVAL_SHOT = 0.3f;
    private float intervalShot;

    private static int hp = 5;
    /// <summary>
    /// hpを取得する
    /// </summary>
    public static int Hp
    { 
        get { return hp; }
        set { hp = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // トリガー化
        GetComponent<BoxCollider>().isTrigger = true;

        // コンポーネントの取得
        Rigidbody rb = GetComponent<Rigidbody>();

        // 重力落下、z移動、回転の禁止
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        
        inputMove = Vector3.zero;

        AC = new ActionControll(); // インプットアクションを取得
        AC.Player.Move.started += OnMove; // 全てのアクションにイベントを登録
        AC.Player.Move.performed += OnMove;
        AC.Player.Move.canceled += OnMove;
        AC.Player.Shot.started += OnShot;
        AC.Player.Shot.canceled += OnShot;
        AC.Enable(); // InputActionを機能させる為に有効化する。

        isShot = false;
        intervalShot = 0;

        hp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // 弾の発射
        if (isShot) SpawnBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        // ダメージ
        hp--;

        // ゲーム終了
        if (hp <= 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        AC.Disable(); // インプットアクションを破棄
    }

    /// <summary>
    /// Moveアクションの入力を取得する
    /// </summary>
    /// <param name="context">Moveアクションの入力値</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        // 十分にジョイスティックが倒れていない判定
        if (inputMove.sqrMagnitude < 0.01f) return;

        // 入力方向へ移動する
        transform.position += inputMove.normalized * Time.fixedDeltaTime;

        // 移動制限
        transform.position =
            new Vector3
            (
                Mathf.Clamp(transform.position.x, -2.5f, 2.5f),
                Mathf.Clamp(transform.position.y, -0.5f, 2.5f),
                transform.position.z
            );
    }

    /// <summary>
    /// Shotアクションの入力を取得する
    /// </summary>
    /// <param name="context">Shotアクションの入力</param>
    private void OnShot(InputAction.CallbackContext context)
    {
        // nullチェック
        if(bullet == null)
        {
            Debug.LogError("プレイヤーの弾が未設定です");
            return;
        }

        isShot ^= true;
    }

    /// <summary>
    /// 弾の生成
    /// </summary>
    private void SpawnBullet()
    {
        if (intervalShot <= 0)
        {
            intervalShot = INTERVAL_SHOT;

            Instantiate
            (
                bullet,
                transform.position + new Vector3(position_xSpawnBullet, 0),
                Quaternion.identity
            );
        }
        else intervalShot -= Time.deltaTime;
    }
}
