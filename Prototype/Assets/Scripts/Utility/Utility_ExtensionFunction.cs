using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utility
{
    #region Action
    public static void UInvoke(this System.Action action)
    {
        if (action != null)
        {
            action();
        }
    }

    public static void UInvoke<T>(this System.Action<T> action, T value)
    {
        if (action != null)
        {
            action(value);
        }
    }

    public static void UInvoke<T1, T2>(this System.Action<T1, T2> action, T1 value1, T2 value2)
    {
        if (action != null)
        {
            action(value1, value2);
        }
    }

    public static void UInvoke<T1, T2, T3>(this System.Action<T1, T2, T3> action, T1 value1, T2 value2, T3 value3)
    {
        if (action != null)
        {
            action(value1, value2, value3);
        }
    }

    public static void UInvoke<T1, T2, T3, T4>(this System.Action<T1, T2, T3, T4> action, T1 value1, T2 value2, T3 value3, T4 value4)
    {
        if (action != null)
        {
            action(value1, value2, value3, value4);
        }
    }

    public static void UInvoke<T1, T2, T3, T4, T5>(this System.Action<T1, T2, T3, T4, T5> action, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
    {
        if (action != null)
        {
            action(value1, value2, value3, value4, value5);
        }
    }
    #endregion Action

    #region Func
    public static TResult UInvoke<TResult>(this System.Func<TResult> func)
    {
        if (func != null)
        {
            return func();
        }
        else
        {
            return default(TResult);
        }
    }

    public static TResult UInvoke<T1, TResult>(this System.Func<T1, TResult> func, T1 value1)
    {
        if (func != null)
        {
            return func(value1);
        }
        else
        {
            return default(TResult);
        }
    }

    public static TResult UInvoke<T1, T2, TResult>(this System.Func<T1, T2, TResult> func, T1 value1, T2 value2)
    {
        if (func != null)
        {
            return func(value1, value2);
        }
        else
        {
            return default(TResult);
        }
    }

    public static TResult UInvoke<T1, T2, T3, TResult>(this System.Func<T1, T2, T3, TResult> func, T1 value1, T2 value2, T3 value3)
    {
        if (func != null)
        {
            return func(value1, value2, value3);
        }
        else
        {
            return default(TResult);
        }
    }

    public static TResult UInvoke<T1, T2, T3, T4, TResult>(this System.Func<T1, T2, T3, T4, TResult> func, T1 value1, T2 value2, T3 value3, T4 value4)
    {
        if (func != null)
        {
            return func(value1, value2, value3, value4);
        }
        else
        {
            return default(TResult);
        }
    }

    public static TResult UInvoke<T1, T2, T3, T4, T5, TResult>(this System.Func<T1, T2, T3, T4, T5, TResult> func, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
    {
        if (func != null)
        {
            return func(value1, value2, value3, value4, value5);
        }
        else
        {
            return default(TResult);
        }
    }
    #endregion Func

    #region Vector3
    public static Vector3 X(this Vector3 vector, float x)
    {
        vector.x = x;
        return vector;
    }

    public static Vector3 Y(this Vector3 vector, float y)
    {
        vector.y = y;
        return vector;
    }

    public static Vector3 Z(this Vector3 vector, float z)
    {
        vector.z = z;
        return vector;
    }
    #endregion Vector3

    #region GameObject
    public static void USetActive<T>(this T tValue, bool isActive) where T : Component
    {
        if (tValue != null)
        {
            tValue.gameObject.SetActive(isActive);
        }
    }

    public static void USetActive(this GameObject gameObject, bool isActive)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(isActive);
        }
    }
    #endregion GameObject

    #region UILabel
    public static void UText(this UILabel label, string text)
    {
        if (label != null)
        {
            label.text = text;
        }
    }

    public static void UTextFormat(this UILabel label, string format, params object[] args)
    {
        if (label != null)
        {
            label.text = string.Format(format, args);
        }
    }
    #endregion UILabel

    #region UISprite
    public static void USpriteName(this UISprite sprite, string spriteName)
    {
        if (sprite != null)
        {
            sprite.spriteName = spriteName;
        }
    }
    #endregion UISprite

    #region UITexture
    public static void UMainTexture(this UITexture texture, Texture image)
    {
        if (texture != null)
        {
            texture.mainTexture = image;
        }
    }

    public static void UShader(this UITexture texture, Shader shader)
    {
        if (texture != null)
        {
            texture.shader = shader;
        }
    }
    #endregion UITexture

    #region UISlider
    public static void UValue(this UISlider slider, float value)
    {
        if(slider != null)
        {
            slider.value = value;
        }
    }
    #endregion UISlider

    #region UIEventListener
    public static void UOnClick(this GameObject gameObject, UIEventListener.VoidDelegate onClick)
    {
        if (gameObject != null)
        {
            UIEventListener.Get(gameObject).onClick = onClick;
        }
    }
    #endregion UIEventListener
}
