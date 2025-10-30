using Implementation.FileManager;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;
using System;
using System.Globalization;
using static System.Formats.Asn1.AsnWriter;


namespace Mstech.ADV.Config
{
    public class MapsterConfig
    {

        private readonly IServiceScopeFactory _serviceProvider;
        public MapsterConfig(IServiceScopeFactory serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Register()
        {
            var pc=new PersianCalendar();
            var scope = _serviceProvider.CreateScope();
            var _fileManagerService = scope.ServiceProvider.GetService<FileManagerService>();
            // Use the service here


        }
    }
}
