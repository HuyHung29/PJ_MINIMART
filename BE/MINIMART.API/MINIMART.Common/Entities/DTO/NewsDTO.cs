﻿using Microsoft.AspNetCore.Http;
using MINIMART.Common.Entities.Models;

namespace MINIMART.Common.Entities.DTO
{
    public class NewsDTO : News
    {
        public IFormFile? Thumbnail { get; set; }

        public string? OldThumbnail { get; set; }
    }
}
