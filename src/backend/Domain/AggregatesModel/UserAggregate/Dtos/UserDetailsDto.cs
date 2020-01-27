using System;
using System.Collections.Generic;

namespace Domain.AggregatesModel.UserAggregate.Dtos
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public DateTimeOffset? DataFimDoBloqueio { get; set; }
        public int ContadorErroSenha { get; set; }
        public IEnumerable<string> Grupos { get; set; }
    }
}
