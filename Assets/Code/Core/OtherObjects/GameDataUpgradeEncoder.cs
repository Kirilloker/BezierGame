using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GameDataUpgradeEncoder
{
    private static int EffectsLength = Enum.GetNames(typeof(UpgradeEffect)).Length;

    public static Dictionary<string, float> EncodeUpgradeEffects(List<int> upgradeLvls)
    {
        Dictionary<string, float> upgrades = new Dictionary<string, float>();
       
        for (int i = 0; i < EffectsLength; i++)
        {
            switch (i)
            {
                //Speed Upgrades
                case 4:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Increase max speed", 15f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Increase adding speed", 1f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Increase speedup drop chance", 0.25f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Increase adding speed", 0.5f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Increase speedup chance", 0.25f);
                            goto case 0;

                        case 0:
                            break;
                    }
                    break;
                    
                //Size Upgrades
                case 6:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Decrease min size", 0.25f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Increase size decrease", 1f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Increase sizedec drop chance", 0.25f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Increase size decrease", 0.5f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Increase sizedec drop chance", 0.25f);
                            goto case 0;

                        case 0:
                            break;
                    }
                    break;
                
                //Shield Upgrades   
                case 1:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Increase shield timer", 6f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Increase shield drop chance", 0.25f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Increase shield timer", 5f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Increase shield drop chance", 0.25f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Increase shield timer", 4f);
                            goto case 0;

                        case 0:
                            upgrades.Add("Open shield upgrade", 0f);
                            break;
                    }
                    break;
                
                //Player Upgrades
                case 7:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Increase base health", 1f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Decrease base size", 0.2f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Increase base speed", 5f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Increase base health", 1f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Decrease base size", 0.2f);
                            goto case 0;

                        case 0:
                            upgrades.Add("Increase base speed", 5f);
                            break;
                    }
                    break;
               
                //Health Upgrades
                case 5:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Increase max health", 6f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Increase adding health", 1f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Increase heathup drop chance", 0.25f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Increase adding health", 1f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Increase heathup drop chance", 0.25f);
                            goto case 0;

                        case 0:
                            break;
                    }
                    break;
                
                //Slowmoutin Upgrades
                case 2:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Increase slowmoution effect", 0.25f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Increase slowmoution timer", 8f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Increase slowmoution drop chance", 1.5f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Increase slowmoution effect", 0.25f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Increase slowmoution timer", 4f);
                            goto case 0;

                        case 0:
                            upgrades.Add("Open slowmoution upgrade", 0f);
                            break;
                    }
                    break;
                   
                //Magnet Upgrades
                case 0:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Magniting healthup", 0f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Increase magnet drop chance", 0.25f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Magniting speedup", 0f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Magniting sise decrease", 0f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Increase magnet timer", 7f);
                            goto case 0;

                        case 0:
                            upgrades.Add("Open magnet upgrade", 0f);
                            break;
                    }
                    break;
                    
                //Scrore Mult Upgrades
                case 3:
                    switch (upgradeLvls[i])
                    {
                        case 5:
                            upgrades.Add("Increase xscore multiplyer", 1f);
                            goto case 4;

                        case 4:
                            upgrades.Add("Increase xscore timer", 12f);
                            goto case 3;

                        case 3:
                            upgrades.Add("Increase xscore multiplyer", 1f);
                            goto case 2;

                        case 2:
                            upgrades.Add("Increase xscore drop chance", 0.25f);
                            goto case 1;

                        case 1:
                            upgrades.Add("Increase xscore timer", 8f);
                            goto case 0;

                        case 0:
                            upgrades.Add("Open xscore upgrade", 0f);
                            break;
                    }
                    break;
            }
        }
        return upgrades;
    }
}