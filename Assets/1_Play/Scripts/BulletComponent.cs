using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float speedMove = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // トリガー化
        GetComponent<SphereCollider>().isTrigger = true;

        // 重力落下、z移動、回転の禁止
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        if (transform.position.x > 3.0f) Destroy(gameObject);
        else transform.Translate(speedMove * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Player") Destroy(gameObject);
    }
}
