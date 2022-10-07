using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    bool blinking;
    MeshRenderer rend;
    [SerializeField] Material hitMat, notHitMat;
    WaitForSeconds waitTime = new WaitForSeconds(1f);

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public void SetHit()
    {
        if(!blinking)
        {
            StartCoroutine(Blink());
        }
    }

    public IEnumerator Blink()
    {
        blinking = true;
        rend.material = hitMat;
        yield return waitTime;
        rend.material = notHitMat;
        blinking = false; 
    }
}
