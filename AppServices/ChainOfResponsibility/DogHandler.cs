using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.ChainOfResponsibility
{
    public class DogHandler : AbstractHandler, IChainHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "MeatBall")
            {
                return $"Dog: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
