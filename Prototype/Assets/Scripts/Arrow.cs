using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private enum State
    {
        Ready, 
        Shooting,
        Arrival, 
    }

    public float mass;
    public float power;

    private State m_state = State.Ready;

    private Vector3 m_force = Define.VECTOR3_ZERO;

    private System.Action<Vector3> m_onArrival;

    private readonly Vector3 START_POSITION = new Vector3(0f, 0.5f, 1f);

    public void Init(System.Action<Vector3> onArrival)
    {
        m_onArrival = onArrival;
    }

    public void Play()
    {
        m_state = State.Ready;

        transform.position = START_POSITION;
    }

    public void Stop()
    {
        m_state = State.Arrival;

        m_onArrival.UInvoke(transform.position);
    }

    public void Shoot(Vector3 angle)
    {
        Debug.Log("Arrow.Shoot() angle = " + angle);

        m_force = angle * power;

        m_state = State.Shooting;
    }

    private void FixedUpdate()
    {
        if (m_state == State.Shooting)
        {
            m_force += Define.GRAVITY * mass * Time.fixedDeltaTime;
            transform.position += m_force * Time.fixedDeltaTime;
            transform.forward = m_force.normalized;

            if (transform.position.y < -1f)
            {
                Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Stop();
        }
    }
}
