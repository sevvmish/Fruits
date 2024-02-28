using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    public CellTypes cellType { get; private set; }

    [SerializeField] private GameObject fruit1;
    [SerializeField] private GameObject fruit2;
    
    public void SetData(CellTypes _type)
    {
        cellType = _type;

        switch(cellType)
        {
            case CellTypes.fruit1:
                fruit1.SetActive(true);
                break;

            case CellTypes.fruit2:
                fruit2.SetActive(true);
                break;
        }

        
        
    }
}

public enum CellTypes
{
    none,
    fruit1,
    fruit2
}
