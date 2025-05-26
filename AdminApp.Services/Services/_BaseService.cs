using AdminApp.Infrastructure.Interfaces;
using AdminApp.Services.Interfaces;
using AdminApp.Utils.Models;
using System.Linq.Expressions;

namespace AdminApp.Services.Services
{
    public class _BaseService<T> : _IBaseService<T> where T : class
    {
        private readonly _IBaseRepository<T> _repository;

        public _BaseService(_IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ResultOperation<T>> GetAllAsync()
        {
            ResultOperation<T> result = new();
            try
            {
                result.Results = await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                result.StateOperation = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultOperation<T>> GetByIdAsync(int id)
        {
            ResultOperation<T> result = new();
            try
            {
                result.Result = await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                result.StateOperation = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultOperation<T>> GetByAsync(Expression<Func<T, bool>> func)
        {
            ResultOperation<T> result = new();
            try
            {
                result.Result = await _repository.GetByAsync(func);
            }
            catch (Exception ex)
            {
                result.StateOperation = false;
                result.Message = ex.Message;
            }
            return result; 
        }

        public async Task<ResultOperation> AddAsync(T entity)
        {
            ResultOperation result = new();
            try
            {
                await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                result.StateOperation = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultOperation> UpdateAsync(T entity)
        {
            ResultOperation result = new();
            try
            {
                await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                result.StateOperation = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultOperation> DeleteAsync(int id)
        {
            ResultOperation result = new();
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                result.StateOperation = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
