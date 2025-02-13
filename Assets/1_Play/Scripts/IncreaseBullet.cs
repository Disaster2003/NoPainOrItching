using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Vector3 lastVelocity;

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
        if (rb.velocity != Vector3.zero)
        {
            // 速度の取得
            lastVelocity = rb.velocity;
        }
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
        Vector3 velocityReflection = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);

        // 反射角度
        Quaternion angleReflection = Quaternion.Euler(0, 0, Random.Range(15, 60));

        if (Vector3.Dot(angleReflection * velocityReflection, collision.contacts[0].normal) >= 0)
        {
            // 反対に設定した角度分反射
            ReflectObject(angleReflection * velocityReflection);
        }
        else
        {
            // 真反対に反射
            ReflectObject(velocityReflection);
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
    }
}
