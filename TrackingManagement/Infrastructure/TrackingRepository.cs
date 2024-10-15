using TrackingManagement.Domain;

namespace TrackingManagement.Infrastructure;

public class TrackingRepository
{
    private readonly TrackingMySQLContext _SQLcontext;
    private readonly TrackingMongoDBContext _MongoDBcontext;

    public TrackingRepository(TrackingMySQLContext context, TrackingMongoDBContext mongoDBcontext)
    {
        _SQLcontext = context;
        _MongoDBcontext = mongoDBcontext;
    }

    public async Task<TrackingData> AddTrackingAsync(TrackingData tracking)
    {
        try
        {
            await _SQLcontext.Trackings.AddAsync(tracking);
            await _SQLcontext.SaveChangesAsync();


            //TODO await _MongoDBcontext.SQLToMongoDB();

            return _SQLcontext.Trackings.FirstOrDefault(p => p.TrackingId == tracking.TrackingId);
        }
        catch
        {
            return null;
        }

    }

    public async Task<TrackingData> UpdateOrderAsync(TrackingData tracking)
    {
        try
        {
            _SQLcontext.Trackings.Update(tracking);
            await _SQLcontext.SaveChangesAsync();

            return _SQLcontext.Trackings.FirstOrDefault(p => p.TrackingId == tracking.TrackingId);
        }
        catch
        {
            return null;
        }
    }

    public async Task<TrackingData?> GetTrackingAsync(int trackingId)
    {
        return await _SQLcontext.Trackings.FindAsync(trackingId);
    }
}