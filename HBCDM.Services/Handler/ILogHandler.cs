using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCD.Service.Handler
{
    public interface ILogHandler
    {
        string SystemLocation { get; set; }

        void WriteException(Exception ex);

        void WriteException(string message, Exception ex);

        void WriteInformation(string message);

        void WriteWarning(string message);
    }
}
