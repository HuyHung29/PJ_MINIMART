namespace MINIMART.Common.Entities.DTO
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public ErrorResult? Error { get; set; }
    }
}
