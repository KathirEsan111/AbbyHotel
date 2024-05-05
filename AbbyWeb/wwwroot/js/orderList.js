var datatable;
$(document).ready(function () {
    datatable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/Order",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "pickupName", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickupTime", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                            <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2"
                                    ><i class="bi bi-pencil"></i></a>
                           
                             </div>`
                }, "width": "15%"
            }
        ], "width": "100%"

    });
});