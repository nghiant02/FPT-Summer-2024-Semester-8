using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;
using AutoMapper;
using BusinessObjects.Dto;

namespace Services.Implementation
{
    public class PromotionService(
        IPromotionRepository promotionRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper) : IPromotionService
    {
        public IPromotionRepository PromotionRepository { get; } = promotionRepository;
        public IUserRepository UserRepository { get; } = userRepository;
        public IRoleRepository RoleRepository { get; } = roleRepository;
        public IMapper Mapper { get; } = mapper;

        public async Task<int> CreatePromotion(string id, PromotionDto promotionDto)
        {
            var checkUser = await UserRepository.GetById(id);
            var checkRole = await RoleRepository.GetById(checkUser.RoleId);
            if (checkRole.RoleId != "2")
            {
                throw new UnauthorizedAccessException("Only managers can create promotions");
            }
            var promotion = Mapper.Map<Promotion>(promotionDto);
            promotion.ApproveManager = checkUser.Username;
            return await PromotionRepository.Create(promotion);
        }

        public async Task<int> DeletePromotion(string id)
        {
            return await PromotionRepository.Delete(id);
        }

        public async Task<IEnumerable<Promotion?>?> GetPromotions()
        {
            return await PromotionRepository.Gets();
        }

        public Task<Promotion?> GetPromotionById(string id)
        {
            return PromotionRepository.GetById(id);
        }

        public async Task<int> UpdatePromotion(string id, PromotionDto promotionDto)
        {
            return await PromotionRepository.Update(id, Mapper.Map<Promotion>(promotionDto));
        }
    }
}