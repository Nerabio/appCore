using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppServices.ChainOfResponsibility;
using AppServices.Services;
using Microsoft.AspNetCore.Mvc;

using SmartHouse.Models;
using Logging;

namespace SmartHouse.Controllers
{
    public class HomeController : Controller
    {
        

        protected static ILogger _logger = LoggerManager.Create(typeof(Program));

        private IAppService _appService;
        private readonly IHandlerFactory<IMessageParameterHandler> _parameterHandlerFactory;
        private readonly IChainCreator<IChainHandler> _chainCreator;

        public HomeController(IAppService appService, IHandlerFactory<IMessageParameterHandler> parameterHandlerFactory, IChainCreator<IChainHandler> chainCreator)
        {
            
            _appService = appService;
            _parameterHandlerFactory = parameterHandlerFactory;
            _chainCreator = chainCreator;
        }

        public IActionResult Index()
        {
            //var handler = _parameterHandlerFactory.GetHandler("xxx");
            //string paramValue = handler.GetValue("sdvsd d sdv s dv sdv s dv sd v ");
            _logger.Debug("Message displayed: fgfgfghfh");
            var rootElem = _chainCreator.GetChain();
            rootElem.Handle("Nut");


            //var monkey = new MonkeyHandler();
            //var squirrel = new SquirrelHandler();
            //var dog = new DogHandler();
            //var obj = monkey.SetNext(monkey).SetNext(squirrel).SetNext(dog);
            //var rootElem = obj.getChainRootElement();
            //rootElem.Handle("Nut");

            var strOut = _appService.Send();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
