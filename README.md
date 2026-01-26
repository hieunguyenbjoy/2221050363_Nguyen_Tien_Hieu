# Net Core API 
2221050363_Nguyen_Tien_Hieu
1. Cấu trúc thư mục dự án
Controllers
Chứa các lớp Controller dùng để nhận request từ người dùng, xử lý dữ liệu và trả về kết quả.
Models
Chứa các lớp dữ liệu của hệ thống (ví dụ: sinh viên, người dùng, sản phẩm…). Model dùng để truyền dữ liệu giữa Controller và View hoặc làm việc với cơ sở dữ liệu.
Views
Chứa các file giao diện (.cshtml).
Mỗi Controller thường có một thư mục View riêng. Thư mục Shared chứa các giao diện dùng chung như layout và trang lỗi.
wwwroot
Chứa tài nguyên tĩnh như CSS, JavaScript, hình ảnh, thư viện Bootstrap, jQuery…
Program.cs
Là điểm bắt đầu chạy ứng dụng, dùng để cấu hình dịch vụ và định tuyến.
2. Định tuyến (Routing)
Routing giúp xác định URL sẽ được xử lý bởi Controller và Action nào.
Cấu trúc thường gồm: Controller – Action – Id (tùy chọn).
Khi người dùng truy cập một đường dẫn, hệ thống sẽ dựa vào quy tắc định tuyến để gọi đúng Controller và phương thức xử lý tương ứng.
3. Namespace trong C#
Namespace dùng để tổ chức các lớp trong chương trình và tránh trùng tên.
Mỗi thư mục trong dự án thường tương ứng với một namespace, giúp quản lý mã nguồn rõ ràng và dễ mở rộng.
4. Controller
Controller là thành phần xử lý logic chính của ứng dụng.
Nhiệm vụ:
Nhận yêu cầu từ người dùng
Xử lý dữ liệu
Gửi dữ liệu sang View
Mỗi hàm trong Controller gọi là một Action.
5. View
View là giao diện hiển thị cho người dùng, viết bằng Razor (.cshtml).
View nhận dữ liệu từ Controller và hiển thị ra trình duyệt.
6. Cách MVC hoạt động
Người dùng gửi yêu cầu từ trình duyệt
Routing xác định Controller và Action
Controller xử lý yêu cầu
Controller gửi dữ liệu sang View
View hiển thị kết quả cho người dùng