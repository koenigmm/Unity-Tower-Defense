using System;
using UnityEngine;

//TODO obsolet?
public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDestroyed;
    

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
        print("destroy");
    }

    public void DestroyTarget()
    {
        Destroy(this);
    }
}
