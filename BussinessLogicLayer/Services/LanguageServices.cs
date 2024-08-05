using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public class SharedResource { }
    public  class LanguageServices
    {
        private readonly IStringLocalizer _localizer;
        public LanguageServices(IStringLocalizer stringLocalizer, IStringLocalizerFactory factory  )
        {
                 

            var type = typeof( SharedResource );
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer= factory.Create("SharedResource", assemblyName.Name);
        }
        public LocalizedString GetKey(string key) {
            Console.WriteLine(key);
            return _localizer[key];
        
        
        }


    }
}
