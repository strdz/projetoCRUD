var departamentos = [];
var produtos = [];
var listaFiltrada = [];

$("#btn_filtros").click(function () {
    $("#filters_section").toggleClass("invisible");
});

function LimparCampos(campo) {
    switch (campo) {
        case "filtros":
            $("#txt_filtroCodigoProduto").val("");
            $("#txt_filtroDescricao").val("");
            $("#select_filtroAtivo").val(0);
            $("#select_filtroDepartamento").val(0);
            break;
        case "modal":
            $("#txt_IdProduto").val("");
            $("#txt_CodigoProduto").val("");
            $("#txt_modalDescricao").val("");
            $("#txt_modalPreco").val("");
            $("#select_modalDepartamento").val(0);
            $("#select_modalStatus").val(0);
            break;
    }
}

function CarregaDepartamentos() {
    $.ajax({
        url: "/Produto/Produto/CarregaDepartamentos",
        type: 'GET',
        async: false,
        success: function (data) {
            if (data !== "[]") {
                $("#select_modalDepartamento option ").remove(":not(option[value='0'])");
                $("#select_filtroDepartamento option ").remove(":not(option[value='0'])");                
                departamentos = JSON.parse(data);
                for (var item in departamentos) {
                    $('#select_modalDepartamento').append('<option value="' + departamentos[item].id + '">' + departamentos[item].nome + '</option>');
                    $('#select_filtroDepartamento').append('<option value="' + departamentos[item].id + '">' + departamentos[item].nome + '</option>');
                }
            }
        }
    });
}

function CarregaProdutos() {
    $("#lista_Item .list_item").remove();

    $.ajax({
        url: "/Produto/Produto/Listar",
        type: 'GET',
        async: false,
        success: function (data) {
            if (data !== "[]") {
                produtos = JSON.parse(data);
                produtos.forEach(function (element) {
                    if (element.STATUS === "A") {
                        $('#lista_Item').append(
                            '<div class="list_item blue_row" onclick="">'
                            + '<ul id="' + element.ID + '">'
                            + '<li class="col-1">' + element.ID + '</li>'
                            + '<li class="col-1">' + element.CODIGO + '</li>'
                            + '<li class="col-3">' + element.DESCRICAO + '</li>'
                            + '<li class="col-2">' + departamentos[element.ID_DEPARTAMENTO - 1].nome + '</li>'
                            + '<li class="col-1">' + element.USUARIO + '</li>'
                            + '<li class="col-3"><button type ="button" class="btn btn-info" id ="btn_editarUsuario" onclick ="EditarProduto(this);"> <i class="fa fa-edit"></i> Editar</button> / <button type="button" class="btn btn-danger" id="btn_bloquearUsuario" onclick="ExcluirProduto(this);"><i class="fa fa-close"></i> Excluir</button>'
                            + '</ul>'
                            + '</div>');
                    }
                });
            }
        }
    });
}

function EditarProduto(elemento) {
    var Id_Produto = elemento.parentElement.parentElement.id;
    var dados = produtos.filter(x => x.ID.toString() === Id_Produto.toString());

    $("#txt_IdProduto").val(dados[0].ID);
    $("#txt_CodigoProduto").val(dados[0].CODIGO);
    $("#txt_modalDescricao").val(dados[0].DESCRICAO);
    $("#txt_modalPreco").val(dados[0].PRECO);
    $("#select_modalDepartamento").val(dados[0].ID_DEPARTAMENTO);
    $("#select_modalStatus").val(dados[0].STATUS === "A" ? "true" : "false");

    AbrirModal();
}

function GravarProduto() {
    var data = new Object();

    if (ValidaProduto()) {
        data = {
            Id: $("#txt_IdProduto").val() === "" ? 0 : $("#txt_IdProduto").val(),
            Codigo: $("#txt_CodigoProduto").val(),
            Descricao: $("#txt_modalDescricao").val(),
            Preco: $("#txt_modalPreco").val(),
            Id_Departamento: $("#select_modalDepartamento").val(),
            Status: $("#select_modalStatus").val()
        };

        $.ajax({
            url: "/Produto/Produto/Adiciona",
            type: 'POST',
            datatype: "json",
            data: { produto: data },
            success: function (obj) {
                if (obj !== "[]") {
                    $("#cadastroProduto").modal("hide");
                    CarregaProdutos();
                    alert(obj);
                }
            }
        });
    }
}


function ExcluirProduto(elemento) {
    var Id_Produto = elemento.parentElement.parentElement.id;
    var data = new Object();
    if (confirm("Deseja realmente remover este produto?")) {
        data = {
            Id: Id_Produto
        };

        $.ajax({
            url: "/Produto/Produto/Remover",
            type: 'POST',
            datatype: "json",
            data: { produto: data },
            success: function (obj) {
                if (obj !== "[]") {
                    $("#cadastroProduto").modal("hide");
                    CarregaProdutos();
                    alert(obj);
                }
            }
        });
    }
}

function AbrirModal(novo = false) {
    if (novo) {
        LimparCampos("modal");
    }
    $("#cadastroProduto").modal("show");
}

function ValidaProduto() {
    var Codigo = $("#txt_CodigoProduto").val();
    var Descricao = $("#txt_modalDescricao").val();
    var Preco = $("#txt_modalPreco").val();
    var Id_Departamento = $("#select_modalDepartamento").val();
    var Status = $("#select_modalStatus").val();

    var valida = true;

    if (Codigo === "") {
        valida = false;
        $("#invalidaCodigo").removeClass("invisivel");
    }
    if (Descricao === "") {
        valida = false;
        $("#invalidaDescricao").removeClass("invisivel");
    }
    if (Preco === "") {
        valida = false;
        $("#invalidaPreco").removeClass("invisivel");
    }
    if (Id_Departamento === "0") {
        valida = false;
        $("#invalidaDepartamento").removeClass("invisivel");
    }
    if (Status === "0") {
        valida = false;
        $("#invalidaStatus").removeClass("invisivel");
    }

    return valida;
}

function FiltrarLista() {

    listaFiltrada = [];

    var filtroCodigo = $("#txt_filtroCodigoProduto").val();
    var filtroDescricao = $("#txt_filtroDescricao").val();
    var filtroAtivo = $("#select_filtroAtivo").val() === "0" ? "A" : $("#select_filtroAtivo").val() === "S" ? "A" : $("#select_filtroAtivo").val() === "T" ? "T" : "N";
    var filtroDepartamento = parseInt($("#select_filtroDepartamento").val());

    $("#lista_Item .list_item").remove();

    produtos.forEach(function (element) {
        var achouItem = true;

        if (filtroCodigo !== element["CODIGO"] && filtroCodigo !== "") {
            achouItem = false;
        }

        if (filtroDepartamento !== element["ID_DEPARTAMENTO"] && filtroDepartamento !== 0) {
            achouItem = false;
        }

        if (filtroAtivo !== "T" && filtroAtivo !== "") {
            if (filtroAtivo !== element["STATUS"] && filtroAtivo !== "") {
                achouItem = false;
            }
        }

        if (element["DESCRICAO"].indexOf(filtroDescricao) === -1) {
            achouItem = false;
        }
        if (achouItem) {

            listaFiltrada.push(element);
        }
    });

    listaFiltrada.forEach(function (element) {
        $('#lista_Item').append(
            '<div class="list_item '+ (element.STATUS === "A" ? "blue_row" : "red_row") + '">'
            + '<ul id="' + element.ID + '">'
            + '<li class="col-1">' + element.ID + '</li>'
            + '<li class="col-1">' + element.CODIGO + '</li>'
            + '<li class="col-3">' + element.DESCRICAO + '</li>'
            + '<li class="col-2">' + departamentos[element.ID_DEPARTAMENTO - 1].nome + '</li>'
            + '<li class="col-1">' + element.USUARIO + '</li>'
            + '<li class="col-3"><button type ="button" class="btn btn-info" id ="btn_editarUsuario" onclick ="EditarProduto(this);"> <i class="fa fa-edit"></i> Editar</button> / <button type="button" class="btn btn-danger" id="btn_bloquearUsuario" onclick="ExcluirProduto(this);"><i class="fa fa-close"></i> Excluir</button>'
            + '</ul>'
            + '</div>');
    });
}