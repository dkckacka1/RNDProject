// 현재 스테이지 상태
public enum BattleState
{
    INIT,
    READY,
    BATTLE,
    STOP,
    DEFEAT,
    WIN
}

public enum CombatState
{
    Idle,
    CHASESTART,
    Chase,
    CHASEEND,
    BATTLESTART,
    Attack,
    Dead
}

// 행동 트리 상태(미사용)
public enum Stats
{
    UPDATE,
    FAILURE,
    SUCCESS
}

// 장비아이템 타입
public enum EquipmentItemType
{
    Weapon,
    Armor,
    Pants,
    Helmet,
}

// 장비아이템 등급
public enum EquipmentItemTier
{
    Normal,
    Rare,
    Unique,
    Legendary
}

public enum IncantType
{ 
    prefix,
    suffix
}

public enum alignmentType
{
    left,
    right,
    center,
    justified,
    flush
}

public enum DropItemType
{
    GachaItemScroll,
    reinfoceScroll,
    IncantScroll
}