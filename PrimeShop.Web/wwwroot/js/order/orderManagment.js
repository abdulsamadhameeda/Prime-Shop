

$(document).ready(function () {
    $('#ordersTable').DataTable({

        processing: false,
        serverSide: true,
        ordering: false,
        pageLength: 5,
        lengthMenu: [5, 10, 20, 30],
        ajax: {
            url: '/Order/GetOrdersData',
            type: 'POST',
        },
        columns: [
            { data: 'id', name: 'Id' },
            {
                data: 'orderDate',
                name: 'OrderDate',
                render: function (data) {
                    if (!data) return '';
                    var date = new Date(data);
                    return date.toLocaleDateString();
                }
            },
            {
                data: 'totalAmount',
                name: 'TotalAmount',
                render: function (data) {
                    return parseFloat(data).toFixed(2) + " $";
                }
            },
            {
                data: 'status',
                name: 'Status',
                render: function (data) {
                    let badgeClass = "badge badge-secondary";
                    let statusText = data;

                    if (data === 1 || data === "Pending") {
                        badgeClass = "badge badge-warning text-dark";
                        statusText = "Pending";
                    }
                    else if (data === 2 || data === "Processing") {
                        badgeClass = "badge badge-info";
                        statusText = "Processing";
                    }
                    else if (data === 3 || data === "Shipped") {
                        badgeClass = "badge badge-primary";
                        statusText = "Shipped";
                    }
                    else if (data === 4 || data === "Delivered") {
                        badgeClass = "badge badge-success";
                        statusText = "Delivered";
                    }
                    else if (data === 5 || data === "Cancelled") {
                        badgeClass = "badge badge-danger";
                        statusText = "Cancelled";
                    }

                    return `<span class="${badgeClass}">${statusText}</span>`;
                }
            },
            { data: 'shippingStreet', name: 'ShippingStreet' },
            { data: 'shippingCity', name: 'ShippingCity' },
            {
                data: 'id',
                orderable: false,
                render: function (data) {
                    return `
                                <div class="form-button-action">

                                    <button type="button" class="btn btn-link btn-primary btn-lg" onclick="openUpdateModal('${data}')" data-bs-toggle="tooltip" title="update">
                                     <i class="fa fa-edit"></i>
                                     </button>
                                    <button type="button" class="btn btn-link btn-danger" onclick="confirmDelete('${data}')" data-bs-toggle="tooltip" title="delete">
                                        <i class="fa fa-times"></i>
                                    </button>
                                </div>
                            `;
                }
            }
        ]


    });
    loadCitiesLookup();
});



function confirmDelete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#ff6c00',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            deleteOrder(id);
        }
    });
}




function deleteOrder(id) {
    $.ajax({
        url: '/Order/DeleteOrder?id=' + id,
        type: 'DELETE',
        success: function (response) {

            Swal.fire({
                toast: true,
                position: 'bottom-end',
                icon: 'success',
                title: 'the order has been removed ',
                showConfirmButton: false,
                timer: 2000,
                timerProgressBar: true,
                background: '#FFA500',
                color: '#fff',
                iconColor: '#fff',
                showClass: {
                    popup: 'swal2-noanimation'
                },
                hideClass: {
                    popup: ''
                }
            });

            $('#ordersTable').DataTable().ajax.reload(null, false);
        },
        error: function (xhr, status, error) {
            Swal.fire(
                'Error!',
                'Something went wrong. Please try again.',
                'error'
            );
            console.error("Delete failed: ", error);
        }
    });
}


let updateOrderModalInstance = null;

function openUpdateModal(orderId) {
    $.ajax({
        url: `/Order/GetByIdOrder?id=${orderId}`,
        type: 'GET',
        success: function (response) {

            $('#editOrderId').val(response.id);
            $('#editCustomerId').val(response.customerId); 
            $('#editOrderDateRaw').val(response.orderDate);
            $('#editTotalAmountRaw').val(response.totalAmount);


            $('#editOrderStatus').val(response.status);
            $('#editShippingStreet').val(response.shippingStreet);

            $('#editShippingCity').val(response.shippingCity);

            loadCitiesLookup(function () {
                $('#editShippingCity').val(response.shippingCity);
            });
            if (response.orderDate) {
                let date = new Date(response.orderDate);
                $('#editOrderDate').val(date.toLocaleDateString());
            } else {
                $('#editOrderDate').val('N/A');
            }

            $('#editTotalAmount').val(response.totalAmount + ' $');


            if (!updateOrderModalInstance) {
                updateOrderModalInstance = new bootstrap.Modal(document.getElementById('updateOrderModal'));
            }
            updateOrderModalInstance.show();
        },
        error: function (error) {
            Swal.fire('error!');
        }
    });
}


function loadCitiesLookup(callback) {
    $.ajax({
        url: '/Order/GetCities',
        type: 'GET',
        success: function (cities) {
            let select = $('#editShippingCity');
            select.empty();
            select.append('<option value="">Select City</option>');

            cities.forEach(city => {
                select.append(`<option value="${city.value}">${city.value}</option>`);
            });

            if (callback) callback();
        },
        error: function () {
            console.error("error loading city");
        }
    });
}
function UpdateOrder() {
    var formData = {
        id: $('#editOrderId').val(),
        customerId: $('#editCustomerId').val(),
        orderDate: $('#editOrderDateRaw').val(),
        totalAmount: parseFloat($('#editTotalAmountRaw').val()), 
        status: parseInt($('#editOrderStatus').val()),
        shippingStreet: $('#editShippingStreet').val(),
        shippingCity: $('#editShippingCity').val()
    };

    $.ajax({
        url: '/Order/UpdateOrder',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            updateOrderModalInstance.hide();

            Swal.fire({
                toast: true,
                position: 'bottom-end',
                icon: 'success',
                title: 'the order has been updated ',
                showConfirmButton: false,
                timer: 2000,
                timerProgressBar: true,
                background: '#FFA500',
                color: '#fff',
                iconColor: '#fff',
                showClass: {
                    popup: 'swal2-noanimation'
                },
                hideClass: {
                    popup: ''
                }
            });

            $('#ordersTable').DataTable().ajax.reload(null, false);
        },
        error: function (xhr) {
            Swal.fire('Error!', 'Something went wrong!', 'error');
        }
    });
}


