using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Interfaces;
using Chain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers
{
    [ApiController]
    [Route("api/Company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService) => _companyService = companyService;

        [HttpGet(Name = nameof(Get) + "Companies")]
        public async Task<IActionResult> Get()
        {
            var companies = await _companyService.GetAll();

            return Ok(companies);
        }


        [HttpGet("{id}", Name = nameof(GetById) + "Companies")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var company = await _companyService.Get(id);

            return company is null ? NotFound() : Ok(company);
        }

        [HttpPost("[action]", Name = nameof(Create) + "Companies")]
        public async Task<IActionResult> Create([FromBody] CompanyDto input)
        {
            var company = await _companyService.Create(input);

            return CreatedAtAction(nameof(GetById) + "Companies", company);
        }

        [HttpPut("{id}/[action]", Name = nameof(Modify) + "Companies")]
        public async Task<IActionResult> Modify([FromRoute] Guid id, [FromBody] CompanyDto company)
        {
            await _companyService.Update(id, company);

            return NoContent();
        }

        [HttpDelete("{id}/[action]", Name = nameof(Delete) + "Companies")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _companyService.Delete(id);

            return NoContent();
        }
    }
}
