# Hudson Rooms – FnB Management Requirement

## Input requirement

- Nhà hàng: **The Hudson Rooms – Capella Hanoi**
- Quy mô: khoảng **40–60 chỗ**
- Mục tiêu: xây dựng ứng dụng quản lý FnB.

## Scope implemented in this repository

1. Quản lý cấu hình nhà hàng theo profile có dải sức chứa (`MinSeats`, `MaxSeats`).
2. Quản lý danh sách bàn và số ghế từng bàn.
3. Quản lý menu cơ bản (mã món, tên món, đơn giá).
4. Luồng vận hành phục vụ:
   - Mở bàn theo số khách.
   - Thêm món vào order của bàn.
   - Chốt bàn để tạo bill chi tiết.
5. Báo cáo cuối ca:
   - Tổng số khách đã phục vụ.
   - Số order đã đóng.
   - Tổng doanh thu.

## Constraint

- Tổng số ghế cấu hình phải nằm trong dải **40–60**; nếu vượt phạm vi sẽ báo lỗi nghiệp vụ.
