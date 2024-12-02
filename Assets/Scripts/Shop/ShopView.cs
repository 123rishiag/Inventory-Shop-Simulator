using ServiceLocator.Event;

namespace ServiceLocator.Shop
{
    public class ShopView
    {
        private ShopController shopController;

        public ShopView(ShopController _shopController)
        {
            shopController = _shopController;
        }

        public void UpdateUI(EventService _eventService)
        {
            _eventService.OnShowItemEvent.Invoke(shopController.GetModel().UISection,
                shopController.GetModel().SelectedItemType);

            _eventService.OnShopUpdatedEvent.Invoke(shopController.GetModel().SelectedItemType,
                shopController.GetFilteredItemsCount());
        }
    }
}