using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Models;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Application.Contract.Ports.Services;
using Chain.Application.Models;
using Chain.Domain.Entities;
using Microsoft.AspNetCore.Http.Internal;

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

            if (company is null)
                return null;

            return new CompanyDto(company!.Id, company!.Name);
        }

        public async ValueTask<IEnumerable<CompanyDto>> GetAll()
        {
            var companies = await _companyRepository.GetAsync();

            if (companies is null)
                return Enumerable.Empty<CompanyDto>();

            return companies.Select(c => new CompanyDto(c!.Id, c!.Name));
        }

        public async ValueTask<IEnumerable<ProductDto>> GetCompanyProducts(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company is null)
                return Enumerable.Empty<ProductDto>();

            using (var memoryStream = new MemoryStream())
                return company.Products.Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.FullEnglishName,
                    p.Description,
                    p.Quantity,
                    p.Price,
                    new CompanyDto(p.Company.Id, p.Company.Name),
                    new CategoryDto(p.Category.Id, p.Category.Title, p.Category.LimitOrder),
                    p.Attachments.Select(a => new AttachmentDto(new FormFile(memoryStream, 0, a.Image.Length, null, a.ImageTitle), a.ImageMimeType, a.Alt, a.ImageTitle)).ToList()));
        }
    }
}
