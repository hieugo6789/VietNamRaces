using VietNamRaces.API.Models.Domain;

namespace VietNamRaces.API.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
