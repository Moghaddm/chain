using Chain.Application.Contract.Helpers.Api;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Interfaces;
using Chain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chain.Api.Controllers.Company.V1
{
    [ApiController]
    [Route("api/v{version:apiversion}/Companies")]
    [ApiVersion(ConstantHelper.ApiVersion)]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService) => _companyService = companyService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAll();

            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid companyId)
        {
            var company = await _companyService.Get(companyId);

            return company is null ? NotFound() : Ok(company);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CompanyDto input)
        {
            var companyId = await _companyService.Create(input);

            return CreatedAtAction(nameof(Index), companyId);
        }

        [HttpPut("{companyId}/[action]")]
        public async Task<IActionResult> Modify([FromRoute] Guid companyId, [FromBody] CompanyDto company)
        {
            await _companyService.Update(companyId, company);

            return NoContent();
        }

        [HttpDelete("{companyId}/[action]")]
        public async Task<IActionResult> Delete([FromRoute] Guid companyId)
        {
            await _companyService.Delete(companyId);

            return NoContent();
        }
    }
}
