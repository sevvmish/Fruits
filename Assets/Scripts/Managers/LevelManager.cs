using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
                break;
        }
    }
}
