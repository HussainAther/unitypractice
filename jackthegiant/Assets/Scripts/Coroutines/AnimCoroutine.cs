using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimCoroutine
{
    // this is for avoiding stardard wait coroutine to be affected with setting Time.setScale to 0
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < (start + time))
            yield return null;
    }
}
