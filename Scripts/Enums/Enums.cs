

public enum EnvActions
{
    DoorAction,
    KeyAction,
    PuzzelAction,
}



public enum ObjectsToObserver
{
    None,
    Water,
    WoodFloor,
    Terrain,
    ObjectLoot,
    ObjectPlace,
    NPC,
    Interactors,
}




public enum PlayerStateEnum
{
    IncreaseHealth,
    DecreaseHealth,
    IncreaseSanity,
    DecreaseSanity,
}

public enum Damage
{
    HealthDamage,
    SanityDamage,
}


public enum InteractorActions
{
    Loot,
    Inspect,
    Place,
    Talk,
}



public enum Senarios
{
    None,
    EvilBanshee,
}

public enum NPC
{
    None,
    Elias,
    Alistair, 
    ChiefJosh,
    Bob,
    Marry,
    Emilia,
    Jenny,
    Clark,

}

public enum SenariosVersion
{
    None,
    Version_1,
    Version_2,
    Version_3,
    Version_4,
    Version_5,
}

public enum Options
{
    None,
    Option_1,
    Option_2,
    Option_3,
}


public enum StateZero
{
    HealthZero,
    SanityZero,
}

public enum CheckPointCallTypes
{
    Respawn,
    ReduceTries,
    RemoveTries,
}