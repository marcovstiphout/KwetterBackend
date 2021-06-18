using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IMongoDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
