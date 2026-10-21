function confirmWishlistDelete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#ff6c00',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then(function (result) {
        if (result.isConfirmed) {
            deleteWishlistItem(id);
        }
    });
}

function deleteWishlistItem(id) {
    if (!jwtToken) {
        Swal.fire('Please login to manage your wishlist.');
        return;
    }

    $.ajax({
        url: '/Wishlist/Remove',
        type: 'POST',
        data: { id: id },

        success: function () {
            $('#wishlist-row-' + id).remove();

            if ($('#wishlist-items tr').length === 0) {
                $('#wishlist-items').html(`
                    <tr>
                        <td colspan="3" class="p-5">
                            <div class="alert text-center mb-0"
                                 style="background-color: #fff8f0; color: #333; border: 1px solid #ffe8cc; font-size: 18px;">
                                Your wishlist is empty
                            </div>
                        </td>
                    </tr>
                `);
            }

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
            Swal.fire(
                'Error!',
                'Something went wrong. Please try again.',
                'error'
            );
        }
    });
}