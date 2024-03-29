﻿using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Rules
{
    public class BrandBusinessRules
    {
        private readonly IBrandRepository _brandRepository;

        public BrandBusinessRules(IBrandRepository someFeatureEntityRepository)
        {
            _brandRepository = someFeatureEntityRepository;
        }

        public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Brand> result = await _brandRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException(Messages.DublicatedBrandName);
        }

        public void BrandShouldExistWhenRequest(Brand brand)
        {
            if (brand == null) throw new BusinessException(Messages.BrandDoesNotExist);
        }
    }
}
