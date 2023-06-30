using Early_Bird_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.Extensions.Caching.Memory;

namespace Early_Bird_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private List<Package> _packageList;
        private string _packageCacheKey = "Packages";

        public PackageController(ILogger<PackageController> logger, IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet]
        [Route("/package")]
        public List<Package> GetListOfPackages() 
        {
            // Try to get the list out of the memory cache, else its goingt o be the default list
            if (!_cache.TryGetValue(_packageCacheKey, out _packageList)) _packageList = Misc.DefaultPackages.List();
            return _packageList;
        }

        [HttpGet]
        [Route("/package/{KolliId}")]
        public IActionResult GetPackage(string KolliId)
        {
            List<string> errors = new List<string>();

            // Validate the KolliId so its correct length and starts with 999
            Misc.ValidationHelper.ValidateKolliId(KolliId, errors);

            // Check if we got any errors and return a BadRequest with the errors
            if (errors.Count > 0)
            {
                return BadRequest(string.Join(" - ", errors));
            }
            else
            {
                // Try to get memory cached list
                if (!_cache.TryGetValue(_packageCacheKey, out _packageList)) _packageList = Misc.DefaultPackages.List();

                // Check if we can find the package in the list
                if (_packageList.Any(x => x.KolliId == KolliId))
                {
                    var package = _packageList.First(x => x.KolliId == KolliId);
                    return Ok(package);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [Route("/package")]
        public IActionResult Post([FromBody] Package package)
        {
            List<string> errors = new List<string>();

            // Validate the KolliId so its correct length and starts with 999
            Misc.ValidationHelper.ValidateKolliId(package.KolliId, errors);

            // Check if we got any errors and return a BadRequest with the errors
            if (errors.Count > 0)
            {
                return BadRequest(string.Join(" - ", errors));
            }
            else
            {
                // Try to get memory cached list
                if (!_cache.TryGetValue(_packageCacheKey, out _packageList)) _packageList = Misc.DefaultPackages.List();

                // Check if we already have a package with the KolliId this new package have
                if (_packageList.Any(x => x.KolliId == package.KolliId))
                {
                    return Conflict("There already exist a package with KolliId = " + package.KolliId);
                }
                else
                {
                    // Add the new package to the list of packages
                    _packageList.Add(package);

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for 100 days
                        .SetSlidingExpiration(TimeSpan.FromDays(100));

                    // Save data in cache.
                    _cache.Set(_packageCacheKey, _packageList, cacheEntryOptions);
                }

                if (!package.IsValid)
                {
                    Misc.ValidationHelper.CheckPackageValues(package, errors);

                    return BadRequest(string.Join(" - ", errors));

                }
                else
                {
                    return Ok(package);
                }
            }
        }
    }
}
