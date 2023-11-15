namespace DigitalSignage.dn.WebApiCore.DtoModels
{
    public class Configuration
    {
        public Configuration(bool checkPermissions) {
            this.CheckPermission = checkPermissions;
        }

        public readonly bool CheckPermission;
    }
}
