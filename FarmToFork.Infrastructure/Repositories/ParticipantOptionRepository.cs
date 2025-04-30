

namespace FarmToFork.Infrastructure.Repositories;

public class ParticipantOptionRepository : IParticipationOptionRepository
{
    private readonly AppDbContext _appDbContext;

    public ParticipantOptionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<ParticipationOption>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
           ? await _appDbContext.ParticipationOptions.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
           : await _appDbContext.ParticipationOptions.OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<ParticipationOption>> GetAsync(Expression<Func<ParticipationOption, bool>> predicate)
    {
        return await _appDbContext
            .ParticipationOptions
            .Where(x => x.IsActive)
            .OrderBy(x => x.Id)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ParticipationOption> GetByIdAsync(int id)
    {
        return await _appDbContext.ParticipationOptions.SingleAsync(x => x.Id == id);
    }

    public async Task<ParticipationOption> AddAsync(ParticipationOption entity)
    {
        EntityEntry<ParticipationOption> insertedParticipantOption = await _appDbContext.ParticipationOptions.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedParticipantOption.Entity;
    }

    public async Task UpdateAsync(ParticipationOption entity)
    {
        ParticipationOption participantOptionToBeUpdated = await _appDbContext.ParticipationOptions.SingleAsync(x => x.Id == entity.Id);

        participantOptionToBeUpdated.Name = entity.Name;
        participantOptionToBeUpdated.NepaliName = entity.NepaliName;
        participantOptionToBeUpdated.Code = entity.Code;
        participantOptionToBeUpdated.OrderNumber = entity.OrderNumber;
        participantOptionToBeUpdated.IsActive = entity.IsActive;
        participantOptionToBeUpdated.ModifiedBy = entity.ModifiedBy;
        participantOptionToBeUpdated.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<ParticipationOption> DeleteAsync(ParticipationOption entity)
    {
        try
        {
            ParticipationOption participationOptionToBeDeleted = await _appDbContext.ParticipationOptions.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<ParticipationOption> deletedParticipantOption = _appDbContext.Remove(participationOptionToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedParticipantOption.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not Microsoft.Data.SqlClient.SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Participant option is in use"),
                _ => new Exception(exception.Message)
            };

        }
    }
}

