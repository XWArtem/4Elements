using System.Collections.Generic;

public class DataStorageTowers
{
    // main
    public readonly List<TowerBase> TowersLvl1 = new List<TowerBase>()
    {
        new TowerBase("Fire Tower", 4, 5, AttackDistanceType.low, FireRate.fast, 100, ResourceType.iron, TowerType.fire, AttackSpecialEffect.none),
        new TowerBase("Water Tower", 7, 8, AttackDistanceType.medium, FireRate.avarage, 100, ResourceType.iron, TowerType.water, AttackSpecialEffect.slow),
        new TowerBase("Air Tower", 5, 7, AttackDistanceType.medium, FireRate.slow, 100, ResourceType.iron, TowerType.air, AttackSpecialEffect.doubleShot),
        new TowerBase("Earth Tower", 4, 7, AttackDistanceType.high, FireRate.verySlow, 100, ResourceType.iron, TowerType.earth, AttackSpecialEffect.AoE),
    };

    public readonly List<TowerBase> TowersLvl2 = new List<TowerBase>()
    {
        new TowerBase("Fire Tower", 8, 10, AttackDistanceType.low, FireRate.fast, 100, ResourceType.iron, TowerType.fire, AttackSpecialEffect.none),
        new TowerBase("Water Tower", 14, 16, AttackDistanceType.medium, FireRate.avarage, 100, ResourceType.iron, TowerType.water, AttackSpecialEffect.slow),
        new TowerBase("Air Tower", 10, 14, AttackDistanceType.medium, FireRate.slow, 100, ResourceType.iron, TowerType.air, AttackSpecialEffect.doubleShot),
        new TowerBase("Earth Tower", 4, 16, AttackDistanceType.high, FireRate.verySlow, 100, ResourceType.iron, TowerType.earth, AttackSpecialEffect.AoE),
    };

    public readonly List<TowerBase> TowersLvl3 = new List<TowerBase>()
    {
        new TowerBase("Fire Tower", 12, 15, AttackDistanceType.low, FireRate.veryFast, 150, ResourceType.iron, TowerType.fire, AttackSpecialEffect.none),
        new TowerBase("Water Tower", 21, 24, AttackDistanceType.medium, FireRate.avarage, 150, ResourceType.iron, TowerType.water, AttackSpecialEffect.slow),
        new TowerBase("Air Tower", 15, 21, AttackDistanceType.medium, FireRate.slow, 150, ResourceType.iron, TowerType.air, AttackSpecialEffect.doubleShot),
        new TowerBase("Earth Tower", 4, 22, AttackDistanceType.high, FireRate.verySlow, 150, ResourceType.iron, TowerType.earth, AttackSpecialEffect.AoE),
    };

    // support
    public readonly List<SupportTowerBase> supportTowers = new List<SupportTowerBase>()
    {
        new SupportTowerBase("Speed Support Tower", 100, ResourceType.diamond, SupportTowerType.speed),
        new SupportTowerBase("Distance Support Tower", 100, ResourceType.diamond, SupportTowerType.distance),
        new SupportTowerBase("Power Support Tower", 100, ResourceType.diamond, SupportTowerType.power)
    };


    // characteristics
    public readonly Dictionary<AttackDistanceType, float> TowerDistanceValue = new Dictionary<AttackDistanceType, float>()
    {
        {AttackDistanceType.low, 2.4f},
        {AttackDistanceType.medium, 3f},
        {AttackDistanceType.high, 5f}
    };

    public readonly Dictionary<FireRate, float> TowerAttackDelay = new Dictionary<FireRate, float>()
    {
        {FireRate.verySlow, 5.8f},
        {FireRate.slow, 2.8f},
        {FireRate.avarage, 2f},
        {FireRate.fast, 1.2f},
        {FireRate.veryFast, 0.8f},
    };

    public readonly Dictionary<TowerType, float> TowerProjectileFlightTimeBase = new Dictionary<TowerType, float>()
    {
        {TowerType.fire, 0.4f},
        {TowerType.air, 0.4f},
        {TowerType.water, 0.4f},
        {TowerType.earth, 1.8f},
    };
}
