$(document).ready(function () {
    filters(null, null, null); 

    $('#price-filter-form').on('submit', function (e) {
        e.preventDefault(); 

        var categoryId = $('#selected-category-id').val() || null;

        var minPrice = $('input[name="minPrice"]').val() || null;
        var maxPrice = $('input[name="maxPrice"]').val() || null;

        filters(categoryId, minPrice, maxPrice);
    });


   




});
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

function filters(categoryId, minPrice, maxPrice) {
    $('#selected-category-id').val(categoryId);

    
    $.ajax({
        url: '/Product/FilterProducts',
        type: 'GET',
        data: {
            categoryId: categoryId,
            minPrice: minPrice,
            maxPrice: maxPrice
        },
        success: function (products) {
            $.get('/Wishlist/GetWishlistItems', function (wishlistItems) {

            var html = '';

            if (products.length === 0) {
                html = '<div class="col-12 text-center"><p>No products found.</p></div>';
            } else {

                products.forEach(function (product) {
                    var isOutOfStock = product.stockQuantity <= 0;
                    var imgSrc = product.imageUrl
                        ? product.imageUrl
                        : '/img/product/p1.jpg';
                    var wishlistItem = wishlistItems.find(
                        item => item.productId.toLowerCase() === product.id.toLowerCase()
                    );
                    html += `
                        <div class="col-lg-4 col-md-6">
                            <div class="single-product">
                                <img class="img-fluid" src="${imgSrc}" alt="${product.name}"
                                     style="background-color: #f8f9fa; object-fit: contain; width: 100%; height: 250px;">
                                <div class="product-details">
                                    <h6>${product.name}</h6>
                                    <div class="price">
                                        <h6>$${product.price}</h6>
                                    </div>
                                    <div class="prd-bottom">
                                        <a ${!isOutOfStock ? `onclick="addToBasket('${product.id}')"` : ''}
                                            class="social-info"
                                            style="${isOutOfStock ? 'pointer-events: none; opacity: 0.5; cursor: not-allowed;' : 'cursor: pointer;'}">
                                            <span class="ti-bag"></span>
                                            <p class="hover-text">${isOutOfStock ? 'Out of stock' : 'add to bag'}</p>
                                        </a>
                                       <a href="#"
   class="social-info"
   data-wishlist-id="${wishlistItem ? wishlistItem.id : ''}"
   onclick="addToWishlist('${product.id}', this); return false;">
    <span class="wishlist-icon ${wishlistItem ? 'fas' : 'far'} fa-heart"></span>
    <p class="hover-text">Wishlist</p>
</a>
                                        <a href="/Product/ProductDetails/${product.id}" class="social-info">
                                            <span class="lnr lnr-eye"></span>
                                            <p class="hover-text">view more</p>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                });
            }

                $('#products-container').html(html);
            });
        }
    });
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
