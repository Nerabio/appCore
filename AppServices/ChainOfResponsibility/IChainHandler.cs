using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.ChainOfResponsibility
{
    // Интерфейс Обработчика объявляет метод построения цепочки обработчиков. Он
    // также объявляет метод для выполнения запроса.
    public interface IChainHandler
    {
        IChainHandler SetNext(IChainHandler handler);
        object Handle(object request);
    }
}
