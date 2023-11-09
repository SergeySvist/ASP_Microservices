using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMS.Infrastructure.EventProcessors
{
    public interface IInventoryItemUpdatesNotificationsProcessor
    {
        Task ProcessInventoryItemNameUpdatedNotification(InventoryItemNameUpdatedNotification updateNotification);
        Task ProcessInventoryItemPriceUpdatedNotification(InventoryItemPriceUpdatedNotification updateNotification);
    }
}
