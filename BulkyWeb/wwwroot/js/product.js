var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            {
                data: 'productImages',
                render: function (data) {
                    if (data.length > 0) {
                        return `<img src="${data[0].imageUrl}" width="70px" style="border-radius:5px; border:1px solid #bbb;" />`;
                    }
                    return `<img src="https://placehold.co/70x100?text=No+Image" width="70px" />`;
                },
                width: "10%"
            },
            { data: 'title', title: 'Product Name', width: "20%" },
            { data: 'isbn', title: 'SKU', width: "10%" },
            { data: 'listPrice', title: 'Price', width: "10%" },
            { data: 'author', title: 'Brand', width: "15%" },
            { data: 'category.name', title: 'Category', width: "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="btn-group w-100" role="group">
                        <a href="/admin/product/upsert?id=${data}" class="btn btn-sm btn-outline-primary mx-1">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-sm btn-outline-danger mx-1">
                            <i class="bi bi-trash-fill"></i>
                        </a>
                    </div>`;
                },
                width: "20%"
            }
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}