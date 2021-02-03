using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CardCollection.Domain.Models;
using CardCollection.Dtos;
using CardCollection.Helpers;
using CardCollection.Persistence.Data;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace CardCollection.Controllers
{
    [Route("api/collection/{collectionId}/photos")]
    [ApiController]
    public class PhotosController : Controller
    {
        private readonly ICardsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;


        public PhotosController(ICardsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost("/card/{cardId}")]
        public async Task<IActionResult> AddPhotoForUser(int collectionId, int cardId,
            [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var cardFromRepo = await _repo.GetCollectionCardById(collectionId, cardId);

            if (cardFromRepo == null)
            {
                return NotFound();
            }

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(300).Height(600)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!cardFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            cardFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }
    }
}
