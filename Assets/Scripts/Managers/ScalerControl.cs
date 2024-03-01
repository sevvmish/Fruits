using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerControl : MonoBehaviour
{
    public static ScalerControl Instance { get; private set; }

    private List<Transform> scalers = new List<Transform>();
    
    private float _time;

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
        if (!scalers.Contains(t)) scalers.Add(t);
    }

    public void Remove(Transform t)
    {
        if (scalers.Contains(t)) scalers.Remove(t);
    }

    private void Update()
    {
        if (_time > 1)
        {
            _time = 0;
            shakeAll();
        }
        else
        {
            _time += Time.deltaTime;
        }
    }

    private void shakeAll()
    {
        if (scalers.Count > 0)
        {            
            for (int i = 0; i < scalers.Count; i++)
            {
                scalers[i].DOPunchScale(Vector3.one * 1.25f, 0.2f).SetEase(Ease.InOutSine);
            }
        }        
    }
}
