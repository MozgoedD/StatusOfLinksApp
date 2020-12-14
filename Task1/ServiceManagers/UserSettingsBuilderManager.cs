using System;
using System.Collections.Generic;
using System.Text;
using Task1.Classes;
using Task1.Services.Abstract;

namespace Task1.ServiceManagers
{
    public class UserSettingsBuilderManager: IUserSettignsBuilder
    {
        IUserSettignsBuilder _userSettingsBuilder;

        public UserSettingsBuilderManager(IUserSettignsBuilder userSettignsBuilder)
        {
            _userSettingsBuilder = userSettignsBuilder;
        }

        public Settings GetUserSettings()
        {
            return _userSettingsBuilder.GetUserSettings();
        }
    }
}
