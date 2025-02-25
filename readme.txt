# 2D Top Down RPG Game

## Concept

- Bạn sẽ vào vai người chơi để khám phá khu rừng ấm áp nhưng đầy rẫy nhưng nguy hiểm.

- Nhân vật sẽ có 3 thông số cơ bản: Heart, Stamina và Gold (chưa triển khai hệ thống Economy).

- Khi hết 5 Heart nhân vật sẽ được hồi sinh về màn chơi đầu tiên.

- 1 Stamina nhân vật sẽ lướt được 1 lần, mỗi Stamina có 3 giây thời gian hồi.

- Weapon:
    + Sword: cấn chiến, gây 1 damage, thời gian hồi tấn công 0.5 giây.
    + Bow: tầm xa, gây 1 damage, thời gian hồi tấn công 0.7 giây.
    + Staff: tầm xa, gây 2 damage, thời gian hồi tấn công 1.2 giây.
    
- Enemy:
    + Slime (Xanh): cận chiến, 5HP.
    + Grape (Tím): tầm xa, 7HP.
    + Ghost (Ma): tầm xa, 10HP.


## Những chức năng đang có và cần update:

- Hiện tại có 3 khu vực theo độ khó tăng dần, khu vực thứ 3 sẽ có Boss.
- Game còn hạn chế về UI/UX, các màn chơi, chức năng, hệ thống trò chơi,... dần dần sẽ được bổ sung thêm.


## Cách chơi

- Di chuyển nhân vật:
    + Phím "A": Sang trái.
    + Phím "D": Sang phải.
    + Phím "W": Lên trên.
    + Phím "S": Xuống dưới.
    + Phím "Space": Lướt một đoạn theo hướng di chuyển và tiêu hao 1 Stamina.

- Đổi vũ khí: các số từ 1 -> 5 tương ứng với các Box item.

- Tấn công: nhấn chuột trái để sử dụng vũ khí tương ứng để tấn công.

- Nhặt vật phẩm: khi lại đủ gần vật phẩm sẽ dần tiến về phía người chơi.
    + Heart: tăng 1 Heart.
    + Stamina: tăng 1 Stamina.
    + Gold coin: tăng 1 Gold.

- Nhấn "Esc" hoặc nút Pause góc phải trên màn hình để pause game.