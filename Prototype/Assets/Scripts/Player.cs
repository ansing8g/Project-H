using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Ready, 
        Aim,
        Aiming, 
        Shoot, 
        Shooting,
        Arrival, 
    }

    public GameCamera gameCamera;
    public Arrow arrow;
    public Transform objectPower;

    public UILabel labelState;
    public UILabel labelLog;

    public float power;
    public float touchRatio;

    private State m_state = State.Ready;
    private int? m_index = null;
    private float m_powerLength = 1f;

    private Vector3 m_previousPosition = Define.VECTOR3_ZERO;
    private Vector3 m_startAngle = Define.VECTOR3_ZERO;
    private Vector3 m_currentAngle = Define.VECTOR3_ZERO;

    private void Start()
    {
        if(arrow != null)
        {
            arrow.Init(OnArrival);
        }

        Play();

        LineManager.Instance.Hide();
    }

    private void Play()
    {
        m_state = State.Aim;

        if (arrow != null)
        {
            arrow.Play();
            arrow.transform.rotation = gameCamera.transform.rotation;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Aim(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Aiming(Input.mousePosition, m_powerLength);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(m_powerLength);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LineManager.Instance.Play(arrow, gameCamera.transform.forward, power);
        }
#else
        if(Input.touchCount == 2)
        {
            if(m_index.HasValue == false)
            {
                m_index = (Input.touches[0].position.y > Input.touches[1].position.y) ? 0 : 1;
            }

            Vector3 point = Input.touches[m_index.Value].position;
            m_powerLength = (Input.touches[0].position - Input.touches[1].position).magnitude / touchRatio;
            Aim(point);
            Aiming(point, m_powerLength);
        }
        else if(m_state == State.Aiming)
        {
            m_index = null;
            Shoot(m_powerLength);
        }
#endif
        //labelState.UText(m_state.ToString());
    }

    private void Aim(Vector3 point)
    {
        if (m_state == State.Aim)
        {
            m_previousPosition = point;
            m_startAngle = m_currentAngle;

            m_state = State.Aiming;
        }
    }

    private void Aiming(Vector3 point, float powerLength)
    {
        if (m_state == State.Aiming)
        {
            if(objectPower != null)
            {
                objectPower.localScale = Mathf.Clamp01(powerLength) * Vector3.one;
            }

            m_currentAngle = m_startAngle + ((point - m_previousPosition) * 0.1f);
            arrow.transform.rotation = gameCamera.transform.rotation = Quaternion.Euler(-m_currentAngle.y, m_currentAngle.x, 0f);
        }
    }

    private void Shoot(float powerLength)
    {
        m_state = State.Shoot;

        powerLength = Mathf.Clamp01(powerLength);

        if (arrow != null)
        {
            arrow.Shoot(gameCamera.transform.forward, power * powerLength);
        }

        LineManager.Instance.Hide();
        LineManager.Instance.Play(arrow, gameCamera.transform.forward, power * powerLength);
    }

    private void OnArrival(Vector3 position)
    {
        Play();

        LineManager.Instance.Show();
    }
}
