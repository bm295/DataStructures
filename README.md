# DataStructures

Ứng dụng mẫu quản lý FnB cho **The Hudson Rooms – Capella Hanoi** theo kiến trúc **Hexagonal Architecture (Ports & Adapters)**.

## Mục tiêu

- Quản lý quy mô chỗ ngồi của nhà hàng trong khoảng **40–60 chỗ**.
- Mô phỏng vận hành ca dịch vụ:
  - Mở bàn theo số khách.
  - Gọi món theo menu.
  - Chốt bill và tổng kết doanh thu.

## Kiến trúc

- `src/Domain`: model nghiệp vụ + port (`IFnbManagementRepository`).
- `src/Application`: use case `RunHudsonRoomsFnbUseCase`.
- `src/Infrastructure`: adapter lưu trữ in-memory cho bàn, menu, order.
- `src/Adapters/Console`: presenter in báo cáo vận hành ra console.

## Runtime

- .NET: `net10.0`
- C#: `LangVersion=preview`

## Chạy thử

```bash
dotnet run
```

Kết quả kỳ vọng:

- Ứng dụng xác nhận tổng số ghế đang cấu hình nằm trong dải **40–60**.
- In chi tiết bill của các bàn đã phục vụ.
- In tổng kết cuối ca: số order đã đóng, số khách phục vụ, doanh thu.
