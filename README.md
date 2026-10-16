# mel-ba

Unity 2D 탑다운 생존/제작 게임 스크립트 모음입니다.

## 폴더 구조

```
Assets/Scripts/
├── Player/PlayerMovement.cs     # WASD/방향키 이동 (Rigidbody2D)
├── CameraSystem/CameraFollow.cs # 플레이어를 부드럽게 따라가는 카메라
├── Items/
│   ├── ItemNames.cs             # 아이템 이름 상수 (Branch, Stone, Axe)
│   ├── Inventory.cs             # 인벤토리 (최대 150개, 변경 이벤트)
│   └── Crafting.cs              # 도끼 제작 (나뭇가지 1 + 돌 1, C키)
├── World/
│   ├── ResourceNode.cs          # 닿으면 아이템을 떨어뜨리는 나무/바위
│   └── ItemPickup.cs            # 떨어진 아이템 줍기
└── UI/Hotbar.cs                 # 단축창 (기본 3칸, 최대 7칸, 숫자키 1~7)
```

## 게임 흐름

1. 플레이어가 나무/바위(`ResourceNode`)에 닿으면 나뭇가지/돌이 떨어집니다.
2. 떨어진 아이템(`ItemPickup`)에 닿으면 인벤토리에 들어갑니다.
3. `C` 키로 도끼를 제작하면 인벤토리와 단축창에 추가됩니다.
4. 숫자키로 단축창 아이템을 사용합니다. 소모품은 1개씩 줄어들고, 도끼 같은 도구는 사라지지 않습니다.

## 씬 설정 방법

| 오브젝트 | 필요한 컴포넌트 / 설정 |
|---|---|
| Player | 태그 `Player`, `Rigidbody2D`, `Collider2D`, `PlayerMovement`, `Inventory`, `Crafting`, `Hotbar` |
| Main Camera | `CameraFollow` (target = Player) |
| 나무 | `Collider2D`(Is Trigger), `ResourceNode` (dropPrefab = 나뭇가지 프리팹) |
| 바위 | `Collider2D`(Is Trigger), `ResourceNode` (dropPrefab = 돌 프리팹) |
| 나뭇가지 프리팹 | `Collider2D`, `ItemPickup` (itemName = `Branch`) |
| 돌 프리팹 | `Collider2D`, `ItemPickup` (itemName = `Stone`) |

`Crafting`과 `Hotbar`의 `inventory` 필드에 Player의 `Inventory`를, `Crafting.hotbar`에 `Hotbar`를 연결하세요.
