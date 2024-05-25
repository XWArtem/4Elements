using System.Collections.Generic;

public class EnemyWave
{
    public EnemyType Type;
    public int Count;

    public EnemyWave(EnemyType type, int startCount)
    {
        Type = type;
        Count = startCount;
    }
}

public class DataStorageEnemies
{
    public readonly List<EnemyBase> Enemies = new List<EnemyBase>()
    {
        new EnemyBase("Zombie", 25f, EnemySpeed.avarage, EnemyType.zombie, 5),
        new EnemyBase("Ogre", 50f, EnemySpeed.slow, EnemyType.ogre, 8),
        new EnemyBase("Zombie Ogre", 55f, EnemySpeed.avarage, EnemyType.zombieOgre, 10),
        new EnemyBase("Bat", 35f, EnemySpeed.fast, EnemyType.bat, 8),
        new EnemyBase("Spider", 25f, EnemySpeed.veryFast, EnemyType.spider, 7),
        new EnemyBase("Fire Golem", 520f, EnemySpeed.verySlow, EnemyType.fireGolem, 25),
        new EnemyBase("Robot", 990f, EnemySpeed.avarage, EnemyType.robot, 30)
    };

    public readonly List<List<EnemyWave>> EnemyWavesOnLevel = new List<List<EnemyWave>>()
    {
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 10), new EnemyWave(EnemyType.zombie, 10), new EnemyWave(EnemyType.spider, 5), new EnemyWave(EnemyType.fireGolem, 1) } }, // 0 TEST
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 4), new EnemyWave(EnemyType.zombie, 6), new EnemyWave(EnemyType.zombie, 14) } }, // 1
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 6), new EnemyWave(EnemyType.zombie, 10), new EnemyWave(EnemyType.ogre, 5) } }, // 2
        {new List<EnemyWave>(){new EnemyWave(EnemyType.ogre, 6), new EnemyWave(EnemyType.ogre, 7), new EnemyWave(EnemyType.ogre, 9), new EnemyWave(EnemyType.ogre, 10) } }, // 3
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 6), new EnemyWave(EnemyType.ogre, 7), new EnemyWave(EnemyType.zombie, 9), new EnemyWave(EnemyType.ogre, 7)} }, // 4
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 6), new EnemyWave(EnemyType.ogre, 7), new EnemyWave(EnemyType.zombie, 9), new EnemyWave(EnemyType.ogre, 7)} }, // 5
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 6), new EnemyWave(EnemyType.zombie, 7), new EnemyWave(EnemyType.zombie, 12), new EnemyWave(EnemyType.zombieOgre, 7)} }, // 6
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 6), new EnemyWave(EnemyType.zombieOgre, 7), new EnemyWave(EnemyType.zombie, 12), new EnemyWave(EnemyType.zombieOgre, 7), new EnemyWave(EnemyType.zombie, 25) } }, // 7
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombieOgre, 6), new EnemyWave(EnemyType.zombieOgre, 7), new EnemyWave(EnemyType.zombieOgre, 12), new EnemyWave(EnemyType.zombieOgre, 7), new EnemyWave(EnemyType.zombieOgre, 12) } }, // 8
        {new List<EnemyWave>(){new EnemyWave(EnemyType.bat, 6), new EnemyWave(EnemyType.bat, 7), new EnemyWave(EnemyType.bat, 12), new EnemyWave(EnemyType.bat, 7), new EnemyWave(EnemyType.bat, 9)} }, // 9
        {new List<EnemyWave>(){new EnemyWave(EnemyType.bat, 6), new EnemyWave(EnemyType.zombieOgre, 12), new EnemyWave(EnemyType.zombieOgre, 15), new EnemyWave(EnemyType.zombieOgre, 20), new EnemyWave(EnemyType.bat, 9)} }, // 10
        {new List<EnemyWave>(){new EnemyWave(EnemyType.bat, 6), new EnemyWave(EnemyType.zombieOgre, 12), new EnemyWave(EnemyType.zombieOgre, 15), new EnemyWave(EnemyType.zombieOgre, 20), new EnemyWave(EnemyType.bat, 11), new EnemyWave(EnemyType.bat, 30)} }, // 11
        {new List<EnemyWave>(){new EnemyWave(EnemyType.bat, 6), new EnemyWave(EnemyType.bat, 12), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.bat, 20), new EnemyWave(EnemyType.bat, 11), new EnemyWave(EnemyType.bat, 30)} }, // 12
        {new List<EnemyWave>(){new EnemyWave(EnemyType.bat, 6), new EnemyWave(EnemyType.bat, 12), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.bat, 18), new EnemyWave(EnemyType.bat, 20), new EnemyWave(EnemyType.bat, 22)} }, // 13
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombieOgre, 4), new EnemyWave(EnemyType.zombie, 12), new EnemyWave(EnemyType.zombie, 15), new EnemyWave(EnemyType.zombie, 18), new EnemyWave(EnemyType.zombieOgre, 20), new EnemyWave(EnemyType.zombieOgre, 22), new EnemyWave(EnemyType.zombie, 8) } }, // 14
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombieOgre, 4), new EnemyWave(EnemyType.zombie, 12), new EnemyWave(EnemyType.zombie, 15), new EnemyWave(EnemyType.zombie, 18), new EnemyWave(EnemyType.zombieOgre, 15), new EnemyWave(EnemyType.zombieOgre, 15), new EnemyWave(EnemyType.spider, 13) } }, // 15
        {new List<EnemyWave>(){new EnemyWave(EnemyType.bat, 9), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.spider, 18), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.spider, 13) } }, // 16
        {new List<EnemyWave>(){new EnemyWave(EnemyType.bat, 9), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.spider, 18), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.bat, 15), new EnemyWave(EnemyType.spider, 13), new EnemyWave(EnemyType.spider, 21) } }, // 17
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 9), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 18), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 13), new EnemyWave(EnemyType.spider, 21) } }, // 18
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 9), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 18), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 13), new EnemyWave(EnemyType.spider, 21) } }, // 19
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 9), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 18), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 13), new EnemyWave(EnemyType.spider, 21), new EnemyWave(EnemyType.fireGolem, 1) } }, // 20
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 9), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 18), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 2), new EnemyWave(EnemyType.spider, 21), new EnemyWave(EnemyType.fireGolem, 3) } }, // 21
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 9), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.spider, 15), new EnemyWave(EnemyType.spider, 10), new EnemyWave(EnemyType.spider, 40), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 2), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 9) } }, // 22
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 12), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 3), new EnemyWave(EnemyType.fireGolem, 3), new EnemyWave(EnemyType.zombieOgre, 28), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 2), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.fireGolem, 9), new EnemyWave(EnemyType.robot, 2) } }, // 23
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombieOgre, 15), new EnemyWave(EnemyType.zombieOgre, 25), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.robot, 2), new EnemyWave(EnemyType.zombieOgre, 44), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.robot, 2), new EnemyWave(EnemyType.spider, 8), new EnemyWave(EnemyType.fireGolem, 9), new EnemyWave(EnemyType.robot, 3) } }, // 24
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombieOgre, 6), new EnemyWave(EnemyType.bat, 2), new EnemyWave(EnemyType.zombieOgre, 18), new EnemyWave(EnemyType.bat, 6), new EnemyWave(EnemyType.zombieOgre, 34), new EnemyWave(EnemyType.bat, 30), new EnemyWave(EnemyType.bat, 22), new EnemyWave(EnemyType.bat, 12), new EnemyWave(EnemyType.zombieOgre, 40), new EnemyWave(EnemyType.zombieOgre, 42) } }, // 25
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombieOgre, 10), new EnemyWave(EnemyType.zombieOgre, 12), new EnemyWave(EnemyType.zombieOgre, 4), new EnemyWave(EnemyType.bat, 2), new EnemyWave(EnemyType.zombieOgre, 34), new EnemyWave(EnemyType.bat, 30), new EnemyWave(EnemyType.fireGolem, 3), new EnemyWave(EnemyType.fireGolem, 4), new EnemyWave(EnemyType.spider, 18), new EnemyWave(EnemyType.fireGolem, 8), new EnemyWave(EnemyType.spider, 100) } }, // 26
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.bat, 14), new EnemyWave(EnemyType.bat, 10), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 1), new EnemyWave(EnemyType.fireGolem, 3), new EnemyWave(EnemyType.fireGolem, 6), new EnemyWave(EnemyType.fireGolem, 6), new EnemyWave(EnemyType.fireGolem, 6), new EnemyWave(EnemyType.robot, 12) } }, // 27
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 13), new EnemyWave(EnemyType.spider, 9), new EnemyWave(EnemyType.spider, 10), new EnemyWave(EnemyType.spider, 14), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.spider, 100), new EnemyWave(EnemyType.spider, 60), new EnemyWave(EnemyType.robot, 10) } }, // 28
        {new List<EnemyWave>(){new EnemyWave(EnemyType.spider, 13), new EnemyWave(EnemyType.bat, 9), new EnemyWave(EnemyType.bat, 2), new EnemyWave(EnemyType.spider, 14), new EnemyWave(EnemyType.zombieOgre, 18), new EnemyWave(EnemyType.ogre, 15), new EnemyWave(EnemyType.ogre, 5), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.robot, 2), new EnemyWave(EnemyType.ogre, 20), new EnemyWave(EnemyType.ogre, 40), new EnemyWave(EnemyType.ogre, 30) } }, // 29
        {new List<EnemyWave>(){new EnemyWave(EnemyType.zombie, 3), new EnemyWave(EnemyType.zombieOgre, 5), new EnemyWave(EnemyType.bat, 5), new EnemyWave(EnemyType.spider, 12), new EnemyWave(EnemyType.zombieOgre, 22), new EnemyWave(EnemyType.spider, 45), new EnemyWave(EnemyType.spider, 40), new EnemyWave(EnemyType.robot, 4), new EnemyWave(EnemyType.fireGolem, 9), new EnemyWave(EnemyType.robot, 1), new EnemyWave(EnemyType.spider, 24), new EnemyWave(EnemyType.zombieOgre, 100), new EnemyWave(EnemyType.robot, 20) } }, // 30
    };

    public readonly Dictionary<EnemySpeed, float> EnemySpeedToMaxStep = new Dictionary<EnemySpeed, float>()
    {
        {EnemySpeed.verySlow, 0.001f},
        {EnemySpeed.slow, 0.0016f},
        {EnemySpeed.avarage, 0.003f},
        {EnemySpeed.fast, 0.005f},
        {EnemySpeed.veryFast, 0.008f}
    };
}
