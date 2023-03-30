using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFiringScript : MonoBehaviour
{
    //Inspired by Jakob Knop Rasmussen
    public event Action OnFire = delegate { };

    [SerializeField]
    private float waitTime = 0,5f;
    
    void Awake()
    {
        StartCoroutine(FiringLoop());
    }

    private IEnumerator FiringLoop()
    {
        Debug.Log($"Engaged ice cream launcher at {Time.time}");
        //Should we make the fire rate dynamic by creating the var inside the loop for upgrading towers and such? - Aldís 30.03.23 
        var wait = new WaitForSeconds(waitTime);
        //TODO replace while statement arg with targeting script - Aldís 30.03.23 
        while (true)
        {
            OnFire();
            yield return wait;
        }

        Debug.Log($"Ceased fire at {Time.time}");
    }
}
