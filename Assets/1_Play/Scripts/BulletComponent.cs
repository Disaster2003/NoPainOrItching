using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    private float speedMove = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // �g���K�[��
        GetComponent<SphereCollider>().isTrigger = true;

        // �d�͗����Az�ړ��A��]�̋֎~
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        if (transform.position.x > 3.0f) Destroy(gameObject);
        else transform.Translate(speedMove * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Player") Destroy(gameObject);
    }
}
