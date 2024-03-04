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
    
    [SerializeField] private GameObject smallBombFruit1;
    [SerializeField] private GameObject smallBombFruit2;
    [SerializeField] private GameObject smallBombFruit3;
    [SerializeField] private GameObject smallBombFruit4;
    [SerializeField] private GameObject smallBombFruit5;
    [SerializeField] private GameObject smallBombFruit6;

    [SerializeField] private GameObject lineBombFruit1;
    [SerializeField] private GameObject lineBombFruit2;
    [SerializeField] private GameObject lineBombFruit3;
    [SerializeField] private GameObject lineBombFruit4;
    [SerializeField] private GameObject lineBombFruit5;
    [SerializeField] private GameObject lineBombFruit6;

    private ObjectPool cellPool;
    
    private ObjectPool fruit1Pool;
    private ObjectPool fruit2Pool;
    private ObjectPool fruit3Pool;
    private ObjectPool fruit4Pool;
    private ObjectPool fruit5Pool;
    private ObjectPool fruit6Pool;
    
    private ObjectPool smallBombFruit1Pool;
    private ObjectPool smallBombFruit2Pool;
    private ObjectPool smallBombFruit3Pool;
    private ObjectPool smallBombFruit4Pool;
    private ObjectPool smallBombFruit5Pool;
    private ObjectPool smallBombFruit6Pool;

    private ObjectPool lineBombFruit1Pool;
    private ObjectPool lineBombFruit2Pool;
    private ObjectPool lineBombFruit3Pool;
    private ObjectPool lineBombFruit4Pool;
    private ObjectPool lineBombFruit5Pool;
    private ObjectPool lineBombFruit6Pool;

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

        smallBombFruit1Pool = new ObjectPool(Globals.POOL_LIMIT / 2 , smallBombFruit1, transform);
        smallBombFruit2Pool = new ObjectPool(Globals.POOL_LIMIT / 2, smallBombFruit2, transform);
        smallBombFruit3Pool = new ObjectPool(Globals.POOL_LIMIT / 2, smallBombFruit3, transform);
        smallBombFruit4Pool = new ObjectPool(Globals.POOL_LIMIT / 2, smallBombFruit4, transform);
        smallBombFruit5Pool = new ObjectPool(Globals.POOL_LIMIT / 2, smallBombFruit5, transform);
        smallBombFruit6Pool = new ObjectPool(Globals.POOL_LIMIT / 2, smallBombFruit6, transform);

        lineBombFruit1Pool = new ObjectPool(Globals.POOL_LIMIT / 2, lineBombFruit1, transform);
        lineBombFruit2Pool = new ObjectPool(Globals.POOL_LIMIT / 2, lineBombFruit2, transform);
        lineBombFruit3Pool = new ObjectPool(Globals.POOL_LIMIT / 2, lineBombFruit3, transform);
        lineBombFruit4Pool = new ObjectPool(Globals.POOL_LIMIT / 2, lineBombFruit4, transform);
        lineBombFruit5Pool = new ObjectPool(Globals.POOL_LIMIT / 2, lineBombFruit5, transform);
        lineBombFruit6Pool = new ObjectPool(Globals.POOL_LIMIT / 2, lineBombFruit6, transform);
    }

    public CellControl GetCell()
    {
        return cellPool.GetObject().GetComponent<CellControl>();
    }

    public void ReturnCell(GameObject g)
    {
        cellPool.ReturnObject(g);
    }

    
    public GameObject GetFruitByType(CellTypes _type, CellActions _action)
    {
        switch(_action)
        {
            case CellActions.simple:
                switch (_type)
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
                break;

            case CellActions.line_explosion_vertical:
                switch (_type)
                {
                    case CellTypes.fruit1:
                        return lineBombFruit1Pool.GetObject();

                    case CellTypes.fruit2:
                        return lineBombFruit2Pool.GetObject();

                    case CellTypes.fruit3:
                        return lineBombFruit3Pool.GetObject();

                    case CellTypes.fruit4:
                        return lineBombFruit4Pool.GetObject();

                    case CellTypes.fruit5:
                        return lineBombFruit5Pool.GetObject();

                    case CellTypes.fruit6:
                        return lineBombFruit6Pool.GetObject();
                }
                break;

            case CellActions.line_explosion_horizontal:
                switch (_type)
                {
                    case CellTypes.fruit1:
                        return lineBombFruit1Pool.GetObject();

                    case CellTypes.fruit2:
                        return lineBombFruit2Pool.GetObject();

                    case CellTypes.fruit3:
                        return lineBombFruit3Pool.GetObject();

                    case CellTypes.fruit4:
                        return lineBombFruit4Pool.GetObject();

                    case CellTypes.fruit5:
                        return lineBombFruit5Pool.GetObject();

                    case CellTypes.fruit6:
                        return lineBombFruit6Pool.GetObject();
                }
                break;

            case CellActions.small_explosion:
                switch (_type)
                {
                    case CellTypes.fruit1:
                        return smallBombFruit1Pool.GetObject();

                    case CellTypes.fruit2:
                        return smallBombFruit2Pool.GetObject();

                    case CellTypes.fruit3:
                        return smallBombFruit3Pool.GetObject();

                    case CellTypes.fruit4:
                        return smallBombFruit4Pool.GetObject();

                    case CellTypes.fruit5:
                        return smallBombFruit5Pool.GetObject();

                    case CellTypes.fruit6:
                        return smallBombFruit6Pool.GetObject();
                }
                break;
        }

        
        throw new NotImplementedException();
    }

    

    public void ReturnFruitByType(CellTypes _type, CellActions _action, GameObject g)
    {
        g.transform.parent = transform;

        switch (_action)
        {
            case CellActions.simple:
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
                break;

            case CellActions.line_explosion_vertical:
                switch (_type)
                {
                    case CellTypes.fruit1:
                        lineBombFruit1Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit2:
                        lineBombFruit2Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit3:
                        lineBombFruit3Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit4:
                        lineBombFruit4Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit5:
                        lineBombFruit5Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit6:
                        lineBombFruit6Pool.ReturnObject(g);
                        break;
                }
                break;

            case CellActions.line_explosion_horizontal:
                switch (_type)
                {
                    case CellTypes.fruit1:
                        lineBombFruit1Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit2:
                        lineBombFruit2Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit3:
                        lineBombFruit3Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit4:
                        lineBombFruit4Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit5:
                        lineBombFruit5Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit6:
                        lineBombFruit6Pool.ReturnObject(g);
                        break;
                }
                break;

            case CellActions.small_explosion:
                switch (_type)
                {
                    case CellTypes.fruit1:
                        smallBombFruit1Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit2:
                        smallBombFruit2Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit3:
                        smallBombFruit3Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit4:
                        smallBombFruit4Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit5:
                        smallBombFruit5Pool.ReturnObject(g);
                        break;

                    case CellTypes.fruit6:
                        smallBombFruit6Pool.ReturnObject(g);
                        break;
                }
                break;
        }

    }

}
