using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out var hit,100000f,Physics.AllLayers))
        {

            var blinker = hit.transform.GetComponent<Blinker>();
            blinker.SetHit();
            
        }
        Debug.DrawRay(ray.origin,ray.direction * 100,Color.red);
    }
}
