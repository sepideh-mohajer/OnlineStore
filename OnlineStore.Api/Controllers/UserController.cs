using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Controllers.Base;
using OnlineStore.Business.Interfaces.Base;
using OnlineStore.Models.Dtos;
using OnlineStore.Models.Users;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CrudBaseController<User, UserRequestDto, UserResponseDto>
    {
        public UserController(IBaseService<User, UserRequestDto, UserResponseDto> service)
            : base(service)
        {
        }

    }
}
