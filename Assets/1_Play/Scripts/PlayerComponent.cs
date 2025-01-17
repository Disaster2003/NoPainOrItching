using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponent : MonoBehaviour
{
    private Vector3 inputMove;

    private ActionControll AC;

    [SerializeField] private GameObject bullet;
    private bool isShot; // true = ���˒�, false = ������

    [SerializeField, Header("�e�̐������s��x���W")]
    private float position_xSpawnBullet = 0.3f;
    [SerializeField, Header("�e�̔��ˊԊu(�b)")]
    private float INTERVAL_SHOT = 0.3f;
    private float intervalShot;

    private static int hp = 5;
    /// <summary>
    /// hp���擾����
    /// </summary>
    public static int Hp
    { 
        get { return hp; }
        set { hp = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // �g���K�[��
        GetComponent<BoxCollider>().isTrigger = true;

        // �R���|�[�l���g�̎擾
        Rigidbody rb = GetComponent<Rigidbody>();

        // �d�͗����Az�ړ��A��]�̋֎~
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        
        inputMove = Vector3.zero;

        AC = new ActionControll(); // �C���v�b�g�A�N�V�������擾
        AC.Player.Move.started += OnMove; // �S�ẴA�N�V�����ɃC�x���g��o�^
        AC.Player.Move.performed += OnMove;
        AC.Player.Move.canceled += OnMove;
        AC.Player.Shot.started += OnShot;
        AC.Player.Shot.canceled += OnShot;
        AC.Enable(); // InputAction���@�\������ׂɗL��������B

        isShot = false;
        intervalShot = 0;

        hp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // �e�̔���
        if (isShot) SpawnBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        // �_���[�W
        hp--;

        // �Q�[���I��
        if (hp <= 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        AC.Disable(); // �C���v�b�g�A�N�V������j��
    }

    /// <summary>
    /// Move�A�N�V�����̓��͂��擾����
    /// </summary>
    /// <param name="context">Move�A�N�V�����̓��͒l</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        // �\���ɃW���C�X�e�B�b�N���|��Ă��Ȃ�����
        if (inputMove.sqrMagnitude < 0.01f) return;

        // ���͕����ֈړ�����
        transform.position += inputMove.normalized * Time.fixedDeltaTime;

        // �ړ�����
        transform.position =
            new Vector3
            (
                Mathf.Clamp(transform.position.x, -2.5f, 2.5f),
                Mathf.Clamp(transform.position.y, -0.5f, 2.5f),
                transform.position.z
            );
    }

    /// <summary>
    /// Shot�A�N�V�����̓��͂��擾����
    /// </summary>
    /// <param name="context">Shot�A�N�V�����̓���</param>
    private void OnShot(InputAction.CallbackContext context)
    {
        // null�`�F�b�N
        if(bullet == null)
        {
            Debug.LogError("�v���C���[�̒e�����ݒ�ł�");
            return;
        }

        isShot ^= true;
    }

    /// <summary>
    /// �e�̐���
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
