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

        AC = new ActionControll(); // インプットアクションを取得
        AC.Player.Move.started += OnMove; // 全てのアクションにイベントを登録
        AC.Player.Move.performed += OnMove;
        AC.Player.Move.canceled += OnMove;
        AC.Enable(); // InputActionを機能させる為に有効化する。
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
}
