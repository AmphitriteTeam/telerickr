﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Telerickr.Services.Startup))]

namespace Telerickr.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
