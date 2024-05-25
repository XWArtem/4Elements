using System.Collections.Generic;

public enum SupportTowerType
{
    none,
    speed,
    distance,
    power
}

public class SupportTowerBase
{


    public static Dictionary<SupportTowerType, string> DescriptionView = new Dictionary<SupportTowerType, string>()
    {
        {SupportTowerType.speed, "Increases the attack speed of any tower" },
        {SupportTowerType.distance, "Increases the attack range of any tower" },
        {SupportTowerType.power, "Increases the attack damage of any tower" }
    };

    public string Name;
    public SupportTowerType SupportTowerType;
    public int Cost;
    public ResourceType ResourceType;

    public SupportTowerBase(string name,
        int cost,
        ResourceType resourceType,
        SupportTowerType supportTowerType)
    {
        Name = name;
        ResourceType = resourceType;
        Cost = cost;
        SupportTowerType = supportTowerType;
    }
}
