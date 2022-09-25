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



        return upgrades;
    }
}

