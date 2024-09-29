$(document).ready(function () {
    $('#emailModal').on('show.bs.modal', function () {
        var form = $('#formGerar');
        $('#quantidade').val(form.find('#quantidade').val());
    });

    $('#emailForm').on('submit', function (event) {
        event.preventDefault();

        var form = $(this);

        $.ajax({
            type: 'POST',
            url: '/Exportar/EnviarEmail',
            data: form.serialize(),
            success: function (response) {
                var alertClass = response.success ? 'alert-success' : 'alert-danger';
                var alertMessage = response.message;

                $('#emailFeedback').html('<div class="alert ' + alertClass + '">' + alertMessage + '</div>');

                if (response.success) {
                    setTimeout(function () {
                        $('#emailModal').modal('hide');
                        $('.modal-backdrop').remove();
                    }, 500);
                }
            },
            error: function () {
                $('#emailFeedback').html('<div class="alert alert-danger">Ocorreu um erro ao enviar o e-mail. Tente novamente.</div>');
            }
        });
    });

    $('#emailModal').on('hidden.bs.modal', function () {
        $('#emailFeedback').empty();
    });
});
