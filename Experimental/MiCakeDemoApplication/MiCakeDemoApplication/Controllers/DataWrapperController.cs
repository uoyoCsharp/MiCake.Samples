﻿using MiCake.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DataWrapperController : ControllerBase
    {
        private IFileProvider _fileProvider;

        //该控制器用于测试DataWrapper的功能
        public DataWrapperController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public IActionResult NoFoundResult()
            => NotFound();

        [HttpGet]
        public IActionResult UnAuthorizedResult()
            => Unauthorized();

        [HttpGet]
        public IActionResult ProblemDetailResult()
            => Problem("There has a problemDetail.If MiCake WrapProblemDetails is true,will be wrapped");

        [HttpGet]
        public IActionResult FileResult()
        {
            FileStreamResult result = null;

            var logoFile = _fileProvider.GetFileInfo("Assets/micake-logo.png");
            if (!logoFile.Exists)
                return NotFound("File doesn't exists");

            result = new FileStreamResult(logoFile.CreateReadStream(), "image/png");
            return result;
        }

        [HttpGet]
        public void VoidResult()
        {
        }

        [HttpGet]
        public Task VoidAsyncResult()
        {
            return Task.CompletedTask;
        }

        [HttpGet]
        public string StringResult()
        {
            return "There result will be wrapped by micake.";
        }

        [HttpGet]
        public int IntResult()
        {
            return 1008611;
        }

        [HttpGet]
        public List<int> ListResult()
        {
            return new List<int>() { 1, 2, 3, 4, 5 };
        }

        [HttpGet]
        public IActionResult SoftlyExceptionResult()
        {
            throw new SoftlyMiCakeException("该异常会被包装");
        }

        [HttpGet]
        public IActionResult NormalExceptionResult()
        {
            throw new MiCakeException("该异常会被包装为专用的异常类型，当IsDebug配置为true时，将显示堆栈信息。");
        }
    }
}