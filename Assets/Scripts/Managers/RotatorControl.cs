using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorControl : MonoBehaviour
{
    public static RotatorControl Instance { get; private set; }

    private List<Transform> rotators = new List<Transform>();
    private float y;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Add(Transform t)
    {
        if (!rotators.Contains(t)) rotators.Add(t);
    }

    public void Remove(Transform t)
    {
        if (rotators.Contains(t)) rotators.Remove(t);
    }

    private void Update()
    {
        
        //if (y > 360) y = 0;

        if (rotators.Count > 0)
        {
            y += Time.deltaTime * 120;

            for (int i = 0; i < rotators.Count; i++)
            {
                rotators[i].localEulerAngles = new Vector3(rotators[i].localEulerAngles.x, y, rotators[i].localEulerAngles.z);
            }
        }
        else
        {
            y = 0;
        }
    }
}
