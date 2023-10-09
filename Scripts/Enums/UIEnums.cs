public enum UIDisplay
{
    On,
    Off,
    OffAll,
}

public enum TorchControl
{
    PlayLogic,
    PlayUI,
}


public enum GameGUI
{
    InspectorGUI,
    InventoryGUI,
    ReaderGUI,
    MainGameGUI,
    GameSelectGUI,
    SettingsGUI,
    PauseGUI,
    DialogGUI,
    MainMenu,
    PauseMenu,
    Settings,
    SelectionUI,
    AboutUI,
    LoadingUI,
    WarningUI,
}


public enum UIAnimations
{
    Fade,
    FadeIn,
    FadeOut,
    Move,
    MoveIn,
    MoveOut,
    MoveInOut,
    Scale,
    ScaleIn,
    ScaleOut,
    FadeInFadeOut,
}


public enum PlayerPrefsNames
{
    UIActive,
    AudioVolumeNB,
    AudioVolumeB,
    CurrentSenario,
    CurrentSenarioVersion,
    FrameRate,
    Quality,
    Resolution,
    Difficulty,
    MouseSensitivity,
    ViewDistance,
    TreeDensity,
    GrassDensity,
}

public enum ScenarioName
{
    MainMenu = 0 ,
    EvilBanshee = 1,
    CursedPriest = 2,
}

public enum OtherLoadingCases
{
    Respawn,
}

public enum WarningTypes
{
    QuitGame,
    ReturnMain,
    PlayerWin,
    PlayerRevive,
    PlayerLose,
}

public enum WarningOption
{
    OneOption,
    TwoOptions,
}