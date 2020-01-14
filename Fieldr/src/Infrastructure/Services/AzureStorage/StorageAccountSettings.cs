using System;
using System.Collections.Generic;
using System.Text;

namespace Fieldr.Infrastructure.Services.AzureStorage
{
    public class StorageAccountSettings
    {
        public string AccountNameOption { get; set; }
        public string AccountKeyOption { get; set; }
        public string FullImagesContainerNameOption { get; set; }
        public string ScalledImagesContainerNameOption { get; set; }
    }
}
