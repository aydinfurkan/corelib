namespace CoreLib.Models.Response
{
    public class VerifyUserResponseModel
    {
        public bool IsValid { get; set; }

        public VerifyUserResponseModel(bool isValid)
        {
            IsValid = isValid;
        }
    }
}