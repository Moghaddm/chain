using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Models;
using Chain.Domain.Entities;

namespace Chain.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
            => (_companyRepository, _unitOfWork) = (companyRepository, unitOfWork);

        public async Task<Guid> Create(CompanyDto companyDto)
        {
            Company company = new(companyDto.Name);

            await _companyRepository.CreateAsync(company);

            await _unitOfWork.SaveChangesAsync();

            return company.Id;
        }

        public async Task Update(Guid id, CompanyDto companyDto)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            company!.UpdateName(companyDto.Name);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            await _companyRepository.RemoveAsync(company!);

            await _unitOfWork.SaveChangesAsync();
        }

        public async ValueTask<CompanyDto> Get(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            return new CompanyDto(company!.Name);
        }

        public async ValueTask<IEnumerable<CompanyDto>> GetAll()
        {
            var companies = await _companyRepository.GetAsync();

            return companies.Select(c => new CompanyDto(c!.Name));
        }

        public async ValueTask<IEnumerable<ProductDto>> GetCompanyProducts(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            return company.Products.Select(p => new ProductDto(
                p.Name,
                p.FullEnglishName,
                p.Description,
                p.Quantity,
                p.Price,
                new CompanyDto(p.Company.Name),
                new CategoryDto(p.Category.Title, p.Category.LimitOrder),
                p.Attachments));
        }
    }
}
