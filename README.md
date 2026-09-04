# SMSSending

Ứng dụng console .NET đơn giản để gửi SMS qua [Twilio](https://www.twilio.com/).

## Yêu cầu

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) trở lên
- Tài khoản Twilio (dùng tài khoản Trial là đủ để test) — lấy `Account SID` và `Auth Token` tại [Twilio Console](https://console.twilio.com/)
- Một số điện thoại Twilio (`from`) và số điện thoại nhận (`to`)
  - Với tài khoản Trial, số `to` **phải được verify** trước trong Twilio Console (Phone Numbers → Verified Caller IDs)

## Cài đặt

1. Clone repo và cài package:

   ```bash
   git clone https://github.com/Toandz1125/SMSSending.git
   cd SMSSending
   dotnet restore
   ```

2. Tạo file `.env` từ mẫu `.env.example` và điền thông tin Twilio của bạn:

   ```bash
   cp .env.example .env
   ```

   Nội dung `.env`:

   ```
   TWILIO_ACCOUNT_SID=ACxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
   TWILIO_AUTH_TOKEN=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
   ```

   > `.env` đã được thêm vào `.gitignore` — **không commit file này lên git** vì nó chứa thông tin xác thực thật.

## Chạy chương trình

```bash
dotnet run
```

Chương trình sẽ:
1. Đọc `TWILIO_ACCOUNT_SID` và `TWILIO_AUTH_TOKEN` từ file `.env` (nếu chưa có sẵn trong biến môi trường hệ thống)
2. Khởi tạo Twilio client
3. Gửi một tin nhắn SMS mẫu
4. In ra `Sid` và `Status` của tin nhắn nếu thành công, hoặc thông tin lỗi (`Code`, `Message`, `MoreInfo`, `Status`) nếu thất bại

## Cấu hình số điện thoại gửi/nhận

Số điện thoại `from` và `to` hiện đang được hard-code trong [Program.cs](Program.cs):

```csharp
from: new PhoneNumber("+16292842479"),
to: new PhoneNumber("+18777804236")
```

Sửa trực tiếp hai giá trị này để dùng số Twilio và số người nhận của bạn. Định dạng số phải theo chuẩn [E.164](https://www.twilio.com/docs/glossary/what-e164) (ví dụ: `+84901234567`).

## Cấu trúc dự án

```
TestSMS/
├── Program.cs        # Logic chính: đọc .env, gọi Twilio API để gửi SMS
├── TestSMS.csproj     # Cấu hình project .NET (target net8.0, phụ thuộc Twilio)
├── TestSMS.slnx       # Solution file
├── .env               # Thông tin xác thực Twilio thật (không commit)
├── .env.example       # Mẫu file .env
└── .gitignore
```

## Xử lý lỗi thường gặp

| Lỗi | Nguyên nhân | Cách khắc phục |
|---|---|---|
| `Authenticate` / 401 | `TWILIO_ACCOUNT_SID` hoặc `TWILIO_AUTH_TOKEN` sai hoặc chưa được set | Kiểm tra lại file `.env`, đảm bảo đã copy đúng từ Twilio Console |
| `The number ... is unverified` | Tài khoản Trial chỉ được gửi tới số đã verify | Verify số nhận trong Twilio Console → Phone Numbers → Verified Caller IDs |
| `.env` không được đọc | Chạy chương trình từ thư mục khác thư mục project | Chạy `dotnet run` từ thư mục gốc chứa `TestSMS.csproj`, hoặc đặt sẵn biến môi trường hệ thống |

## Bảo mật

- Không commit `Account SID` / `Auth Token` thật vào git. File `.env` đã nằm trong `.gitignore`.
- Nếu lỡ commit Auth Token thật lên GitHub, hãy **revoke/rotate token đó ngay** trong Twilio Console rồi cập nhật lại `.env`.

## License

Chưa chỉ định license.
