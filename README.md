# DataStructures

Refactor theo **Hexagonal Architecture (Ports & Adapters)** để tái hiện bug `Đặt hàng = -1` và minh hoạ hướng fix.

## Kiến trúc

- `src/Domain`: Entity/Value Object + Port interface.
- `src/Application`: Use case tái hiện bug và luồng fix.
- `src/Infrastructure`: Adapter lưu trữ in-memory cho reservation.
- `src/Adapters/Console`: Presenter in kết quả ra console.

## Runtime

- .NET: `net10.0`
- C#: `LangVersion=preview` (cho C# 14 theo SDK .NET 10 preview)

## Chạy thử

```bash
dotnet run
```

Kỳ vọng:

- Luồng buggy: `SP000003` và `SP000004` có thể xuống `-1`.
- Luồng fixed: reservation được release đúng và clamp về `0` nếu âm.
