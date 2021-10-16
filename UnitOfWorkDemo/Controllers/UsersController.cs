using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnitOfWorkDemo.Configuration;
using UnitOfWorkDemo.Models;

namespace UnitOfWorkDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUnitOfWork unitOfWork, ILogger<UsersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var models = await _unitOfWork.Users.All();

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetItem(Guid id)
        {
            var model = await _unitOfWork.Users.Get(id);

            return Ok(model);
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateUser(User user)
        {
            if(ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new {user.Id}, user);
            }
            

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.Users.Upsert(model);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _unitOfWork.Users.Get(id);

            if(item == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}