using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdministracaoUsuario.Domain.Commands.UserCommands.ChangePassword;
using AdministracaoUsuario.Domain.Commands.UserCommands.Create;
using AdministracaoUsuario.Domain.Commands.UserCommands.Update;
using Api.Controllers;
using Api.Infrastructure.Filters;
using Api.V1.Usuarios.Models;
using Core.Bus;
using Core.Notifications;
using Core.PagedList;
using Domain.AggregatesModel.UserAggregate;
using Domain.AggregatesModel.UserAggregate.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Usuarios
{
    /// <summary>
    /// Controller de usuários v1
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuariosController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        public UsuariosController(INotificationHandler<DomainNotification> notifications) : base(notifications) { }

        /// <summary>
        /// Lista os usuários cadastrados.
        /// Roles: [usuarios-listar]
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("listar")]
        [Authorize(Roles = "usuarios-listar")]
        [ProducesResponseType(typeof(IPagedList<UserListDto>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Get([FromServices] IUserRepository userRepository,
            [FromQuery]int pageIndex = 0,
            [FromQuery] int pageSize = 20)
        {
            var result = await userRepository.GetPagedList(pageIndex, pageSize);

            return Response(result);
        }

        /// <summary>
        /// Detalha um usuário.
        /// Roles: [usuarios-detalhar,usuarios-adicionar]
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet("{idUsuario}")]
        [Authorize(Roles = "usuarios-detalhar,usuarios-editar")]
        [ProducesResponseType(typeof(UserDetailsDto), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Get([FromServices] IUserRepository userRepository, [FromRoute] Guid idUsuario)
        {
            var result = await userRepository.GetDetails(idUsuario);

            return Response(result);
        }

        /// <summary>
        /// Adiciona um novo usuário.
        /// Roles: [usuarios-adicionar]
        /// </summary>  
        /// <param name="bus"></param>
        /// <param name="novoUsuarioModel"></param>
        /// <returns>Id do Usuário Criado <see cref="UsuarioAdicionadoModel"/></returns>
        [HttpPost]
        [Authorize(Roles = "usuarios-criar")]
        [ProducesResponseType(typeof(UsuarioAdicionadoModel), 201)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Post([FromServices] IMediatorHandler bus, [FromBody] AdicionarUsuarioModel novoUsuarioModel)
        {
            var comando = new CreateNewUserCommand(novoUsuarioModel.Nome, novoUsuarioModel.Login, novoUsuarioModel.Email,
                                                         novoUsuarioModel.Senha, novoUsuarioModel.ConfirmaSenha, novoUsuarioModel.Grupos);
            await bus.SendCommand(comando);
            return ResponseCreated($"api/usuarios/{comando.Id}", new UsuarioAdicionadoModel { Id = comando.Id, Login = comando.UserName });
        }

        /// <summary>
        /// Edita um Usuário.
        /// Roles: [usuarios-editar]
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="idUsuario"></param>
        /// <param name="editarUsuarioModel"></param>
        /// <returns></returns>
        [HttpPut("{idUsuario}")]
        [Authorize(Roles = "usuarios-editar")]
        [ProducesResponseType(typeof(UsuarioAdicionadoModel), 201)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Put([FromServices] IMediatorHandler bus, [FromRoute]  Guid idUsuario,
            [FromBody] EditarUsuarioModel editarUsuarioModel)
        {
            var response = await bus.SendCommand(new UpdateUserCommand(idUsuario, editarUsuarioModel.Nome, editarUsuarioModel.Email, editarUsuarioModel.Grupos));
            return Response(response);
        }

        /// <summary>
        /// Altera a senha de um usuário.
        /// Roles: [usuarios-editar]
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="idUsuario"></param>
        /// <param name="alterarSenha"></param>
        /// <returns></returns>
        [HttpPut("{idUsuario}/alterarsenha")]
        [Authorize(Roles = "usuarios-editar")]
        [ProducesResponseType(typeof(UsuarioAdicionadoModel), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> PutPassword([FromServices] IMediatorHandler bus, [FromRoute]  Guid idUsuario,
            [FromBody] AlterarSenhaModel alterarSenha)
        {
            var response = await bus.SendCommand(new ChangePasswordCommand(idUsuario, alterarSenha.Senha, alterarSenha.ConfirmaSenha));
            return Response(response);
        }
    }
}
