document.addEventListener('DOMContentLoaded', function () {
    const showModal = () => {
        $('#loginModal').modal('show');
    };


    const loginButton = document.getElementById('loginButton');
    if (loginButton) {
        loginButton.addEventListener('click', function (event) {
            showModal();
        });
    }

    $('#loginModal').on('shown.bs.modal', function () {
        console.log('Modal is shown');
    });
});

