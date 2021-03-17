using System;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace RoutingWinApp
{
    public class CFDispatchTrackApplicationSettings
    {
        /// <summary>
        /// Gets the connection string from web.config 
        /// </summary>
        /// <param name="connName">Name of the conn.</param>
        /// <returns></returns>
        public virtual string GetConnectionString(string connName)
        {
            if (ConfigurationManager.ConnectionStrings[connName] != null)
            {
                return ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Gets the specified value by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual string Get(string name)
        {
            if (ConfigurationManager.AppSettings[name] != null)
            {
                return ConfigurationManager.AppSettings[name].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Sets the specified value by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public virtual void Set(string name, string value)
        {
            Configuration config;

            // Verfy if the calling assembly is a web project or not. 
            if (HostingEnvironment.IsHosted)
            {
                config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            }
            else
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }


            try
            {
                config.AppSettings.Settings[name].Value = value;
            }
            catch
            {
                config.AppSettings.Settings.Add(name, value);
            }
            config.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
