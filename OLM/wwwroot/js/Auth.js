// Khởi tạo FirebaseUI
const ui = new firebaseui.auth.AuthUI(firebase.auth());

// Cấu hình FirebaseUI
const uiConfig = {
    callbacks: {
        signInSuccessWithAuthResult: function (authResult, redirectUrl) {
            // Người dùng đã đăng nhập thành công.
            // Trả về false để ngăn chuyển hướng mặc định.
            // Thay vào đó, bạn có thể thực hiện chuyển hướng tùy chỉnh hoặc
            // gửi dữ liệu đến server của bạn.

            // Ví dụ: Gửi ID token đến server
            authResult.user.getIdToken().then(function (idToken) {
                // Gửi idToken đến API endpoint của bạn
                fetch('/api/auth/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ idToken: idToken })
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            // Đăng nhập thành công trên server, chuyển hướng
                            window.location.replace(redirectUrl || '/'); // Chuyển hướng đến trang chủ hoặc URL được chỉ định
                        } else {
                            // Xử lý lỗi đăng nhập trên server
                            console.error('Lỗi đăng nhập trên server:', data.error);
                            // Hiển thị thông báo lỗi cho người dùng (ví dụ: bằng cách cập nhật nội dung của một phần tử trên trang)
                            document.getElementById('login-error').textContent = data.error;
                        }
                    });
            });

            return false;
        },
        uiShown: function () {
            // Giao diện người dùng đã được hiển thị.
            // Bạn có thể thực hiện các thao tác tùy chỉnh ở đây, ví dụ:
            // - Ẩn trình tải (nếu có)
            // - Thay đổi kiểu dáng của giao diện người dùng
            document.getElementById('loader').style.display = 'none';
        }
    },
    signInFlow: 'popup', // Hoặc 'redirect' nếu bạn muốn sử dụng chuyển hướng
    signInSuccessUrl: '/', // URL chuyển hướng sau khi đăng nhập thành công (nếu không sử dụng callback)
    signInOptions: [
        // Các nhà cung cấp đăng nhập bạn muốn hỗ trợ
        firebase.auth.EmailAuthProvider.PROVIDER_ID,
        firebase.auth.GoogleAuthProvider.PROVIDER_ID,
        // firebase.auth.FacebookAuthProvider.PROVIDER_ID,
        // firebase.auth.TwitterAuthProvider.PROVIDER_ID,
        // firebase.auth.GithubAuthProvider.PROVIDER_ID,
        // ... các nhà cung cấp khác ...
    ],
    // Các tùy chọn cấu hình khác
    // - requiredDisplayName: true/false (yêu cầu tên hiển thị cho đăng nhập email)
    // - tosUrl: 'URL đến điều khoản dịch vụ'
    // - privacyPolicyUrl: 'URL đến chính sách bảo mật'
    // - ...
};

// Bắt đầu FirebaseUI
ui.start('#firebaseui-auth-container', uiConfig);