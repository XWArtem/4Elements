using System.Collections.Generic;

public enum EnemySpeed
{
    verySlow,
    slow,
    avarage,
    fast,
    veryFast
}
public enum EnemyType
{
    zombie,
    ogre,
    zombieOgre,
    bat,
    spider,
    fireGolem,
    robot
}

public enum EnemyOutlookDirection
{
    back,
    left,
    right
}

public class EnemyBase
{
    public static Dictionary<EnemySpeed, string> EnemySpeedView = new Dictionary<EnemySpeed, string>()
    {
        {EnemySpeed.verySlow, "Very Slow"},
        {EnemySpeed.slow, "Slow"},
        {EnemySpeed.avarage, "Normal"},
        {EnemySpeed.fast, "Fast"},
        {EnemySpeed.veryFast, "Very Fast"}
    };

    public static Dictionary<EnemyType, string> DescriptionView = new Dictionary<EnemyType, string>()
    {
        {EnemyType.zombie, "The weakest of all enemies. The problem is if there are a lot of them" },
        {EnemyType.ogre, "Ogres are stupid creatures, but strong" },
        {EnemyType.zombieOgre, "Why did the ogre become a zombie if he already had no brains?" },
        {EnemyType.bat, "Fast, but few lives. Try to slow her down" },
        {EnemyType.spider, "Even faster than a bat? Find a new tactic!" },
        {EnemyType.fireGolem, "A golem from which fire flows. The most persistent enemy" },
        {EnemyType.robot, "The robot is trying to retain all the best qualities in itself, try to defeat it" },
    };

    public string Name;

    public float HP;

    public EnemySpeed EnemySpeed;

    public EnemyType EnemyType;

    public int Reward;

    public EnemyBase(string name,
        float HP,
        EnemySpeed enemySpeed,
        EnemyType enemyType,
        int reward)
    {
        Name = name;
        this.HP = HP;
        EnemySpeed = enemySpeed;
        EnemyType = enemyType;
        Reward = reward;
    }
}