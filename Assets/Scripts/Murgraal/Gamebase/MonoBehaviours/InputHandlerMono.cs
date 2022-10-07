using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerMono : MonoBehaviour
{
    void Update()
    {
        InputHandler.HandleInputs();
    }
}

public static class InputHandler
{ 
    private static InputData data;
    public static InputData Data => data;
    
    public static void HandleInputs()
    {
        data.DirectionalInput.x = Input.GetAxis("Horizontal");
        data.DirectionalInput.y = Input.GetAxis("Vertical");
        data.Fire = Input.GetButtonDown("Fire");
        data.Interact = Input.GetButtonDown("Interact");
        data.Jump = Input.GetButtonDown("Jump");
    }
}

public struct InputData
{
    public Vector2 DirectionalInput;
    public bool Interact;
    public bool Jump;
    public bool Fire;
}
