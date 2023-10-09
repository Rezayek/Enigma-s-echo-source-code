public enum PossibleTags
{
    Water,
    Forest,
    FireTorch,
}

public enum RequestsType
{
    AddItem,
    InspectItem,
    PlaceItem,
    ConsummeItem,
    AllItems,
    Talk,
}

public enum Category
{
    None,
    Consummable,
    Object
}

public enum ObjectType
{
    None,
    Clue,
    Artifact,
    Book,
}
public enum ConsummableType
{
    None,
    Health,
    Sanity,
}


public enum NotifierMessageType
{
    ItemAdd,
    ItemRemove,
    ItemHealthConsumme,
    ItemSanityConsumme,
    ItemPlace
}
