using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    private enum STATE_ENEMY
    {
        SIN_Y,  // sin�g��y���W�ɓK��
        SIN_Z,  // sin�g��z���W�ɓK��
        CIRCLE, // �~��
        SHAKE,  // �h��?
        RAIN,   // �J
        CHAOS,  // �J�I�X
    }
    [SerializeField] private STATE_ENEMY state_enemy;

    private Vector3 positionStart;

    private float time;
    private int plusAndMinus;

    private float radian, radius;

    // Start is called before the first frame update
    void Start()
    {
        // �g���K�[��
        GetComponent<BoxCollider>().isTrigger = true;

        //transform.position = positionStart;
        if(transform.position.x < 0) plusAndMinus = 1;
        else plusAndMinus = -1;

        radian = 0;
        radius = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float sin = 0.5f * Mathf.Sin(2 * Time.time);

        switch (state_enemy)
        {
            case STATE_ENEMY.SIN_Y:
                if (transform.position.x <= -3.0f) Destroy(gameObject);

                transform.Translate(-Time.deltaTime, 0, 0);
                transform.position = new Vector3(transform.position.x, positionStart.y + sin, positionStart.z);
                break;
            case STATE_ENEMY.SIN_Z:
                if (transform.position.x <= -3.0f) Destroy(gameObject);

                transform.Translate(-Time.deltaTime, 0, 0);
                transform.position = new Vector3(transform.position.x, positionStart.y, positionStart.z + sin);
                break;
            case STATE_ENEMY.CIRCLE:
                if (positionStart.x < 0)
                {
                    if (transform.position.x >= 3.0f)
                    {
                        Destroy(gameObject);
                    }
                }
                else if (transform.position.x <= -3.0f) Destroy(gameObject);

                transform.position = new Vector3(positionStart.x + Mathf.Cos(radian) * radius, positionStart.y + Mathf.Sin(radian) * radius, -7.5f);
                radian += 2 * Time.deltaTime;
                radius += 0.3f * Time.deltaTime;
                break;
            case STATE_ENEMY.SHAKE:
                if (Mathf.Abs(transform.position.x) >= 3.0f) Destroy(gameObject);

                time += Time.deltaTime;
                transform.position = new Vector3(time * sin, positionStart.y, positionStart.z);
                break;
            case STATE_ENEMY.RAIN:
                if (transform.position.x <= 0) Destroy(gameObject);

                transform.Translate(0, -Time.deltaTime, 0);
                break;
            case STATE_ENEMY.CHAOS:
                if (transform.position.y <= 0) Destroy(gameObject);

                transform.Translate(plusAndMinus * Time.deltaTime, -Time.deltaTime, 0);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Bullet")) Destroy(gameObject);
    }
}
