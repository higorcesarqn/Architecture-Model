using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Infrastructure.Filters;
using Core.Notifications;
using Domain.AggregatesModel.PermissionAggregate;
using Domain.AggregatesModel.PermissionAggregate.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Permissoes
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PermissaoController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        public PermissaoController(INotificationHandler<DomainNotification> notifications) : base(notifications) { }

        /// <summary>
        /// Lista as permissões.
        /// Roles: [grupos-adicionar, grupos-editar]
        /// </summary>
        /// <param name="permissionRepository"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "grupos-adicionar, grupos-editar")]
        [ProducesResponseType(typeof(PermissaoDto), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Get([FromServices]IPermissionRepository permissionRepository)
        {
            return Response(await permissionRepository.ListAll());
        }
    }
}
