$(document).ready(function () {
    // 1. Vừa vào trang là đi lấy dữ liệu bảng
    loadStudentTable();

    function loadStudentTable(page = 1) {
        $.ajax({
            url: '/Student/GetStudents',
            type: 'GET',
            data: { page: page, pageSize: 10 },
            success: function (result) {
                // Nhận HTML cái bảng và đè lên chữ "Đang tải dữ liệu..."
                $('#studentTableContainer').html(result);
            },
            error: function () {
                $('#studentTableContainer').html('<p class="text-danger text-center">Có lỗi xảy ra khi tải dữ liệu!</p>');
            }
        });
    }

    // 2. Bấm nút "Thêm Sinh Viên Mới" trên màn hình Index
    $('#btnCreate').click(function () {
        // Dùng Ajax đi lấy cái Form _Create.cshtml về
        $.get('/Student/Create', function (htmlForm) {
            // Nhét form vào div ẩn và cho nó hiện lên thành Popup
            $('#modalContainer').html(htmlForm);
            $('#studentModal').modal('show'); 
        });
    });

    // 3. Bấm nút "Lưu Sinh Viên" ở trong cái Form vừa hiện ra
    $(document).on('click', '#btnSaveCreate', function () {
        // Gom toàn bộ dữ liệu em vừa gõ trong form
        let formData = $('#frmCreate').serialize(); 

        // Gửi ngầm dữ liệu lên Controller
        $.post('/Student/Create', formData, function (response) {
            if (response.success === true) {
                // Lưu thành công thì giấu cái Popup đi
                $('#studentModal').modal('hide'); 
                
                // Dọn dẹp phông nền đen của popup
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                
                // Gọi Ajax tải lại bảng danh sách mới tinh
                loadStudentTable(); 
                
                alert("Thêm sinh viên thành công!");
            } else {
                alert("Lưu thất bại, vui lòng kiểm tra lại thông tin!");
            }
        });
    });
});