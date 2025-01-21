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
        // �R���|�[�l���g�̎擾
        rb = GetComponent<Rigidbody>();

        // �d�͗����Az�ړ��A��]�̋֎~
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        // ���L�����֔�΂�
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
            // ���x�̎擾
            lastVelocity = rb.velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("Wall")) return;

        // null�`�F�b�N
        if(clone == null)
        {
            Debug.LogError("�N���[�������ݒ�ł�");
            return;
        }

        // ���ˑ��x
        Vector3 reflectVelocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);

        Quaternion angleReflection = Quaternion.Euler(0, 0, Random.Range(15, 60));

        if (Vector3.Dot(angleReflection * reflectVelocity, collision.contacts[0].normal) >= 0)
        {
            // ���΂ɐݒ肵���p�x������
            ReflectObject(angleReflection * reflectVelocity);
        }
        else
        {
            // �^���΂ɔ���
            ReflectObject(reflectVelocity);
        }

        // �N���[���̐���
        Instantiate(clone, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// �I�u�W�F�N�g�𔽎˂�����
    /// </summary>
    /// <param name="_reflectVelocity">���ˑ��x�E�p�x</param>
    private void ReflectObject(Vector3 _reflectVelocity)
    {
        rb.velocity = _reflectVelocity;
        clone.GetComponent<Rigidbody>().velocity = rb.velocity;
    }
}
