using System.Configuration;

namespace Compiler.Model
{
    // Изменения языка через файл конфигурации App.config
    public class ChangeLanguage
    {
        public void UpdateConfig(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value;
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value);
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine(MyString.ErrorConfigure + ex.Message);
                throw;
            }
        }
    }
}
