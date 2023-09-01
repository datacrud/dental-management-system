namespace DM.Core
{
    public class AppSettingsDto
    {
        public AppSettingsDto(string key)
        {
            Dev = $"{key}:dev";
            Prod = $"{key}:prod";
        }

        public string Dev { get; set; }
        public string Prod { get; set; }


        public string GetKey(string env)
        {
            if (env.Contains(AppConstants.Localhost))
            {
                return Dev;
            }

            return Prod;
        }
    }
}