public enum SoundCategory
{
    Background,
    NoBackground
}

public enum SoundType1
{
    None,
    Walk,
    Run,
    WalkTerrain,
    RunTerrain,
    WalkWater,
    RunWater,
    WalkWoodFloor,
    RunWoodFloor,
    Torch,
    Looting,
    NPCDialog,
    Banshee,
    LossHealth,
    LossSanity,
    BabyCry,
    RockSlide,
    UIButtonTap,
    WomenEvilLaugh,
}


[System.Flags]
public enum SoundType2
{
    None = 1,
    Wind = 2,
    Beach = 4,
    MenuMusic = 8,
    OwlType1 = 16,
    OwlType2 = 32,
    OwlType3 = 64,
    OwlType4 = 128,
    WolfType1 = 256,
    WolfType2 = 512,
    WolfType3 = 1024,
    WolfType4 = 2048,
    CrowType1 = 4096,
    CrowType2 = 8192,
    CrowType3 = 16384,
    CrowType4 = 32768,
}