using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class NewsService : BaseService<News>, INewsService
    {
        private readonly IPhotoService _photoService;
        public NewsService(IBaseDL<News> baseDL, IPhotoService photoService) : base(baseDL)
        {
            _photoService = photoService;
        }

        public async Task<ServiceResponse<News>> Insert(NewsDTO news, string userName)
        {
            var res = new ServiceResponse<News>();

            var validateResult = await base.HandleValidate(news);

            if (validateResult.IsValid)
            {
                if (news.Thumbnail != null)
                {
                    var thumbnail = await _photoService.AddPhotoAsync(news.Thumbnail);

                    if (thumbnail != null)
                    {
                        var inNews = new News
                        {
                            NewsId = news.NewsId,
                            Title = news.Title,
                            Description = news.Description,
                            Thumbnail = thumbnail.Url.ToString()
                        };

                        var insertedRecord = await _baseDL.Insert(inNews, userName);

                        if (insertedRecord != null)
                        {
                            res.Success = true;
                            res.Data = insertedRecord;
                            res.Message = string.Format(Resource.InsertSuccess, "tin tức");
                        }
                        else
                        {
                            res.Success = false;
                            res.Message = string.Format(Resource.InsertError, "tin tức");
                            res.Error = new ErrorResult
                            {
                                DevMes = string.Format(Resource.NotExist, "tin tức"),
                                UserMes = Resource.HelpText
                            };
                        }
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = string.Format(Resource.InsertError, "tin tức");
                        res.Error = new ErrorResult
                        {
                            DevMes = Resource.UploadError,
                            UserMes = Resource.HelpText
                        };
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Ảnh minh họa không được để trống";
                }
            }
            else
            {
                throw new ValidateException(Resource.ValidateError, validateResult.Errors);
            }

            return res;
        }

        public async Task<ServiceResponse<News>> Update(Guid id, NewsDTO news, string userName)
        {
            var res = new ServiceResponse<News>();

            var validateResult = await base.HandleValidate(news);

            if (validateResult.IsValid)
            {
                News upNews = new News();
                if (news.Thumbnail != null)
                {
                    var thumbnail = await _photoService.AddPhotoAsync(news.Thumbnail);
                    if (thumbnail != null)
                    {
                        upNews = new News
                        {
                            NewsId = news.NewsId,
                            Title = news.Title,
                            Description = news.Description,
                            Thumbnail = thumbnail.Url.ToString(),
                            CreatedDate = news.CreatedDate
                        };
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = string.Format(Resource.InsertError, "tin tức");
                        res.Error = new ErrorResult
                        {
                            DevMes = string.Format(Resource.NotExist, "tin tức"),
                            UserMes = Resource.HelpText
                        };
                    }
                }
                else
                {
                    upNews = new News
                    {
                        NewsId = news.NewsId,
                        Title = news.Title,
                        Description = news.Description,
                        Thumbnail = news.OldThumbnail,
                        CreatedDate = news.CreatedDate
                    };
                }
                var isUpdated = await _baseDL.Update(id, upNews, userName);

                if (isUpdated != null)
                {
                    res.Success = true;
                    res.Data = upNews;
                    res.Message = string.Format(Resource.UpdateSuccess, "tin tức");
                }
                else
                {
                    res.Success = false;
                    res.Message = string.Format(Resource.UpdateError, "tin tức");
                    res.Error = new ErrorResult
                    {
                        DevMes = string.Format(Resource.NotExist, "tin tức"),
                        UserMes = Resource.HelpText
                    };
                }
            }
            else
            {
                throw new ValidateException(Resource.ValidateError, validateResult.Errors);
            }

            return res;
        }
    }
}
