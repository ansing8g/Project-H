using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : Singleton<LineManager>
{
    public LineRenderer lineRenderer;

    private List<Vector3> m_listPoint = new List<Vector3>();

    private Vector3 m_position = Define.VECTOR3_ZERO;
    private Vector3 m_force = Define.VECTOR3_ZERO;
    private const float SIMULATION_TIME = 0.02f;

    public void Play(Arrow arrow, Vector3 angle)
    {
        m_listPoint.Clear();

        if (arrow != null)
        {
            float mass = arrow.mass;
            m_position = arrow.transform.position;
            m_force = angle * arrow.power;

            m_listPoint.Add(m_position);

            while (m_position.y > -1f)
            {
                m_force += (Define.GRAVITY * mass) * SIMULATION_TIME;
                m_position += m_force * SIMULATION_TIME;

                m_listPoint.Add(new Vector3(m_position.x, m_position.y, m_position.z));
            }

            lineRenderer.positionCount = m_listPoint.Count;
            lineRenderer.SetPositions(m_listPoint.ToArray());
        }
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = 0, iMax = m_listPoint.Count; i < iMax; i++)
    //    {
    //        Gizmos.DrawSphere(m_listPoint[i], 1f);
    //    }
    //}
}
