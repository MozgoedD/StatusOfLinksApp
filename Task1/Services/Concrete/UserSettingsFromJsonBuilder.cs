using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Task1.Classes;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    public class UserSettingsFromJsonBuilder : IUserSettignsBuilder
    {
        IConfigurationRoot configuration;

        public UserSettingsFromJsonBuilder(string configPath)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath)
                .Build();
        }

        public Settings GetUserSettings()
        {
            var settings = new Settings();
            configuration.Bind(settings);
            return settings;
        }
    }
}
