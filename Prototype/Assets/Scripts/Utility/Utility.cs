using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = Object.FindFirstObjectByType<T>();
                
                if(m_instance == null)
                {
                    GameObject gObject = new GameObject(typeof(T).Name);
                    m_instance = gObject.AddComponent<T>();
                }
            }

            return m_instance;
        }
    }
}

public static partial class Utility
{
    public static IEnumerator UDelay(float time, System.Action onEnd)
    {
        yield return new WaitForSeconds(time);

        onEnd.UInvoke();
    }

    public static IEnumerator UWait(System.Func<bool> onWait, System.Action onEnd)
    {
        while(onWait.UInvoke())
        {
            yield return null;
        }

        onEnd.UInvoke();
    }
}
