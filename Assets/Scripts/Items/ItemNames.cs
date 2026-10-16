// 아이템 이름을 한 곳에서 관리해 오타로 인한 버그를 방지합니다.
public static class ItemNames
{
    public const string Branch = "Branch";
    public const string Stone = "Stone";
    public const string Axe = "Axe";

    // 도구는 사용해도 소모되지 않습니다.
    public static bool IsTool(string itemName)
    {
        return itemName == Axe;
    }
}
