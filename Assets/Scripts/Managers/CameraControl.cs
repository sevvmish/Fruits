using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera mainCamera;

    public void SetData(Camera c)
    {
        mainCamera = c;

        UpdateCamera();
    }

    public void UpdateCamera()
    {
        
        if (Globals.MainPlayerData.AdvOff)
        {
            if (Globals.FieldDimention.x == 6)
            {
                mainCamera.orthographicSize = 7;
            }
            else if (Globals.FieldDimention.x == 7)
            {
                mainCamera.orthographicSize = 8;
            }
        }
        else
        {
            if (Globals.FieldDimention.x == 6)
            {
                mainCamera.orthographicSize = 6;
            }
            else if (Globals.FieldDimention.x == 7)
            {
                mainCamera.orthographicSize = 7;
            }
        }
            
        
    }
}
