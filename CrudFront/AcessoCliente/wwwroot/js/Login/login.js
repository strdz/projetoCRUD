function Logar() {
    var valida = true;

    if ($("#loginUsuario").val() === "") {
        valida = false;
        $("#invalidaLoginUsuario").removeClass("invisivel");
    }
    if ($("#senhaUsuario").val() === "") {
        valida = false;
        $("#invalidaLoginSenha").removeClass("invisivel");
    }
    if (grecaptcha.getResponse(recaptchaLogin) === "") {
        valida = false;
        $("#invalidaLoginCaptcha").removeClass("invisivel");
    }

    if (valida) {
        var data = new FormData();

        data.append("Usuario", $("#loginUsuario").val());
        data.append("Senha", $("#senhaUsuario").val());
        data.append("ValidaCaptcha", grecaptcha.getResponse(recaptchaLogin));

        $.ajax({
            url: "/Usuario/Login",
            type: 'POST',
            async: false,
            contentType: false,
            processData: false,
            data: data,
            success: function (obj) {
                if (obj !== "[]") {
                    if (obj.redirectToUrl !== undefined) {
                        window.location.href = obj.redirectToUrl;
                    } else if (obj.error) {
                        alert("Usuario ou senha Incorreta.");
                    } else {
                        alert("Usuário logado com sucesso.");
                    }
                }
            }
        });
    }
}

function RegistrarUsuario() {
    var data = new Object();
    if (validaRegistro()) {
        data = {
            Nome: $("#txt_modalUsuarioNomeCompleto").val(),
            Usuario: $("#txt_modalUsuario").val(),
            Email: $("#txt_modalUsuarioEmail").val(),
            Senha: $("#txt_modalSenha").val(),
            ValidaCaptcha: grecaptcha.getResponse(recaptchaCadastro)
        };

        $.ajax({
            url: "/Usuario/Registra",
            type: 'POST',
            datatype: "json",
            data: { usuario: data },
            success: function (obj) {
                if (obj !== "[]") {
                    alert(obj);
                    $('#modal_cadastroUsuario').modal('hide');
                }
            }
        });
    }
}

$("#logout").click(function () {
    $.ajax({
        url: "/Usuario/Logout",
        type: 'POST',
        async: false,
        contentType: false,
        processData: false,
        success: function (obj) {
            if (obj !== "[]") {
                if (obj.redirectToUrl !== undefined) {
                    window.location.href = obj.redirectToUrl;
                }

            }
        }
    });
});

function validaRegistro() {
    var Nome = $("#txt_modalUsuarioNomeCompleto").val();
    var Usuario = $("#txt_modalUsuario").val();
    var Email = $("#txt_modalUsuarioEmail").val();
    var ConfirmaEmail = $("#txt_modalConfirmaUsuarioEmail").val();
    var Senha = $("#txt_modalSenha").val();
    var ConfirmaSenha = $("#txt_modalConfirmaSenha").val();

    var valida = true;

    if (Nome === "") {
        valida = false;
        $("#invalidaNome").removeClass("invisivel");
    }
    if (Usuario === "") {
        valida = false;
        $("#invalidaUsuario").removeClass("invisivel");
    }
    if (Email === "" || ConfirmaEmail !== Email || ConfirmaEmail === "") {
        valida = false;
        $("#invalidaEmail").removeClass("invisivel");
    }
    if (Senha === "") {
        valida = false;
        $("#invalidaSenha").removeClass("invisivel");
    }
    if (ConfirmaSenha === "" || ConfirmaSenha !== Senha) {
        valida = false;
        $("#invalidaSenhaConfirma").removeClass("invisivel");
    }
    if (grecaptcha.getResponse(recaptchaCadastro) === "") {
        valida = false;
        $("#invalidaCadastroCaptcha").removeClass("invisivel");
    }

    return valida;
}

function Redirecionar(pagina = "") {
    if (pagina == "menu") {
        window.location.href = "http://localhost:53287/Cliente/Dashboard";
    } else if (pagina == "produto") {
        window.location.href = "http://localhost:53287/Produto/Produto";
    }
}