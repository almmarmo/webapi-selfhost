using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiSelfHost
{
    using Microsoft.Owin;
    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class LoggingComponent
    {
        AppFunc _next;
        public LoggingComponent(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {

            IOwinContext context = new OwinContext(environment);

            var stream = context.Response.Body;
            MemoryStream buffer = new MemoryStream();
            context.Response.Body = buffer;

            await _next(environment);

            buffer.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(buffer);
            string content = await reader.ReadToEndAsync();

            Console.WriteLine("Response:");
            Console.WriteLine(content);

            buffer.Seek(0, SeekOrigin.Begin);
            await buffer.CopyToAsync(stream);

            buffer.Dispose();
            stream.Dispose();
        }
    }
}
