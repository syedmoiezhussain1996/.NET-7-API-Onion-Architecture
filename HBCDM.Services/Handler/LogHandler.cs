using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCD.Service.Handler
{
    public class LogHandler : ILogHandler
    {
		
		private readonly ILogger<LogHandler> _logger;
        private const string _systemLocationCustomField = "System Location: {@SystemLocation}";

        public string SystemLocation { get; set; }

        public LogHandler(ILogger<LogHandler> logger) => this._logger = logger;

        public void WriteException(Exception ex)
        {
            string message = "Message: " + ex.Message + ". System Location: {@SystemLocation}";
            this._logger.LogError(ex, message, (object)this.SystemLocation);
        }

        public void WriteException(string message, Exception ex)
        {
            string message1 = message + " Source: " + ex.Source + ". Message: " + ex.Message + ". System Location: {@SystemLocation}";
            this._logger.LogError(ex, message1, (object)this.SystemLocation);
        }

        public void WriteInformation(string message) => this._logger.LogInformation("Message: " + message + ". System Location: {@SystemLocation}", (object)this.SystemLocation);

        public void WriteWarning(string message) => this._logger.LogWarning("Message: " + message + ". System Location: {@SystemLocation}", (object)this.SystemLocation);
    }
}
