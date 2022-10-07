using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Vector3 wantedCamPos;
    Vector3 wantedCamDirection;
    [SerializeField] private float camMoveSpeed;
    [SerializeField] private float camRotateSpeed;
    Camera currentCam;

    ActionHandler HorizontalMovementHandler;

    // Start is called before the first frame update
    void Start()
    {
        currentCam = Camera.main;
        wantedCamPos = currentCam.transform.position;
        
        HorizontalMovementHandler = new ActionHandler
        (
            new ActionBinding(PressType.Hold, KeyCode.W, () => { MoveWantedCameraPos(currentCam.transform.forward); }),
            new ActionBinding(PressType.Hold, KeyCode.D, () => { MoveWantedCameraPos(currentCam.transform.right); }),
            new ActionBinding(PressType.Hold, KeyCode.A, () => { MoveWantedCameraPos(-currentCam.transform.right); }),
            new ActionBinding(PressType.Hold, KeyCode.S, () => { MoveWantedCameraPos(-currentCam.transform.forward); })
        );
    }

    void MoveWantedCameraPos(Vector3 direction)
    {
        var dir = direction;
        dir.y = 0;
        wantedCamPos.TranslateNormalized(dir, camMoveSpeed * Time.deltaTime);
    }

    

    void RotateCamera()
    {
        wantedCamDirection.x += Input.GetAxis("Mouse X");
        wantedCamDirection.y += Input.GetAxis("Mouse Y");
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovementHandler.ProcessAll();
        wantedCamPos.y += Input.GetAxis("Mouse ScrollWheel") * 3f;
        Mathf.Clamp(wantedCamPos.y, -20, 20);
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RotateCamera();
            currentCam.transform.localRotation = Quaternion.Euler(-wantedCamDirection.y, wantedCamDirection.x,0);
        }


    }
    
    private void LateUpdate()
    {
        currentCam.transform.position = wantedCamPos;
    }
}

public static class Vector3Extensions
{
    public static void TranslateNormalized(this ref Vector3 vector, Vector3 direction, float speed)
    {
        vector += direction.normalized * speed; 
    }
}



public class ActionHandler
{
    private List<ActionBinding> actionsBinds = new List<ActionBinding>();

    public ActionHandler(params ActionBinding[] actions)
    {
        foreach (var action in actions)
        {
            actionsBinds.Add(action);
        }
    }

    public void ProcessAll()
    {
        foreach (var actionBind in actionsBinds)
        {
            actionBind.Process();
        }
    }

}

public enum PressType
{
    Press,
    Hold,
    Release
}
public class ActionBinding
{
    public ActionBinding(PressType pressType, KeyCode keyCode, Action actionToExecute)
    {
        this.pressType = pressType;
        this.keyCode = keyCode;
        this.actionToExecute = actionToExecute;
    }

    private PressType pressType;
    private KeyCode keyCode;
    private Action actionToExecute;

    public void Process()
    {
        switch (pressType)
        {
            case PressType.Press:
                if (Input.GetKeyDown(keyCode))
                {
                    actionToExecute?.Invoke();
                }
                break;
            case PressType.Hold:
                if (Input.GetKey(keyCode))
                {
                    actionToExecute?.Invoke();
                }
                break;
            case PressType.Release:
                if (Input.GetKeyUp(keyCode))
                {
                    actionToExecute?.Invoke();
                }
                break;
        }

    }

    public bool ProcessMultiInput(List<KeyCode> keyCodes)
    {
        var wasExectued = false;
        var pressCount = 0;
        switch (pressType)
        {
            case PressType.Press:
                foreach (var keyCode in keyCodes)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        pressCount++;
                    }
                }
                if (pressCount == keyCodes.Count)
                {
                    actionToExecute?.Invoke();
                    wasExectued = true;
                }
                break;
            case PressType.Hold:
                foreach (var keyCode in keyCodes)
                {
                    if (Input.GetKey(keyCode))
                    {
                        pressCount++;
                    }
                }
                if (pressCount == keyCodes.Count)
                {
                    actionToExecute?.Invoke();
                    wasExectued = true;
                }

                break;
            case PressType.Release:
                foreach (var keyCode in keyCodes)
                {
                    if (Input.GetKeyUp(keyCode))
                    {
                        pressCount++;
                    }
                }
                if (pressCount == keyCodes.Count)
                {
                    actionToExecute?.Invoke();
                    wasExectued = true;
                }
                break;
        }
        return wasExectued;
    }

}


