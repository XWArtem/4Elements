using System.Collections.Generic;

public enum AttackDistanceType
{
    low,
    medium,
    high
}

public enum FireRate
{
    verySlow,
    slow,
    avarage,
    fast,
    veryFast
}

public enum TowerType
{
    none,
    fire,
    air,
    water,
    earth
}

public enum AttackSpecialEffect
{
    none,
    slow,
    doubleShot,
    AoE
}

public class TowerBase
{
    public static Dictionary<AttackDistanceType, string> AttackDistanceView = new Dictionary<AttackDistanceType, string>()
    {
        {AttackDistanceType.low, "Short Range"},
        {AttackDistanceType.medium, "Avarage Range"},
        {AttackDistanceType.high, "Long Range"}
    };

    public static Dictionary<FireRate, string> FireRateView = new Dictionary<FireRate, string>()
    {
        {FireRate.verySlow, "Very Low Speed"},
        {FireRate.slow, "Low Speed"},
        {FireRate.avarage, "Avarage Speed"},
        {FireRate.fast, "High Speed"},
        {FireRate.veryFast, "Very High Speed"}
    };

    public static Dictionary<TowerType, string> DescriptionView = new Dictionary<TowerType, string>()
    {
        {TowerType.fire, "The Fire Tower attacks very quickly, but has a short attack range" },
        {TowerType.air, "Attacks two enemies at once, but has a slow rate of fire" },
        {TowerType.water, "Moderate damage and attack range. Slows down the speed of enemies" },
        {TowerType.earth, "Shoots a large piece of earth, thereby hitting a large number of opponents" },
    };

    public string Name;

    public int DamageMin;
    public int DamageMax;

    public AttackDistanceType AttackDistanceType;

    public FireRate FireRate;

    public int Cost;

    public ResourceType ResourceType;

    public TowerType TowerType;

    public AttackSpecialEffect AttackSpecialEffect;

    public TowerBase(string name, 
        int dmgMin, 
        int dmgMax, 
        AttackDistanceType attackDistanceType, 
        FireRate fireRate, 
        int cost, 
        ResourceType resType,
        TowerType towerType, 
        AttackSpecialEffect attackSpecialEffect)
    {
        Name = name;
        DamageMin = dmgMin;
        DamageMax = dmgMax;
        AttackDistanceType = attackDistanceType;
        FireRate = fireRate;
        Cost = cost;
        ResourceType = resType;
        TowerType = towerType;
        AttackSpecialEffect = attackSpecialEffect;
    }
}
