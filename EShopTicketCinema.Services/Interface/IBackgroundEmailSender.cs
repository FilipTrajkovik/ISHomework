using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShopTicketCinema.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
