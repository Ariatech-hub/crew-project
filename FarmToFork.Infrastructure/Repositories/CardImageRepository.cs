using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Infrastructure.Repositories
{
    public class CardImageRepository : ICardImageRepository
    {
        private readonly AppDbContext _appDbContext;
        public CardImageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CardImage> AddAsync(CardImage entity)
        {
            EntityEntry<CardImage> cardImage = await _appDbContext.CardImages.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return cardImage.Entity;
        }

        public async Task<CardImage> DeleteAsync(CardImage entity)
        {
            CardImage cardImageToBeDeleted = await _appDbContext.CardImages.SingleAsync(x => x.Id == entity.Id);
            _appDbContext.Remove(cardImageToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return cardImageToBeDeleted;
        }

        public async Task<IEnumerable<CardImage>> GetAllAsync(bool disableTracking = true)
        {
            IEnumerable<CardImage> cardImages =  disableTracking
                ? await _appDbContext.CardImages.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _appDbContext.CardImages.OrderByDescending(x => x.Id).ToListAsync();
            return cardImages;
        }

        public Task<IEnumerable<CardImage>> GetAsync(Expression<Func<CardImage, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<CardImage> GetByIdAsync(int id)
        {
            return await _appDbContext.CardImages.SingleAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(CardImage entity)
        {
            CardImage cardImageToBeUpdated = await _appDbContext.CardImages.SingleAsync(x => x.Id == entity.Id);

            cardImageToBeUpdated.Name = entity.Name;
            cardImageToBeUpdated.FileName = entity.FileName;
            cardImageToBeUpdated.ModifiedBy = entity.ModifiedBy;
            cardImageToBeUpdated.ModifiedDate = entity.ModifiedDate;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
