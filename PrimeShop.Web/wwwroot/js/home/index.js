function addToBasket(productId) {
    var basket = JSON.parse(localStorage.getItem('basket')) || [];
    var token = jwtToken;
    if (token) {
        $.ajax({
            url: '/Basket/AddItemToBasket',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify([{ productId: productId, quantity: 1 }]),
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
                alert('Error adding to basket!');
            }
        });
    }
    else {
        var existing = basket.find(x => x.productId === productId);
        if (existing) {
            existing.quantity += 1;
        } else {
            basket.push({ productId: productId, quantity: 1 });
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