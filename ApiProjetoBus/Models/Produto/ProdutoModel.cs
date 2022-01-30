using ConexaoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProjetoBus.Models.Produto
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Decimal Preco { get; set; }
        public int Id_Departamento { get; set; }
        public bool Status { get; set; }
        public int Id_Usuario { get; set; }
        
        public string Adicionar()
        {
            var retorno = "";
            try
            {
                Conexao conexao = new Conexao();

                var Codigo = $"'{this.Codigo}'";
                var Preco = $"'{this.Preco}'";
                var Descricao = $"'{this.Descricao}'"; 
                var Id_Departamento = $"'{this.Id_Departamento}'";
                var Status = this.Status == true ? "A" : "N";
                var Id_Usuario = $"'{this.Id_Usuario}'";

                if (conexao.InserirDados("INSERT INTO PRODUTOS (ID, CODIGO, PRECO, DESCRICAO, ID_DEPARTAMENTO, STATUS, ID_USUARIO) VALUES (0, " + Codigo + ", " + Preco + ", " + Descricao + ", " + Id_Departamento + ", '" + Status + "', " + Id_Usuario + ")") == 0)
                {
                    retorno = "SUCESSO";
                } else
                {
                    retorno = "Falha ao adicionar no banco de dados, verifique novamente os dados.";
                }
            }

            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }
        public string Alterar()
        {
            var retorno = "";
            try
            {

                Conexao conexao = new Conexao();

                var Preco = $"'{this.Preco}'".Replace(",",".");
                var Status = this.Status == true ? "A" : "N";

                if (conexao.InserirDados($"UPDATE PRODUTOS SET CODIGO = '{this.Codigo}', PRECO = {Preco}, DESCRICAO = '{this.Descricao}', ID_DEPARTAMENTO = '{this.Id_Departamento}', STATUS = '{Status}', ID_USUARIO_ALTERACAO = '{this.Id_Usuario}' WHERE ID = '{this.Id}'") == 0)
                {
                    retorno = "SUCESSO";
                } else
                {
                    retorno = "Falha ao atualizar no banco de dados, verifique novamente os dados.";
                }
            
            }

            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        //POR NAO REMOVER FISICAMENTE, SÓ ESTÁ ALTERANDO O STATUS PARA INATIVO.
        public string Remover()
        {
            var retorno = "";
            try
            {

                Conexao conexao = new Conexao();

                if (conexao.InserirDados($"UPDATE PRODUTOS SET STATUS = 'N', ID_USUARIO_ALTERACAO = '{this.Id_Usuario}' WHERE ID = '{this.Id}'") == 0)
                {
                    retorno = "SUCESSO";
                }
                else
                {
                    retorno = "Falha ao atualizar no banco de dados, verifique novamente os dados.";
                }

            }

            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        public IEnumerable<Dictionary<string, dynamic>> Listar()
        {

            string sql = " SELECT PR.ID AS ID, PR.CODIGO AS CODIGO, PR.DESCRICAO AS DESCRICAO, PR.ID_DEPARTAMENTO AS ID_DEPARTAMENTO, PR.PRECO AS PRECO, PR.STATUS AS STATUS, USUARIOS.NOME AS USUARIO ";
            sql += @" FROM PRODUTOS PR";
            sql += @" LEFT JOIN USUARIOS ON USUARIOS.ID = PR.ID_USUARIO";

            Conexao conexao = new Conexao();
            return conexao.CarregaLista(sql);

        }
    }
}