
function addToBasket(productId, stockQuantity) {
    var basket = JSON.parse(localStorage.getItem('basket')) || [];
    var quantityInput = parseInt(document.getElementById('sst')?.value) || 1;
    var token = jwtToken;

    if (stockQuantity <= 0) {
        Swal.fire({
            icon: 'error',
            title: 'Out of Stock',
            text: 'Sorry, this product is currently sold out!',
            confirmButtonColor: '#ff6c00'
        });
        return;
    }

    if (quantityInput > stockQuantity) {
        Swal.fire({
            icon: 'warning',
            title: 'Limit Exceeded!',
            html: `Sorry, we only have <b>${stockQuantity}</b> items left in stock. <br>Please lower your quantity.`,
            confirmButtonColor: '#ff6c00'
        });
        return; 
    }

    if (quantityInput <= 0) {
        Swal.fire({
            icon: 'warning',
            title: 'Invalid Quantity',
            text: 'Please enter a valid quantity of 1 or more.',
            confirmButtonColor: '#ff6c00'
        });
        return;
    }
    if (token) {
        $.ajax({
            url: '/Basket/AddItemToBasket',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify([{ productId: productId, quantity: quantityInput }]),
            success: function () {
                Swal.fire({
                    toast: true,
                    position: 'bottom-end',
                    icon: 'success',
                    title: 'Added to basket!',
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
            },
            error: function () {
                Swal.fire({
                    toast: true,
                    position: 'bottom-right',
                    icon: 'error',
                    title: 'Error adding to basket!',
                    showConfirmButton: false,
                    timer: 2000,
                    background: '#ff4444',
                    color: '#fff',
                    iconColor: '#fff'
                });
            }
        });
    } else {
        var existing = basket.find(x => x.productId === productId);

        if (existing) {
            if (existing.quantity + quantityInput > stockQuantity) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Limit Exceeded!',
                    text: `You already have ${existing.quantity} of this item in your cart. Adding ${quantityInput} more exceeds our stock of ${stockQuantity}.`,
                    confirmButtonColor: '#ff6c00'
                });
                return;
            }
            existing.quantity += quantityInput;
        } else
        {
            basket.push({ productId: productId, quantity: quantityInput });
        }

        localStorage.setItem('basket', JSON.stringify(basket));
        Swal.fire({
            toast: true,
            position: 'bottom-end',
            icon: 'success',
            title: 'Added to basket!',
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
    }
}


$(function () {
    const button = $('#wishlist-button');
    const productId = button.attr('data-product-id').toLowerCase();

    $.get('/Wishlist/GetWishlistItems', function (items) {
        var item = items.find(function (x) {
            return x.productId.toLowerCase() === productId;
        });

        if (item) {
            button.attr('data-wishlist-id', item.id);
            button.find('.wishlist-icon')
                .removeClass('far')
                .addClass('fas');
        }
    });
});


function addToWishlist(productId, link) {
    var token = jwtToken;
    const heart = $(link).find('.wishlist-icon');

    if (!token) {
        Swal.fire({
            toast: true,
            position: 'bottom-end',
            icon: 'warning',
            title: 'Please login to use your wishlist.',
            showConfirmButton: false,
            timer: 2000
        });
        return;
    }

    if (heart.hasClass('fas')) {


        $.ajax({
            url: '/Wishlist/Remove',
            type: 'POST',
            data: {
                id: $(link).attr('data-wishlist-id')
            },
            success: function () {
                heart.removeClass('fas').addClass('far');
                $(link).removeAttr('data-wishlist-id');

                Swal.fire({
                    toast: true,
                    position: 'bottom-end',
                    icon: 'success',
                    title: 'Removed from wishlist!',
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
            },
            error: function () {
                alert('Error removing from wishlist!');
            }
        });


        return;
    }

    $.ajax({
        url: '/Wishlist/Add',
        type: 'POST',
        data: { productId: productId },
        success: function (id) {
            heart.removeClass('far').addClass('fas');
            $(link).attr('data-wishlist-id', id);

            Swal.fire({
                toast: true,
                position: 'bottom-end',
                icon: 'success',
                title: 'Added to Wishlist!',
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
        },
        error: function () {
            alert('Error adding to wishlist!');
        }
    });
}
