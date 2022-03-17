using DecryptService.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecryptService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecryptService : ControllerBase
    {
        private readonly ILogger<DecryptService> _logger;

        public DecryptService(ILogger<DecryptService> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public FileResponse Get(string path)
        {
            var response = new FileResponse
            {
                FilePath = path,
                Sucess = true,
                Message = ""
            };

            return response;
        }
    }
}
