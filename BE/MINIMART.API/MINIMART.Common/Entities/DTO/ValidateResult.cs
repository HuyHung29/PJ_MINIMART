namespace MINIMART.Common.Entities.DTO
{
    public class ValidateResult
    {
        public bool IsValid { get; set; } = true;

        public Dictionary<string, string>? Errors { get; set; }
    }
}
