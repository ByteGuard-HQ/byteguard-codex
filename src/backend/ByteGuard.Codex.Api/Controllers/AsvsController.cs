using ByteGuard.Codex.Core.Models;
using ByteGuard.Codex.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ByteGuard.Codex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AsvsController : ControllerBase
    {
        private readonly AsvsService _asvsService;

        public AsvsController(AsvsService asvsService)
        {
            _asvsService = asvsService;
        }

        /// <summary>
        /// Get ASVS versions
        /// </summary>
        /// <response code="200">Returns the metadata about all the currently registered and configure ASVS versions.</response>
        [HttpGet]
        [ProducesResponseType<List<AsvsVersionMetadata>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsvsVersionsMetadata()
        {
            var versionsMetadata = await _asvsService.GetVersionsMetadataAsync();

            return Ok(versionsMetadata);
        }

        /// <summary>
        /// Get ASVS version
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <response code="200">Returns ASVS version details..</response>
        /// <response code="404">ASVS version with the given identifier could not be found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType<AsvsVersionDetails>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsvsVersion(Guid id)
        {
            var version = await _asvsService.GetVersionAsync(id);

            if (version is null)
            {
                return NotFound();
            }

            return Ok(version);
        }
    }
}
