using NSwag;
using NSwag.CodeGeneration.TypeScript;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TsModelGenerator
{
    /// <summary>
    /// 用于根据Swagger来生成对应的typescript model.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("准备生成，请键入回车键开始......");
            Console.ReadLine();

            var swaggerJsonUrl = @"http://localhost:5000/swagger/MiCake%20Demo%20Application/swagger.json";

            var document = await OpenApiDocument.FromUrlAsync(swaggerJsonUrl);

            var settings = new TypeScriptClientGeneratorSettings
            {
                ImportRequiredTypes = false,
                GenerateClientClasses = false,      //是否生成请求的Client，因为使用了自定义的httpClient，所以不使用生成的版本。
                GenerateDtoTypes = true,
            };

            var generator = new TypeScriptClientGenerator(document, settings);
            var code = generator.GenerateFile();

            //生成的路径，为了避免手动手改的内容被替换，所以不直接生成到前端项目的解决方案中。
            File.WriteAllText("nswagCode.ts", code);

            Console.WriteLine("生成完成......");
            Console.ReadLine();
        }
    }
}
