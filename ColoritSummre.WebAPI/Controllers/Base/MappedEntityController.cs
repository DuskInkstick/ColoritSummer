using AutoMapper;
using ColoritSummer.Interfaces.Entities;
using ColoritSummer.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ColoritSummer.WebAPI.Controllers.Base
{
    [ApiController, Route("api/[controller]")]
    public class MappedEntityController<T, TBase> : ControllerBase
         where T : IEntity
         where TBase : IEntity
    {
        private readonly IRepository<TBase> _repository;
        private readonly IMapper _mapper;

        protected MappedEntityController(IRepository<TBase> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected virtual TBase GetBase(T item) => _mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => _mapper.Map<T>(item);
        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => _mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> item) => _mapper.Map<IEnumerable<T>>(item);

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount()
            => Ok(await _repository.GetCount());

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(GetItem(await _repository.GetAll()));

        [HttpGet("items[[{Skip:int}:{Count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<T>>> Get(int Skip, int Count) =>
            Ok(GetItem(await _repository.Get(Skip, Count)));

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetById(id);
            if(item != null)
                return Ok(GetItem(item));

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await _repository.Add(GetBase(item));
            return CreatedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            var result = await _repository.Update(GetBase(item));
            if (result == null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _repository.DeleteById(id);
            if (result == null)
                return NotFound(id);

            return Ok(GetItem(result));
        }
    }
}
