using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CellTypes[] ApprovedFruitTypeCells { get; private set; }
    public Dictionary<CellActions, int> ApprovedCellActions { get; private set; }

    private int level;

    public void SetData()
    {
        level = Globals.MainPlayerData.Lvl;

        setLevelData();
    }

    private void setLevelData()
    {
        switch(level)
        {
            case 0:
                Globals.FieldDimention = new Vector2(7, 10);
                Globals.VictoryCondition = new Dictionary<CellTypes, int>
                {
                    {CellTypes.fruit1, 10 },
                    {CellTypes.fruit2, 10 }
                };
                ApprovedFruitTypeCells = approvedSimpleCellsThree1();

                ApprovedCellActions = new Dictionary<CellActions, int> 
                {
                    {CellActions.simple, 90 },
                    {CellActions.small_explosion, 10 }
                };

                break;
        }
    }

    private CellTypes[] approvedSimpleCellsAll()
    {
        return new CellTypes[] { 
            CellTypes.fruit1, 
            CellTypes.fruit2, 
            CellTypes.fruit3, 
            CellTypes.fruit4, 
            CellTypes.fruit5, 
            CellTypes.fruit6 
        };
    }

    private CellTypes[] approvedSimpleCellsThree1()
    {
        return new CellTypes[] {
            CellTypes.fruit1,
            CellTypes.fruit2,
            CellTypes.fruit3,
            CellTypes.fruit5,
        };
    }

    private CellTypes[] approvedSimpleCellsThree2()
    {
        return new CellTypes[] {
            CellTypes.fruit1,
            CellTypes.fruit4,
            CellTypes.fruit5
        };
    }

    private CellTypes[] approvedSimpleCellsThree3()
    {
        return new CellTypes[] {
            CellTypes.fruit1,
            CellTypes.fruit5,
            CellTypes.fruit6
        };
    }
}
