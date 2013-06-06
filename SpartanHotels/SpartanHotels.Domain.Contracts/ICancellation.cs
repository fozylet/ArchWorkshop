using SpartanHotels.Entities;

namespace SpartanHotels.Domain.Contracts
{
    public interface ICancellation
    {
        CancellationResponse Cancel(CancellationRequest request);
    }
}
