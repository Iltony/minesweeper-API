using System.Resources;

namespace MWServices
{
    public class ServicesResourceManager : IServicesResourceManager
    {
        private ResourceManager _resourceManager;

        public ServicesResourceManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public ResourceManager ResourceManager
        {
            get
            {
                return _resourceManager;
            }
            set
            {
                _resourceManager = value;
            }
        }
    }
}
