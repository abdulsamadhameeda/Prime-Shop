$(function () {
    $('#contactForm').on('submit', submitContactForm);
});

function submitContactForm(e) {
    e.preventDefault();

    const form = $(this);

    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            form[0].reset();

         Swal.fire({
    toast: true,
    position: 'bottom-end',
    icon: 'success',
    title: 'Message sent successfully!',
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
        error: function (xhr) {
          Swal.fire({
    toast: true,
    position: 'bottom-end',
    icon: 'error',
    title: 'Error sending your message!',
    showConfirmButton: false,
    timer: 2000,
    timerProgressBar: true,
    background: '#ff4444',
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
    });
}