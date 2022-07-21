using AutoMapper;
using TemplateNetCore.Domain.Dto.Transactions;
using TemplateNetCore.Domain.Entities.Transactions;
using TemplateNetCore.Domain.Enums.Transactions;
using TemplateNetCore.Domain.Interfaces.Transactions;
using TemplateNetCore.Domain.Interfaces.Users;
using TemplateNetCore.Repository;

namespace TemplateNetCore.Service.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public TransactionService(IUnityOfWork unityOfWork, IMapper mapper, IUserService userService)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _unityOfWork.TransactionRepository.GetAllAsync(new string[] { "User" });
        }

        public async Task<Transaction> Save(Guid userId, PostTransactionDto postTransactionDto)
        {
            var transaction = _mapper.Map<Transaction>(postTransactionDto);

            transaction.Status = TransactionStatus.Pending;
            transaction.User = await _userService.GetById(userId);

            await _unityOfWork.TransactionRepository.AddAsync(transaction);
            await _unityOfWork.CommitAsync();

            return transaction;
        }
    }
}
