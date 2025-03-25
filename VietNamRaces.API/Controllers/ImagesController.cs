using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietNamRaces.API.Models.Domain;
using VietNamRaces.API.Models.DTO.Images;
using VietNamRaces.API.Repository;

namespace VietNamRaces.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequestDto uploadImageRequestDto)
        {
            ValidateFileUpload(uploadImageRequestDto);

            if(ModelState.IsValid)
            {
                // convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = uploadImageRequestDto.File,
                    FileExtension = Path.GetExtension(uploadImageRequestDto.File.FileName),
                    FileSizeInBytes = uploadImageRequestDto.File.Length,
                    FileName = uploadImageRequestDto.FileName,
                    FileDescription = uploadImageRequestDto.FileDescription,
                };

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadImageRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) 
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10MB, please upload a smaller size file");
            }
        }
    }
}
