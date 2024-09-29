document.addEventListener('DOMContentLoaded', function () {
    var senha = document.getElementById('Senha');
    var confirmarSenha = document.getElementById('ConfirmarSenha');
    var feedback = document.getElementById('confirmPasswordFeedback');

    confirmarSenha.addEventListener('input', function () {
        if (senha.value !== confirmarSenha.value) {
            feedback.style.display = 'block';
        } else {
            feedback.style.display = 'none';
        }
    });
});
