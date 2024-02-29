using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CellTypes[] ApprovedSimpleCells { get; private set; }

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
                Globals.FieldDimention = new Vector2(6, 8);
                Globals.VictoryCondition = new Dictionary<CellTypes, int>
                {
                    {CellTypes.fruit1, 10 },
                    {CellTypes.fruit2, 10 }
                };
                ApprovedSimpleCells = approvedSimpleCellsAll();
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
            CellTypes.fruit6 };
    }
}
