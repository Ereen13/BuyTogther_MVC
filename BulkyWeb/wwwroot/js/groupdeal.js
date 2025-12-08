var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/groupdeals/getall' },
        "columns": [
            { data: 'title', title: 'Deal Name', width: "20%" },
            { data: 'originalPrice', title: 'Original Price', width: "10%" },
            { data: 'groupPrice', title: 'Group Price', width: "10%" },
            { data: 'currentParticipants', title: 'Participants', width: "10%" },
            { data: 'maxParticipants', title: 'Max Participants', width: "10%" },
            { data: 'endDate', title: 'End Date', width: "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="btn-group w-100" role="group">
                        <a href="/admin/groupdeals/upsert?id=${data}" class="btn btn-sm btn-outline-primary mx-1">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a onClick=Delete('/admin/groupdeals/delete/${data}') class="btn btn-sm btn-outline-danger mx-1">
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
