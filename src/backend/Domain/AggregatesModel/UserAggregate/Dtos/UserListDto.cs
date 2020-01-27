using System;

namespace Domain.AggregatesModel.UserAggregate.Dtos
{
    public class UserListDto
    {  
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public DateTimeOffset? DataFimDoBloqueio { get; set; }
        public int ContadorErroSenha { get; set; }
    }
}
