$(document).ready(function () {
    loadEmployeeData();

    $("#saveEmployeeBtn").click(function () {
        addEmployee();
    });
});

function loadEmployeeData() {
    $('#employeeTable').DataTable({
        ajax: {
            url: "https://localhost:7086/api/employee",
            dataType: "JSON",
            dataSrc: "data"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            {
                data: null,
                render: function (data, type, row) {
                    return row.firstName + " " + row.lastName;
                }
            },
            {
                data: "birthDate",
                render: function (data, type, row) {
                    return moment(data).format("DD MMMM YYYY");
                }
            },
            {
                data: "gender",
                render: function (data, type, row) {
                    return data === 1 ? "Male" : "Female";
                }
            },
            {
                data: "hiringDate",
                render: function (data, type, row) {
                    return moment(data).format("DD MMMM YYYY");
                }
            },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="deleteEmployee('${row.guid}')" class="btn btn-secondary">Delete</button>`;
                }
            }
        ]
    });
}

function addEmployee() {
    var employeeData = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        birthDate: $("#birthDate").val(),
        gender: parseInt($("#gender").val()),
        hiringDate: $("#hiringDate").val(),
        email: $("#email").val(),
        phoneNumber: $("#phoneNumber").val()
    };

    $.ajax({
        url: "https://localhost:7086/api/employee",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(employeeData),
    }).done(function (result) {
        Swal.fire({
            title: 'Success',
            text: 'Data has been successfully inserted',
            icon: 'success'
        }).then(function () {
            location.reload();
        });
    }).fail(function (error) {
        Swal.fire({
            title: 'Oops',
            text: 'Failed to insert data. Please try again.',
            icon: 'error'
        });
    });
}

function deleteEmployee(guid) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Changes cannot be reverted!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Delete Data'
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7086/api/employee?guid=" + guid,
                type: "DELETE",
            }).done(function (result) {
                Swal.fire({
                    title: 'Deleted',
                    text: 'Data has been successfully deleted',
                    icon: 'success'
                }).then(function () {
                    location.reload();
                });
            }).fail(function (error) {
                alert("Failed to delete data. Please try again!");
            });
        }
    });
}
