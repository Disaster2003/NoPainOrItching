using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerComponent : MonoBehaviour
{
    Vector3 inputMove;

    private ActionControll AC;

    // Start is called before the first frame update
    void Start()
    {
        inputMove = Vector3.zero;

        AC = new ActionControll(); // �C���v�b�g�A�N�V�������擾
        AC.Player.Move.started += OnMove; // �S�ẴA�N�V�����ɃC�x���g��o�^
        AC.Player.Move.performed += OnMove;
        AC.Player.Move.canceled += OnMove;
        AC.Enable(); // InputAction���@�\������ׂɗL��������B
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
}
