
$(document).ready(function () {
    loadOrderSummary();
});

function loadOrderSummary() {
    $.ajax({
        url: '/Basket/GetBasketItems',
        type: 'GET',
        success: function (items) {
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

                        html += `
            <li>
                <a href="#">${product.name}
                    <span class="middle">x ${quantity}</span>
                    <span class="last">$${itemTotal.toFixed(2)}</span>
                </a>
            </li>
        `;
                    });

                    $('#order-items').html(html);

                    $('#order-total').text('$' + total.toFixed(2));
                    $('#total-amount').val(total.toFixed(2));
                }
            });
        }
    });
}


$('input[name="paymentMethod"]').change(function () {
    if ($(this).val() === 'Visa') {
        $('#visa-form').show();
    } else {
        $('#visa-form').hide();
    }
});

