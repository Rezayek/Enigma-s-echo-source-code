public enum Enemy
{
    Banshee,
}

public enum EnemySounds
{
    ScreamAngry,
    ScreamPain,
    ScreamScary,
    ScreamAgony,
    Cry,
    Laught,
    ScreamAttack,
}

public enum AIAnimationsNames
{
    Idle,
    Attack,
    Crying,
    Agony,
    Pain,
    Scream,
    Death,
}

public enum RewardCallType
{
    Reward,
    Punish,
}


public enum RewardType
{
    None,
    NoMoreFront,
    NoMoreBehind,
    BigAllowed,
    SmallAllowed,
    Attack,
    
}

public enum PunishType
{
    None,
    StillFrontPlayer,
    StillBehindPlayer,
    BigNotAlloawed,
    NoAttack,
    NoRepeat,
}


public enum States
{
    None,
    Wandering,
    Attack,
    Hunt,
    Frenzy,
    Vanish,
    Death,
}

public enum LocationState
{
    Available,
    NoAvailable,
}

public enum LocationIds
{
    Location_1,
    Location_2,
    Location_3,
}

public enum AIAniamtions
{
    Idle,
    Run,
    Attack,
}