using Application.DTOs.AlertCategories;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Services.Interfaces.AppTroopers.SecurityTipCategories;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.Implementations.AppTroopers.SecurityTips
{
    public class SecurityTipCategoryService : ISecurityTipCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityTipCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<SecurityTipCategory> GetByIdAsync(string id)
        {
            return await _unitOfWork.SecurityTipCategories.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<SecurityTipCategory>> GetAllAsync()
        {
            return await _unitOfWork.SecurityTipCategories.GetAllAsync();
        }

        public async Task<IReadOnlyList<SecurityTipCategory>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _unitOfWork.SecurityTipCategories.GetPagedReponseAsync(pageNumber, pageSize);
        }

        public async Task<SecurityTipCategory> AddAsync(SecurityTipCategory entity, string userId = null)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _unitOfWork.SecurityTipCategories.AddAsync(entity, userId);
                await _unitOfWork.CommitTransactionAsync();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateAsync(SecurityTipCategory entity, string userId = null)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.SecurityTipCategories.UpdateAsync(entity, userId);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteAsync(SecurityTipCategory entity, string userId = null)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.SecurityTipCategories.DeleteAsync(entity, userId);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> IsUniqueCategoryNameAsync(string categoryName)
        {
            return await _unitOfWork.SecurityTipCategories.IsUniqueCategoryNameAsync(categoryName);
        }

        public async Task<List<CategoryTypeWithCategoriesDto>> GetCategoryTypesWithCategoriesAsync()
        {
            return await _unitOfWork.SecurityTipCategories.GetCategoryTypesWithCategoriesAsync();
        }
    }
}
