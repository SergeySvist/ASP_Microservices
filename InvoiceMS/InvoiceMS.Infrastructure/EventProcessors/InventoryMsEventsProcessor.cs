using InventoryMS.Events;
using InvoiceMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvoiceMS.Infrastructure.EventProcessors
{
    public class InventoryMsEventsProcessor : IInventoryMsEventsProcessor
    {
        private readonly IInventoryItemUpdateNotificationsProcessor _updatesNotificationProcessor;

        public InventoryMsEventsProcessor(IInventoryItemUpdateNotificationsProcessor invoiceService)
        {
            _updatesNotificationProcessor = invoiceService;
        }

        public async Task<bool> ProcessEvent(InventoryMsEvent eventReceived)
        {
            switch (eventReceived.EventType)
            {
                case EventType.InventoryItemPriceUpdated:
                    {
                        InventoryItemPriceUpdatedPayload? eventPayload = JsonSerializer.Deserialize<InventoryItemPriceUpdatedPayload>(eventReceived.EventPayload);

                        if (eventPayload != null)
                        {
                            InventoryItemPriceUpdatedNotification updateNotification = new InventoryItemPriceUpdatedNotification
                            {
                                ItemId = eventPayload.ItemId,
                                NewPrice = eventPayload.NewPrice,
                                OldPrice = eventPayload.OldPrice,
                            };

                            await _updatesNotificationProcessor.ProcessInventoryItemPriceUpdatedNotification(updateNotification);

                            Debug.WriteLine($"Inventory Item {eventPayload.ItemId} price updated to {eventPayload.NewPrice}");
                        }
                        break;
                    }
                case EventType.InventoryItemNameUpdated:
                    {
                        InventoryItemNameUpdatedPayload? eventPayload = JsonSerializer.Deserialize<InventoryItemNameUpdatedPayload>(eventReceived.EventPayload);

                        if (eventPayload != null)
                        {
                            InventoryItemNameUpdatedNotification updateNotification = new InventoryItemNameUpdatedNotification
                            {
                                ItemId = eventPayload.ItemId,
                                NewName = eventPayload.NewName,
                                OldName = eventPayload.OldName,
                            };

                            await _updatesNotificationProcessor.ProcessInventoryItemNameUpdatedNotification(updateNotification);
                        }
                        break;
                    }
            }

            return true;
        }
    }
}
