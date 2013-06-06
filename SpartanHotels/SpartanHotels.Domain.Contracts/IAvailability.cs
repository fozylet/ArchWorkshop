using SpartanHotels.Entities;

namespace SpartanHotels.Domain.Contracts
{
    public interface IAvailability
    {
        /// <summary>
        /// Gets the availability status, given a request
        /// </summary>
        AvailabilityResponse Get(AvailabilityRequest request);
    }
}
