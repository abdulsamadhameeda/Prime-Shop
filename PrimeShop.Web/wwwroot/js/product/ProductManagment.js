
$(document).ready(function () {
    $('#DataTable').DataTable({

        processing: false,
        serverSide: true,
        ordering: false,
        pageLength: 5,
        lengthMenu: [5, 10, 20, 30],
        ajax: {
            url: '/Product/GetProductsData',
            type: 'POST',
        },
        columns: [
            {
                data: 'imageUrl',
                name: 'ImageUrl',
                orderable: false,
                render: function (data) {
                    var src = (data && data.length > 0) ? data : '/img/product/p1.jpg';
                    return `<img src="${src}" alt="product" style="width:50px;height:50px;object-fit:cover;border-radius:4px;" />`;
                }
            },
            { data: 'name', name: 'Name' },
            {
                data: 'description',
                name: 'Description',
                render: function (data) {
                    if (!data) return '';
                    return data.length > 60 ? data.substring(0, 60) + '...' : data;
                }
            },
            {
                data: 'price',
                name: 'Price',
                render: function (data) {
                    return parseFloat(data).toFixed(2) + " $";
                }
            },
            {
                data: 'stockQuantity',
                name: 'StockQuantity',
                render: function (data) {
                    let badgeClass = "badge badge-success";
                    if (data === 0) {
                        badgeClass = "badge badge-danger";
                    } else if (data < 10) {
                        badgeClass = "badge badge-warning";
                    }
                    return `<span class="${badgeClass}">${data}</span>`;
                }
            },
            { data: 'categoryName', name: 'Category' },
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
});
let updateProductModalInstance = null;


// function openUpdateModal(productId) {
//     $.ajax({
//         url: `/Product/GetByIdProduct?id=${productId}`,
//         type: 'GET',
//         success: function (response) {
//             console.log("البيانات القادمة من السيرفر:", response);
//             $('#editProductId').val(response.id);
//             $('#editName').val(response.name);
//             $('#editPrice').val(response.price);
//             $('#editStockQuantity').val(response.stockQuantity);
//             $('#editDescription').val(response.description);

//             $('#editImage').val('');

//             let productImage = response.productImage;
//             if (productImage) {
//                 if (productImage.startsWith('data:image')) {
//                     $('#imagePreview').attr('src', productImage);
//                 } else {
//                     $('#imagePreview').attr('src', 'data:image/png;base64,' + productImage);
//                 }
//                 $('#existingImageUrl').val(productImage);
//             } else {
//                 $('#imagePreview').attr('src', '/images/no-image.png');
//                 $('#existingImageUrl').val('');
//             }


//             loadCategories(function () {
//                 $('#editCategory').val(response.categoryId || response.category);
//             });




//             if (!updateProductModalInstance) {
//                 updateProductModalInstance = new bootstrap.Modal(document.getElementById('updateProductModal'));
//             }
//             updateProductModalInstance.show();
//         },
//         error: function (error) {
//             Swal.fire('error!');
//         }
//     });
// }
function previewSelectedImage(input) {
    if (input.files && input.files[0]) {
        $('#imagePreview').attr('src', URL.createObjectURL(input.files[0]));
    }
}
function openUpdateModal(productId) {
    $.ajax({
        url: `/Product/GetByIdProduct?id=${productId}`,
        type: 'GET',
        success: function (response) {
            console.log("البيانات القادمة من السيرفر:", response);

            $('#editProductId').val(response.id);
            $('#editName').val(response.name);
            $('#editPrice').val(response.price);
            $('#editStockQuantity').val(response.stockQuantity);
            $('#editDescription').val(response.description);

            $('#editImage').val('');

            let imageUrl = response.imageUrl;

            if (imageUrl) {
                $('#existingImageUrl').val(imageUrl);
                $('#imagePreview').attr('src', imageUrl);
            } else {
                $('#existingImageUrl').val('');
                $('#imagePreview').attr('src', '/images/no-image.png');
            }

            loadCategories(function () {
                $('#editCategory').val(response.categoryId || response.category);
            });

            if (!updateProductModalInstance) {
                updateProductModalInstance = new bootstrap.Modal(document.getElementById('updateProductModal'));
            }
            updateProductModalInstance.show();
        },
        error: function (error) {
            Swal.fire('Error!', 'Could not load product data', 'error');
        }
    });
}

function loadCategories(callback) {
    $.ajax({
        url: '/Category/GetCategories',
        type: 'GET',
        success: function (categories) {
            let categorySelect = $('#editCategory');
            categorySelect.empty();

            categorySelect.append('<option value="">Select Categories</option>');

            $.each(categories, function (index, category) {
                categorySelect.append(
                    `<option value="${category.id}">${category.name}</option>`
                );
            });

            if (typeof callback === 'function') {
                callback();
            }
        },
        error: function (error) {
            console.error(error);
        }
    });
}

// function UpdateProduct() {

//     var formData = {
//         id: $('#editProductId').val(),
//         name: $('#editName').val(),
//         price: parseFloat($('#editPrice').val()),
//         categoryId: $('#editCategory').val(),
//         stockQuantity: parseInt($('#editStockQuantity').val()),
//         description: $('#editDescription').val(),
//         productImage: $('#editImage')[0].files[0] || $('#existingImageUrl').val()
//     };
//     if (!formData) {
//         Swal.fire({
//             title: 'warning',
//             text: "Please fill in all required fields!",
//             icon: 'warning',
//             confirmButtonColor: '#FFA500',
//             confirmButtonText: 'ok',
//         });
//         return
//     }
//     $.ajax({
//         url: '/Product/UpdateProduct',
//         type: 'POST',
//         contentType: 'application/json',
//         data: JSON.stringify(formData),
//         success: function (response) {

//             updateProductModalInstance.hide();

//             Swal.fire({
//                 toast: true,
//                 position: 'bottom-end',
//                 icon: 'success',
//                 title: 'the product has been updated ',
//                 showConfirmButton: false,
//                 timer: 2000,
//                 timerProgressBar: true,
//                 background: '#FFA500',
//                 color: '#fff',
//                 iconColor: '#fff',
//                 showClass: {
//                     popup: 'swal2-noanimation'
//                 },
//                 hideClass: {
//                     popup: ''
//                 }
//             });

//             $('#productsTable').DataTable().ajax.reload(null, false);

//         },
//         error: function (xhr) {
//             Swal.fire('Error!', 'Something went wrong!', 'error');
//         }
//     });
// }
function UpdateProduct() {
    let name = $('#editName').val();
    let price = parseFloat($('#editPrice').val());
    let categoryId = $('#editCategory').val();

    if (!name || !price || !categoryId) {
        Swal.fire({
            title: 'warning',
            text: "Please fill in all required fields!",
            icon: 'warning',
            confirmButtonColor: '#FFA500',
            confirmButtonText: 'ok',
        });
        return;
    }

    let formData = new FormData();
    formData.append('Id', $('#editProductId').val());
    formData.append('Name', name);
    formData.append('Price', price);
    formData.append('CategoryId', categoryId);
    formData.append('StockQuantity', parseInt($('#editStockQuantity').val()));
    formData.append('Description', $('#editDescription').val());
            formData.append('ImageUrl', $('#existingImageUrl').val() || '');

    let fileInput = $('#editImage')[0];
    if (fileInput.files && fileInput.files[0]) {
        formData.append('image', fileInput.files[0]);
    }

    $.ajax({
        url: '/Product/UpdateProduct',
        type: 'POST',
        data: formData,
        processData: false,   // مهم
        contentType: false,   // مهم
        success: function (response) {
            updateProductModalInstance.hide();
            Swal.fire({
                toast: true, position: 'bottom-end', icon: 'success',
                title: 'the product has been updated',
                showConfirmButton: false, timer: 2000, timerProgressBar: true,
                background: '#FFA500', color: '#fff', iconColor: '#fff',
                showClass: { popup: 'swal2-noanimation' }, hideClass: { popup: '' }
            });
            $('#DataTable').DataTable().ajax.reload(null, false);
        },
        error: function () {
            Swal.fire('Error!', 'Something went wrong!', 'error');
        }
    });
}