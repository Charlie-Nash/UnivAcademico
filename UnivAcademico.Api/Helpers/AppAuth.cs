namespace UnivAcademico.Api.Helpers
{
    public class AppAuth
    {
        private readonly IConfiguration _configuration;

        public AppAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AppSecretKeyValidation(string appSecretKey)
        {
            if (string.IsNullOrWhiteSpace(appSecretKey))
            {
                return false;
            }

            var clavesPermitidas = _configuration.GetSection("AppValidation:AppSecretKeys").Get<List<string>>() ?? [];

            return clavesPermitidas.Contains(appSecretKey);
        }
    }
}
