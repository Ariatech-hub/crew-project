using FarmToFork.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.Services
{
    public interface ICardImageService
    {
        Task<IEnumerable<CardImageDto>> GetCardImages();
        Task<CardImageInsertDto> AddCardImage(CardImageInsertDto cardImage);
        Task UpdateCardImage(CardImageDto image);
        Task<CardImageDto> GetCardImageById(int id);
        Task<CardImageDto> DeleteCardImage(CardImageDeleteDto cardImage);
    }
    internal class CardImageService : ICardImageService
    {
        private readonly ICardImageRepository _cardImageRepository;
        private readonly IGeneralUtility _generalUtility;
        

        public CardImageService(ICardImageRepository cardImageRepository, IGeneralUtility generalUtility)
        {
            _cardImageRepository = cardImageRepository;
            _generalUtility = generalUtility;
        }
        public async Task<IEnumerable<CardImageDto>> GetCardImages()
        {
            IEnumerable<CardImage> cardImages = await _cardImageRepository.GetAllAsync();
            IEnumerable<CardImageDto> mappedcardImages = ObjectMapper.Mapper.Map<IEnumerable<CardImageDto>>(cardImages);
            return mappedcardImages;
            
        }
        
        public async Task<CardImageInsertDto> AddCardImage(CardImageInsertDto cardImage)
        {
            CardImage mappedCardImage = ObjectMapper.Mapper.Map<CardImage>(cardImage);
            mappedCardImage.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedCardImage.CreatedDate = _generalUtility.GetCurrentNepalTime();
            return ObjectMapper.Mapper.Map<CardImageInsertDto>(await _cardImageRepository.AddAsync(mappedCardImage));

        }

        public async Task UpdateCardImage(CardImageDto image)
        {
            CardImage mappedCardImage = ObjectMapper.Mapper.Map<CardImage>(image);
            await _cardImageRepository.UpdateAsync(mappedCardImage);
        }

        public async Task<CardImageDto> GetCardImageById(int id)
        {
           CardImage cardImage = await _cardImageRepository.GetByIdAsync(id);
           CardImageDto mappedCardImage = ObjectMapper.Mapper.Map<CardImageDto>(cardImage);
            return mappedCardImage;
        }

        public async Task<CardImageDto> DeleteCardImage(CardImageDeleteDto cardImage)
        {
            CardImage image = ObjectMapper.Mapper.Map<CardImage>(cardImage);
            CardImage cardImageToBeDeleted = await _cardImageRepository.DeleteAsync(image);
            CardImageDto mappedCardImage = ObjectMapper.Mapper.Map<CardImageDto>(cardImageToBeDeleted);
            return mappedCardImage;
        }

    }
}
