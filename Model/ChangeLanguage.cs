using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Compiler.Model
{
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
                    config.AppSettings.Settings.Add(key, value); // Добавить, если нет
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                // Обработка исключений при работе с конфигурацией
                Console.WriteLine("Ошибка при обновлении конфигурации: " + ex.Message);
                // Log the exception details for debugging.
                // Consider throwing a custom exception with more context.
                throw; // re-throw the exception to be handled by the caller
            }
        }
    }
}
