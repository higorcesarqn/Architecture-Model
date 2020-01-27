namespace Infra.EntityFramework.PostgreSQL.Constants
{
    public static class TableConsts
    {
        public static class IdentidadeSchema
        {
            public const string DefaultSchema = "identidade";
            public const string Permission = "permission";
            public const string RoleClaims = "role_claims";
            public const string Role = "role";
            public const string UserClaim = "user_claim";
            public const string User = "user";
            public const string UserRole = "user_role";
            public const string UserLogin = "user_login";
            public const string UserToken = "user_token";
        }

        public static class PessoaSchema
        {
            public const string DefaultSchema = "pessoa";
            public const string Endereco = "tbl_endereco";
            public const string Pessoa = "tbl_pessoa";
            public const string Telefone = "tbl_telefone";
            public const string Documento = "tbl_documento";

            public const string Escolaridade = "dom_escolaridade";
            public const string TipoEndereco = "dom_tipo_endereco";
            public const string TipoPessoa = "dom_tipo_pessoa";
            public const string TipoTelefone = "dom_tipo_telefone";
            public const string TipoDocumento = "dom_tipo_documento";
        }
    }
}
