public enum ResourceType
{
    iron,
    diamond
}

public class ResourcesBase
{
    //public ResourceType ResourceType;
    public int Iron;
    public int Diamond;

    public ResourcesBase(int valueIron, int valueDiamond)
    {
        Iron = valueIron;
        Diamond = valueDiamond;
    }
}
