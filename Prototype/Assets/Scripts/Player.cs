using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static InvBaseItem;

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

    private State m_state = State.Ready;
    private int? m_index = null;

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
        if(m_state == State.Aim)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Aim(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Aiming(Input.mousePosition);
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                LineManager.Instance.Play(arrow, gameCamera.transform.forward);
            }
#else
            if(Input.touchCount == 2)
            {
                if(m_index.HasValue == false)
                {
                    m_index = (Input.touches[0].position.y > Input.touches[1].position.y) ? 0 : 1;
                }

                Vector3 point = Input.touches[m_index.Value].position;
                Aim(point);
                Aiming(point);
            }
            else if(m_state == State.Aiming)
            {
                m_index = null;
                Shoot();
            }
#endif
        }
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

    private void Aiming(Vector3 point)
    {
        if (m_state == State.Aiming)
        {
            m_currentAngle = m_startAngle + ((point - m_previousPosition) * 0.1f);
            arrow.transform.rotation = gameCamera.transform.rotation = Quaternion.Euler(-m_currentAngle.y, m_currentAngle.x, 0f);
        }
    }

    private void Shoot()
    {
        m_state = State.Shoot;

        if (arrow != null)
        {
            arrow.Shoot(gameCamera.transform.forward);
        }
    }

    private void OnArrival(Vector3 position)
    {
        Play();
    }
}
