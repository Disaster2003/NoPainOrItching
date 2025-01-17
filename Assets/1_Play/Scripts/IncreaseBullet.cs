using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBullet : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastVelocity;
    private Quaternion ANGLE_BOUNCE = Quaternion.Euler(0, 0, 30);

    [SerializeField] private GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントの取得
        rb = GetComponent<Rigidbody>();

        // 重力落下、z移動、回転の禁止
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        // より広い方へ飛ばす
        Vector3 velocity = Vector3.zero;
        if (transform.position.x < 0) velocity += Vector3.right;
        else velocity += Vector3.left;
        if (transform.position.y < 0) velocity += Vector3.up;
        else velocity += Vector3.down;
        rb.AddForce(velocity, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // 速度の取得
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("Wall")) return;

        // nullチェック
        if(clone == null)
        {
            Debug.LogError("クローンが未設定です");
            return;
        }

        // 反射速度
        Vector3 reflectVelocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);

        if (Vector3.Dot(ANGLE_BOUNCE * reflectVelocity, collision.contacts[0].normal) >= 0)
        {
            // 反対に設定した角度分反射
            ReflectObject(ANGLE_BOUNCE * reflectVelocity);
        }
        else
        {
            // 真反対に反射
            ReflectObject(reflectVelocity);
        }

        // クローンの生成
        Instantiate(clone, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// オブジェクトを反射させる
    /// </summary>
    /// <param name="_reflectVelocity">反射速度・角度</param>
    private void ReflectObject(Vector3 _reflectVelocity)
    {
        rb.velocity = _reflectVelocity;
        clone.GetComponent<Rigidbody>().velocity = rb.velocity;
    }
}
