$(document).ready(function () {
    loadOrders(1);

    $('#order-status-filter, #order-from-date, #order-to-date, #orders-per-page')
        .on('change', function () {
            loadOrders(1);
        });

    $('#orders-pagination').on('click', 'a[data-page]', function (e) {
        e.preventDefault();
        loadOrders(parseInt($(this).attr('data-page')));
    });
});



function loadOrders(page) {
    var pageSize = parseInt($('#orders-per-page').val()) || 12;

    $.ajax({
        url: '/Order/FilterMyOrders',
        type: 'GET',
        data: {
            status: $('#order-status-filter').val() || null,
            fromDate: $('#order-from-date').val() || null,
            toDate: $('#order-to-date').val() || null,
            draw: page,
            start: (page - 1) * pageSize,
            length: pageSize
        },
        success: function (result) {
            renderOrders(result.data || []);
            renderOrdersPagination(
                result.recordsFiltered || 0,
                pageSize,
                page
            );
        },
        error: function () {
            $('#orders-container').html(
                '<div class="alert alert-danger text-center">Could not load orders. Please try again.</div>'
            );
            $('#orders-pagination').empty();
        }
    });
}

function renderOrders(orders) {
    if (orders.length === 0) {
        $('#orders-container').html(
            '<div class="alert text-center" role="alert" style="background-color: #fff8f0; color: #333;">You don\'t have any orders matching these filters.</div>'
        );
        return;
    }

    var html = '';

    orders.forEach(function (order) {
        var safeId = escapeHtml(order.id);
        var collapseId = 'collapse_' + safeId.replaceAll('-', '');
        var headingId = 'heading_' + safeId.replaceAll('-', '');
        var status = getOrderStatus(order.status);
        var total = Number(order.totalAmount || 0).toFixed(2);

        html += `
            <div class="card mb-3 shadow-sm" style="border-left: 5px solid #FFA500; border-radius: 8px;">
                <div class="card-header p-0" style="background: #fff;" id="${headingId}">
                    <h5 class="mb-0">
                        <button class="btn btn-link btn-block text-left p-4 d-flex justify-content-between align-items-center collapsed text-decoration-none"
                                data-toggle="collapse"
                                data-target="#${collapseId}"
                                aria-expanded="false"
                                aria-controls="${collapseId}">
                            <div>
                                <span class="font-weight-bold h6 d-block mb-1" style="color: #333;">
                                    Order #${safeId}
                                </span>
                                <small class="text-muted">
                                    <i class="fa fa-calendar mr-1"></i>
                                    ${formatOrderDate(order.orderDate)}
                                </small>
                            </div>
                            <span class="badge ${status.cssClass} p-2">${status.name}</span>
                        </button>
                    </h5>
                </div>

                <div id="${collapseId}" class="collapse" aria-labelledby="${headingId}">
                    <div class="card-body p-4">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <small class="text-muted d-block">Order ID</small>
                                <strong>${safeId}</strong>
                            </div>
                            <div class="col-md-6 mb-3">
                                <small class="text-muted d-block">Shipping Street</small>
                                <strong>${escapeHtml(order.shippingStreet)}</strong>
                            </div>
                            <div class="col-md-6 mb-3">
                                <small class="text-muted d-block">Shipping City</small>
                                <strong>${escapeHtml(order.shippingCity)}</strong>
                            </div>
                            <div class="col-md-6 mb-3">
                                <small class="text-muted d-block">Total Amount</small>
                                <strong style="color: #FFA500; font-size: 1.2rem;">$${total}</strong>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;
    });

    $('#orders-container').html(html);
}

function renderOrdersPagination(recordsFiltered, pageSize, currentPage) {
    var totalPages = Math.ceil(recordsFiltered / pageSize);

    if (totalPages <= 1) {
        $('#orders-pagination').empty();
        return;
    }

    var html = '';

    if (currentPage > 1) {
        html += `<a href="#" class="prev-arrow" data-page="${currentPage - 1}">
                    <i class="fa fa-long-arrow-left" aria-hidden="true"></i>
                 </a>`;
    }

    for (var page = 1; page <= totalPages; page++) {
        html += `<a href="#" data-page="${page}" class="${page === currentPage ? 'active' : ''}">${page}</a>`;
    }

    if (currentPage < totalPages) {
        html += `<a href="#" class="next-arrow" data-page="${currentPage + 1}">
                    <i class="fa fa-long-arrow-right" aria-hidden="true"></i>
                 </a>`;
    }

    $('#orders-pagination').html(html);
}

function getOrderStatus(status) {
    var statuses = {
        '1': { name: 'Pending', cssClass: 'badge-warning' },
        '2': { name: 'Processing', cssClass: 'badge-info' },
        '3': { name: 'Shipped', cssClass: 'badge-primary' },
        '4': { name: 'Delivered', cssClass: 'badge-success' },
        '5': { name: 'Cancelled', cssClass: 'badge-danger' },
        pending: { name: 'Pending', cssClass: 'badge-warning' },
        processing: { name: 'Processing', cssClass: 'badge-info' },
        shipped: { name: 'Shipped', cssClass: 'badge-primary' },
        delivered: { name: 'Delivered', cssClass: 'badge-success' },
        cancelled: { name: 'Cancelled', cssClass: 'badge-danger' }
    };

    return statuses[String(status).toLowerCase()]
        || { name: 'Unknown', cssClass: 'badge-dark' };
}

function formatOrderDate(value) {
    var date = String(value).split('T')[0];
    var parts = date.split('-');

    return parts.length === 3
        ? `${parts[2]}/${parts[1]}/${parts[0]}`
        : date;
}

function escapeHtml(value) {
    return $('<div>').text(value || '').html();
}
