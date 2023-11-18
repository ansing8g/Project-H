using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
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

    public Camera uiCamera;
    public GameCamera gameCamera;
    public Arrow arrow;
    public UISlider sliderPower;

    public UILabel labelState;
    public UILabel labelLog;

    public float power;
    public float touchRatio;

    private State m_state = State.Ready;
    private int? m_index = null;
    private float m_powerLength = 1f;

    private Ray m_ray;
    private RaycastHit m_raycastHit;

    private Vector3 m_previousPosition = Define.VECTOR3_ZERO;
    private Vector3 m_startAngle = Define.VECTOR3_ZERO;
    private Vector3 m_currentAngle = Define.VECTOR3_ZERO;

    private int m_uiLayer = 0;

    private void Awake()
    {
        m_uiLayer = 1 << LayerMask.NameToLayer("UI");
    }

    private void Start()
    {
        if (arrow != null)
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
        if(uiCamera != null && Input.GetMouseButton(0))
        {
            m_ray = uiCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(m_ray, out m_raycastHit, float.MaxValue, m_uiLayer))
            {
                if(sliderPower != null)
                {
                    sliderPower.UValue(Mathf.Clamp01((Input.mousePosition.y - sliderPower.transform.localPosition.y) / 400f));
                }
            }
            else
            {
#if UNITY_EDITOR
                if (Input.GetMouseButtonDown(0))
                {
                    Aim(Input.mousePosition);
                }

                if (Input.GetMouseButton(0))
                {
                    Aiming(Input.mousePosition, m_powerLength * sliderPower.value);
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
                    Aiming(point, m_powerLength * sliderPower.value);
                }
                else if(m_state == State.Aiming)
                {
                    m_index = null;
                    Shoot(m_powerLength * sliderPower.value);
                }
#endif
            }
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(m_powerLength * sliderPower.value);
        }
#else
        else if(m_state == State.Aiming)
        {
            m_index = null;
            Shoot(m_powerLength * sliderPower.value);
        }
#endif

        Vector3 angle = arrow.transform.rotation.eulerAngles;
        labelState.UTextFormat("( {0:0} , {1:0} )", (angle.y > 180f) ? angle.y - 360f : angle.y, -((angle.x > 180f) ? angle.x - 360f : angle.x));
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