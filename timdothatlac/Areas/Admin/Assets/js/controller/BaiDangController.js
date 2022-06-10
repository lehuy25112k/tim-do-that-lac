var baiDang = {
    init: function () {
        baiDang.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/BaiDang/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Đã duyệt');
                    }
                    else {
                        btn.text('Chưa duyệt');
                    }
                }
            });
        });
    }
}
baiDang.init();