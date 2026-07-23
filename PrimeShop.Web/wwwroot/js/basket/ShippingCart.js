$(document).ready(function () {
    var basket = JSON.parse(localStorage.getItem('basket')) || [];

    var token = jwtToken;

    if (basket.length === 0) {
        if (token) {
            loadCartFromDb();
        }
        else {
            $('#checkout-section').hide();
            $('#basket-items').html('<tr><td colspan="4" class="p-5"><div class="alert text-center" role="alert" style="background-color: #fff8f0; color: #333; border: 1px solid #ffe8cc; font-size: 18px;">Your cart is empty </div></td></tr>');
        }
        return;
    }


    if (token) {
        $.ajax({
            url: '/Basket/AddItemToBasket',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(basket),
            success: function () {
                console.log('Basket synced and saved to DB');

                localStorage.removeItem('basket');

                loadCartFromDb();
            },
            error: function (err) {
                console.error('Error add basket Item:', err);
            }
        });
    }
    else {
        loadCartProducts(basket);
    }
});

function loadCartProducts(currentBasket) {
    var ids = currentBasket.map(x => x.productId);
    if (ids.length === 0) {
        $('#checkout-section').hide();
        $('#basket-items').html('<tr><td colspan="4" class="p-5"><div class="alert text-center" role="alert" style="background-color: #fff8f0; color: #333; border: 1px solid #ffe8cc; font-size: 18px;">Your cart is empty </div></td></tr>');
        $('#basket-total').text('$0.00');
        return;
    }
    $.ajax({
        url: '/Basket/GetProductsByIds',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(ids),
        success: function (products) {
            var html = '';
            var total = 0;

            products.forEach(function (product) {
                var item = currentBasket.find(x => x.productId === product.id);
                var quantity = item ? item.quantity : 1;
                var itemTotal = product.price * quantity;
                total += itemTotal;
                var imgSrc = product.productImage ? `data:image/png;base64,${product.productImage}` : '/img/product/p1.jpg';
                html += `
    <tr>
        <td>
            <div class="media">
                <div class="d-flex">
                    <img src="${imgSrc}"     class="img-fluid" style="max-width: 80px;" alt="" >
                </div>
                <div class="media-body">
                    <p>${product.name}</p>
                </div>
            </div>
        </td>
        <td><h5>$${product.price}</h5></td>
        <td>${quantity}</td>
        <td><h5>$${itemTotal.toFixed(2)}</h5></td>
        <td>   <button type="button" class="btn  btn-danger" onclick="confirmDelete('${product.id}')" data-bs-toggle="tooltip" title="delete">
                                        <i class="fa fa-times fs-6"></i>
                                    </button> </td>

    </tr>
    `;
            });

            $('#basket-items').html(html);
            $('#basket-total').text('$' + total.toFixed(2));
        }
    });
}

function loadCartFromDb() {
    $.ajax({
        url: '/Basket/GetBasketItems',
        type: 'GET',
        success: function (items) {
            if (items.length === 0) {
                $('#checkout-section').hide();
                $('#basket-items').html('<tr><td colspan="4" class="p-5"><div class="alert text-center" role="alert" style="background-color: #fff8f0; color: #333; border: 1px solid #ffe8cc; font-size: 18px;">Your cart is empty</div></td></tr>');
                return
            };

            var ids = items.map(x => x.productId);

            $.ajax({
                url: '/Basket/GetProductsByIds',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(ids),
                success: function (products) {
                    var html = '';
                    var total = 0;

                    products.forEach(function (product) {
                        var item = items.find(x => x.productId === product.id);
                        var quantity = item ? item.quantity : 1;
                        var itemTotal = product.price * quantity;
                        total += itemTotal;
                        var basketItemId = item ? item.id : '';
                        html += `
                                                <tr>
                                                    <td>
                                                        <div class="media">
                                                            <div class="d-flex">
                                                               <img src="${product.productImage ? `data:image/png;base64,${product.productImage}` : '/img/product/p1.jpg'}"
                                                                 class="img-fluid" style="max-width: 80px;" alt="">
                                                            </div>
                                                            <div class="media-body">
                                                                <p>${product.name}</p>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td><h5>$${product.price}</h5></td>
                                                    <td>${quantity}</td>
                                                    <td><h5>$${itemTotal.toFixed(2)}</h5></td>
                                                    <td>   <button type="button" class="btn  btn-danger" onclick="confirmDelete('${basketItemId}')" data-bs-toggle="tooltip" title="delete">
                                                           <i class="fa fa-times fs-6"></i>
                                                           </button>
                                                    </td>                                          
                                                </tr>`
                            ;
                    });

                    $('#basket-items').html(html);

                    $('#basket-total').text('$' + total.toFixed(2));
                }
            });
        }
    });
}

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
            deleteItem(id);
        }
    });
}

function deleteItem(id) {
    var token = jwtToken;

    if (token) {
        $.ajax({
            url: '/Basket/DeleteItem?id=' + id,
            type: 'DELETE',
            success: function (response) {

                showSuccessToast();
                loadCartFromDb();


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
    else {

        var basket = JSON.parse(localStorage.getItem('basket')) || [];

        basket = basket.filter(x => x.productId !== id);
        localStorage.setItem('basket', JSON.stringify(basket));

        showSuccessToast();
        loadCartProducts(basket);

    }
}

function showSuccessToast() {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'success',
        title: 'Your item has been removed from the basket.',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true,
        background: '#FFA500',
        color: '#fff',
        iconColor: '#fff',
        showClass: { popup: 'swal2-noanimation' },
        hideClass: { popup: '' }
    });
}

