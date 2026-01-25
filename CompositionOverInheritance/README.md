# Demo Composition over Inheritance

Ví dụ minh họa nguyên tắc *composition over inheritance* bằng cách để `Character` giữ một `IAttackBehavior` có thể hoán đổi được thay vì mở rộng hàng loạt subclass cho từng kiểu tấn công.

## Giải thích ngắn

- `Character` chứa một trường `_attackBehavior` và chỉ biết giao diện `IAttackBehavior`. Các lớp `SwordAttack`, `BowAttack`, `SpellAttack` thực hiện cách tấn công cụ thể.
- Trong `Program.cs`, mỗi nhân vật được khởi tạo với một hành vi ban đầu, rồi ta gọi `SetAttackBehavior` để thay đổi hành vi mà không cần lớp con mới.
- Mỗi `Console.WriteLine` dùng tiếng Việt để mô tả trạng thái và hành động của nhân vật.

## Chạy mẫu

```
dotnet run
```

Sinh ra dòng chữ mô tả "Rurik ...", "Lora ..." và kết thúc bằng thông điệp nhấn mạnh lợi thế của composition.
