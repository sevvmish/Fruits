using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssetManager : MonoBehaviour
{
    [SerializeField] private GameObject cellExample;
    [SerializeField] private GameObject fruit1;
    [SerializeField] private GameObject fruit2;
    [SerializeField] private GameObject fruit3;
    [SerializeField] private GameObject fruit4;
    [SerializeField] private GameObject fruit5;
    [SerializeField] private GameObject fruit6;

    private ObjectPool cellPool;
    private ObjectPool fruit1Pool;
    private ObjectPool fruit2Pool;
    private ObjectPool fruit3Pool;
    private ObjectPool fruit4Pool;
    private ObjectPool fruit5Pool;
    private ObjectPool fruit6Pool;

    // Start is called before the first frame update
    public void SetData()
    {
        cellPool = new ObjectPool(Globals.POOL_LIMIT * 6, cellExample, transform);
        fruit1Pool = new ObjectPool(Globals.POOL_LIMIT, fruit1, transform);
        fruit2Pool = new ObjectPool(Globals.POOL_LIMIT, fruit2, transform);
        fruit3Pool = new ObjectPool(Globals.POOL_LIMIT, fruit3, transform);
        fruit4Pool = new ObjectPool(Globals.POOL_LIMIT, fruit4, transform);
        fruit5Pool = new ObjectPool(Globals.POOL_LIMIT, fruit5, transform);
        fruit6Pool = new ObjectPool(Globals.POOL_LIMIT, fruit6, transform);
    }

    public CellControl GetCell()
    {
        return cellPool.GetObject().GetComponent<CellControl>();
    }

    public void ReturnCell(GameObject g)
    {
        cellPool.ReturnObject(g);
    }

    public GameObject GetFruitByType(CellTypes _type)
    {
        switch(_type)
        {
            case CellTypes.fruit1:
                return fruit1Pool.GetObject();

            case CellTypes.fruit2:
                return fruit2Pool.GetObject();

            case CellTypes.fruit3:
                return fruit3Pool.GetObject();

            case CellTypes.fruit4:
                return fruit4Pool.GetObject();

            case CellTypes.fruit5:
                return fruit5Pool.GetObject();

            case CellTypes.fruit6:
                return fruit6Pool.GetObject();
        }

        throw new NotImplementedException();
    }

    public void ReturnFruitByType(CellTypes _type, GameObject g)
    {
        switch (_type)
        {
            case CellTypes.fruit1:
                fruit1Pool.ReturnObject(g);
                break;

            case CellTypes.fruit2:
                fruit2Pool.ReturnObject(g);
                break;

            case CellTypes.fruit3:
                fruit3Pool.ReturnObject(g);
                break;

            case CellTypes.fruit4:
                fruit4Pool.ReturnObject(g);
                break;

            case CellTypes.fruit5:
                fruit5Pool.ReturnObject(g);
                break;

            case CellTypes.fruit6:
                fruit6Pool.ReturnObject(g);
                break;
        }
    }

}
